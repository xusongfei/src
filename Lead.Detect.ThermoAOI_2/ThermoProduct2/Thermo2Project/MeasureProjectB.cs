using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOI2.ThermoProduct2.Thermo2Project
{
    /// <summary>
    /// profile measure machine project
    /// </summary>
    public class MeasureProjectB : MeasureProject
    {
        public MeasureProjectB()
        {
        }

        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();
        public List<PosXYZ> UpLaserPos { get; set; } = new List<PosXYZ>();
        public List<PosXYZ> DownLaserPos { get; set; } = new List<PosXYZ>();
    }
}