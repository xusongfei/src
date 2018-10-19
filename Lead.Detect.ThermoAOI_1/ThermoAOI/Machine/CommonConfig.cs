using System.ComponentModel;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOI.Machine.Common
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CommonConfig
    {
        public CommonConfig()
        {
        }

        [Category("Barcode")]
        public string LeftBarcodeCOM { get; set; } = "COM7";
        [Category("Barcode")]
        public string RightBarcodeCOM { set; get; } = "COM9";



        [Category("TASK")]
        public double LJumpHeightUp { get; set; } = -50;
        [Category("TASK")]
        public double RJumpHeightUp { get; set; } = -50;

        [Category("TASK")]
        public double LJumpHeightDown { get; set; } = -10;
        [Category("TASK")]
        public double RJumpHeightDown { get; set; } = -10;

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


        [Category("GT")]
        public string LeftGtController { set; get; } = "192.168.9.11";
        [Category("GT")]
        public int LeftGtPort { get; set; } = 64000;
        [Category("GT")]
        public string RightGtController { set; get; } = "192.168.10.11";

        [Category("GT")]
        public int RightGtPort { get; set; } = 64000;


        [Category("AUTO TRY RUN")]
        public bool LeftAutoTryRun { get; set; } = false;

        [Category("AUTO TRY RUN")]
        public bool RightAutoTryRun { get; set; } = false;



        [Category("AUTO")]
        public bool LeftFinSensorCheck { get; set; } = true;

        [Category("AUTO")]
        public bool RightFinSensorCheck { get; set; } = true;

        [Category("AUTO")]
        public bool BeepOnProductNG { get; set; } = false;


        [Category("AUTO TEST")]
        public bool IsRepeatTest { get; set; } = false;
    }
}