using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{

    /// <summary>
    /// position measure machine project
    /// </summary>
    public class MeasureProjectA : MeasureProject
    {
        public MeasureProjectA()
        {
        }

        [Description("测试点")]
        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();


        public override string ToString()
        {
            return $"{base.ToString()}\r\n{string.Join("\r\n", CapturePos.Select(p => p.ToString()))}";
        }
    }
}