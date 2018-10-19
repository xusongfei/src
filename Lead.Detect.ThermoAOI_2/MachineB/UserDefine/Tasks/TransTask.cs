using System.Collections.Generic;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks
{
    public class TransTask : StationTask
    {
        public TestProcessControl<Thermo2ProductB> TestProcessControl = new TestProcessControl<Thermo2ProductB>();

        public DualStartButton DualStartButton;
        public MultiDiSensorCheck DualSensorCheck;
        public MultiClampCylinders MultiClampCylinders;

        public IVioEx VioMeasureStart;
        public IVioEx VioMeasureFinish;

        public MeasureTask MeasureTask;


        public PlatformEx Platform;


        public MachineSettings CfgSettings;
        public MeasureProjectB Project;
        public Thermo2ProductB Product;


        public TransTask(int id, string name, Station station) : base(id, name, station)
        {
            DualStartButton = new DualStartButton()
            {
                DiStart1 = station.Machine.Find<IDiEx>("DiStart1"),
                DiStart2 = station.Machine.Find<IDiEx>("DiStart2"),
            };

            DualSensorCheck = new MultiDiSensorCheck()
            {
                DISensors = new List<IDiEx>()
                {
                    station.Machine.Find<IDiEx>("DiSensor1"),
                    station.Machine.Find<IDiEx>("DiSensor2"),
                }
            };

            MultiClampCylinders = new MultiClampCylinders()
            {
                Clamps = new List<ICylinderEx>()
                {
                    station.Machine.Find<ICylinderEx>("LRCY"),
                    station.Machine.Find<ICylinderEx>("FBCY"),
                }
            };


            Platform = station.Machine.Find<PlatformEx>("TransPlatform");

            VioMeasureStart = station.Machine.Find<IVioEx>("VioMeasureC1Start1");
            VioMeasureFinish = station.Machine.Find<IVioEx>("VioMeasureC1Finish1");
        }

        protected override int ResetLoop()
        {
            //load files
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);


            //reset vio
            VioMeasureStart.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            //reset gui
            TestProcessControl.OnTestStartEvent(null);

            //wait task finsih
            MeasureTask = Station.Machine.Find<StationTask>("MeasureTask") as MeasureTask;
            MeasureTask.AssertNoNull(this);
            MeasureTask.WaitResetFinish(this);

            MeasureTask.Project = Project;


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


            //show barcode read form;
            RunBarcodeScanner();


            //Wait start button
            DualStartButton.WaitStart(this, CfgSettings.AutoDryRun);
            //check position sensor ok
            if (CfgSettings.SensorEnable && !DualSensorCheck.Check(this))
            {
                return 0;
            }

            MultiClampCylinders.Clamp(this);


            //create product
            Product = new Thermo2ProductB();
            MeasureTask.Product = Product;
            TestProcessControl.OnTestStartEvent(Product);

            TestProcessControl.OnTestingEvent(Product);
            RunMeasureLoop("camera1", new[] {"Work11", "Work12", "Work13"}, 3);
            RunMeasureLoop("laser1", new[] {"Work30", "Work31"}, 2);
            RunMeasureLoop("laser2", new[] {"Work40", "Work41"}, 2);
            RunMeasureLoop("camera2", new[] {"Work21", "Work22", "Work23"}, 3);

            //todo process data
            {
                Product.UpdateStatus();
                Product.Save();
                CfgSettings.Production.Update(Product);
            }

            //move wait
            Platform.MoveAbs("Wait");
            MultiClampCylinders.Reset(this);

            TestProcessControl.OnTestFinishEvent(Product);
            return 0;
        }

        private void RunBarcodeScanner()
        {
        }

        private void RunMeasureLoop(string loopName, string[] workPos, int stepCount)
        {
            //move measure 2
            var step = 0;

            while (step++ < stepCount)
            {
                Platform.MoveAbs(workPos[step - 1]);
                //measure loop
                {
                    VioMeasureStart.SetVio(this);
                    Log($"Measure {loopName} {step} Process Start");


                    VioMeasureFinish.WaitVioAndClear(this);
                    Log($"Measure {loopName} {step} Process Finish");

                    //todo process data 2
                    {
                    }
                }
            }
        }
    }
}