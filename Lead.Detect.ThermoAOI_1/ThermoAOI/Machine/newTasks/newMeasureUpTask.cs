using System;
using System.ComponentModel;
using System.Threading;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI.Calibration;
using Lead.Detect.ThermoAOI.Common;
using Lead.Detect.ThermoAOI.Machine.Common;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;

namespace Lead.Detect.ThermoAOI.Machine.newTasks
{
    public class newMeasureUpTask : StationTask
    {
        public PlatformType PlatformType;

        [Description("INPUT")]
        public IVioEx VioTransFinish;

        [Description("OUTPUT")]
        public IVioEx VioMeasureFinish;


        public PlatformEx Platform;
        public double PlatformJumpHeight;
        public PosXYZ PosWait;
        public PosXYZ PosGtWork;


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


        public newMeasureUpTask(int id, string name, Station station) : base(id, name, station)
        {
        }

        protected override int ResetLoop()
        {
            CfgCalib = Machine.Ins.Settings.Calibration;

            //clear vio
            VioTransFinish.SetVio(this, false);
            VioMeasureFinish.SetVio(this, false);

            PosWait = Platform["Wait"] as PosXYZ;
            if (PosWait == null)
            {
                ThrowException($"{Name} not teach Wait");
                return -1;
            }
            PosGtWork = Platform["GtWork"] as PosXYZ;
            if (PosGtWork == null)
            {
                ThrowException($"{Name} not teach GtWork");
                return -1;
            }

            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home(new[] { 1, 1, 0 }, -1);
            Platform.EnterAuto(this).MoveAbs(PosWait, checkLimit: false);

            return 0;
        }

        protected override int RunLoop()
        {
            //in case of manual operations
            Platform.EnterAuto(this);



            //wait vio start
            VioTransFinish.WaitVioAndClear(this);
            {
                //move wait
                Platform.MoveAbs(PosWait);

                //measure up test positions
                bool isFirst = true;
                foreach (var testPos in CfgProductSettings.UpTestPositions)
                {
                    //transform gt work
                    var newpos = testPos;
                    if (Station.Id == 1)
                    {
                        newpos = testPos + Machine.Ins.Settings.Common.LeftGTSYSOffset;
                    }
                    else if (Station.Id == 2)
                    {
                        newpos = testPos + Machine.Ins.Settings.Common.RightGTSYSOffset;
                    }

                    var gtWorkZ = PosGtWork.Z - CfgProductSettings.Height;
                    var gtWork = CalibrationConfig.TransformToPlatformPos(CfgCalib, PlatformType, newpos, gtWorkZ);
                    Log($"Transform {testPos.Name} {testPos.Description} {newpos} To {gtWork}");

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
                    Thread.Sleep(500);
                    var gtRaw = GtController?.ReadData();
                    if (gtRaw != null)
                    {
                        ProductData.RawDataUp.Add(new PosXYZ()
                        {
                            Name = testPos.Name,
                            Description = testPos.Description,
                            X = testPos.X,
                            Y = testPos.Y,
                            Z = gtRaw[0],
                            OffsetX = gtRaw[0],
                            OffsetZ = gtWorkZ,

                        });
                    }
                }

                //move wait
                if (CfgProductSettings.UpTestPositions.Count > 0)
                {
                    Platform.Jump(PosWait, PlatformJumpHeight);
                }

            }
            //set vio finish
            VioMeasureFinish.SetVio(this);



            if (Machine.Ins.Settings.Common.IsRepeatTest)
            {
                ProductData.Save("RepeatUp");
                ProductData.RawDataUp.Clear();
            }
            return 0;
        }
    }


    public class newLeftMeasureUp : newMeasureUpTask
    {
        public newLeftMeasureUp(int id, string name, Station station) : base(id, name, station)
        {
            PlatformType = PlatformType.LUp;

            VioTransFinish = Machine.Ins.Find<IVioEx>("LTransFinishUp");
            VioMeasureFinish = Machine.Ins.Find<IVioEx>("LMeasureFinishUp");

            Platform = Machine.Ins.Find<PlatformEx>("LeftUp");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.AutoHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.Auto));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("LeftCarrier"), SafeCheckType.Manual));
        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.LJumpHeightUp;

            //load cfg
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


    public class newRightMeasureUp : newMeasureUpTask
    {
        public newRightMeasureUp(int id, string name, Station station) : base(id, name, station)
        {
            PlatformType = PlatformType.RUp;

            VioTransFinish = Machine.Ins.Find<IVioEx>("RTransFinishUp");
            VioMeasureFinish = Machine.Ins.Find<IVioEx>("RMeasureFinishUp");

            Platform = Machine.Ins.Find<PlatformEx>("RightUp");
            Platform.SafeChecks.Clear();
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.AutoHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.Auto));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.ManualHome));
            Platform.SafeChecks.Add(new UpSafeCheck(Machine.Ins.Find<PlatformEx>("RightCarrier"), SafeCheckType.Manual));
        }

        protected override int ResetLoop()
        {
            PlatformJumpHeight = Machine.Ins.Settings.Common.RJumpHeightUp;

            //load cfg
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
