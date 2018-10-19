using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.CameraControl;

namespace Lead.Detect.MeasureComponents.Calibration
{
    public class MachineACalib : AutoCalib
    {
        public ICameraEx Camera1 { get; set; }
        public PlatformEx Platform { get; set; }


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
