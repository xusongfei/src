using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
    /// <summary>
    /// profile measure machine project
    /// </summary>
    public class MeasureProjectB : MeasureProject
    {
        public MeasureProjectB()
        {
        }

        [Description("上相机拍照点")]
        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();
        [Description("上激光测试点")]
        public List<PosXYZ> UpLaserPos { get; set; } = new List<PosXYZ>();
        [Description("下激光测试点")]
        public List<PosXYZ> DownLaserPos { get; set; } = new List<PosXYZ>();


        public override string ToString()
        {
            return $"{base.ToString()}\r\n"
                   + $"CapturePos:\r\n{string.Join("\r\n", CapturePos.Select(p => p.ToString()))}\r\n"
                   + $"UpLaserPos:\r\n{string.Join("\r\n", UpLaserPos.Select(p => p.ToString()))}\r\n"
                   + $"DownLaserPos:\r\n{string.Join("\r\n", DownLaserPos.Select(p => p.ToString()))}";
        }
    }
}