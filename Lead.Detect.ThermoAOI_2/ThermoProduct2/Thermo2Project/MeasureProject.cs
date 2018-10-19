using System.Xml.Serialization;
using Lead.Detect.FrameworkExtension.common;

namespace Lead.Detect.ThermoAOI2.ThermoProduct2.Thermo2Project
{
    [XmlInclude(typeof(MeasureProjectA))]
    [XmlInclude(typeof(MeasureProjectB))]
    public class MeasureProject : UserSettings<MeasureProject>
    {
        public string ProjectName { get; set; }
        public string ProductName { get; set; }
        public string Version { get; set; }

        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}