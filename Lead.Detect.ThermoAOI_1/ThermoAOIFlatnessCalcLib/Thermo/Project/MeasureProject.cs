using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Product;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo2;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Project
{
    [XmlInclude(typeof(MeasureProject1))]
    [XmlInclude(typeof(MeasureProjectA))]
    [XmlInclude(typeof(MeasureProjectB))]
    public class MeasureProject : UserSettings<MeasureProject>
    {

        public MeasureProject()
        {
            ProjectName = "default.mprj";
            ProjectName = "default";
            Version = "v0.1";
        }



        [Category("工程"), Description("测试文件名称")]
        public string ProjectName { get; set; }
        [Category("工程"), Description("测试文件版本")]
        public string Version { get; set; }



        [Category("产品信息"), Description("产品名称")]
        public string ProductName { get; set; }

        [Category("产品信息"), Description("产品类型")]
        public ProductType ProductType { get; set; }

        [Category("产品信息"), Description("产品类型")]
        public int TypeId { get; set; }

        [Category("产品信息"), Description("产品编号")]
        public string PartID { get; set; }

        [Category("产品信息"), Description("产品高度")]
        public double Height { get; set; }



        [Category("测试"), Description("测试项")]
        public List<SPCItem> SPCItems { get; set; } = new List<SPCItem>();



        public override bool CheckIfNormal()
        {
            if (ProductType == ProductType.VaporChamber)
            {
                if (Height > 10)
                {
                    return false;
                }

                else if (ProductType == ProductType.FullModule)
                {
                    if (Height < 60)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("ProjectName:" + ProjectName);
            sb.AppendLine("Version:" + Version);
            sb.AppendLine();
            sb.AppendLine("ProductName:" + ProductName);
            sb.AppendLine("ProductType:" + ProductType.ToString());
            sb.AppendLine("TypeId:" + TypeId.ToString());
            sb.AppendLine("PartID:" + PartID);
            sb.AppendLine("Height:" + Height.ToString("F2"));
            sb.AppendLine();
            foreach (var spcItem in SPCItems)
            {
                sb.AppendLine(spcItem.ToString());
            }

            return sb.ToString();
        }
    }
}