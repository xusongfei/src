using System.Collections.Generic;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

namespace Lead.Detect.ThermoAOI2.MachineA.UserDefine.Tasks
{
    public class CarrierLoader
    {
        public StationTask Task;

        public ICylinderEx CyLeft;
        public ICylinderEx CyBack;
        public ICylinderEx CyRight;
        public ICylinderEx CyFront;


        public IDoEx Vaccum1;
        public IDoEx Vaccum2;

        public IDiEx VaccumSensor1;
        public IDiEx VaccumSensor2;


        public IDiEx CarrierSensor1;
        public IDiEx CarrierSensor2;


        public bool IsProductExists()
        {
            return CarrierSensor1.GetDiSts() || CarrierSensor2.GetDiSts();
        }



        public void ClampVC()
        {
            if (!CyLeft.WaitDi(Task, timeout: 0, isErrorOnSignalError: false) || !CyBack.WaitDi(Task, timeout: 0, isErrorOnSignalError: false))
            {
                new[] { CyLeft, CyFront }.SetDo(Task, new[] { true, true });
            }

            new[] { CyRight, CyBack }.SetDo(Task, new[] { true, true });

            Vaccum1.SetDo(true);
            Vaccum2.SetDo(true);

            new[] { CyRight, CyBack }.SetDo(Task, new[] { false, false });
            new[] { CyLeft, CyFront }.SetDo(Task, new[] { false, false });
        }


        public void ClampModule()
        {
            if (!CyLeft.WaitDi(Task, timeout: 0, isErrorOnSignalError: false) || !CyBack.WaitDi(Task, timeout: 0, isErrorOnSignalError: false))
            {
                new[] { CyLeft, CyFront }.SetDo(Task, new[] { true, true });
            }

            new[] { CyRight, CyBack }.SetDo(Task, new[] { true, true });

            //Vaccum1.SetDo(true);
            //Vaccum2.SetDo(true);

            //new[] { CyRight, CyBack }.SetDo(Task, new[] { false, false });
            //new[] { CyLeft, CyFront }.SetDo(Task, new[] { false, false });
        }


        public void ReleaseVC()
        {
            new[] { CyLeft, CyFront }.SetDo(Task, new[] { true, true });

            Vaccum1.SetDo(false);
            Vaccum2.SetDo(false);
        }


        public void ReleaseModule()
        {
            new[] { CyLeft, CyFront }.SetDo(Task, new[] { true, true });
            new[] { CyRight, CyBack }.SetDo(Task, new[] { false, false });
            Vaccum1.SetDo(false);
            Vaccum2.SetDo(false);
        }

    }



    /// <summary>
    /// Machine A 传送任务
    /// </summary>
    public class TransTask : StationTask
    {
        public TestProcessControl<Thermo2ProductA> TestProcessControl = new TestProcessControl<Thermo2ProductA>();

        public DualStartButton DualStartButton;
        public MultiDiSensorCheck MultiSensorCheck;
        public CarrierLoader CarrierLoader;



        public IVioEx VioMeasureStart;
        public IVioEx VioMeasureFinish;


        public MeasureTask MeasureTask;

        public PlatformEx Platform;


        public MachineSettings CfgSettings;
        public MeasureProjectA Project;
        public Thermo2ProductA Product;


        public TransTask(int id, string name, Station station) : base(id, name, station)
        {
            DualStartButton = new DualStartButton
            {
                DiStart1 = station.Machine.Find<IDiEx>("DiStart1"),
                DiStart2 = station.Machine.Find<IDiEx>("DiStart2")
            };

            MultiSensorCheck = new MultiDiSensorCheck
            {
                DISensors = new List<IDiEx>()
                {
                    station.Machine.Find<IDiEx>("DiSensor2"),
                    station.Machine.Find<IDiEx>("DiSensor3"),
                    station.Machine.Find<IDiEx>("DiSensor1"),
                }
            };


            CarrierLoader = new CarrierLoader()
            {
                Task = this,
                CyLeft = station.Machine.Find<ICylinderEx>("LCY"),
                CyBack = station.Machine.Find<ICylinderEx>("BCY"),
                CyFront = station.Machine.Find<ICylinderEx>("FCY"),
                CyRight = station.Machine.Find<ICylinderEx>("RCY"),


                Vaccum1 = station.Machine.Find<IDoEx>("DOVaccum1"),
                Vaccum2 = station.Machine.Find<IDoEx>("DOVaccum2"),

                VaccumSensor1 = station.Machine.Find<IDiEx>("DiVaccum1"),
                VaccumSensor2 = station.Machine.Find<IDiEx>("DiVaccum2"),

                CarrierSensor1 = station.Machine.Find<IDiEx>("DiSensor2"),
                CarrierSensor2 = station.Machine.Find<IDiEx>("DiSensor3"),
            };


            VioMeasureStart = station.Machine.Find<IVioEx>("VioMeasureStart");
            VioMeasureFinish = station.Machine.Find<IVioEx>("VioMeasureFinish");


            Platform = station.Machine.Find<PlatformEx>("TransPlatform");
        }

        protected override int ResetLoop()
        {
            //load files
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectA)) as MeasureProjectA;
            Project.AssertNoNull(this);

            Platform.AssertPosTeached("Wait", this);

            //reset vio
            VioMeasureStart.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            var isProductFound = CarrierLoader.IsProductExists();
            if (isProductFound)
            {
                CarrierLoader.ClampVC();
            }
            else
            {
                CarrierLoader.ReleaseVC();
            }


            //reset
            TestProcessControl.OnTestStartEvent(null);
            MeasureTask = Station.Machine.Find<StationTask>("MeasureTask") as MeasureTask;
            MeasureTask.AssertNoNull(this);
            MeasureTask.WaitResetFinish(this);


            if (isProductFound)
            {
                CarrierLoader.ReleaseVC();
                Station.Machine.Beep();
            }


            //reset platform
            Platform.EnterAuto(this);
            //Platform.EnterAuto(this).Servo();
            //Platform.EnterAuto(this).Home();
            //Platform.EnterAuto(this).MoveAbs("Wait");
            return 0;
        }


        protected override int RunLoop()
        {
            //start assert
            Platform.AssertAutoMode(this);
            Platform.LocateInPos("Wait");
            Project.AssertNoNull(this);


            //wait start button
            DualStartButton.WaitStart(this, CfgSettings.AutoDryRun);

            //check position sensor ok
            if (CfgSettings.SensorEnable)
            {
                //select sensor pattern by product
                var vcSensorPattern = new[] { true, true, false };
                var moduleSensorPattern = new[] { true, true, true };
                bool[] sensor;
                if (Project.ProductName.Contains("NoFin") || Project.ProductName.ToUpper().Contains("VC"))
                {
                    sensor = vcSensorPattern;
                }
                else
                {
                    sensor = moduleSensorPattern;
                }
                if (!MultiSensorCheck.CheckByPattern(this, sensor))
                {
                    return 0;
                }
            }

            //create new product, pass to measure task
            Product = new Thermo2ProductA();
            Product.SPCItems = Project.SPCItems;
            MeasureTask.Product = Product;
            Log("Start:" + Product.ToString(), LogLevel.Info);

            //clamp cylinders
            if (Project.ProductName.Contains("NoFin") || Project.ProductName.ToUpper().Contains("VC"))
            {
                CarrierLoader.ClampVC();
            }
            else
            {
                CarrierLoader.ClampModule();
            }

            TestProcessControl.OnTestStartEvent(Product);

            //start measure task
            Platform.MoveAbs("Work");
            VioMeasureStart.SetVio(this);
            {
                TestProcessControl.OnTestingEvent(Product);
            }
            VioMeasureFinish.WaitVioAndClear(this);

            //move wait pos
            Platform.MoveAbs("Wait");

            if (Project.ProductName.Contains("WithFin") || Project.ProductName.Contains("Module"))
            {
                CarrierLoader.ReleaseModule();
            }
            else
            {
                CarrierLoader.ReleaseVC();
            }


            //todo process data
            {
                Product.UpdateStatus();
                Product.Save();
                CfgSettings.Production.Update(Product);

                Log("Finish:" + Product.ToString(), LogLevel.Info);
            }

            TestProcessControl.OnTestFinishEvent(Product);
            return 0;
        }
    }
}
