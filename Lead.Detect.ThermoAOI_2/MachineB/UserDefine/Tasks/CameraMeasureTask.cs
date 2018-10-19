using System;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks
{
    public class CameraMeasureTask : StationTask
    {
        public int Stage1StepCount;
        public int Stage2StepCount;

        public IVioEx VioMeasureStart1;
        public IVioEx VioMeasureFinish1;


        public ThermoCameraB Camera;
        public PlatformEx Platform;


        public MachineSettings CfgSettings;
        public MeasureProjectB Project;
        public Thermo2ProductB Product;


        public CameraMeasureTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart1 = station.Machine.Find<IVioEx>("VioMeasureC1Start1");
            VioMeasureFinish1 = station.Machine.Find<IVioEx>("VioMeasureC1Finish1");

            if (FrameworkExtenion.IsSimulate)
            {
                Camera = new ThermoCameraBSim();
            }
            else
            {
                Camera = new ThermoCameraB();
            }
            Platform = station.Machine.Find<PlatformEx>("CameraPlatform");
        }

        protected override int ResetLoop()
        {
            //load settings
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);


            //reset vio
            VioMeasureStart1.SetVio(this, false);
            VioMeasureFinish1.SetVio(this, false);


            //connect camera
            try
            {
                Camera.Disconnect();
                Camera.Connect();

                Camera.SwitchProduct(1);
            }
            catch (Exception ex)
            {
                Log($"{Camera} Connect Fail:{ex.Message}");
                return 0;
            }



            //reset platform
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


            //measure process 1
            var stage1Step = 0;
            var capturePos1 = Project.CapturePos.FindAll(p => p.Name == "C1");
            while (stage1Step++ < Stage1StepCount)
            {
                VioMeasureStart1.WaitVioAndClear(this);
                if (capturePos1.Count > 0)
                {
                    //assert
                    Project.AssertNoNull(this);
                    Product.AssertNoNull(this);

                    //measure
                    var pos = capturePos1[stage1Step - 1];
                    {
                        //move capture pos, adjust focus
                        Platform.MoveAbs(pos);

                        //trigger
                        Camera.TriggerProduct(stage1Step);
                        Log($"{Camera.Name} Trigger {stage1Step} {Camera.GetResult(string.Empty)}");

                        //todo process cur data
                    }

                    //todo process all data
                    {

                    }
                }
                VioMeasureFinish1.SetVio(this);
            }

            //measure process 2
            var stage2Step = 0;
            var capturePos2 = Project.CapturePos.FindAll(p => p.Name == "C2");
            while (stage2Step++ < Stage1StepCount)
            {
                VioMeasureStart1.WaitVioAndClear(this);
                if (capturePos2.Count > 0)
                {
                    //assert
                    Project.AssertNoNull(this);
                    Product.AssertNoNull(this);

                    //measure
                    var pos = capturePos2[stage2Step - 1];
                    {
                        //move capture pos, adjust focus
                        Platform.MoveAbs(pos);

                        //trigger
                        Camera.TriggerProduct(3 + stage2Step);
                        Log($"{Camera.Name} Trigger {3 + stage2Step} {Camera.GetResult(string.Empty)}");
                        //todo process cur data
                    }

                    //todo process all data
                    {

                    }
                }
                VioMeasureFinish1.SetVio(this);
            }


            Platform.MoveAbs("Wait");

            return 0;
        }
    }
}
