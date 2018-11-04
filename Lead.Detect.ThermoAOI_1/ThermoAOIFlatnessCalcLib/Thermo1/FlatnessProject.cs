using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.Thermo;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProductSettings : UserSettings<ProductSettings>
    {
        [Description("产品名称")]
        public string ProductName { get; set; }

        [Description("产品高度")]
        public double Height { get; set; }


        [Description("上工站测试点")]
        public List<PosXYZ> UpTestPositions { get; set; } = new List<PosXYZ>();

        [Description("下工站测试点")]
        public List<PosXYZ> DownTestPositions { get; set; } = new List<PosXYZ>();


        [Description("测试项")]
        public List<SPCItem> SPCItems { get; set; } = new List<SPCItem>();


        [Description("版本号")]
        public string Version { get; set; }


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


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FlatnessProject : UserSettings<FlatnessProject>
    {
        public FlatnessProject()
        {
            ProjectName = "default.fprj";
            ProductSettings = new ProductSettings();
        }


        public ThermoProductType ThermoProductType { get; set; }
        public string PartID { get; set; }
        public string ProjectName { get; set; }
        public ProductSettings ProductSettings { get; set; }

        public override bool CheckIfNormal()
        {
            return ProductSettings != null;
        }


        public MeasureProject1 Convert()
        {
            var mprj = new MeasureProject1();
            mprj.ProjectName = ProjectName;
            mprj.ProductName = ProductSettings.ProductName;
            mprj.PartID = PartID;

            mprj.ThermoProductType = ProductSettings.ProductName.Contains("NoFin") ? ThermoProductType.VaporChamber : ThermoProductType.FullModule;

            mprj.Height = ProductSettings.Height;
            mprj.UpTestPositions = ProductSettings.UpTestPositions;
            mprj.DownTestPositions = ProductSettings.DownTestPositions;

            mprj.SPCItems = ProductSettings.SPCItems;

            mprj.Version = ProductSettings.Version;

            return mprj;
        }
    }

}