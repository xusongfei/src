using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.Helper;
using Lead.Detect.FrameworkExtension.stateMachine;
using System.Windows.Forms;
using System;

namespace Lead.Detect.ThermoAOI.Calibration
{
    /// <summary>
    ///
    /// gem1:
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

        [Category("传送上料配置")]
        public IPlatformPos PlatformCarrierWait { get; set; }

        [Category("传送上料配置")]
        public IPlatformPos PlatformCarrierWork { get; set; }


        [Category("上工站配置")]
        public PlatformEx Platform1 { get; set; }

        [Category("上工站配置")]
        public IPlatformPos Platform1PosWait { get; set; }

        [Category("上工站配置")]
        public double JumpHeight1 { get; set; } = -50;


        [Category("下工站配置")]
        public PlatformEx Platform2 { get; set; }

        [Category("下工站配置")]
        public IPlatformPos Platform2PosWait { get; set; }

        [Category("下工站配置")]
        public PosXYZ Platform2PosGtCalib1 { get; set; }

        public PosXYZ Platform2PosGtCalib2 { get; set; }

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
        public PosXYZ OutputPlatform1Origin { get; set; }

        [Category("OUTPUT")]
        public PosXYZ OutputPlatform2GtOffset { get; set; }


        public override void InitCalib()
        {
            do_clampy_cy?.SetDo(this, true);
            do_gt2_cy?.SetDo(this, false);

            PlatformCarrier?.EnterAuto(this).MoveAbs(PlatformCarrierWork);
        }

        public override void UninitCalib()
        {
            do_clampy_cy?.SetDo(this, false);
            do_gt2_cy?.SetDo(this, false);

            Platform1?.EnterAuto(this).Jump(Platform1PosWait, JumpHeight1);
            Platform2?.EnterAuto(this).Jump(Platform2PosWait, JumpHeight2);

            PlatformCarrier?.EnterAuto(this).MoveAbs(PlatformCarrierWait);
        }


        public override void DoCalib()
        {
            Platform1?.EnterAuto(this).MoveAbs(Platform1PosWait);


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
            Platform1?.EnterAuto(this).Jump(Platform1PosWait, JumpHeight1);
            Log($"上平台Align点位标定 完成\n------------------------------------------------------");
            OnCalibProgress(50);


            Platform2?.EnterAuto(this).MoveAbs(Platform2PosWait);
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
            if (Platform2PosGtCalib1 != null && Platform2PosGtCalib2 != null)
            {
                Platform2?.EnterAuto(this).Jump(Platform2PosGtCalib1, JumpHeight1);
                Platform2?.EnterAuto(this).Jump(Platform2PosGtCalib2, JumpHeight1);

                var gt1 = Platform2PosGtCalib1 as PosXYZ;
                var gt2 = Platform2PosGtCalib2 as PosXYZ;
                OutputPlatform2GtOffset = gt2 - gt1;
            }


            //复位平台2
            Platform2?.EnterAuto(this).Jump(Platform2PosWait, JumpHeight2);
            Log($"下平台GT1GT2偏移标定 完成\n------------------------------------------------------");


            //复位治具
            PlatformCarrier?.EnterAuto(this).MoveAbs(PlatformCarrierWait);
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
                        Station = Machine.Machine.Ins.Find<Station>("LeftStation"),

                        //carrier
                        PlatformCarrier = Machine.Machine.Ins.Find<PlatformEx>("LeftCarrier"),
                        PlatformCarrierWait = Machine.Machine.Ins.Find<PlatformEx>("LeftCarrier")["Wait"],
                        PlatformCarrierWork = Machine.Machine.Ins.Find<PlatformEx>("LeftCarrier")["Work"],

                        do_clampy_cy = Machine.Machine.Ins.Find<ICylinderEx>("LClampCylinderY"),

                        //up
                        Platform1 = Machine.Machine.Ins.Find<PlatformEx>("LeftUp"),
                        Platform1PosWait = Machine.Machine.Ins.Find<PlatformEx>("LeftUp")["Wait"],

                        //down
                        Platform2 = Machine.Machine.Ins.Find<PlatformEx>("LeftDown"),
                        Platform2PosWait = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["Wait"],
                        Platform2PosGtCalib1 = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["DownAlign1"] as PosXYZ,
                        Platform2PosGtCalib2 = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["GtOffsetCalib"] as PosXYZ,

                        do_gt2_cy = Machine.Machine.Ins.Find<ICylinderEx>("LGTCylinder"),

                        JumpHeight1 = -50,
                        JumpHeight2 = -15,


                        AlignPosUp = Machine.Machine.Ins.Find<PlatformEx>("LeftUp").Positions.FindAll(p => p.Name.StartsWith("UpAlign")).Cast<PosXYZ>().ToList(),
                        AlignPosDown = Machine.Machine.Ins.Find<PlatformEx>("LeftDown").Positions.FindAll(p => p.Name.StartsWith("DownAlign")).Cast<PosXYZ>().ToList(),
                    };

                case "Right":
                    return  new AutoPlatformUp2DownAlignCalib
                    {
                        CalibInfo = $"RightPlatformAlign",
                        Station = Machine.Machine.Ins.Find<Station>("RightStation"),

                        //carrier
                        PlatformCarrier = Machine.Machine.Ins.Find<PlatformEx>("RightCarrier"),
                        PlatformCarrierWait = Machine.Machine.Ins.Find<PlatformEx>("RightCarrier")["Wait"],
                        PlatformCarrierWork = Machine.Machine.Ins.Find<PlatformEx>("RightCarrier")["Work"],

                        do_clampy_cy = Machine.Machine.Ins.Find<ICylinderEx>("LClampCylinderY"),

                        //up
                        Platform1 = Machine.Machine.Ins.Find<PlatformEx>("RightUp"),
                        Platform1PosWait = Machine.Machine.Ins.Find<PlatformEx>("RightUp")["Wait"],

                        //down
                        Platform2 = Machine.Machine.Ins.Find<PlatformEx>("RightDown"),
                        Platform2PosWait = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["Wait"],
                        Platform2PosGtCalib1 = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["DownAlign1"] as PosXYZ,
                        Platform2PosGtCalib2 = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["GtOffsetCalib"] as PosXYZ,

                        do_gt2_cy = Machine.Machine.Ins.Find<ICylinderEx>("RGTCylinder"),

                        JumpHeight1 = -50,
                        JumpHeight2 = -15,


                        AlignPosUp = Machine.Machine.Ins.Find<PlatformEx>("RightUp").Positions.FindAll(p => p.Name.StartsWith("UpAlign")).Cast<PosXYZ>().ToList(),
                        AlignPosDown = Machine.Machine.Ins.Find<PlatformEx>("RightDown").Positions.FindAll(p => p.Name.StartsWith("DownAlign")).Cast<PosXYZ>().ToList(),
                    };
            }

            throw new  Exception("Create Up 2 Down XY Calib Fail");
        }

        public static void SaveAlignCalib(AutoPlatformUp2DownAlignCalib calib)
        {
            if (MessageBox.Show($"保存{calib.Station.Name}标定数据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (calib.Station.Id == 1)
                    {
                        Machine.Machine.Ins.Settings.Calibration.LeftTransform = calib.OutputTransForm;
                        Machine.Machine.Ins.Settings.Calibration.LeftGtOffset = calib.OutputPlatform2GtOffset;
                    }
                    else if (calib.Station.Id == 2)
                    {
                        Machine.Machine.Ins.Settings.Calibration.RightTransform = calib.OutputTransForm;
                        Machine.Machine.Ins.Settings.Calibration.RightGtOffset = calib.OutputPlatform2GtOffset;
                    }

                    Machine.Machine.Ins.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存标定数据异常:{ex.Message}");
                }
            }
        }
    }
}