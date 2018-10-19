using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.LaserControl;

namespace Lead.Detect.MeasureComponents.Calibration
{
    public class MachineBLaserCalib : AutoCalib
    {

        public ILineLaserEx Laser1 { get; set; }
        public ILineLaserEx Laser2 { get; set; }
        public PlatformEx Laser1Platform { get; set; }
        public PlatformEx Laser2Platform { get; set; }
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
