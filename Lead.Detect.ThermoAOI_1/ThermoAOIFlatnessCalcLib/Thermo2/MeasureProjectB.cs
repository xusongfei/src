using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIProductLib.Thermo;

namespace Lead.Detect.ThermoAOIProductLib.Thermo2
{
    /// <summary>
    /// profile measure machine project
    /// </summary>
    public class MeasureProjectB : MeasureProject
    {
        public MeasureProjectB()
        {
        }



        [Category("测试"), Description("相机拍照点")]
        public List<PosXYZU> CapturePos { get; set; } = new List<PosXYZU>();

        [Category("测试"), Description("上激光测试点")]
        public List<PosXYZU> UpLaserPos { get; set; } = new List<PosXYZU>();

        [Category("测试"), Description("下激光测试点")]
        public List<PosXYZU> DownLaserPos { get; set; } = new List<PosXYZU>();



        public override string ToString()
        {
            return $"{base.ToString()}\r\n"
                   + $"CapturePos:\r\n{string.Join("\r\n", CapturePos.Select(p => p.ToString()))}\r\n"
                   + $"UpLaserPos:\r\n{string.Join("\r\n", UpLaserPos.Select(p => p.ToString()))}\r\n"
                   + $"DownLaserPos:\r\n{string.Join("\r\n", DownLaserPos.Select(p => p.ToString()))}";
        }
    }
}