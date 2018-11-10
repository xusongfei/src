using System;
using System.Collections.Generic;
using System.Threading;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.UtilsFramework;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOIProductLib.Thermo2;

namespace Lead.Detect.ThermoAOI2.MachineA.UserDefine.Tasks
{
    /// <summary>
    /// Machine A 相机测量任务
    /// </summary>
    public class MeasureTask : StationTask
    {

        public IVioEx VioMeasureStart;
        public IVioEx VioMeasureFinish;


        public ThermoCameraA Camera;
        public PlatformEx Platform;


        public MachineSettings CfgSettings;
        public bool CfgEnableRelCoordMode;


        public MeasureProjectA Project;
        public Thermo2ProductA Product;


        public MeasureTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart = station.Machine.Find<IVioEx>("VioMeasureStart");
            VioMeasureFinish = station.Machine.Find<IVioEx>("VioMeasureFinish");


            Camera = new ThermoCameraA();


            Platform = station.Machine.Find<PlatformEx>("MeasurePlatform");

            var toFilePos = new Func<double[], double[]>(d => (new PosXYZ(d) - new PosXYZ(Platform["Origin"]?.Data())).Data());
            var toMovePos = new Func<double[], double[]>(d => (new PosXYZ(d) + new PosXYZ(Platform["Origin"]?.Data())).Data());
            Platform.PosConvertFuncs.Clear();
            Platform.PosConvertFuncs.Add("FILE", toFilePos);
            Platform.PosConvertFuncs.Add("MOVE", toMovePos);
        }


        protected override int ResetLoop()
        {
            //load files
            CfgSettings = Machine.Ins.Settings;
            CfgEnableRelCoordMode = CfgSettings.EnableRelCoordMode;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectA)) as MeasureProjectA;
            Project.AssertNoNull(this);

            if (CfgEnableRelCoordMode)
            {
                Platform.AssertPosTeached("FocusOrigin", this);
            }
            Platform.AssertPosTeached("Wait", this);


            //reset vio
            VioMeasureStart.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);


            //reconnect camera
            try
            {
                Camera.Disconnect();
                var ret = Camera.Connect();
                if (!ret)
                {
                    Log($"{Camera} Connect Error", LogLevel.Error);
                }

                Camera.SwitchProduct(Project.TypeId);
                Log($"{Camera} SwitchProduct {Project.TypeId}");
            }
            catch (Exception ex)
            {
                Log($"{Camera} Server Connect Fail: {ex.Message}", LogLevel.Error);
            }

            //reset platforms
            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home();
            Platform.EnterAuto(this).MoveAbs("Wait");
            return 0;
        }


        protected override int RunLoop()
        {
            //start assert
            Platform.AssertAutoMode(this);
            Platform.LocateInPos("Wait");
            Project.AssertNoNull(this);


            //safe height check
            var safeHeight = Platform["SafeHeight"]?.Data()[2];
            var productHeight = Project.Height;
            safeHeight = safeHeight - productHeight;

            if (Platform.CurPos[2] > safeHeight)
            {
                Log($"Platform {Platform.Name} Z SAFE Height Error: {Platform.CurPos[2]:F2} > {safeHeight:F2}", LogLevel.Error);
            }


            //wait start
            VioMeasureStart.WaitVioAndClear(this);
            {
                //assert
                Product.AssertNoNull(this);

                //clear client recv buffer
                try
                {
                    var msg1 = Camera.GetResult("result", 1);
                    Log($"RunLoop Start Clear Client Buffer: {msg1}");
                }
                catch (Exception ex)
                {
                    Log($"RunLoop Start Clear Client Buffer Error: {ex.Message}");
                }


                var isFirst = true;
                PosXYZ lastPos = null;
                int index = 0;
                //start measure loop
                foreach (var pos in Project.CapturePos)
                {
                    var newPos = pos;
                    if (CfgEnableRelCoordMode)
                    {
                        //conver FILE POS TO MOVE POS
                        newPos = new PosXYZ(Platform.GetPos("MOVE", pos.Data()));
                        newPos.Name = pos.Name;
                        newPos.Description = pos.Description;
                        Log($"EnableRelCoordMode Transform {pos} To {newPos}");
                    }


                    if (newPos.Z > safeHeight)
                    {
                        Log($"{newPos.Z} > Z limit ERROR", LogLevel.Error);
                    }

                    //optimize jump move
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                            Platform.Jump(newPos, 0, zLimit: -12);
                        }
                        else
                        {
                            //select jumpHeight
                            if (newPos.Z < lastPos.Z)
                            {
                                var jumpHeight = -8 + newPos.Z - lastPos.Z;
                                Platform.Jump(newPos, jumpHeight, zLimit: -12);
                            }
                            else
                            {
                                Platform.Jump(newPos, 0, zLimit: -12);
                            }
                        }
                        lastPos = newPos;
                    }

                    //capture
                    index = TriggerCamera(pos, index);

                    if (CfgSettings.QuitOnProductError)
                    {
                        if (Product.Status == ProductStatus.ERROR)
                        {
                            Log($"Quit {Name} Loop On Error: {Product.Error}");
                            break;
                        }
                    }
                }


                //process result
                try
                {
                    if (string.IsNullOrEmpty(Product.Error))
                    {
                        var result = Camera.GetResult("result");
                        var msgData = result.Split(',');
                        Product.Status = msgData[0] == "OK" ? ProductStatus.OK : ProductStatus.NG;
                        if (msgData.Length > 2)
                        {
                            var spcItems = new List<Tuple<string, double>>();

                            //parse msgData to spcs
                            for (int i = 1; i < msgData.Length; i++)
                            {
                                if (string.IsNullOrEmpty(msgData[i]))
                                {
                                    continue;
                                }

                                var spc = msgData[i].Split(':');
                                if (spc.Length >= 2)
                                {
                                    try
                                    {
                                        var s = spc[0].Split('_')[1];
                                        var val = double.Parse(spc[1]);
                                        spcItems.Add(new Tuple<string, double>(s, val));

                                        Product.RawData.Add(val);
                                    }
                                    catch (Exception e)
                                    {
                                        Log($"{spc[0]} {spc[1]} SetSpcError:{e.Message}");
                                    }
                                }
                            }

                            //set spc
                            foreach (var s in spcItems)
                            {
                                Product.SetSpcItem(s.Item1, s.Item2);
                            }
                        }
                        Log($"Camera GetResult: {result}");
                    }

                    Product.SetSpcItem("STS", Product.Status == ProductStatus.OK ? 0 : 2);
                }
                catch (Exception ex)
                {
                    Log($"Camera GetResult Exception: {ex.Message}");
                }

                Platform.MoveAbs("Wait");

            }
            //measure finish
            VioMeasureFinish.SetVio(this);
            return 0;
        }

        private int TriggerCamera(PosXYZ pos, int index)
        {
            if (pos.Description == "barcode")
            {
                var ret = Camera.TriggerBarcode();
                if (!ret)
                {
                    Log($"{Camera} TriggerBarcode Error");
                    Product.Error = "BARCODEERROR";
                }

                Log($"{Camera} TriggerBarcode {index} {(ret ? "OK" : "ERROR")} {Camera.GetResult(string.Empty)} {Camera.LastError}");
            }
            else
            {
                //only add index on trigger product
                index++;
                Thread.Sleep(CfgSettings.CaptureDelay);
                var ret = Camera.TriggerProduct(index);
                if (!ret)
                {
                    Log($"{Camera} TriggerProduct {index} Error");
                    Product.Error = $"CAPTURE{index}ERROR";
                }

                Log($"{Camera} TriggerProduct {index} {(ret ? "OK" : "ERROR")} {Camera.GetResult(string.Empty)} {Camera.LastError}");
            }

            return index;
        }


        private void SelectJumpHeight(ref PosXYZ newPos, PosXYZ pos)
        {
            //get focus z
            var focusBarcode = Platform["FocusBarcode"]?.Data()[2];
            var focusInnerZ = Platform["FocusInner"]?.Data()[2];
            var focusOuterZ = Platform["FocusOuter"]?.Data()[2];
            var focusProfileZ = Platform["FocusProfile"]?.Data()[2];
            var focusPedZ = Platform["FocusPed"]?.Data()[2];


            //select obj pos z height
            //switch (pos.Description)
            //{
            //    case "barcode":
            //    case "Barcode":
            //        newPos.Z = focusBarcode ?? 0;
            //        break;
            //    case "inner":
            //    case "Inner":
            //        newPos.Z = focusInnerZ ?? 0;
            //        break;
            //    case "outer":
            //    case "Outer":
            //        newPos.Z = focusOuterZ ?? 0;
            //        break;
            //    case "profile":
            //    case "Profile":
            //        newPos.Z = focusProfileZ ?? 0;
            //        break;
            //    case "ped":
            //    case "Ped":
            //        newPos.Z = focusPedZ ?? 0;
            //        break;
            //    default:
            //        Log($"POS ERROR : {index} {pos} Not Found Z Focus");
            //        continue;
            //}

            //update product height
            //if (newPos.Z > 0)
            //{
            //    newPos.Z = newPos.Z - productHeight;
            //}
        }
    }
}
