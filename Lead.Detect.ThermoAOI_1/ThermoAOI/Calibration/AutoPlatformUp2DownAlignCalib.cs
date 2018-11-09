using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension; 
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI.Machine1.UserDefine;
using Lead.Detect.Utility.Transformation;

namespace Lead.Detect.ThermoAOI.Machine1.Calibration
{
    /// <summary>
    /// 上下坐标系标定
    /// gen1:
    ///     calibrate by manual methods,
    ///     save aligned points to AlignPosUp && AlignPosDown
    ///
    /// gen2:
    /// todo: to utilize calibration with new calibration method
    /// </summary>
    public class AutoPlatformUp2DownAlignCalib : AutoCalib
    {
        public AutoPlatformUp2DownAlignCalib()
        {
        }


        [Category("传送上料配置")]
        public ICylinderEx do_clampy_cy { get; set; }
        [Category("传送上料配置")]
        public PlatformEx PlatformCarrier { get; set; }



        [Category("上工站配置")]
        public PlatformEx Platform1 { get; set; }
        [Category("上工站配置")]
        public double JumpHeight1 { get; set; } = -50;


        [Category("下工站配置")]
        public PlatformEx Platform2 { get; set; }
        [Category("下工站配置")]
        public PosXYZ Platform2GtOffsetCalibGT1 { get; set; }
        [Category("下工站配置")]
        public PosXYZ Platform2GtOffsetCalibGT2 { get; set; }
        [Category("下工站配置")]
        public double JumpHeight2 { get; set; } = -15;
        [Category("下工站配置")]
        public ICylinderEx do_gt2_cy { get; set; }


        [Category("标定")]
        public List<PosXYZ> AlignPosUp { get; set; }
        [Category("标定")]
        public List<PosXYZ> AlignPosDown { get; set; }


        [Category("OUTPUT")]
        public TransformParams OutputTransForm { get; set; }
        [Category("OUTPUT")]
        public PosXYZ OutputPlatform2GtOffset { get; set; }


        public override void InitCalib()
        {
            do_clampy_cy?.SetDo(this, true);
            do_gt2_cy?.SetDo(this, false);

            PlatformCarrier?.EnterAuto(this).MoveAbs("Work");
        }

        public override void UninitCalib()
        {
            do_clampy_cy?.SetDo(this, false);
            do_gt2_cy?.SetDo(this, false);

            Platform1?.EnterAuto(this).Jump("Wait", JumpHeight1);
            Platform2?.EnterAuto(this).Jump("Wait", JumpHeight2);

            PlatformCarrier?.EnterAuto(this).MoveAbs("Wait");
        }


        public override void DoCalib()
        {
            Platform1?.EnterAuto(this).MoveAbs("Wait");


            //上工站点位数据采集
            Log($"上平台Align点位标定\n------------------------------------------------------");
            List<PosXYZ> upAlignPos = new List<PosXYZ>();
            bool isFirst = true;
            foreach (var pos in AlignPosUp)
            {
                if (isFirst)
                {
                    isFirst = false;
                    Platform1?.EnterAuto(this).Jump(pos, 0);
                }
                else
                {
                    Platform1?.EnterAuto(this).Jump(pos, JumpHeight1);
                }

                upAlignPos.Add(pos);
                DataList.Add(pos.ToString());
                Log($"CurPos {pos.ToString()}", LogLevel.Info);
            }
            //复位平台1
            Platform1?.EnterAuto(this).Jump("Wait", JumpHeight1);
            Log($"上平台Align点位标定 完成\n------------------------------------------------------");
            OnCalibProgress(50);



            Platform2?.EnterAuto(this).MoveAbs("Wait");
            //下工站点位 数据采集
            Log($"下平台Align点位标定\n------------------------------------------------------");
            List<PosXYZ> downAlingPos = new List<PosXYZ>();
            isFirst = true;
            foreach (var pos in AlignPosDown)
            {
                if (isFirst)
                {
                    isFirst = false;
                    Platform2?.EnterAuto(this).Jump(pos, 0);
                }
                else
                {
                    Platform2?.EnterAuto(this).Jump(pos, JumpHeight2);
                }

                downAlingPos.Add(pos);
                DataList.Add(pos.ToString());
                Log($"CurPos {pos.ToString()}", LogLevel.Info);
            }

            Log($"下平台Align点位标定 完成\n------------------------------------------------------");


            //计算下平台GT2偏移
            Log($"下平台GT1GT2偏移标定\n------------------------------------------------------");
            if (Platform2GtOffsetCalibGT1 != null && Platform2GtOffsetCalibGT2 != null)
            {
                Platform2?.EnterAuto(this).Jump(Platform2GtOffsetCalibGT1, JumpHeight1);
                Platform2?.EnterAuto(this).Jump(Platform2GtOffsetCalibGT2, JumpHeight1);

                var gt1 = Platform2GtOffsetCalibGT1 as PosXYZ;
                var gt2 = Platform2GtOffsetCalibGT2 as PosXYZ;
                OutputPlatform2GtOffset = gt2 - gt1;
                OutputPlatform2GtOffset.Z = 0;
            }


            //复位平台2
            Platform2?.EnterAuto(this).Jump("Wait", JumpHeight2);
            Log($"下平台GT1GT2偏移标定 完成\n------------------------------------------------------");


            //复位治具
            PlatformCarrier?.EnterAuto(this).MoveAbs("Wait");
            OnCalibProgress(100);


            //计算上下平台坐标转换
            {
                var ret = XyzPlarformCalibration.CalcAffineTransform(
                    upAlignPos.Select(p => new PosXYZ(p.X, p.Y, 0)).ToList(),
                    downAlingPos.Select(p => new PosXYZ(p.X, p.Y, 0)).ToList());

                Log($"计算上下平台转换矩阵:\r\n"
                    + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6},{ret.Item1[0, 3]:F6}\r\n"
                    + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6},{ret.Item1[1, 3]:F6}\r\n"
                    + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6},{ret.Item1[2, 3]:F6}\r\n"
                    + $"{ret.Item1[3, 0]:F6},{ret.Item1[3, 1]:F6},{ret.Item1[3, 2]:F6},{ret.Item1[3, 3]:F6}\r\n", LogLevel.Info);

                Log($"计算上下平台转换误差:\r\n{ret.Item2:F2}\r\n", LogLevel.Info);

                OutputTransForm = new TransformParams(ret.Item1);
            }

            Log($"GT1GT2偏移:\r\n{OutputPlatform2GtOffset}\r\n", LogLevel.Info);
        }


        public static AutoPlatformUp2DownAlignCalib CreateAlignCalib(string platform)
        {
            switch (platform)
            {
                case "Left":
                    return new AutoPlatformUp2DownAlignCalib
                    {
                        CalibInfo = $"LeftPlatformAlign",
                        Station = Machine.Ins.Find<Station>("LeftStation"),

                        //carrier
                        PlatformCarrier = Machine.Ins.Find<PlatformEx>("LeftCarrier"),

                        do_clampy_cy = Machine.Ins.Find<ICylinderEx>("LClampCylinderY"),

                        //up
                        Platform1 = Machine.Ins.Find<PlatformEx>("LeftUp"),

                        //down
                        Platform2 = Machine.Ins.Find<PlatformEx>("LeftDown"),
                        Platform2GtOffsetCalibGT1 = Machine.Ins.Find<PlatformEx>("LeftDown")["DownAlign1"] as PosXYZ,
                        Platform2GtOffsetCalibGT2 = Machine.Ins.Find<PlatformEx>("LeftDown")["GtOffsetCalib"] as PosXYZ,

                        do_gt2_cy = Machine.Ins.Find<ICylinderEx>("LGTCylinder"),

                        JumpHeight1 = -50,
                        JumpHeight2 = -15,


                        AlignPosUp = Machine.Ins.Find<PlatformEx>("LeftUp").Positions.FindAll(p => p.Name.StartsWith("UpAlign")).Cast<PosXYZ>().ToList(),
                        AlignPosDown = Machine.Ins.Find<PlatformEx>("LeftDown").Positions.FindAll(p => p.Name.StartsWith("DownAlign")).Cast<PosXYZ>().ToList(),
                    };

                case "Right":
                    return new AutoPlatformUp2DownAlignCalib
                    {
                        CalibInfo = $"RightPlatformAlign",
                        Station = Machine.Ins.Find<Station>("RightStation"),

                        //carrier
                        PlatformCarrier = Machine.Ins.Find<PlatformEx>("RightCarrier"),

                        do_clampy_cy = Machine.Ins.Find<ICylinderEx>("LClampCylinderY"),

                        //up
                        Platform1 = Machine.Ins.Find<PlatformEx>("RightUp"),

                        //down
                        Platform2 = Machine.Ins.Find<PlatformEx>("RightDown"),
                        Platform2GtOffsetCalibGT1 = Machine.Ins.Find<PlatformEx>("RightDown")["DownAlign1"] as PosXYZ,
                        Platform2GtOffsetCalibGT2 = Machine.Ins.Find<PlatformEx>("RightDown")["GtOffsetCalib"] as PosXYZ,

                        do_gt2_cy = Machine.Ins.Find<ICylinderEx>("RGTCylinder"),

                        JumpHeight1 = -50,
                        JumpHeight2 = -15,


                        AlignPosUp = Machine.Ins.Find<PlatformEx>("RightUp").Positions.FindAll(p => p.Name.StartsWith("UpAlign")).Cast<PosXYZ>().ToList(),
                        AlignPosDown = Machine.Ins.Find<PlatformEx>("RightDown").Positions.FindAll(p => p.Name.StartsWith("DownAlign")).Cast<PosXYZ>().ToList(),
                    };
            }

            throw new Exception("Create Up 2 Down XY Calib Fail");
        }

        public static void SaveAlignCalib(AutoPlatformUp2DownAlignCalib calib)
        {
            if (MessageBox.Show($"保存{calib.Station.Name}标定数据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (calib.Station.Id == 1)
                    {
                        Machine.Ins.Settings.Calibration.LeftTransform = calib.OutputTransForm;
                        Machine.Ins.Settings.Calibration.LeftGtOffset = calib.OutputPlatform2GtOffset;
                    }
                    else if (calib.Station.Id == 2)
                    {
                        Machine.Ins.Settings.Calibration.RightTransform = calib.OutputTransForm;
                        Machine.Ins.Settings.Calibration.RightGtOffset = calib.OutputPlatform2GtOffset;
                    }

                    Machine.Ins.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存标定数据异常:{ex.Message}");
                }
            }
        }
    }
}