using System;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.UtilsFramework;
using Lead.Detect.ThermoAOI.Machine1.Common;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using Lead.Detect.Utility.Transformation;

namespace Lead.Detect.ThermoAOI.Machine1.Machine.newTasks
{
    public class newMeasureDownTask : StationTask
    {
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
        public Thermo1Product Product;


        public MeasureProject1 Project;
        public MachineSettings CfgSettings;

        public BarcodeHelper BarcodeController;
        public string BarcodeCOM;

        public newMeasureDownTask(int id, string name, Station station) : base(id, name, station)
        {
            BarcodeController = new BarcodeHelper();
        }


        protected override int ResetLoop()
        {
            //load cfg
            CfgSettings = Machine.Ins.Settings;

            //clear vio
            VioTransInp.SetVio(this, false);
            VioBarcodeFinish.SetVio(this, false);
            VioTransFinish.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            //load axis pos
            Platform.AssertPosTeached("Wait", this);
            Platform.AssertPosTeached("GtWork1", this);
            Platform.AssertPosTeached("GtWork2", this);
            Platform.AssertPosTeached($"Barcode{Project.TypeId}", this);

            PosGtWork1 = Platform["GtWork1"] as PosXYZ;
            PosGtWork2 = Platform["GtWork2"] as PosXYZ;


            if (!DoGTCylinder.SetDo(this, false, ignoreOrWaringOrError: true))
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
            Platform.EnterAuto(this).MoveAbs("Wait", checkLimit: false);

            //move gt cylinder push pos
            if (!DoGTCylinder.SetDo(this, true, ignoreOrWaringOrError: true))
            {
                //show alarm
                ThrowException($"GT Cylinder Set Fail!");
            }

            return 0;
        }

        protected override int RunLoop()
        {
            //in case of manual operations
            Platform.AssertAutoMode(this);

            if (!DoGTCylinder.SetDo(this, true, ignoreOrWaringOrError: true))
            {
                ThrowException($"GT Cylinder SET Fail!");
            }


            //optimize wait position
            if (!CfgSettings.Common.OptimizeDownWait)
            {
                //move to wait
                Platform.MoveAbs("Wait");
            }
            else
            {
                var pos = $"Barcode{Project.TypeId}";
                if (Platform.ExistsPos(pos))
                {
                    Platform.Jump(pos, 0);
                }
                Log($"{Platform.Name} Wait At {pos}");
            }

            //safe height check
            if (Platform.CurPos[2] > 1)
            {
                Log($"{Name} {Platform.Name} Z Height Error: {Platform.CurPos[2]:F2} > 1", LogLevel.Error);
            }

            //read barcode
            VioTransInp.WaitVioAndClear(this);
            {
                var pos = $"Barcode{Project.TypeId}";
                if (Platform.ExistsPos(pos))
                {
                    Platform.Jump(pos, 0);
                    ReadBarcode();
                }
            }
            VioBarcodeFinish.SetVio(this);

            //measure down gt
            VioTransFinish.WaitVioAndClear(this);
            {
                //measure down pos
                MeasureDownGt();

                //reset z axis
                Platform.MoveAbs(2, "Wait");
                Platform.Home(2);

                if (!CfgSettings.Common.OptimizeDownWait)
                {
                    Platform.MoveAbs("Wait", checkLimit: false);
                }
                else
                {
                    Platform.MoveAbs(2, "Wait", checkLimit: false);
                }
            }
            //set vio finish
            VioMeasureFinish.SetVio(this);

            return 0;
        }

        private void ReadBarcode()
        {
            if (Product != null)
            {
                Product.Barcode = BarcodeController.Trigger();
                Log($"Read Barcode: {Product.Barcode}");
            }
        }

        private void MeasureDownGt()
        {
            //start measure
            if (Project.DownTestPositions.Count == 0)
            {
                Log($"MeasureGt No Test Points");
                return;
            }

            //run measure loop
            bool isFirst = true;
            PosXYZ lastPos = PosXYZ.Zero;
            foreach (var pos in Project.DownTestPositions)
            {
                int gt;
                PosXYZ gtWorkZ;
                PosXYZ newpos;

                //add pos offset
                if (!AddPosOffset(pos, out gt, out gtWorkZ, out newpos))
                {
                    Log($"AddPosOffset GT Raw Index {gt} Error");
                    continue;
                }


                //convert pos to gt pos
                var gtPosZ = gtWorkZ.Z - pos.Z;
                var gtPos = PosXYZ.Zero;
                if (gt == 1)
                {
                    gtPos = new PosXYZ(Platform.GetPos("P->DOWN1", newpos.Data())) { Z = gtPosZ };
                }
                else if (gt == 2)
                {
                    gtPos = new PosXYZ(Platform.GetPos("P->DOWN2", newpos.Data())) { Z = gtPosZ };
                }
                else
                {
                    Log($"GT Raw Index {gt} Error", LogLevel.Error);
                }

                Log($"Transform {pos.Name} {pos.Description} {newpos} To {gtPos}");


                //jump gt pos
                PlatformJumpPos(isFirst, pos, lastPos, gtPos);
                lastPos = pos;
                isFirst = false;

                //read gt
                System.Threading.Thread.Sleep(CfgSettings.Common.GtReadDelay);
                var gtRaw = GtController?.ReadData();
                if (gtRaw != null)
                {
                    Product.RawDataDown.Add(new PosXYZ()
                    {
                        Name = pos.Name,
                        Description = pos.Description,
                        X = pos.X,
                        Y = pos.Y,
                        Z = gtRaw[gt],
                        OffsetX = gtRaw[gt],
                        OffsetZ = gtPosZ,
                    });
                }
                else
                {
                    Log($"GtController ReadData Error", LogLevel.Error);
                }
            }

        }

        private bool AddPosOffset(PosXYZ pos, out int gt, out PosXYZ gtWork, out PosXYZ newpos)
        {
            //add sys offset
            if (pos.Description == "GT1")
            {
                gt = 1;
                var leftGtSysOffset = CfgSettings.Common.LeftGT1SYSOffset;
                var rightGtSysOffset = CfgSettings.Common.RightGT1SYSOffset;
                gtWork = PosGtWork1;
                if (Station.Id == 1)
                {
                    newpos = pos + leftGtSysOffset;
                }
                else if (Station.Id == 2)
                {
                    newpos = pos + rightGtSysOffset;
                }
                else
                {
                    newpos = PosXYZ.Zero;
                    return false;
                }
            }
            else if (pos.Description == "GT2")
            {
                gt = 2;
                var leftGtSysOffset = CfgSettings.Common.LeftGT2SYSOffset;
                var rightGtSysOffset = CfgSettings.Common.RightGT2SYSOffset;
                gtWork = PosGtWork2;
                if (Station.Id == 1)
                {
                    newpos = pos + leftGtSysOffset;
                }
                else if (Station.Id == 2)
                {
                    newpos = pos + rightGtSysOffset;
                }
                else
                {
                    newpos = PosXYZ.Zero;
                    return false;
                }
            }
            else
            {
                gt = 0;
                newpos = PosXYZ.Zero;
                gtWork = PosXYZ.Zero;
                return false;
            }

            return true;
        }

        private void PlatformJumpPos(bool isFirst, PosXYZ pos, PosXYZ lastPos, PosXYZ gtPos)
        {
            if (isFirst)
            {
                if (pos.Name == "outer")
                {
                    if (!DoGTCylinder.SetDo(this, false, ignoreOrWaringOrError: true))
                    {
                        ThrowException($"GT Cylinder RESET Fail!");
                    }
                }
                Log($"{Platform.Name} {Platform.Description} Jump {pos}");
                Platform.Jump(gtPos, 0);
            }
            else
            {
                if (pos.Name == "outer" || pos.Name == "down")
                {
                    if (pos.Name == "outer")
                    {
                        if (!DoGTCylinder.SetDo(this, false, ignoreOrWaringOrError: true))
                        {
                            ThrowException($"GT Cylinder RESET Fail!");
                        }
                    }
                    else
                    {
                        if (lastPos.Name == "outer")
                        {
                            if (!DoGTCylinder.SetDo(this, true, ignoreOrWaringOrError: true))
                            {
                                ThrowException($"GT Cylinder SET Fail!");
                            }
                        }
                    }

                    var outerJumpHeight = 0.5 - Platform.CurPos[2];

                    Log($"{Platform.Name} {Platform.Description} Jump {pos}");
                    Platform.Jump(gtPos, outerJumpHeight);
                }
                else
                {
                    if (lastPos.Name == "outer")
                    {
                        if (!DoGTCylinder.SetDo(this, true, ignoreOrWaringOrError: true))
                        {
                            ThrowException($"GT Cylinder SET Fail!");
                        }
                    }

                    //calc jump height
                    var jumpHeight = 0d;
                    if (pos.Z > lastPos.Z)
                    {
                        //need long dist jump
                        jumpHeight = lastPos.Z - pos.Z + PlatformJumpHeight;

                        //check jump height
                        var lastGtValue = Product.RawDataDown.Last().OffsetX + 0.2;
                        var a147offset = 2d;
                        var gtOffset = pos.Description == "GT1" ? 0 : 3;
                        var minJumpHeight = lastPos.Z - pos.Z - (lastGtValue + a147offset + gtOffset);
                        if (jumpHeight > minJumpHeight)
                        {
                            //not enough jump height to avoid gt1 collision
                            Log($"not enough jump height {jumpHeight} to avoid gt1 collision {minJumpHeight:F2}", LogLevel.Warning);
                            jumpHeight = minJumpHeight;
                        }
                    }
                    else
                    {
                        jumpHeight = PlatformJumpHeight;

                        //check jump height
                        var lastGtValue = Product.RawDataDown.Last().OffsetX + 0.2;
                        var a147offset = 2d;
                        var gtOffset = pos.Description == "GT1" ? 0 : 3;
                        var minJumpHeight = -(lastGtValue + a147offset + gtOffset);
                        if (jumpHeight > minJumpHeight)
                        {
                            //not enough jump height to avoid gt1 collision
                            Log($"not enough jump height {jumpHeight} to avoid gt1 collision {minJumpHeight:F2}", LogLevel.Warning);
                            jumpHeight = minJumpHeight;
                        }
                    }

                    Log($"{Platform.Name} {Platform.Description} Jump {pos}");
                    Platform.Jump(gtPos, jumpHeight);
                }
            }
        }
    }


    public class newLeftMeasureDown : newMeasureDownTask
    {
        public newLeftMeasureDown(int id, string name, Station station) : base(id, name, station)
        {
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


            var pToDown1 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.LeftUpTransform);
                pnew = XyzPlarformCalibration.AffineTransform(pnew, Machine.Ins.Settings.Calibration.LeftTransform);
                return pnew.Data();
            });

            var pToDown2 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.LeftUpTransform);
                pnew = XyzPlarformCalibration.AffineTransform(pnew, Machine.Ins.Settings.Calibration.LeftTransform);
                pnew = pnew + Machine.Ins.Settings.Calibration.LeftGtOffset;
                return pnew.Data();
            });


            var down1ToP = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftTransform, new PosXYZ(d));
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftUpTransform, pnew);
                return pnew.Data();
            });

            var down2ToP = new Func<double[], double[]>(d =>
            {
                var pnew = new PosXYZ(d) - Machine.Ins.Settings.Calibration.LeftGtOffset;
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftTransform, pnew);
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftUpTransform, pnew);
                return pnew.Data();
            });


            var upToDown1 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.LeftTransform);
                return pnew.Data();
            });
            var down1ToUp = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftTransform, new PosXYZ(d));
                return pnew.Data();
            });

            var upToDown2 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.LeftTransform);
                pnew = pnew + Machine.Ins.Settings.Calibration.LeftGtOffset;
                return pnew.Data();
            });
            var down2ToUp = new Func<double[], double[]>(d =>
            {
                var pnew = new PosXYZ(d) - Machine.Ins.Settings.Calibration.LeftGtOffset;
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftTransform, pnew);
                return pnew.Data();
            });


            Platform.PosConvertFuncs.Add("P->DOWN1", pToDown1);
            Platform.PosConvertFuncs.Add("P->DOWN2", pToDown2);
            Platform.PosConvertFuncs.Add("DOWN1->P", down1ToP);
            Platform.PosConvertFuncs.Add("DOWN2->P", down2ToP);

            Platform.PosConvertFuncs.Add("UP->DOWN1", upToDown1);
            Platform.PosConvertFuncs.Add("DOWN1->UP", down1ToUp);
            Platform.PosConvertFuncs.Add("UP->DOWN2", upToDown2);
            Platform.PosConvertFuncs.Add("DOWN2->UP", down2ToUp);
        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.LJumpHeightDown;

            BarcodeCOM = Machine.Ins.Settings.Common.LeftBarcodeCOM;

            try
            {
                //load product settings
                Project = MeasureProject1.Load(Machine.Ins.Settings.LeftProjectFilePath, typeof(MeasureProject1)) as MeasureProject1;
                Project.AssertNoNull(this);
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


            var pToDown1 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.RightUpTransform);
                pnew = XyzPlarformCalibration.AffineTransform(pnew, Machine.Ins.Settings.Calibration.RightTransform);

                return pnew.Data();
            });

            var pToDown2 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.RightUpTransform);
                pnew = XyzPlarformCalibration.AffineTransform(pnew, Machine.Ins.Settings.Calibration.RightTransform);
                pnew = pnew + Machine.Ins.Settings.Calibration.RightGtOffset;
                return pnew.Data();
            });


            var down1ToP = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightTransform, new PosXYZ(d));
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightUpTransform, pnew);

                return pnew.Data();
            });

            var down2ToP = new Func<double[], double[]>(d =>
            {
                var pnew = new PosXYZ(d) - Machine.Ins.Settings.Calibration.RightGtOffset;
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightTransform, pnew);
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightUpTransform, pnew);
                return pnew.Data();
            });


            var upToDown1 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.RightTransform);
                return pnew.Data();
            });
            var down1ToUp = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightTransform, new PosXYZ(d));
                return pnew.Data();
            });

            var upToDown2 = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.RightTransform);
                pnew = pnew + Machine.Ins.Settings.Calibration.RightGtOffset;
                return pnew.Data();
            });
            var down2ToUp = new Func<double[], double[]>(d =>
            {
                var pnew = new PosXYZ(d) - Machine.Ins.Settings.Calibration.RightGtOffset;
                pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightTransform, pnew);
                return pnew.Data();
            });


            Platform.PosConvertFuncs.Add("P->DOWN1", pToDown1);
            Platform.PosConvertFuncs.Add("P->DOWN2", pToDown2);
            Platform.PosConvertFuncs.Add("DOWN1->P", down1ToP);
            Platform.PosConvertFuncs.Add("DOWN2->P", down2ToP);

            Platform.PosConvertFuncs.Add("UP->DOWN1", upToDown1);
            Platform.PosConvertFuncs.Add("DOWN1->UP", down1ToUp);
            Platform.PosConvertFuncs.Add("UP->DOWN2", upToDown2);
            Platform.PosConvertFuncs.Add("DOWN2->UP", down2ToUp);
        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.RJumpHeightDown;

            BarcodeCOM = Machine.Ins.Settings.Common.RightBarcodeCOM;

            try
            {
                //load product settings
                Project = MeasureProject1.Load(Machine.Ins.Settings.RightProjectFilePath, typeof(MeasureProject1)) as MeasureProject1;
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