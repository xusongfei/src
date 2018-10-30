﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI2.MachineB.View;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks
{
    public class TransTask : StationTask
    {
        public TestProcessControl<Thermo2ProductB> TestProcessControl = new TestProcessControl<Thermo2ProductB>();

        public DualStartButton DualStartButton;
        public MultiDiSensorCheck MultiSensorCheck;
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

            MultiSensorCheck = new MultiDiSensorCheck()
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
                    station.Machine.Find<ICylinderEx>("FBCY"),
                    station.Machine.Find<ICylinderEx>("LRCY"),
                }
            };


            Platform = station.Machine.Find<PlatformEx>("TransPlatform");

            VioMeasureStart = station.Machine.Find<IVioEx>("VioMeasureStart");
            VioMeasureFinish = station.Machine.Find<IVioEx>("VioMeasureFinish");
        }

        protected override int ResetLoop()
        {
            //load files
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);

            Platform.AssertPosTeached("Wait", this);


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
            var barcode = RunBarcodeScanner();
            if (CfgSettings.QuitOnProductError && string.IsNullOrEmpty(barcode))
            {
                return 0;
            }


            //Wait start button
            DualStartButton.WaitStart(this, CfgSettings.AutoDryRun);
            //check position sensor ok
            if (CfgSettings.SensorEnable && !MultiSensorCheck.Check(this))
            {
                return 0;
            }

            //create product
            Product = new Thermo2ProductB()
            {
                Barcode = barcode, SPCItems = Project.SPCItems,
            };
            MeasureTask.Product = Product;


            //start measure
            MultiClampCylinders.Clamp(this);
            TestProcessControl.OnTestStartEvent(Product);
            VioMeasureStart.SetVio(this, true);
            {
                TestProcessControl.OnTestingEvent(Product);
            }
            VioMeasureFinish.WaitVioAndClear(this);
            Platform.MoveAbs("Wait");
            MultiClampCylinders.Reset(this);


            //save product data
            {
                Product.UpdateStatus();
                CfgSettings.Production.Update(Product);
                TestProcessControl.OnTestFinishEvent(Product);
                try
                {
                    Product.Save();
                }
                catch (Exception e)
                {
                    Log($"Save Fail:{e.Message}", LogLevel.Warning);
                    Station.Machine.Beep();
                }
            }
            return 0;
        }

        private string RunBarcodeScanner()
        {
            if (CfgSettings.BarcodeEnable)
            {
                var barcodeForm = new ScanBarcodeForm()
                {
                    BarcodeLen = CfgSettings.BarcodeLength,
                    BarcodePattern = CfgSettings.BarcodePattern,
                    Task = this,
                };

                if (barcodeForm.ShowDialog() == DialogResult.OK)
                {
                    return barcodeForm.Barcode;
                }
            }

            return string.Empty;
        }


    }
}