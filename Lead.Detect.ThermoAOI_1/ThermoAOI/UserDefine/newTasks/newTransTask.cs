using System;
using System.ComponentModel;
using System.Threading;
using Lead.Detect.DatabaseHelper;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.Utils;
using Lead.Detect.MachineUtilityLib.UtilsFramework;
using Lead.Detect.ThermoAOI.Machine1.Calibration;
using Lead.Detect.ThermoAOI.Machine1.Common;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using Lead.Detect.ThermoAOIProductLib.Thermo1Calculator;
using Lead.Detect.ThermoAOIProductLib.ThermoDataConvert;

namespace Lead.Detect.ThermoAOI.Machine1.UserDefine.newTasks
{
    public class XyClamper
    {
        public StationTask Task;

        public ICylinderEx CyX;
        public ICylinderEx CyY;

        public void ClampVC()
        {
            CyY.SetDo(Task, true, 100, null);
            CyY.SetDo(Task, false, 150, null);
            CyX.SetDo(Task, true, 200, null);
            CyX.SetDo(Task, false, 100, null);
            CyY.SetDo(Task, true, 100, null);
        }

        public void ClampModule()
        {
            CyY.SetDo(Task, true, 100, null);
            CyY.SetDo(Task, false, 100, null);
            CyX.SetDo(Task, true, 100, null);
            CyX.SetDo(Task, false, 100, null);
            CyY.SetDo(Task, true, 100, null);
            CyX.SetDo(Task, true, 100, null);
        }

        public void Release()
        {
            CyY.SetDo(Task, false, 100, null);
            CyX.SetDo(Task, false, 100, null);
        }
    }


    /// <summary>
    /// 载具平移任务
    /// </summary>
    public class newTransTask : StationTask
    {
        public TestProcessControl<Thermo1Product> TestProcessControl = new TestProcessControl<Thermo1Product>();

        public IDiEx DISensorCheck1;
        public IDiEx DISensorCheck2;
        public IDiEx DISensorCheckFin1;
        public IDiEx DISensorCheckFin2;
        public IDiEx DIStart1;
        public IDiEx DIStart2;

        public XyClamper Clamper;


        public ICylinderEx DoClampCylinderX;
        public ICylinderEx DoClampCylinderY;
        public IDoEx DOBtnLight1;
        public IDoEx DOBtnLight2;


        //set vio
        [Description("OUTPUT")] public IVioEx VioTransInp;
        [Description("OUTPUT")] public IVioEx VioTransFinishUp;
        [Description("OUTPUT")] public IVioEx VioTransFinishDown;

        //wait vio
        [Description("INPUT")] public IVioEx VioBarcodeFinish;
        [Description("INPUT")] public IVioEx VioMeasureFinishUp;
        [Description("INPUT")] public IVioEx VioMeasureFinishDown;

        [Description("INPUT")] public newMeasureUpTask WaitTaskUp;
        [Description("INPUT")] public newMeasureDownTask WaitTaskDown;


        public IDoEx DOBrakeZPress;
        public PlatformEx Platform;

        public KeyenceGT GtController;
        public string GtAddress;
        public int GtPort;

        public Thermo1Product Product;
        public MeasureProject1 Project;
        public MachineSettings CfgSettings;
        public Thermo1GeometryCalculator Thermo1GeometryCalculator;


        public DataUploadHelper UploadHelper;


        public newTransTask(int id, string name, Station station) : base(id, name, station)
        {
            GtController = new KeyenceGT();
        }


        protected override int ResetLoop()
        {
            Clamper = new XyClamper()
            {
                Task = this,
                CyX = DoClampCylinderX,
                CyY = DoClampCylinderY,
            };

            Platform.AssertPosTeached("Wait", this);
            Platform.AssertPosTeached("Work", this);

            CfgSettings = Machine.Ins.Settings;


            if (!Project.CheckIfNormal())
            {
                ThrowException($"Project Error: {Project.ProductName}");
            }


            Thermo1GeometryCalculator = Thermo1CalculatorMgr.Ins.New(Project.ProductName);
            if (Thermo1GeometryCalculator == null || !Thermo1GeometryCalculator.CheckIfNormal())
            {
                ThrowException($"Station {Name} Load Thermo1GeometryCalculator for {Project.ProductName} Fail");
            }


            try
            {
                Product = new Thermo1Product();
                Product.ProductType = Project.ThermoProductType.ToString();
                Product.Description = string.Join("-", new[] {Station.Name, Project.ProductName, CfgSettings.Version});
                Product.SPCItems = Project.SPCItems;

                TestProcessControl.OnTestStartEvent(Product);


                //upload data
                if (CfgSettings.Uploader.Enable)
                {
                    //init uploader
                    UploadHelper = DataUploadFactory.Ins.Create(CfgSettings.Uploader.UploaderName, CfgSettings.Uploader);
                    if (UploadHelper == null)
                    {
                        Log($"创建上传模块失败: {CfgSettings.Uploader.UploaderName} 不存在", LogLevel.Error);
                    }
                    else
                    {
                        UploadData();
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"连接FTP ERROR: {ex.Message}", LogLevel.Error);
            }


            DOBtnLight1.SetDo(false);
            DOBtnLight2.SetDo(false);
            Thread.Sleep(500);
            DOBtnLight1.SetDo();
            DOBtnLight2.SetDo();


            //reset vio
            VioTransInp.SetVio(this, false);
            VioBarcodeFinish.SetVio(this, false);
            VioTransFinishUp.SetVio(this, false);
            VioTransFinishDown.SetVio(this, false);

            //start resetting process
            //check air pressure
            //if (!DIAirPressure1.GetDiSts(MotionWrapper))
            //{
            //    return -1;
            //}

            //check gt controller
            try
            {
                if (GtController == null)
                {
                    ThrowException("GT Controller not Created");
                }

                if (GtController.Connected)
                {
                    GtController.Close();
                    GtController = new KeyenceGT();
                }

                GtController.Connect(GtAddress, GtPort);
            }
            catch (Exception e)
            {
                ThrowException($"GT Controller Connected Fail:{e.Message}");
            }


            //wait measure task
            WaitTaskDown.AssertNoNull(this);
            WaitTaskDown.WaitResetFinish(this);
            WaitTaskUp.AssertNoNull(this);
            WaitTaskUp.WaitResetFinish(this);


            WaitTaskUp.GtController = GtController;
            WaitTaskDown.GtController = GtController;

            DOBrakeZPress.SetDo();

            Clamp(true);

            //home platform
            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home();
            Platform.EnterAuto(this).MoveAbs("Wait", checkLimit: false);

            //cy clamp
            Clamper.Release();

            //check sensor
            //while (!DISensorCheck1.GetDiSts( false) || !DISensorCheck2.GetDiSts( false))
            //{
            //    MessageBox.Show("product found");
            //}

            DOBtnLight1.SetDo(false);
            DOBtnLight2.SetDo(false);

            RunningState = RunningState.WaitRun;
            GtController.RunGtService(this);
            return 0;
        }

        protected override int RunLoop()
        {
            //in case of manual operations
            Platform.AssertAutoMode(this);


            //wait start
            while ((!DIStart1.GetDiSts() || !DIStart2.GetDiSts()))
            {
                Thread.Sleep(100);
                JoinIfPause();
                AbortIfCancel("cancel trans wait start");
                if (Station.Id == 1 && Machine.Ins.Settings.Common.LeftAutoTryRun)
                {
                    break;
                }

                if (Station.Id == 2 && Machine.Ins.Settings.Common.RightAutoTryRun)
                {
                    break;
                }
            }

            Log(string.Empty, LogLevel.None);


            if ((Station.Id == 1 && Machine.Ins.Settings.Common.LeftSensorCheck)
                || (Station.Id == 2 && Machine.Ins.Settings.Common.RightSensorCheck))
            {
                //检查定位传感器
                if (!DISensorCheck1.GetDiSts() || !DISensorCheck2.GetDiSts())
                {
                    Station.Machine.Beep();
                    Log($"{Station.Name} - {Name} 定位传感器检测异常", LogLevel.Warning);
                    return 0;
                }
            }

            if ((Station.Id == 1 && Machine.Ins.Settings.Common.LeftFinSensorCheck)
                || (Station.Id == 2 && Machine.Ins.Settings.Common.RightFinSensorCheck))
            {
                //检查fin传感器
                if (!CheckProductFin())
                {
                    //return to wait start
                    return 0;
                }
            }

            //new product
            Product = new Thermo1Product()
            {
                ProductType = Project.ThermoProductType.ToString(),
                Description = Station.Name + "-" + Project.ProductName + "-" + CfgSettings.Version,
                SPCItems = Project.SPCItems,
            };
            Product.ClearSpc();
            //push data to measure tasks
            WaitTaskDown.Product = Product;
            WaitTaskUp.Product = Product;
            TestProcessControl.OnTestStartEvent(Product);
            TestProcessControl.OnTestingEvent(Product);

            DOBtnLight1.SetDo();
            DOBtnLight2.SetDo();

            //cy clamp
            if (Project.ProjectName.Contains("HeightCalib"))
            {
                DoClampCylinderY.SetDo(this, true, 100, ignoreOrWaringOrError: null);
            }
            else if (Project.ThermoProductType == ThermoProductType.FullModule)
            {
                Clamper.ClampModule();
            }
            else if (Project.ThermoProductType == ThermoProductType.VaporChamber)
            {
                Clamper.ClampVC();
            }
            else
            {
                Clamp(true);
            }


            //move work 
            Platform.MoveAbs(0, "Work");
            {
                VioTransInp.SetVio(this);

                //start waiting
                Log("Measure Start......\n-----------------------------------------------", LogLevel.Info);
                //set measure start
                VioTransFinishUp.SetVio(this);
                VioTransFinishDown.SetVio(this);
                {
                    //wait barcode finish update barcode
                    VioBarcodeFinish.WaitVioAndClear(this);
                    TestProcessControl.OnTestingEvent(Product);
                }
                //wait measure finish
                VioMeasureFinishUp.WaitVioAndClear(this);
                VioMeasureFinishDown.WaitVioAndClear(this);
                Log("Measure Finish......\n-----------------------------------------------", LogLevel.Info);
            }
            //move wait pos
            Platform.MoveAbs(0, "Wait");

            //update results
            //calc flatness
            if (Thermo1GeometryCalculator != null)
            {
                //transform raw data to same coord
                GTTransform.TransformRawData(Station.Name, CfgSettings.Calibration, Product);
                var data = Thermo1GeometryCalculator.Calculate(Product);
                Log($"Flatness Calc: {data.ToString()}");
            }

            SaveProductData();

            Clamper.Release();
            DOBtnLight1.SetDo(false);
            DOBtnLight2.SetDo(false);

            return 0;
        }

        private bool CheckProductFin()
        {
            if (Project.ThermoProductType == ThermoProductType.VaporChamber)
            {
                //fin sensor1 must be true (upfin)
                //fin sensor2 must be false
                bool status = DISensorCheckFin2.GetDiSts(false) && DISensorCheckFin1.GetDiSts();
                if (!status)
                {
                    Station.Machine.Beep();
                    var err = $"{Station.Name} - {Name} - {Project.ProductName} FIN 传感器异常";
                    Log(err, LogLevel.Warning);
                    return false;
                }
            }
            else if (Project.ThermoProductType == ThermoProductType.FullModule)
            {
                //fin sensor1 must be false (upfin)
                //fin sensor2 must be false
                bool status = DISensorCheckFin2.GetDiSts(false) && DISensorCheckFin1.GetDiSts(false);
                if (!status)
                {
                    Station.Machine.Beep();
                    var err = $"{Station.Name} - {Name} - {Project.ProductName} FIN 传感器异常";
                    Log(err, LogLevel.Warning);
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }


        private void Clamp(bool status)
        {
            new[] {DoClampCylinderX, DoClampCylinderY}.SetDo(this, new[] {status, status}, 100, null);
        }

        private void SaveProductData()
        {
            //save production data
            try
            {
                Product.UpdateStatus();
                if (Station.Id == 1)
                {
                    Machine.Ins.Settings.ProductionLeft.Update(Product);
                    Product.Save("LeftData");
                    Product.ToEntity().Save();
                }
                else if (Station.Id == 2)
                {
                    Machine.Ins.Settings.ProductionRight.Update(Product);
                    Product.Save("RightData");
                    Product.ToEntity().Save();
                }

                Log($"Save Product Finish: {Product}");

                UploadData();
            }
            catch (Exception ex)
            {
                Log($"保存数据失败：{ex.Message}", LogLevel.Warning);
                Station.Machine.Beep();
            }
            finally
            {
                if (Machine.Ins.Settings.Common.BeepOnProductNG && Product.Status != ProductStatus.OK)
                {
                    Station.Machine.Beep();
                }
            }

            TestProcessControl.OnTestFinishEvent(Product);
        }

        private void UploadData()
        {
            if (CfgSettings.Uploader.Enable)
            {
                var csvData = ThermoProductConvertHelper.Convert(Product, CfgSettings.Uploader);
                UploadHelper?.Upload(csvData);
                Log("Upload CSVDATA Finish:" + csvData, LogLevel.Info);
            }
        }
    }


    public class newLeftTransTask : newTransTask
    {
        public newLeftTransTask(int id, string name, Station station) : base(id, name, station)
        {
            DISensorCheck1 = Machine.Ins.Find<IDiEx>("LDISensorCheck1");
            DISensorCheck2 = Machine.Ins.Find<IDiEx>("LDISensorCheck2");
            DISensorCheckFin1 = Machine.Ins.Find<IDiEx>("LDISensorCheckFin1");
            DISensorCheckFin2 = Machine.Ins.Find<IDiEx>("LDISensorCheckFin2");
            DIStart1 = Machine.Ins.Find<IDiEx>("LDIStart1");
            DIStart2 = Machine.Ins.Find<IDiEx>("LDIStart2");

            DOBtnLight1 = Machine.Ins.Find<IDoEx>("LDOBtnLight1");
            DOBtnLight2 = Machine.Ins.Find<IDoEx>("LDOBtnLight2");
            DoClampCylinderX = Machine.Ins.Find<ICylinderEx>("LClampCylinderX");
            DoClampCylinderY = Machine.Ins.Find<ICylinderEx>("LClampCylinderY");
            DOBrakeZPress = Machine.Ins.Find<IDoEx>("LDOBrakeZPress");

            VioTransInp = Machine.Ins.Find<IVioEx>("LTransInp");
            VioBarcodeFinish = Machine.Ins.Find<IVioEx>("LBarcodeFinish");
            VioTransFinishUp = Machine.Ins.Find<IVioEx>("LTransFinishUp");
            VioTransFinishDown = Machine.Ins.Find<IVioEx>("LTransFinishDown");
            VioMeasureFinishUp = Machine.Ins.Find<IVioEx>("LMeasureFinishUp");
            VioMeasureFinishDown = Machine.Ins.Find<IVioEx>("LMeasureFinishDown");


            Platform = Machine.Ins.Find<PlatformEx>("LeftCarrier");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("LeftUp"), Machine.Ins.Find<PlatformEx>("LeftDown"), Machine.Ins.Find<ICylinderEx>("LGTCylinder"),
                SafeCheckType.Manual));
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("LeftUp"), Machine.Ins.Find<PlatformEx>("LeftDown"), Machine.Ins.Find<ICylinderEx>("LGTCylinder"),
                SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("LeftUp"), Machine.Ins.Find<PlatformEx>("LeftDown"), Machine.Ins.Find<ICylinderEx>("LGTCylinder"),
                SafeCheckType.Auto));
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("LeftUp"), Machine.Ins.Find<PlatformEx>("LeftDown"), Machine.Ins.Find<ICylinderEx>("LGTCylinder"),
                SafeCheckType.AutoHome));
        }


        protected override int ResetLoop()
        {
            WaitTaskDown = Machine.Ins.Find<StationTask>("LeftMeasureDown") as newMeasureDownTask;
            WaitTaskUp = Machine.Ins.Find<StationTask>("LeftMeasureUp") as newMeasureUpTask;

            GtAddress = Machine.Ins.Settings.Common.LeftGtController;
            GtPort = Machine.Ins.Settings.Common.LeftGtPort;

            //load product settings
            try
            {
                Project = MeasureProject.Load(Machine.Ins.Settings.LeftProjectFilePath, typeof(MeasureProject1)) as MeasureProject1;
                Project.AssertNoNull(this);
            }
            catch (Exception ex)
            {
                ThrowException($"{Name} 加载工程文件异常: {ex.Message}");
            }

            return base.ResetLoop();
        }
    }


    public class newRightTransTask : newTransTask
    {
        public newRightTransTask(int id, string name, Station station) : base(id, name, station)
        {
            DISensorCheck1 = Machine.Ins.Find<IDiEx>("RDISensorCheck1");
            DISensorCheck2 = Machine.Ins.Find<IDiEx>("RDISensorCheck2");
            DISensorCheckFin1 = Machine.Ins.Find<IDiEx>("RDISensorCheckFin1");
            DISensorCheckFin2 = Machine.Ins.Find<IDiEx>("RDISensorCheckFin2");
            DIStart1 = Machine.Ins.Find<IDiEx>("RDIStart1");
            DIStart2 = Machine.Ins.Find<IDiEx>("RDIStart2");

            DOBtnLight1 = Machine.Ins.Find<IDoEx>("RDOBtnLight1");
            DOBtnLight2 = Machine.Ins.Find<IDoEx>("RDOBtnLight2");
            DoClampCylinderX = Machine.Ins.Find<ICylinderEx>("RClampCylinderX");
            DoClampCylinderY = Machine.Ins.Find<ICylinderEx>("RClampCylinderY");
            DOBrakeZPress = Machine.Ins.Find<IDoEx>("RDOBrakeZPress");

            VioTransInp = Machine.Ins.Find<IVioEx>("RTransInp");
            VioBarcodeFinish = Machine.Ins.Find<IVioEx>("RBarcodeFinish");
            VioTransFinishUp = Machine.Ins.Find<IVioEx>("RTransFinishUp");
            VioTransFinishDown = Machine.Ins.Find<IVioEx>("RTransFinishDown");
            VioMeasureFinishUp = Machine.Ins.Find<IVioEx>("RMeasureFinishUp");
            VioMeasureFinishDown = Machine.Ins.Find<IVioEx>("RMeasureFinishDown");


            Platform = Machine.Ins.Find<PlatformEx>("RightCarrier");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("RightUp"), Machine.Ins.Find<PlatformEx>("RightDown"), Machine.Ins.Find<ICylinderEx>("RGTCylinder"),
                SafeCheckType.Manual));
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("RightUp"), Machine.Ins.Find<PlatformEx>("RightDown"), Machine.Ins.Find<ICylinderEx>("RGTCylinder"),
                SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("RightUp"), Machine.Ins.Find<PlatformEx>("RightDown"), Machine.Ins.Find<ICylinderEx>("RGTCylinder"),
                SafeCheckType.Auto));
            Platform.SafeChecks.Add(new CarrierSafeCheck(Machine.Ins.Find<PlatformEx>("RightUp"), Machine.Ins.Find<PlatformEx>("RightDown"), Machine.Ins.Find<ICylinderEx>("RGTCylinder"),
                SafeCheckType.AutoHome));
        }

        protected override int ResetLoop()
        {
            WaitTaskDown = Machine.Ins.Find<StationTask>("RightMeasureDown") as newMeasureDownTask;
            WaitTaskUp = Machine.Ins.Find<StationTask>("RightMeasureUp") as newMeasureUpTask;


            GtAddress = Machine.Ins.Settings.Common.RightGtController;
            GtPort = Machine.Ins.Settings.Common.RightGtPort;

            try
            {
                //load product settings
                Project = MeasureProject.Load(Machine.Ins.Settings.RightProjectFilePath, typeof(MeasureProject1)) as MeasureProject1;
                Project.AssertNoNull(this);
            }
            catch (Exception ex)
            {
                ThrowException($"{Name} 加载工程文件异常: {ex.Message}");
            }

            return base.ResetLoop();
        }
    }
}