using System.ComponentModel;
using Lead.Detect.FrameworkExtension.common;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FlatnessProject : UserSettings<FlatnessProject>
    {
        public FlatnessProject()
        {
            ProjectName = "default.fprj";
            ProductSettings = new ProductSettings();
        }


        public string ProjectName { get; set; }
        public ProductSettings ProductSettings { get; set; }
        public override bool CheckIfNormal()
        {
            return ProductSettings != null;
        }
    }
}