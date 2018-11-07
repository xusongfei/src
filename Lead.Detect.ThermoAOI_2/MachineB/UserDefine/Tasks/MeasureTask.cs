using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.UtilsFramework;
using Lead.Detect.MeasureComponents.LaserControl;
using Lead.Detect.MeasureComponents.LMILaser;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOIProductLib.Thermo2;
using Lead.Detect.Utility.FittingHelper;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks
{
    public class MeasureTask : StationTask
    {
        public IVioEx VioMeasureStart;
        public IVioEx VioMeasureFinish;


        public ThermoCameraB Camera;
        public PlatformEx CPlatform;

        public LmiLaser Laser1;
        public PlatformEx L1Platform;

        public LmiLaser Laser2;
        public PlatformEx L2Platform;


        public MachineSettings CfgSettings;
        public bool CfgEnableRelCoordMode;


        public MeasureProjectB Project;
        public Thermo2ProductB Product;


        public MeasureTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart = station.Machine.Find<IVioEx>("VioMeasureStart");
            VioMeasureFinish = station.Machine.Find<IVioEx>("VioMeasureFinish");

            Camera = new ThermoCameraB();

            CPlatform = station.Machine.Find<PlatformEx>("CameraPlatform");
            {
                var toFilePos = new Func<double[], double[]>(d => (new PosXYZU(d) - new PosXYZU(CPlatform["FocusOrigin"]?.Data())).Data());
                var toMovePos = new Func<double[], double[]>(d => (new PosXYZU(d) + new PosXYZU(CPlatform["FocusOrigin"]?.Data())).Data());
                CPlatform.PosConvertFuncs.Clear();
                CPlatform.PosConvertFuncs.Add("FILE", toFilePos);
                CPlatform.PosConvertFuncs.Add("MOVE", toMovePos);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                Laser1 = new LineLaserSim();
            }
            else
            {
                Laser1 = new LmiLaser();
                Laser1.Name = "Top";
                Laser1.IpStr = "192.168.1.10";
            }

            L1Platform = station.Machine.Find<PlatformEx>("Laser1Platform");
            {
                var toFilePos = new Func<double[], double[]>(d => (new PosXYZU(d) - new PosXYZU(L1Platform["LaserOrigin"]?.Data())).Data());
                var toMovePos = new Func<double[], double[]>(d => (new PosXYZU(d) + new PosXYZU(L1Platform["LaserOrigin"]?.Data())).Data());
                L1Platform.PosConvertFuncs.Clear();
                L1Platform.PosConvertFuncs.Add("FILE", toFilePos);
                L1Platform.PosConvertFuncs.Add("MOVE", toMovePos);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                Laser2 = new LineLaserSim();
            }
            else
            {
                Laser2 = new LmiLaser();
                Laser2.Name = "Bottom";
                Laser2.IpStr = "192.168.2.10";
            }

            L2Platform = station.Machine.Find<PlatformEx>("Laser2Platform");
            {
                var toFilePos = new Func<double[], double[]>(d => (new PosXYZU(d) - new PosXYZU(L2Platform["LaserOrigin"]?.Data())).Data());
                var toMovePos = new Func<double[], double[]>(d => (new PosXYZU(d) + new PosXYZU(L2Platform["LaserOrigin"]?.Data())).Data());
                L2Platform.PosConvertFuncs.Clear();
                L2Platform.PosConvertFuncs.Add("FILE", toFilePos);
                L2Platform.PosConvertFuncs.Add("MOVE", toMovePos);
            }
        }


        protected override int ResetLoop()
        {
            //load settings
            CfgSettings = Machine.Ins.Settings;
            CfgEnableRelCoordMode = CfgSettings.EnableRelCoordMode;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);

            if (CfgEnableRelCoordMode)
            {
                CPlatform.AssertPosTeached("FocusOrigin", this);
                L1Platform.AssertPosTeached("LaserOrigin", this);
                L2Platform.AssertPosTeached("LaserOrigin", this);
            }
            CPlatform.AssertPosTeached("Wait", this);
            L1Platform.AssertPosTeached("Wait", this);
            L2Platform.AssertPosTeached("Wait", this);


            //reset vio
            VioMeasureStart.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            //connect camera server
            try
            {
                Camera.Disconnect();
                var ret = Camera.Connect();
                if (!ret)
                {
                    Log($"{Camera} Connect Error", LogLevel.Error);
                }
                else
                {
                    Log($"{Camera} Connect Success");
                }


                //todo switch product
                Camera.SwitchProduct(Project.TypeId);
                Log($"{Camera} SwitchProduct {Project.TypeId}");
            }
            catch (Exception ex)
            {
                Log($"{Camera} Connect Fail:{ex.Message}", LogLevel.Error);
            }


            //connect laser1
            try
            {
                Laser1.AcceleratorIp = CfgSettings.Laser1AcceleratorIp;
                Laser1.EnableAccelerator = CfgSettings.EnableLaserAccelerator;
                Laser1.EnableSaveRec = CfgSettings.EnableSaveRec;
                Laser1.Name = "Top";
                Laser1.IpStr = CfgSettings.Laser1IP;

                Laser1.Disconnect();
                var ret = Laser1.Connect();
                if (!ret)
                {
                    Log($"{Laser1.Name} Connect Error", LogLevel.Error);
                }
                else
                {
                    Log($"{Laser1.Name} Connect Success");
                }
            }
            catch (Exception ex)
            {
                Log($"{Laser1.Name} Connect Fail:{ex.Message}", LogLevel.Error);
            }


            //connect laser2
            try
            {
                Laser2.AcceleratorIp = CfgSettings.Laser2AcceleratorIp;
                Laser2.EnableAccelerator = CfgSettings.EnableLaserAccelerator;
                Laser2.EnableSaveRec = CfgSettings.EnableSaveRec;
                Laser2.Name = "Bottom";
                Laser2.IpStr = CfgSettings.Laser2IP;

                Laser2.Disconnect();
                var ret = Laser2.Connect();
                if (!ret)
                {
                    Log($"{Laser2.Name} Connect Error", LogLevel.Error);
                }
                else
                {
                    Log($"{Laser2.Name} Connect Success");
                }
            }
            catch (Exception ex)
            {
                Log($"{Laser2.Name} Connect Fail:{ex.Message}", LogLevel.Error);
            }


            //reset platform
            var measureAxis = new[] { CPlatform.AX, L1Platform.AX, L2Platform.AX };
            var measureAxisHomeVm = new[] { CPlatform.AX.HomeVm, L1Platform.AX.HomeVm, L2Platform.AX.HomeVm };
            var measureAxisSpeed = new[] { CPlatform.AX.AxisSpeed, L1Platform.AX.AxisSpeed, L2Platform.AX.AxisSpeed };
            measureAxis.ServoEnable(this, true);
            measureAxis.Home(this, measureAxisHomeVm);
            measureAxis.MoveAbs(this, new[] { CPlatform["Wait"].Data()[0], L1Platform["Wait"].Data()[0], L2Platform["Wait"].Data()[0] }, measureAxisSpeed);

            CPlatform.EnterAuto(this);
            L1Platform.EnterAuto(this);
            L2Platform.EnterAuto(this);

            return 0;
        }


        protected override int RunLoop()
        {
            //start assert
            CPlatform.AssertAutoMode(this);
            CPlatform.LocateInPos("Wait");
            L1Platform.AssertAutoMode(this);
            L1Platform.LocateInPos("Wait");
            L2Platform.AssertAutoMode(this);
            L2Platform.LocateInPos("Wait");


            //clear server send msgs
            try
            {
                Camera.SwitchProduct(Project.TypeId);
                var msg = Camera.GetResult("result", 1);
                Log($"{Camera} Clear Recv Buffer : {msg}");
            }
            catch (Exception e)
            {
                Log($"{Camera} Clear Recv Buffer Fail: {e.Message}");
            }


            VioMeasureStart.WaitVioAndClear(this);
            while (true)
            {
                int cameraIndex = 1;
                RunCameraLoop("camera1", ref cameraIndex, Project.CapturePos.FindAll(p => p.Name == "C1").ToList());
                if (CfgSettings.QuitOnProductError && Product.Status == ProductStatus.ERROR)
                {
                    break;
                }

                RunLaserLoop("laser1", Laser1, L1Platform, Project.UpLaserPos.FindAll(p => p.Name == "L1").ToList());
                if (CfgSettings.QuitOnProductError && Product.Status == ProductStatus.ERROR)
                {
                    break;
                }

                RunLaserLoop("laser2", Laser2, L2Platform, Project.DownLaserPos.FindAll(p => p.Name == "L2").ToList());
                if (CfgSettings.QuitOnProductError && Product.Status == ProductStatus.ERROR)
                {
                    break;
                }

                RunCameraLoop("camera2", ref cameraIndex, Project.CapturePos.FindAll(p => p.Name == "C2").ToList());
                break;
            }

            Product.SetSpcItem("STS", Product.Status == ProductStatus.OK ? 0 : 2);
            VioMeasureFinish.SetVio(this);

            return 0;
        }


        public void RunCameraLoop(string loopName, ref int triggerIndex, List<PosXYZU> triggerPos)
        {
            //assert
            Project.AssertNoNull(this);
            Product.AssertNoNull(this);


            //run camera loop
            var step = 0;
            while (step++ < triggerPos.Count)
            {
                Log($"{loopName} {Camera.Name} RunCameraLoop {step}........");
                //move capture pos
                var pos = triggerPos[step - 1];

                if (CfgEnableRelCoordMode)
                {
                    var newPos = CPlatform.GetPos("MOVE", pos.Data());
                    Log($"{loopName} {Camera.Name} EnableRelCoordMode Transform {pos} To {newPos}");
                    CPlatform.MoveAbs(newPos);
                }
                else
                {
                    CPlatform.MoveAbs(pos);
                }

                //trigger
                var trigger = triggerIndex + step - 1;
                var ret = Camera.TriggerProduct(trigger);
                var result = Camera.GetResult(string.Empty);
                if (ret)
                {
                    Log($"{loopName} {Camera.Name} Trigger OK {trigger} {result} {Camera.LastError}");


                    //parse camera recv result
                    var dataStr = result.Split(':');
                    try
                    {
                        var profile = loopName == "camera1" ? Product.RawData_C1Profile : Product.RawData_C2Profile;

                        //P1S1:0.1,0.2,0.3,0.4,0.5,0.6,0.7,0.8
                        //P1S1:Failed
                        if (dataStr.Length >= 2)
                        {
                            if (dataStr[1] != "Failed")
                            {
                                dataStr = dataStr[1].Split(',');

                                var dataList = new List<double>();
                                for (int i = 0; i < dataStr.Length; i++)
                                {
                                    if (string.IsNullOrEmpty(dataStr[i]))
                                    {
                                        continue;
                                    }
                                    dataList.Add(double.Parse(dataStr[i]));
                                }

                                profile.Add(new List<PosXYZ>());
                                profile.Last().AddRange(dataList.Select(d => new PosXYZ(0, 0, d)).ToArray());
                            }
                            else
                            {
                                var dataList = new List<double>();
                                for (int i = 0; i < 8; i++)
                                {
                                    dataList.Add(0);
                                }

                                profile.Add(new List<PosXYZ>());
                                profile.Last().AddRange(dataList.Select(d => new PosXYZ(0, 0, d)));
                            }
                        }
                        else
                        {
                            Log($"{loopName} {Camera} Recv Data Error:{dataStr[0]}");
                        }
                    }
                    catch (Exception e)
                    {
                        Product.Error = "CameraDataFormatError";
                        Log($"{loopName} {Camera} Parse Recv Data Error: {Camera.LastError} {e.Message}");
                    }
                }
                else
                {
                    Product.Error = Camera.LastError;
                    Log($"{loopName} {Camera.Name} Trigger Error {trigger} {result} {Camera.LastError}");
                }


                if (CfgSettings.QuitOnProductError && Product.Status == ProductStatus.ERROR)
                {
                    Log($"Quit {loopName} {Camera.Name} On Error: {Product.Error}");
                    return;
                }
            }


            UpdateCameraSpcs(loopName);


            //upate next camera loop start triggerIndex
            triggerIndex += step - 1;
        }

        private void UpdateCameraSpcs(string loopName)
        {
            //process camera spec result
            if (loopName == "camera1")
            {
                var spcItem = Product.SPCItems.FirstOrDefault(s => s.Description == "C1");
                if (spcItem != null && Product.RawData_C1Profile.Count > 0)
                {
                    Product.SetSpcItem(spcItem.SPC, Product.RawData_C1Profile.Max(p => p.Select(v => v.Z).Max()));
                }
            }
            else if (loopName == "camera2")
            {
                var spcItem = Product.SPCItems.FirstOrDefault(s => s.Description == "C2");
                if (spcItem != null && Product.RawData_C2Profile.Count > 0)
                {
                    Product.SetSpcItem(spcItem.SPC, Product.RawData_C2Profile.Max(p => p.Select(v => v.Z).Max()));
                }
            }
        }


        public void RunLaserLoop(string loopName, LmiLaser laser, PlatformEx laserPlatform, List<PosXYZU> laserPos)
        {
            //assert
            Project.AssertNoNull(this);
            Product.AssertNoNull(this);


            //start laser rec
            laser.StartRec();
            //run laser loop start
            var step = 0;
            while (step++ < laserPos.Count / 2)
            {
                Log($"{loopName} {laser.Name} RunLaserLoop {step}........");
                var laserIndex = (step - 1) * 2;

                var laserStart = laserPos[laserIndex].Data();
                var laserEnd = laserPos[laserIndex + 1].Data();
                if (CfgEnableRelCoordMode)
                {
                    laserStart = laserPlatform.GetPos("MOVE", laserStart);
                    laserEnd = laserPlatform.GetPos("MOVE", laserEnd);
                    Log($"{loopName} {laser.Name} EnableRelCoordMode Transform");
                }


                //trigger laser
                {
                    //move laser trigger start pos                   
                    laserPlatform.MoveAbs(laserStart);

                    //start laser trigger
                    laser.SetJob($"{laserPos[laserIndex].Description}");
                    laser.Trigger(string.Empty);
                    Log($"{loopName} {laser.Name} Start LaserTrigger {step} {laserPos[laserIndex].Description} {laser.LastError}");

                    //move laser trigger end pos
                    laserPlatform.MoveAbs(laserEnd);
                    Log($"{loopName} {laser.Name} Finish LaserTrigger {step} {laserPos[laserIndex].Description} {laser.LastError}");
                }



                //get laser result
                var gridData = laser.GetResult();
                if (loopName == "laser1" || loopName == "laser2")
                {
                    if (gridData != null)
                    {
                        Log($"{loopName} {laser.Name} GetLaserGridData: \r\nGridNodes: {gridData.Sum(g => g.Count)} \r\nGridCols: {gridData.Count} :\r\n");

                        var profile = loopName == "laser1" ? Product.RawData_UpProfile : Product.RawData_DownProfile;
                        //parse grid data to profile
                        ParseLaserDataToRawProfile(loopName, gridData, profile);
                    }
                    else
                    {
                        Product.Error = "LaserGridDataError";
                    }
                }
                else
                {
                    Product.Error = "LaserLoopError";
                    Log($"{loopName} {laser.Name} loop name error: {laser.LastError}", LogLevel.Error);
                }


                if (CfgSettings.QuitOnProductError && Product.Status == ProductStatus.ERROR)
                {
                    Log($"Quit {loopName} {laser.Name} On Error: {Product.Error}");
                    break;
                }
            }

            //run laser loop finish, save laser rec
            try
            {
                var laserFolder = loopName == "laser1" ? @".\LaserUp" : @".\LaserDown";
                laserFolder = Path.Combine(CfgSettings.RecFolder, laserFolder);

                var recfile = Path.Combine(laserFolder, $"{Product.StartTime.ToString("yyyyMMdd-HHmmss.fff")}_{step}.rec");
                laser.SaveRec(recfile);
            }
            catch (Exception ex)
            {
                Log($"{loopName} {laser.Name} SaveRec Fail: {ex.Message}");
            }


            UpdateLaserSpcs(loopName);
        }

        private void UpdateLaserSpcs(string loopName)
        {
            //process laser spec result
            if (loopName == "laser1")
            {
                //上表面SPC
                var spcItem = Product.SPCItems.FirstOrDefault(s => s.Description == "L1");
                if (spcItem != null && Product.RawData_UpProfile.Count > 0)
                {
                    Product.SetSpcItem(spcItem.SPC, Product.RawData_UpProfile.Max(list => list.Count > 0 ? list.Max(p => p.Z) : 0));
                }
            }
            else if (loopName == "laser2")
            {
                //下表面SPC
                var spcItem = Product.SPCItems.FirstOrDefault(s => s.Description == "L2");
                if (spcItem != null && Product.RawData_DownProfile.Count > 0)
                {
                    Product.SetSpcItem(spcItem.SPC, Product.RawData_DownProfile.Max(list => list.Count > 0 ? list.Max(p => p.Z) : 0));
                }
            }
        }


        /// <summary>
        /// parse fin grid data to fin calc data
        /// 解析fin测量点原始高度数据到计算spec值
        /// </summary>
        /// <param name="loopName"></param>
        /// <param name="gridDataOfFin"></param>
        /// <param name="profile"></param>
        private void ParseLaserDataToRawProfile(string loopName, List<List<PosXYZ>> gridDataOfFin, List<List<PosXYZ>> profile)
        {
            for (var col = 0; col < gridDataOfFin.Count; col++)
            {
                //parse laser col data
                //最后一列数据为所有原始数据，do not parse
                if (gridDataOfFin[col].Count < gridDataOfFin.Last().Count)
                {   
                    //profile add fin col spec data
                    profile.Add(new List<PosXYZ>());

                    //get fin bar col raw data
                    var line = LineParams.FitLine(gridDataOfFin[col]);
                    var maxDist = gridDataOfFin[col].Max(g => line.Distance(g));
                    var minDist = gridDataOfFin[col].Min(g => line.Distance(g));

                    profile.Last().Add(new PosXYZ(line.OX, line.OY, maxDist - minDist));
                    Log($"{loopName} COL:{col} ROW:{gridDataOfFin[col].Count} LineDist: {line.OX:F2} {line.OY:F2} MAX:{maxDist:F2} MIN:{minDist:F2} FLATNESS:{maxDist - minDist:F2}");
                }
                else
                {
                    //parse last col of raw data
                    //profile add fin col spec data
                    //profile.Add(new List<PosXYZ>());
                    //profile.Last().Add(new PosXYZ(0, 0, gridDataOfFin.Last().Count));
                    //Log($"{loopName} COL:{col} ROW:{gridDataOfFin[col].Count} RawData: {gridDataOfFin[col].Count}");
                }
            }
        }
    }
}