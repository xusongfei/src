using System;
using System.Collections.Generic;
using Lead.Detect.DatabaseHelper;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.Utils;
using Lead.Detect.MachineUtilityLib.UtilsFramework;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOIProductLib.Thermo2;
using Lead.Detect.ThermoAOIProductLib.ThermoDataConvert;

namespace Lead.Detect.ThermoAOI2.MachineA.UserDefine.Tasks
{
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


            try
            {
                Product = new Thermo2ProductA();
                Product.ProductType = Project.ProductName;
                Product.Description = Station.Name;
                Product.SPCItems = Project.SPCItems;
                if (Machine.Ins.Settings.EnableFTP)
                {
                    var avcdata = ThermoProductConvertHelper.Convert(Product, "", Machine.Ins.Settings.Description);
                    avcdata.Save(Machine.Ins.Settings.FTPAddress);
                }

            }
            catch (Exception ex)
            {
                Log($"连接FTP ERROR: {ex.Message}", LogLevel.Error);
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
                if (Project.ThermoProductType == ThermoProductType.VaporChamber)
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
            Product = new Thermo2ProductA
            {
                ProductType = Project.ThermoProductType.ToString(),
                Description = Project.ProductName + "-" + CfgSettings.Version,
                SPCItems = Project.SPCItems
            };
            Product.ClearSpc();
            MeasureTask.Product = Product;
            Log("Start:" + Product.ToString(), LogLevel.Info);

            //clamp cylinders
            if (Project.ThermoProductType == ThermoProductType.VaporChamber)
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

            if (Project.ThermoProductType == ThermoProductType.VaporChamber)
            {
                CarrierLoader.ReleaseVC();

            }
            else
            {
                CarrierLoader.ReleaseModule();
            }


            SaveProductData();
            return 0;
        }

        private void SaveProductData()
        {
            //todo process data
            {
                try
                {
                    Product.UpdateStatus();
                    Product.Save();
                    Product.ToEntity().Save();
                    CfgSettings.Production.Update(Product);
                    Log("Save Product Finish:" + Product.ToString(), LogLevel.Info);

                    if (Machine.Ins.Settings.EnableFTP)
                    {
                        var avcdata = ThermoProductConvertHelper.Convert(Product, Project.PartID, Machine.Ins.Settings.Description);
                        avcdata.Save(Machine.Ins.Settings.FTPAddress);
                        avcdata.Save("AVCData");
                        Log("Upload AvcData Finish:" + avcdata.ToString(), LogLevel.Info);
                    }


                    if (Product.Status == ProductStatus.ERROR && CfgSettings.BeepOnProductError)
                    {
                        Station.Machine.Beep();
                    }
                }
                catch (Exception ex)
                {
                    Log($"保存数据失败：{ex.Message}", LogLevel.Warning);
                    Station.Machine.Beep();
                }
            }

            TestProcessControl.OnTestFinishEvent(Product);
        }
    }
}
