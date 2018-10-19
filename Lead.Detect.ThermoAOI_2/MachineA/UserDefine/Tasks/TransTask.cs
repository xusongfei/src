﻿using System.Collections.Generic;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

namespace Lead.Detect.ThermoAOI2.MachineA.UserDefine.Tasks
{
    /// <summary>
    /// Machine A 传送任务
    /// </summary>
    public class TransTask : StationTask
    {
        public TestProcessControl<Thermo2ProductA> TestProcessControl = new TestProcessControl<Thermo2ProductA>();

        public DualStartButton DualStartButton;
        public MultiDiSensorCheck DualSensorCheck;
        public MultiClampCylinders MultiClampCylinders;
        public MultiVaccumWithSensor MultiVaccums;

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

            DualSensorCheck = new MultiDiSensorCheck
            {
                DISensors = new List<IDiEx>()
                {
                    station.Machine.Find<IDiEx>("DiSensor1"),
                    station.Machine.Find<IDiEx>("DiSensor2"),
                    station.Machine.Find<IDiEx>("DiSensor3"),
                }
            };

            MultiClampCylinders = new MultiClampCylinders()
            {
                Clamps = new List<ICylinderEx>()
                {
                    station.Machine.Find<ICylinderEx>("FCY"),
                    station.Machine.Find<ICylinderEx>("BCY"),
                    station.Machine.Find<ICylinderEx>("RCY"),
                    station.Machine.Find<ICylinderEx>("LCY"),
                }
            };

            MultiVaccums = new MultiVaccumWithSensor()
            {
                VaccumDoExs = new List<IDoEx>()
                {
                    station.Machine.Find<IDoEx>("DOVaccum1"),
                    station.Machine.Find<IDoEx>("DOVaccum2"),
                },
                VaccumSensors = new List<IDiEx>()
                {
                    station.Machine.Find<IDiEx>("DiVaccum1"),
                    station.Machine.Find<IDiEx>("DiVaccum2")
                }
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


            //reset vio
            VioMeasureStart.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);


            //reset
            TestProcessControl.OnTestStartEvent(null);
            MeasureTask = Station.Machine.Find<StationTask>("MeasureTask") as MeasureTask;
            MeasureTask.AssertNoNull(this);
            MeasureTask.WaitResetFinish(this);

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


            //select sensor pattern by product
            var vcSensorPattern = new[] { true, true, false };
            var moduleSensorPattern = new[] { true, true, true };
            bool[] sensor;
            if (Project.ProductName.EndsWith("NoFin"))
            {
                sensor = vcSensorPattern;
            }
            else
            {
                sensor = moduleSensorPattern;
            }
            

            //wait start button
            DualStartButton.WaitStart(this, CfgSettings.AutoDryRun);
            //check position sensor ok
            if (CfgSettings.SensorEnable && !DualSensorCheck.CheckByPattern(this, sensor))
            {
                return 0;
            }
            //clamp cylinders
            MultiClampCylinders.Clamp(this);
            MultiVaccums.SetVaccum(this, true);


            //create new product, pass to measure task
            Product = new Thermo2ProductA();
            MeasureTask.Product = Product;
            TestProcessControl.OnTestStartEvent(Product);


            //start measure task
            Platform.MoveAbs("Work");
            VioMeasureStart.SetVio(this);
            {
                TestProcessControl.OnTestingEvent(Product);
            }
            VioMeasureFinish.WaitVioAndClear(this);


            //todo process data
            {
                Product.UpdateStatus();
                Product.Save();
                CfgSettings.Production.Update(Product);
            }


            //move wait pos
            Platform.MoveAbs("Wait");
            MultiClampCylinders.Reset(this);
            MultiVaccums.SetVaccum(this, false);

            TestProcessControl.OnTestFinishEvent(Product);
            return 0;
        }
    }
}
