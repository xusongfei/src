using System;
using System.ComponentModel;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI.Calibration;
using Lead.Detect.ThermoAOI.Common;
using Lead.Detect.ThermoAOI.Machine.Common;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;

namespace Lead.Detect.ThermoAOI.Machine.newTasks
{
    public class newMeasureDownTask : StationTask
    {
        public PlatformType PlatformType;

        public ICylinderEx DoGTCylinder;

        [Description("INPUT")]
        public IVioEx VioTransInp;
        [Description("INPUT")]
        public IVioEx VioTransFinish;

        [Description("OUTPUT")]
        public IVioEx VioBarcodeFinish;
        [Description("OUTPUT")]
        public IVioEx VioMeasureFinish;


        public PlatformEx Platform;
        public double PlatformJumpHeight;
        public PosXYZ PosWait;
        public PosXYZ PosBarcode;
        public PosXYZ PosGtWork1;
        public PosXYZ PosGtWork2;


        /// <summary>
        /// Gt控制器,从transTask加载
        /// </summary>
        [Description("INPUT")]
        public KeyenceGT GtController;
        /// <summary>
        /// 产品测试数据,从transTask加载
        /// </summary>
        [Description("INPUT")]
        public Thermo1Product ProductData;
        public ProductSettings CfgProductSettings;
        public CalibrationConfig CfgCalib;

        public BarcodeHelper BarcodeController;
        public string BarcodeCOM;



        private MotionRecorderHelper mr = new MotionRecorderHelper();

        public newMeasureDownTask(int id, string name, Station station) : base(id, name, station)
        {
            BarcodeController = new BarcodeHelper();
        }


        protected override int ResetLoop()
        {
            //load axis pos
            ResetLoop_LoadAxisPos();

            CfgCalib = Machine.Ins.Settings.Calibration;

            //clear vio
            VioTransInp.SetVio(this, false);
            VioBarcodeFinish.SetVio(this, false);
            VioTransFinish.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            //
            if (!DoGTCylinder.SetDo(this, false))
            {
                ThrowException($"GT Cylinder Reset Fail!");
            }

            //connect gt controller and barcode controller
            try
            {
                if (BarcodeController.IsOpen)
                {
                    BarcodeController.Close();
                }
                BarcodeController.Open(BarcodeCOM);
            }
            catch (Exception e)
            {
                ThrowException($"Barcode Open Fail:{e.Message}");
            }

            //home platform
            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home(new[] { 1, 1, 0 }, -1);
            Platform.EnterAuto(this).MoveAbs(PosWait, checkLimit: false);

            //move gt cylinder push pos
            if (!DoGTCylinder.SetDo(this, true))
            {
                //show alarm
                ThrowException($"GT Cylinder Set Fail!");
            }

            return 0;
        }

        private bool ResetLoop_LoadAxisPos()
        {
            PosWait = Platform["Wait"] as PosXYZ;
            if (PosWait == null)
            {
                ThrowException($"{Name} not teach Wait");
            }
            PosBarcode = Platform["Barcode"] as PosXYZ;
            if (PosBarcode == null)
            {
                ThrowException($"{Name} not teach Barcode");
            }
            PosGtWork1 = Platform["GtWork1"] as PosXYZ;
            if (PosGtWork1 == null)
            {
                ThrowException($"{Name} not teach GtWork1");
            }
            PosGtWork2 = Platform["GtWork2"] as PosXYZ;
            if (PosGtWork2 == null)
            {
                ThrowException($"{Name} not teach GtWork2");
            }
            return true;
        }

        protected override int RunLoop()
        {
            //in case of manual operations
            Platform.EnterAuto(this);
            //if (!DoGTCylinder.SetDo(this, false))
            //{
            //    ThrowException($"GT Cylinder SET Fail!");
            //}
            //System.Threading.Thread.Sleep(500);
            if (!DoGTCylinder.SetDo(this, true))
            {
                ThrowException($"GT Cylinder SET Fail!");
            }
            System.Threading.Thread.Sleep(500);

            mr.InitRecord();

            //move to wait
            mr.RecordMoveStart(new PosXYZ(Platform.CurPos));
            Platform.MoveAbs(PosWait);
            mr.RecordMoveFinish(PosWait as PosXYZ);

            //read barcode
            VioTransInp.WaitVioAndClear(this);
            {
                mr.RecordMoveStart(new PosXYZ(Platform.CurPos));
                Platform.Jump(PosBarcode, 0);
                mr.RecordMoveFinish(PosBarcode as PosXYZ);
                ReadBarcode();
            }
            VioBarcodeFinish.SetVio(this);

            //measure down gt
            VioTransFinish.WaitVioAndClear(this);
            {
                //measure down pos
                MeasureDownGt();


                mr.RecordMoveStart(new PosXYZ(Platform.CurPos));
                //move z safe pos
                Platform.Home(2);
                Platform.MoveAbs(2, PosWait, checkLimit: false);
                //move to wait pos
                Platform.MoveAbs(PosWait);
                mr.RecordMoveFinish(PosWait as PosXYZ);
            }
            //set vio finish
            VioMeasureFinish.SetVio(this);

            Log(mr.DisplatMoveDetails());

            if (Machine.Ins.Settings.Common.IsRepeatTest)
            {
                ProductData.Save("RepeatDown");
                ProductData.RawDataDown.Clear();
            }
            return 0;
        }

        private void ReadBarcode()
        {
            if (ProductData != null)
            {
                ProductData.Barcode = BarcodeController.Trigger();
                Log($"Read Barcode: {ProductData.Barcode}");
            }
        }
        private void MeasureDownGt()
        {
            //start measure
            var testPos = CfgProductSettings.DownTestPositions;
            if (testPos.Count == 0)
            {
                Log($"MeasureGt No Test Points");
                return;
            }

            //run measure loop
            bool isFirst = true;
            PosXYZ lastPos = PosXYZ.Zero;
            foreach (var pos in testPos)
            {
                PosXYZ posGtWork = null;
                var gtRawIndex = 1;
                var newpos = PosXYZ.Zero;
                //calc gt work pos
                if (pos.Description == "GT1")
                {
                    gtRawIndex = 1;
                    var leftGtSysOffset = Machine.Ins.Settings.Common.LeftGT1SYSOffset;
                    var rightGtSysOffset = Machine.Ins.Settings.Common.RightGT1SYSOffset;
                    posGtWork = PosGtWork1;
                    if (Station.Id == 1)
                    {
                        newpos = pos + leftGtSysOffset;
                    }
                    else if (Station.Id == 2)
                    {
                        newpos = pos + rightGtSysOffset;
                    }
                }
                else if (pos.Description == "GT2")
                {
                    gtRawIndex = 2;
                    var leftGtSysOffset = Machine.Ins.Settings.Common.LeftGT2SYSOffset;
                    var rightGtSysOffset = Machine.Ins.Settings.Common.RightGT2SYSOffset;
                    posGtWork = PosGtWork2;
                    if (Station.Id == 1)
                    {
                        newpos = pos + leftGtSysOffset;
                    }
                    else if (Station.Id == 2)
                    {
                        newpos = pos + rightGtSysOffset;
                    }
                }
                else
                {
                    continue;
                }

                var gtWorkZ = posGtWork.Z - pos.Z;
                var gtWork = CalibrationConfig.TransformToPlatformPos(CfgCalib, PlatformType, newpos, gtWorkZ, gtRawIndex);
                Log($"Transform {pos.Name} {pos.Description} {newpos} To {gtWork}");


                mr.RecordMoveStart(new PosXYZ(Platform.CurPos));
                //move to test pos
                if (isFirst)
                {
                    isFirst = false;

                    Platform.Jump(gtWork, 0);
                }
                else
                {
                    if (pos.Name == "outer" || pos.Name == "down")
                    {
                        if (pos.Name == "outer")
                        {
                            if (!DoGTCylinder.SetDo(this, false))
                            {
                                ThrowException($"GT Cylinder SET Fail!");
                            }
                        }

                        var outerJumpHeight = 1 - Platform.CurPos[2];
                        Platform.Jump(gtWork, outerJumpHeight);
                    }
                    else
                    {
                        //calc jump height
                        var jumpHeight = 0d;
                        if (pos.Z > lastPos.Z)
                        {
                            jumpHeight = lastPos.Z - pos.Z + PlatformJumpHeight;
                        }
                        else
                        {
                            jumpHeight = PlatformJumpHeight;
                        }

                        Platform.Jump(gtWork, jumpHeight);
                    }
                }
                mr.RecordMoveFinish(gtWork as PosXYZ);

                lastPos = pos;

                //read gt raw
                System.Threading.Thread.Sleep(500);
                var gtRaw = GtController?.ReadData();
                if (gtRaw != null)
                {
                    ProductData.RawDataDown.Add(new PosXYZ()
                    {
                        Name = pos.Name,
                        Description = pos.Description,
                        X = pos.X,
                        Y = pos.Y,
                        Z = gtRaw[gtRawIndex],
                        OffsetX = gtRaw[gtRawIndex],
                        OffsetZ = gtWorkZ,
                    });
                }

            }//end measure loop

        }
    }


    public class newLeftMeasureDown : newMeasureDownTask
    {
        public newLeftMeasureDown(int id, string name, Station station) : base(id, name, station)
        {
            PlatformType = PlatformType.LDown;

            DoGTCylinder = Machine.Ins.Find<ICylinderEx>("LGTCylinder");

            VioTransInp = Machine.Ins.Find<IVioEx>("LTransInp");
            VioBarcodeFinish = Machine.Ins.Find<IVioEx>("LBarcodeFinish");

            VioTransFinish = Machine.Ins.Find<IVioEx>("LTransFinishDown");
            VioMeasureFinish = Machine.Ins.Find<IVioEx>("LMeasureFinishDown");


            Platform = Machine.Ins.Find<PlatformEx>("LeftDown");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), DoGTCylinder, SafeCheckType.AutoHome));
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), DoGTCylinder, SafeCheckType.Auto));
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), DoGTCylinder, SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), DoGTCylinder, SafeCheckType.Manual));

        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.LJumpHeightDown;

            BarcodeCOM = Machine.Ins.Settings.Common.LeftBarcodeCOM;

            try
            {
                //load product settings
                var fprj = FlatnessProject.Load(Machine.Ins.Settings.LeftProjectFilePath);
                if (fprj == null)
                {
                    ThrowException(Machine.Ins.Settings.LeftProjectFilePath);
                }
                else
                {
                    CfgProductSettings = fprj.ProductSettings;
                }
            }
            catch (Exception ex)
            {
                ThrowException($"{Name} 加载工程文件异常: {ex.Message}");
            }

            return base.ResetLoop();
        }
    }


    public class newRightMeasureDown : newMeasureDownTask
    {
        public newRightMeasureDown(int id, string name, Station station) : base(id, name, station)
        {
            PlatformType = PlatformType.RDown;

            DoGTCylinder = Machine.Ins.Find<ICylinderEx>("RGTCylinder");

            VioTransInp = Machine.Ins.Find<IVioEx>("RTransInp");
            VioBarcodeFinish = Machine.Ins.Find<IVioEx>("RBarcodeFinish");

            VioTransFinish = Machine.Ins.Find<IVioEx>("RTransFinishDown");
            VioMeasureFinish = Machine.Ins.Find<IVioEx>("RMeasureFinishDown");


            Platform = Machine.Ins.Find<PlatformEx>("RightDown");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), DoGTCylinder, SafeCheckType.AutoHome));
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), DoGTCylinder, SafeCheckType.Auto));
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), DoGTCylinder, SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new DownSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), DoGTCylinder, SafeCheckType.Manual));

        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.RJumpHeightDown;

            BarcodeCOM = Machine.Ins.Settings.Common.RightBarcodeCOM;

            try
            {
                //load product settings
                var fprj = FlatnessProject.Load(Machine.Ins.Settings.RightProjectFilePath);
                if (fprj == null)
                {
                    ThrowException(Machine.Ins.Settings.RightProjectFilePath);
                }
                else
                {
                    CfgProductSettings = fprj.ProductSettings;
                }
            }
            catch (Exception ex)
            {
                ThrowException($"{Name} 加载工程文件异常: {ex.Message}");
            }


            return base.ResetLoop();
        }
    }

}