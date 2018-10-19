using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOI2.ThermoProduct2.Thermo2Project
{

    /// <summary>
    /// position measure machine project
    /// </summary>
    public class MeasureProjectA : MeasureProject
    {
        public MeasureProjectA()
        {
        }

        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();
    }
}