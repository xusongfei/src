using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI.Machine1.UserDefine;
using Lead.Detect.Utility.Transformation;

namespace Lead.Detect.ThermoAOI.Machine1.Calibration
{
    /// <summary>
    /// 治具坐标系到上平台坐标系标定
    /// </summary>
    public class AutoPlatformProduct2UpCalib : AutoCalib
    {
        [Category("PLATFORM")]
        public PlatformEx PlatformCarrier { get; set; }



        [Category("PLATFORM")]
        public PlatformEx PlatformUp { get; set; }
        [Category("PLATFORM")]
        public double PlatformUpJumpHeight { get; set; } = -30;



        [Category("CALIBRATION")]
        public List<PosXYZ> ProductPos { get; set; }
        [Category("CALIBRATION")]
        public List<PosXYZ> PlatformUpPos { get; set; }


       
        [Category("OUTPUT")]
        public TransformParams OutputTransform { get; set; }



        public override void InitCalib()
        {
            PlatformCarrier?.EnterAuto(this).MoveAbs("Work");
        }

        public override void UninitCalib()
        {
            PlatformUp?.EnterAuto(this).Jump("Wait", PlatformUpJumpHeight);
            PlatformCarrier?.EnterAuto(this).MoveAbs("Wait");

            PlatformUp?.ExitAuto();
            PlatformCarrier?.ExitAuto();
        }


        public override void DoCalib()
        {
            if (PlatformUp != null)
            {
                var isFirst = true;
                foreach (var pos in PlatformUpPos)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        PlatformUp.EnterAuto(this).Jump(pos, 0);
                    }
                    else
                    {
                        PlatformUp.EnterAuto(this).Jump(pos, PlatformUpJumpHeight);
                    }
                }
            }


            {
                var ret = XyzPlarformCalibration.CalcAffineTransform(ProductPos.Select(p => new PosXYZ(p.X, p.Y, 0)).ToList(), PlatformUpPos.Select(p => new PosXYZ(p.X, p.Y, 0)).ToList());

                Log($"计算产品坐标到上平台转换矩阵:\r\n"
                    + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6},{ret.Item1[0, 3]:F6}\r\n"
                    + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6},{ret.Item1[1, 3]:F6}\r\n"
                    + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6},{ret.Item1[2, 3]:F6}\r\n"
                    + $"{ret.Item1[3, 0]:F6},{ret.Item1[3, 1]:F6},{ret.Item1[3, 2]:F6},{ret.Item1[3, 3]:F6}\r\n", LogLevel.Info);

                Log($"计算误差:\r\n{ret.Item2:F2}\r\n", LogLevel.Info);

                OutputTransform = new TransformParams(ret.Item1);
            }
        }


        public static AutoPlatformProduct2UpCalib CreateProductToUpCalib(string platform)
        {
            switch (platform)
            {
                case "Left":
                    return new AutoPlatformProduct2UpCalib()
                    {
                        CalibInfo = $"LeftUpXYCalib",
                        Station = Machine.Ins.Find<Station>("LeftStation"),
                        ProductPos = Machine.Ins.Find<PlatformEx>("LeftUp").Positions.FindAll(p => p.Name.StartsWith("UpP") && !p.Name.StartsWith("UpPAlign")).Cast<PosXYZ>().ToList(),
                        PlatformUpPos = Machine.Ins.Find<PlatformEx>("LeftUp").Positions.FindAll(p => p.Name.StartsWith("UpPAlign")).Cast<PosXYZ>().ToList(),
                    };

                case "Right":
                    return new AutoPlatformProduct2UpCalib()
                    {
                        CalibInfo = $"RightUpXYCalib",
                        Station = Machine.Ins.Find<Station>("RightStation"),
                        ProductPos = Machine.Ins.Find<PlatformEx>("RightUp").Positions.FindAll(p => p.Name.StartsWith("UpP") && !p.Name.StartsWith("UpPAlign")).Cast<PosXYZ>().ToList(),
                        PlatformUpPos = Machine.Ins.Find<PlatformEx>("RightUp").Positions.FindAll(p => p.Name.StartsWith("UpPAlign")).Cast<PosXYZ>().ToList(),
                    };
            }

            throw new Exception("Create xy calib fail");
        }

        public static void SaveProductToUpCalib(AutoPlatformProduct2UpCalib calib)
        {
            if (MessageBox.Show($"保存{calib.Station.Name}标定数据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (calib.Station.Id == 1)
                    {
                        Machine.Ins.Settings.Calibration.LeftUpTransform = calib.OutputTransform;
                    }
                    else if (calib.Station.Id == 2)
                    {
                        Machine.Ins.Settings.Calibration.RightUpTransform = calib.OutputTransform;
                    }

                    Machine.Ins.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存上平台XY标定数据异常:{ex.Message}");
                }
            }
        }
    }
}