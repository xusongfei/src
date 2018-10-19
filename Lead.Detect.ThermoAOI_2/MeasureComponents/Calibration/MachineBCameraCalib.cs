using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.CameraControl;

namespace Lead.Detect.MeasureComponents.Calibration
{
    public class MachineBCameraCalib : AutoCalib
    {

        public ICameraEx Camera { get; set; }

        public PlatformEx CameraPlatform { get; set; }

        public PlatformEx TransPlatform { get; set; }

        public override void InitCalib()
        {
        }

        public override void DoCalib()
        {

        }

        public override void UninitCalib()
        {

        }




    }
}