using System.ComponentModel;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOI.Machine1.UserDefine
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CommonConfig
    {
        public CommonConfig()
        {
        }

        #region auto

        [Category("AUTO")]
        public bool LeftAutoTryRun { get; set; } = false;
        [Category("AUTO")]
        public bool RightAutoTryRun { get; set; } = false;
        [Category("AUTO")]
        public bool BeepOnProductNG { get; set; } = false;


        [Category("FIN SENSOR 产品FIN高度传感器")]
        public bool LeftFinSensorCheck { get; set; } = true;
        [Category("FIN SENSOR 产品FIN高度传感器")]
        public bool RightFinSensorCheck { get; set; } = true;


        [Category("SENSOR 定位传感器")]
        public bool LeftSensorCheck { get; set; } = true;
        [Category("SENSOR 定位传感器")]
        public bool RightSensorCheck { get; set; } = true;

        #endregion


        #region  components

        [Category("Barcode")]
        public string LeftBarcodeCOM { get; set; } = "COM7";

        [Category("Barcode")]
        public string RightBarcodeCOM { set; get; } = "COM9";


        [Category("GT")]
        public string LeftGtController { set; get; } = "192.168.9.11";

        [Category("GT")]
        public int LeftGtPort { get; set; } = 64000;

        [Category("GT")]
        public string RightGtController { set; get; } = "192.168.10.11";

        [Category("GT")]
        public int RightGtPort { get; set; } = 64000;


        [Category("GT")]
        public int GtReadDelay { get; set; } = 500;

        #endregion


        #region platform

        [Category("PLATFORM")]
        public bool OptimizeDownWait { get; set; } = false;

        [Category("PLATFORM")]
        public double LJumpHeightUp { get; set; } = -50;

        [Category("PLATFORM")]
        public double RJumpHeightUp { get; set; } = -50;

        [Category("PLATFORM")]
        public double LJumpHeightDown { get; set; } = -10;

        [Category("PLATFORM")]
        public double RJumpHeightDown { get; set; } = -10;

        #endregion


        #region platform offset

        [Category("PLATFORM OFFSET")]
        public PosXYZ LeftGTSYSOffset { get; set; } = new PosXYZ();

        [Category("PLATFORM OFFSET")]
        public PosXYZ LeftGT1SYSOffset { get; set; } = new PosXYZ();

        [Category("PLATFORM OFFSET")]
        public PosXYZ LeftGT2SYSOffset { get; set; } = new PosXYZ();

        [Category("PLATFORM OFFSET")]
        public PosXYZ RightGTSYSOffset { get; set; } = new PosXYZ();

        [Category("PLATFORM OFFSET")]
        public PosXYZ RightGT1SYSOffset { get; set; } = new PosXYZ();

        [Category("PLATFORM OFFSET")]
        public PosXYZ RightGT2SYSOffset { get; set; } = new PosXYZ();

        #endregion

    }
}