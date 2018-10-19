using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
    public class Thermo2ProductB : ThermoProduct
    {
        [Description("测试点")]
        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();

        [Description("测试点")]
        public List<PosXYZ> Laser1Pos { get; set; } = new List<PosXYZ>();

        [Description("测试点")]
        public List<PosXYZ> Laser2Pos { get; set; } = new List<PosXYZ>();



        public override string CsvHeaders()
        {
            var sb = new StringBuilder();

            sb.Append(base.CsvHeaders());

            return sb.ToString();
        }

        public override string CsvValues()
        {
            var sb = new StringBuilder();

            sb.Append(base.CsvValues());

            return sb.ToString();
        }


        public override DataTable ToDataTable()
        {
            var dt = base.ToDataTable();

            return dt;

        }
    }
}