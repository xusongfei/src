using System;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

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
        public MeasureProjectA Project;
        public Thermo2ProductA Product;


        public MeasureTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart = station.Machine.Find<IVioEx>("VioMeasureStart");
            VioMeasureFinish = station.Machine.Find<IVioEx>("VioMeasureFinish");

            if (FrameworkExtenion.IsSimulate)
            {
                Camera = new ThermoCameraASim();
            }
            else
            {
                Camera = new ThermoCameraA();
            }


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
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectA)) as MeasureProjectA;
            Project.AssertNoNull(this);


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
            }
            catch (Exception ex)
            {
                Log($"{Camera.ToString()} Server Connect Fail: {ex.Message}", LogLevel.Error);
            }

            //reset platforms
            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home();
            Platform.EnterAuto(this).MoveAbs("Wait");

            //update convert funcs
            var toFilePos = new Func<double[], double[]>(d => (new PosXYZ(d) - new PosXYZ(Platform["Origin"]?.Data())).Data());
            var toMovePos = new Func<double[], double[]>(d => (new PosXYZ(d) + new PosXYZ(Platform["Origin"]?.Data())).Data());
            Platform.PosConvertFuncs.Clear();
            Platform.PosConvertFuncs.Add("FILE", toFilePos);
            Platform.PosConvertFuncs.Add("MOVE", toMovePos);
            return 0;
        }


        protected override int RunLoop()
        {
            //start assert
            Platform.AssertAutoMode(this);
            Platform.LocateInPos("Wait");


            //wait start
            VioMeasureStart.WaitVioAndClear(this);
            {
                //assert
                Project.AssertNoNull(this);
                Product.AssertNoNull(this);

                //get focus z
                var focusInnerZ = Platform["FocusInner"]?.Data()[2];
                var focusOuterZ = Platform["FocusOuter"]?.Data()[2];
                var focusProfileZ = Platform["FocusProfile"]?.Data()[2];
                var focusPedZ = Platform["FocusPed"]?.Data()[2];


                int index = 0;
                foreach (var pos in Project.CapturePos)
                {
                    //conver FILE POS TO MOVE POS
                    var newPos = new PosXYZ(Platform.GetPos("MOVE", pos.Data()));

                    //select obj pos z height
                    switch (pos.Description)
                    {
                        case "inner":
                        case "Inner":
                            newPos.Z = focusInnerZ ?? 0;
                            break;
                        case "outer":
                        case "Outer":
                            newPos.Z = focusOuterZ ?? 0;
                            break;
                        case "barcode":
                        case "profile":
                        case "Profile":
                            newPos.Z = focusProfileZ ?? 0;
                            break;
                        case "ped":
                        case "Ped":
                            newPos.Z = focusPedZ ?? 0;
                            break;
                        default:
                            Log($"POS ERROR : {index} {pos} Not Found Z Focus");
                            continue;
                    }


                    //select jumpHeight
                    var jumpHeight = -10;

                    Platform.Jump(newPos, jumpHeight, zLimit: -12);


                    //capture
                    if (pos.Description == "barcode")
                    {
                        Camera.TriggerBarcode();
                        Log($"{Camera} TriggerBarcode {index} {Camera.GetResult(string.Empty)}");
                    }
                    else
                    {
                        //only add index on trigger product

                        if (!Camera.TriggerProduct(index++))
                        {
                            Product.Error = "CAPTURE ERROR";
                            Log($"{Camera} TriggerProduct {index - 1} {Camera.GetResult(string.Empty)} ERROR");
                            //break;
                        }
                        else
                        {
                            Log($"{Camera} TriggerProduct {index - 1} {Camera.GetResult(string.Empty)} OK");
                        }

                    }
                }


                //todo process all capture data
                {

                }

                Platform.MoveAbs("Wait");
            }
            //measure finish
            VioMeasureFinish.SetVio(this);

            return 0;
        }
    }
}
