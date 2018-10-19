using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProductSettings : UserSettings<ProductSettings>
    {
        public ProductSettings()
        {
            UpTestPositions = new List<PosXYZ>();
            DownTestPositions = new List<PosXYZ>();
            SPCItems = new List<SPCItem>();
        }

        [Description("产品名称")]
        public string ProductName { get; set; }

        [Description("产品高度")]
        public double Height { get; set; }


        [Description("上工站测试点")]
        public List<PosXYZ> UpTestPositions { get; set; }

        [Description("下工站测试点")]
        public List<PosXYZ> DownTestPositions { get; set; }


        [Description("测试项")]
        public List<SPCItem> SPCItems { get; set; }


        [Description("版本号")]
        public int Version { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{ProductName}");
            sb.AppendLine($"{Version}");
            sb.AppendLine($"{Height:F2}");

            sb.AppendLine("SPC");
            foreach (var s in SPCItems)
            {
                sb.AppendLine($"{s.SPC},{s.Name},{s.SPEC:F2},{s.UpLimit:F2},{s.DownLimit:F2}");
            }

            sb.AppendLine("Up");
            foreach (var upPos in UpTestPositions)
            {
                sb.AppendLine($"{upPos.Name},{upPos.X:F2},{upPos.Y:F2},{upPos.Z:F2},{upPos.Description}");
            }

            sb.AppendLine("Down");
            foreach (var upPos in DownTestPositions)
            {
                sb.AppendLine($"{upPos.Name},{upPos.X:F2},{upPos.Y:F2},{upPos.Z:F2},{upPos.Description}");
            }

            return sb.ToString();
        }

        public override bool CheckIfNormal()
        {
            return ProductName != null &&
                   (ProductName.Contains("WithFin") && Height > 60)
                       || ((ProductName.Contains("NoFin") && Height > 0));
        }
    }
}