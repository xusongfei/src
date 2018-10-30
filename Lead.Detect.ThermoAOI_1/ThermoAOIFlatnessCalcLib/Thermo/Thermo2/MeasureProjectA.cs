using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

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

        [Category("产品信息"), Description("产品编号")]
        public int TypeId { get; set; }

        [Category("产品信息"), Description("产品厚度")]
        public double Height { get; set; }


        [Category("测试"), Description("测试点")]
        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());

            sb.AppendLine(TypeId.ToString("F2"));
            sb.AppendLine(Height.ToString("F2"));
            sb.AppendLine();
            sb.AppendLine($"CapturePos:\r\n{string.Join("\r\n", CapturePos.Select(p => p.ToString()))}\r\n");

            return sb.ToString();
        }
    }
}