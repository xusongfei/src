using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using g3;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOI.Common;
using Lead.Detect.ThermoAOI.Machine.Common;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI.Machine.newTasks;
using System.Linq;

namespace Lead.Detect.ThermoAOI.Calibration
{
    class AutoPlatformHeightCalib : AutoCalib
    {
        #region trans

        [Category("传送上料配置")]
        public ICylinderEx do_clampy_cy { get; set; }
        [Category("传送上料配置")]
        public PlatformEx PlatformCarrier { get; set; }
        [Category("传送上料配置")]
        public PosXYZ PlatformCarrierWait { get; set; }
        [Category("传送上料配置")]
        public PosXYZ PlatformCarrierWork { get; set; }

        #endregion

        #region up platform

        [Category("上工站配置")]
        public PlatformEx Platform1 { get; set; }
        [Category("上工站配置")]
        public PosXYZ Platform1PosWait { get; set; }
        [Category("上工站配置")]
        public PosXYZ Platform1PosHeightCalib { get; set; }
        [Category("上工站配置")]
        public double JumpHeight1 { get; set; } = -50;
        public List<PosXYZ> UpHeightCalibPos { get; set; }

        #endregion

        #region down platform

        [Category("下工站配置")]
        public PlatformEx Platform2 { get; set; }
        [Category("下工站配置")]
        public PosXYZ Platform2PosWait { get; set; }
        [Category("下工站配置")]
        public PosXYZ Platform2PosHeightCalibGt1 { get; set; }
        [Category("下工站配置")]
        public PosXYZ Platform2PosHeightCalibGt2 { get; set; }
        [Category("下工站配置")]
        public double JumpHeight2 { get; set; } = -15;
        [Category("下工站配置")]
        public ICylinderEx do_gt2_cy { get; set; }
        public List<PosXYZ> DownHeightCalibPos { get; set; }

        #endregion

        #region misc

        public KeyenceGT GtController { get; set; }
        public PosXYZ StandardHeight { get; set; }

        #endregion


        [Category("OUTPUT")]
        public PosXYZ OutputUpGtCalib { get; set; }
        [Category("OUTPUT")]
        public PosXYZ OutputUpGtHeight { get; set; }
        [Category("OUTPUT")]
        public PlaneParams OutputUpStandardPlane { get; set; }


        [Category("OUTPUT")]
        public PosXYZ OutputDownGt1Calib { get; set; }
        [Category("OUTPUT")]
        public PosXYZ OutputDownGt1Height { get; set; }
        [Category("OUTPUT")]
        public PosXYZ OutputDownGt2Calib { get; set; }
        [Category("OUTPUT")]
        public PosXYZ OutputDownGt2Height { get; set; }
        [Category("OUTPUT")]
        public PlaneParams OutputDownStandardPlane { get; set; }
        [Category("OUTPUT")]
        public PosXYZ OutputStandardHeight { get; set; }



        public override void InitCalib()
        {
            do_gt2_cy?.SetDo(this, false);
            do_clampy_cy.SetDo(this);

            PlatformCarrier?.EnterAuto(this).MoveAbs(PlatformCarrierWork);
        }

        public override void UninitCalib()
        {
            do_gt2_cy?.SetDo(this, false);

            if (Platform1 != null)
            {
                Platform1.EnterAuto(this).Home(2);
                Platform1.EnterAuto(this).MoveAbs(Platform1PosWait, checkLimit: false);
            }


            if (Platform2 != null)
            {
                Platform2.EnterAuto(this).Home(2);
                Platform2.EnterAuto(this).MoveAbs(Platform2PosWait, checkLimit: false);
            }


            PlatformCarrier?.EnterAuto(this).MoveAbs(PlatformCarrierWait);

            do_clampy_cy.SetDo(this, false);
        }


        public override void DoCalib()
        {
            OutputStandardHeight = StandardHeight as PosXYZ;

            Log("上平台GT高度标定\n----------------------------------------------------------");
            {
                //点标定 
                OutputUpGtCalib = Platform1PosHeightCalib as PosXYZ;
                //update z
                if (Platform1 != null && GtController != null)
                {

                    Platform1.EnterAuto(this).Jump(Platform1PosHeightCalib, 0);
                    Thread.Sleep(1000);
                    OutputUpGtCalib.OffsetZ = GtController.ReadData()[0];
                }
                OutputUpGtHeight = new PosXYZ { X = Platform1PosHeightCalib.X, Y = Platform1PosHeightCalib.Y, Z = OutputUpGtCalib.OffsetZ };
                DataList.Add(OutputUpGtCalib.ToString());
                DataList.Add(OutputUpGtHeight.ToString());


                //标准平面标定
                if (UpHeightCalibPos != null)
                {
                    //update z
                    if (Platform1 != null && GtController != null)
                    {
                        foreach (var calibPos in UpHeightCalibPos)
                        {
                            //product to up platform
                            var testpos = CalibrationConfig.TransformToPlatformPos(
                                Machine.Machine.Ins.Settings.Calibration,
                                Station.Id == 1 ? PlatformType.LUp : PlatformType.RUp,
                                calibPos,
                                Platform1PosHeightCalib.Z);
                            Platform1.Jump(testpos, JumpHeight1);
                            Thread.Sleep(1000);
                            calibPos.Z = GtController.ReadData()[0];
                            DataList.Add(calibPos.ToString());
                        }
                    }
                    var fitplane = PlaneParams.FitPlane(UpHeightCalibPos);
                    OutputUpStandardPlane = new PlaneParams() { Normal = fitplane.Normal, Origin = fitplane.Origin };
                    DataList.Add(OutputUpStandardPlane.ToString());
                }
            }

            //复位上平台
            if (Platform1 != null)
            {
                Platform1.EnterAuto(this).Home(2);
                Platform1.EnterAuto(this).MoveAbs(Platform1PosWait, checkLimit: false);
            }
            Log("上平台GT高度标定 完成\n----------------------------------------------------------");


            //下GT高度标定

            //复位下平台Z轴
            if (Platform2 != null)
            {
                Platform2.EnterAuto(this).Home(2);
                Platform2.EnterAuto(this).MoveAbs(2, Platform2PosWait, checkLimit: false);
            }

            Log("下平台GT1高度标定\n----------------------------------------------------------");
            {
                var pos = new PosXYZ(Platform2PosHeightCalibGt1.Data());
                pos.Z = 1;
                Platform2?.EnterAuto(this).Jump(pos, 0);
                do_gt2_cy?.SetDo(this, true);
                Thread.Sleep(500);
                {

                    //GT1GT2 高度差标定
                    OutputDownGt1Calib = Platform2PosHeightCalibGt1 as PosXYZ;
                    //update z
                    if (Platform2 != null && GtController != null)
                    {
                        Platform2?.EnterAuto(this).Jump(Platform2PosHeightCalibGt1, 0);
                        Thread.Sleep(1000);
                        OutputDownGt1Calib.OffsetZ = GtController.ReadData()[1];

                    }
                    OutputDownGt1Height = new PosXYZ() { X = Platform2PosHeightCalibGt1.X, Y = Platform2PosHeightCalibGt1.Y, Z = OutputDownGt1Calib.OffsetZ };


                    //GT1标准平面标定
                    if (DownHeightCalibPos != null)
                    {
                        //update z
                        if (Platform2 != null && GtController != null)
                        {
                            foreach (var calibPos in DownHeightCalibPos)
                            {
                                //product to up platform
                                var testpos = CalibrationConfig.TransformToPlatformPos(
                                    Machine.Machine.Ins.Settings.Calibration,
                                    Station.Id == 1 ? PlatformType.LDown : PlatformType.RDown,
                                    calibPos,
                                    Platform2PosHeightCalibGt1.Z);
                                Platform2.Jump(testpos, JumpHeight2);
                                Thread.Sleep(1000);
                                calibPos.Z = GtController.ReadData()[1];
                                DataList.Add(calibPos.ToString());
                            }
                        }

                        var fitplane = PlaneParams.FitPlane(DownHeightCalibPos);
                        OutputDownStandardPlane = new PlaneParams() { Normal = fitplane.Normal, Origin = fitplane.Origin };
                        DataList.Add(OutputDownStandardPlane.ToString());
                    }

                }
                do_gt2_cy?.SetDo(this, false);
            }


            //复位下平台Z轴
            if (Platform2 != null)
            {
                Platform2.EnterAuto(this).Home(2);
                Platform2.EnterAuto(this).MoveAbs(2, Platform2PosWait, checkLimit: false);
            }
            Log("下平台GT1高度标定  完成\n----------------------------------------------------------");


            Log("下平台GT2高度标定\n----------------------------------------------------------");
            {
                //GT1GT2 高度差标定
                OutputDownGt2Calib = Platform2PosHeightCalibGt2 as PosXYZ;
                //update z
                if (Platform2 != null && GtController != null)
                {
                    //do_gt2_cy?.SetDo(this, true);
                    Platform2?.Jump(Platform2PosHeightCalibGt2, 0);
                    Thread.Sleep(1000);

                    OutputDownGt2Calib.OffsetZ = GtController.ReadData()[2];
                    //do_gt2_cy?.SetDo(this, false);
                }
                OutputDownGt2Height = new PosXYZ() { X = Platform2PosHeightCalibGt2.X, Y = Platform2PosHeightCalibGt2.Y, Z = OutputDownGt2Calib.OffsetZ };

                //复位下平台
                //move platform2 wait
                if (Platform2 != null)
                {
                    Platform2.EnterAuto(this).Home(2);
                    Platform2.EnterAuto(this).MoveAbs(Platform2PosWait, checkLimit: false);
                }
                Log("下平台GT2高度标定  完成\n----------------------------------------------------------");

            }

            ////计算高度标定点的实际高度值
            //{
            //    var normal1 = OutputUpStandardPlane.Normal;
            //    var normal2 = OutputDownStandardPlane.Normal;
            //    var normal = new Vector3d(0, 0, 1);
            //    var th1 = normal.Dot(normal1);
            //    var th2 = normal.Dot(normal2);
            //    var h1 = OutputStandardHeight.Z / th1;
            //    var h2 = OutputStandardHeight.Z / th2;
            //    OutputStandardHeight.OffsetZ = (Math.Abs(h1) + Math.Abs(h2)) / 2;
            //    Log($"Calc Standard Height:{h1:F3},{h2:F3}");
            //}


            //output
            DataList.Add(OutputDownGt1Calib.ToString());
            DataList.Add(OutputDownGt1Height.ToString());
            DataList.Add(OutputDownGt2Calib.ToString());
            DataList.Add(OutputDownGt2Height.ToString());
            DataList.Add(OutputStandardHeight.ToString());

            DataList.Add(OutputUpStandardPlane.ToString());
            DataList.Add(OutputDownStandardPlane.ToString());
        }




        public static AutoPlatformHeightCalib CreateHeightCalib(string platform)
        {
            switch (platform)
            {
                case "Left":
                    return new AutoPlatformHeightCalib
                    {
                        CalibInfo = $"LeftPlatformHeight",
                        Description = "Left",
                        Station = Machine.Machine.Ins.Find<Station>("LeftStation"),

                        //carrier
                        PlatformCarrier = Machine.Machine.Ins.Find<PlatformEx>("LeftCarrier"),
                        PlatformCarrierWait = Machine.Machine.Ins.Find<PlatformEx>("LeftCarrier")["Wait"] as PosXYZ,
                        PlatformCarrierWork = Machine.Machine.Ins.Find<PlatformEx>("LeftCarrier")["Work"] as PosXYZ,

                        do_clampy_cy = Machine.Machine.Ins.Find<ICylinderEx>("LClampCylinderY"),

                        //up
                        Platform1 = Machine.Machine.Ins.Find<PlatformEx>("LeftUp"),
                        Platform1PosWait = Machine.Machine.Ins.Find<PlatformEx>("LeftUp")["Wait"] as PosXYZ,
                        Platform1PosHeightCalib = Machine.Machine.Ins.Find<PlatformEx>("LeftUp")["HeightCalib"] as PosXYZ,
                        UpHeightCalibPos = Machine.Machine.Ins.Find<PlatformEx>("LeftUp").Positions.FindAll(p => p.Name.StartsWith("HeightAlign")).Cast<PosXYZ>().ToList(),

                        //down
                        Platform2 = Machine.Machine.Ins.Find<PlatformEx>("LeftDown"),
                        Platform2PosWait = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["Wait"] as PosXYZ,
                        Platform2PosHeightCalibGt1 = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["HeightCalib1"] as PosXYZ,
                        Platform2PosHeightCalibGt2 = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["HeightCalib2"] as PosXYZ,
                        StandardHeight = Machine.Machine.Ins.Find<PlatformEx>("LeftDown")["StandardHeight"] as PosXYZ,
                        DownHeightCalibPos = Machine.Machine.Ins.Find<PlatformEx>("LeftDown").Positions.FindAll(p => p.Name.StartsWith("HeightAlign")).Cast<PosXYZ>().ToList(),

                        do_gt2_cy = Machine.Machine.Ins.Find<ICylinderEx>("LGTCylinder"),

                        JumpHeight1 = Machine.Machine.Ins.Settings.Common.LJumpHeightUp,
                        JumpHeight2 = Machine.Machine.Ins.Settings.Common.LJumpHeightDown,

                        GtController = (Machine.Machine.Ins.Find<StationTask>("LeftTrans") as newTransTask)?.GtController,
                    };

                case "Right":
                    return new AutoPlatformHeightCalib
                    {
                        CalibInfo = $"RightPlatformHeight",
                        Description = "RIGHT",
                        Station = Machine.Machine.Ins.Find<Station>("RightStation"),

                        //carrier
                        PlatformCarrier = Machine.Machine.Ins.Find<PlatformEx>("RightCarrier"),
                        PlatformCarrierWait = Machine.Machine.Ins.Find<PlatformEx>("RightCarrier")["Wait"] as PosXYZ,
                        PlatformCarrierWork = Machine.Machine.Ins.Find<PlatformEx>("RightCarrier")["Work"] as PosXYZ,

                        do_clampy_cy = Machine.Machine.Ins.Find<ICylinderEx>("RClampCylinderY"),

                        //up
                        Platform1 = Machine.Machine.Ins.Find<PlatformEx>("RightUp"),
                        Platform1PosWait = Machine.Machine.Ins.Find<PlatformEx>("RightUp")["Wait"] as PosXYZ,
                        Platform1PosHeightCalib = Machine.Machine.Ins.Find<PlatformEx>("RightUp")["HeightCalib"] as PosXYZ,
                        UpHeightCalibPos = Machine.Machine.Ins.Find<PlatformEx>("RightUp").Positions.FindAll(p => p.Name.StartsWith("HeightAlign")).Cast<PosXYZ>().ToList(),


                        //down
                        Platform2 = Machine.Machine.Ins.Find<PlatformEx>("RightDown"),
                        Platform2PosWait = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["Wait"] as PosXYZ,
                        Platform2PosHeightCalibGt1 = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["HeightCalib1"] as PosXYZ,
                        Platform2PosHeightCalibGt2 = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["HeightCalib2"] as PosXYZ,
                        StandardHeight = Machine.Machine.Ins.Find<PlatformEx>("RightDown")["StandardHeight"] as PosXYZ,
                        DownHeightCalibPos = Machine.Machine.Ins.Find<PlatformEx>("RightDown").Positions.FindAll(p => p.Name.StartsWith("HeightAlign")).Cast<PosXYZ>().ToList(),

                        do_gt2_cy = Machine.Machine.Ins.Find<ICylinderEx>("RGTCylinder"),

                        JumpHeight1 = Machine.Machine.Ins.Settings.Common.RJumpHeightUp,
                        JumpHeight2 = Machine.Machine.Ins.Settings.Common.RJumpHeightDown,

                        GtController = (Machine.Machine.Ins.Find<StationTask>("RightTrans") as newTransTask)?.GtController,
                    };
            }

            throw new Exception("Create Height Calib Fail");
        }


        public static void SaveHeightCalib(AutoPlatformHeightCalib calib)
        {
            if (MessageBox.Show($"保存{calib.Station.Name}标定数据？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (calib.Station.Id == 1)
                    {
                        Machine.Machine.Ins.Settings.Calibration.LeftHeightCalibGtPos = calib.OutputUpGtCalib;
                        Machine.Machine.Ins.Settings.Calibration.LeftHeightCalibGt1Pos = calib.OutputDownGt1Calib;
                        Machine.Machine.Ins.Settings.Calibration.LeftHeightCalibGt2Pos = calib.OutputDownGt2Calib;
                        Machine.Machine.Ins.Settings.Calibration.LeftHeightGt = calib.OutputUpGtHeight;
                        Machine.Machine.Ins.Settings.Calibration.LeftHeightGt1 = calib.OutputDownGt1Height;
                        Machine.Machine.Ins.Settings.Calibration.LeftHeightGt2 = calib.OutputDownGt2Height;

                        //new
                        Machine.Machine.Ins.Settings.Calibration.LeftUpStandardPlaneGT = calib.OutputUpStandardPlane;
                        Machine.Machine.Ins.Settings.Calibration.LeftDownStandardPlaneGT1 = calib.OutputDownStandardPlane;


                        Machine.Machine.Ins.Settings.Calibration.LeftHeightStandard = calib.StandardHeight as PosXYZ;
                    }
                    else if (calib.Station.Id == 2)
                    {
                        Machine.Machine.Ins.Settings.Calibration.RightHeightCalibGtPos = calib.OutputUpGtCalib;
                        Machine.Machine.Ins.Settings.Calibration.RightHeightCalibGt1Pos = calib.OutputDownGt1Calib;
                        Machine.Machine.Ins.Settings.Calibration.RightHeightCalibGt2Pos = calib.OutputDownGt2Calib;
                        Machine.Machine.Ins.Settings.Calibration.RightHeightGt = calib.OutputUpGtHeight;
                        Machine.Machine.Ins.Settings.Calibration.RightHeightGt1 = calib.OutputDownGt1Height;
                        Machine.Machine.Ins.Settings.Calibration.RightHeightGt2 = calib.OutputDownGt2Height;

                        //new
                        Machine.Machine.Ins.Settings.Calibration.RightUpStandardPlaneGT = calib.OutputUpStandardPlane;
                        Machine.Machine.Ins.Settings.Calibration.RightDownStandardPlaneGT1 = calib.OutputDownStandardPlane;

                        Machine.Machine.Ins.Settings.Calibration.RightHeightStandard = calib.StandardHeight as PosXYZ;
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