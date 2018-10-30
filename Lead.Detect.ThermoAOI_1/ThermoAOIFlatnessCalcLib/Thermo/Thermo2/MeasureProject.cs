using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
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


        [Category("工程文件"), Description("测试文件名称")]
        public string ProjectName { get; set; }
        [Category("工程文件"), Description("测试文件版本")]
        public string Version { get; set; }


        [Category("产品信息"), Description("产品名称")]
        public string ProductName { get; set; }
   

        [Category("测试"), Description("测试项")]
        public List<SPCItem> SPCItems { get; set; } = new List<SPCItem>();


        public override bool CheckIfNormal()
        {
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(ProjectName);
            sb.AppendLine(Version);
            sb.AppendLine();
            sb.AppendLine(ProductName);
            sb.AppendLine();
            foreach (var spcItem in SPCItems)
            {
                sb.AppendLine(spcItem.ToString());
            }

            return sb.ToString();
        }
    }
}