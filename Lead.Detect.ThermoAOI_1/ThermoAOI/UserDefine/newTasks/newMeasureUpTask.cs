using System;
using System.ComponentModel;
using System.Threading;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.UtilsFramework;
using Lead.Detect.ThermoAOI.Machine1.Common;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using Lead.Detect.Utility.Transformation;

namespace Lead.Detect.ThermoAOI.Machine1.UserDefine.newTasks
{
    public class newMeasureUpTask : StationTask
    {
        [Description("INPUT")]
        public IVioEx VioTransFinish;

        [Description("OUTPUT")]
        public IVioEx VioMeasureFinish;


        public PlatformEx Platform;
        public double PlatformJumpHeight;

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


        public newMeasureUpTask(int id, string name, Station station) : base(id, name, station)
        {
        }

        protected override int ResetLoop()
        {
            CfgSettings = Machine.Ins.Settings;

            //clear vio
            VioTransFinish.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            Platform.AssertPosTeached("Wait", this);
            Platform.AssertPosTeached("GtWork", this);

            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home(new[] { 1, 1, 0 }, -1);
            Platform.EnterAuto(this).MoveAbs("Wait", checkLimit: false);

            return 0;
        }

        protected override int RunLoop()
        {
            //in case of manual operations
            Platform.AssertAutoMode(this);
            Platform.LocateInPos("Wait");


            //check safe height
            var safeHeight = Platform["GtWork"].Data()[2] - Project.Height - 15;
            if (Platform.CurPos[2] > safeHeight)
            {
                Log($"{Name} {Platform.Name} SafeHeightError:{Platform.CurPos[2]:F2}>{safeHeight:F2}", LogLevel.Error);
            }

            //wait vio start
            VioTransFinish.WaitVioAndClear(this);
            {
                //move wait
                Platform.MoveAbs("Wait");

                //measure up test positions
                bool isFirst = true;
                foreach (var pos in Project.UpTestPositions)
                {
                    //transform gt work
                    var newpos = AddPosOffset(pos);

                    var gtWorkZ = Platform["GtWork"].Data()[2] - Project.Height;
                    var gtWork = new PosXYZ(Platform.GetPos("P->UP", newpos.Data())) { Z = gtWorkZ };
                    Log($"Transform {pos.Name} {pos.Description} {newpos} To {gtWork}");

                    //move gt work
                    if (isFirst)
                    {
                        isFirst = false;
                        Platform.Jump(gtWork, 0);
                    }
                    else
                    {
                        Platform.Jump(gtWork, PlatformJumpHeight);
                    }

                    //read gt raw
                    Thread.Sleep(CfgSettings.Common.GtReadDelay);
                    var gtRaw = GtController?.ReadData();
                    if (gtRaw != null)
                    {
                        Product.RawDataUp.Add(new PosXYZ()
                        {
                            Name = pos.Name,
                            Description = pos.Description,
                            X = pos.X,
                            Y = pos.Y,
                            Z = gtRaw[0],
                            OffsetX = gtRaw[0],
                            OffsetZ = gtWorkZ,

                        });
                    }
                }

                //move wait
                if (Project.UpTestPositions.Count > 0)
                {
                    Platform.Jump("Wait", PlatformJumpHeight);
                }

            }
            //set vio finish
            VioMeasureFinish.SetVio(this);

            return 0;
        }

        private PosXYZ AddPosOffset(PosXYZ pos)
        {
            var newpos = PosXYZ.Zero;
            if (Station.Id == 1)
            {
                newpos = pos + Machine.Ins.Settings.Common.LeftGTSYSOffset;
            }
            else if (Station.Id == 2)
            {
                newpos = pos + Machine.Ins.Settings.Common.RightGTSYSOffset;
            }
            return newpos;
        }
    }


    public class newLeftMeasureUp : newMeasureUpTask
    {
        public newLeftMeasureUp(int id, string name, Station station) : base(id, name, station)
        {
            VioTransFinish = Machine.Ins.Find<IVioEx>("LTransFinishUp");
            VioMeasureFinish = Machine.Ins.Find<IVioEx>("LMeasureFinishUp");

            Platform = Machine.Ins.Find<PlatformEx>("LeftUp");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.AutoHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.Auto));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.Manual));


            var pToUp = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.LeftUpTransform);
                return pnew.Data();
            });
            var upToP = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftUpTransform, new PosXYZ(d));
                return pnew.Data();
            });


            var upToDown = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.LeftTransform);
                return pnew.Data();
            });
            var downToUp = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.LeftTransform, new PosXYZ(d));
                return pnew.Data();
            });


            Platform.PosConvertFuncs.Add("P->UP", pToUp);
            Platform.PosConvertFuncs.Add("UP->P", upToP);
            Platform.PosConvertFuncs.Add("UP->DOWN", upToDown);
            Platform.PosConvertFuncs.Add("DOWN->UP", downToUp);

        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.LJumpHeightUp;

            //load project
            try
            {
                //load product settings
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


    public class newRightMeasureUp : newMeasureUpTask
    {
        public newRightMeasureUp(int id, string name, Station station) : base(id, name, station)
        {
            VioTransFinish = Machine.Ins.Find<IVioEx>("RTransFinishUp");
            VioMeasureFinish = Machine.Ins.Find<IVioEx>("RMeasureFinishUp");

            Platform = Machine.Ins.Find<PlatformEx>("RightUp");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.AutoHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.Auto));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.Manual));


            var pToUp = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.RightUpTransform);
                return pnew.Data();
            });
            var upToP = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightUpTransform, new PosXYZ(d));
                return pnew.Data();
            });


            var upToDown = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineTransform(new PosXYZ(d), Machine.Ins.Settings.Calibration.RightTransform);
                return pnew.Data();
            });
            var downToUp = new Func<double[], double[]>(d =>
            {
                var pnew = XyzPlarformCalibration.AffineInverseTransform(Machine.Ins.Settings.Calibration.RightTransform, new PosXYZ(d));
                return pnew.Data();
            });


            Platform.PosConvertFuncs.Add("P->UP", pToUp);
            Platform.PosConvertFuncs.Add("UP->P", upToP);
            Platform.PosConvertFuncs.Add("UP->DOWN", upToDown);
            Platform.PosConvertFuncs.Add("DOWN->UP", downToUp);
        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.RJumpHeightUp;

            //load project
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
