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
        public List<PosXYZU> CapturePos { get; set; } = new List<PosXYZU>();
        [Description("测试点")]
        public List<PosXYZU> Laser1Pos { get; set; } = new List<PosXYZU>();
        [Description("测试点")]
        public List<PosXYZU> Laser2Pos { get; set; } = new List<PosXYZU>();


        [Description("相机原始数据")]
        public List<List<double>> RawData_LineProfile { get; set; } = new List<List<double>>();
        [Description("上激光原始数据")]
        public List<List<PosXYZ>> RawData_UpProfile { get; set; } = new List<List<PosXYZ>>();
        [Description("下激光原始数据")]
        public List<List<PosXYZ>> RawData_DownProfile { get; set; } = new List<List<PosXYZ>>();



        public override string CsvHeaders()
        {
            var sb = new StringBuilder();

            sb.Append(base.CsvHeaders());

            sb.Append("CAMERA,");
            for (int i = 0; i < RawData_LineProfile.Count; i++)
            {
                for (int j = 0; j < RawData_LineProfile[i].Count; j++)
                {
                    sb.Append($"C-{i}L{j},");
                }
            }

            sb.Append("LASER1,");
            for (int i = 0; i < RawData_UpProfile.Count; i++)
            {
                for (int j = 0; j < RawData_UpProfile[i].Count; j++)
                {
                    sb.Append($"LU-C{i}R{j},");
                }
            }
            sb.Append("LASER2,");
            for (int i = 0; i < RawData_DownProfile.Count; i++)
            {
                for (int j = 0; j < RawData_DownProfile[i].Count; j++)
                {
                    sb.Append($"LD-C{i}R{j},");
                }
            }

            return sb.ToString();
        }

        public override string CsvValues()
        {
            var sb = new StringBuilder();

            sb.Append(base.CsvValues());

            sb.Append("CAMERA,");
            for (int i = 0; i < RawData_LineProfile.Count; i++)
            {
                for (int j = 0; j < RawData_LineProfile[i].Count; j++)
                {
                    sb.Append($"{RawData_LineProfile[i][j]:F3},");
                }
            }
            sb.Append("LASER1,");
            for (int i = 0; i < RawData_UpProfile.Count; i++)
            {
                for (int j = 0; j < RawData_UpProfile[i].Count; j++)
                {
                    sb.Append($"{RawData_UpProfile[i][j].Z:F3},");
                }
            }
            sb.Append("LASER2,");
            for (int i = 0; i < RawData_DownProfile.Count; i++)
            {
                for (int j = 0; j < RawData_DownProfile[i].Count; j++)
                {
                    sb.Append($"{RawData_DownProfile[i][j].Z:F3},");
                }
            }

            return sb.ToString();
        }


        public override DataTable ToDataTable()
        {
            var dt = base.ToDataTable();

            for (int i = 0; i < RawData_LineProfile.Count; i++)
            {
                for (int j = 0; j < RawData_LineProfile[i].Count; j++)
                {
                    var row = dt.Rows.Add();
                    row[0] = $"C{i}L{j}";
                    row[1] = $"{RawData_LineProfile[i][j]:F3}";
                }
            }


            for (int i = 0; i < RawData_UpProfile.Count; i++)
            {
                for (int j = 0; j < RawData_UpProfile[i].Count; j++)
                {
                    var row = dt.Rows.Add();
                    row[0] = $"LU{i}R{j}";
                    row[1] = $"{RawData_UpProfile[i][j].Z:F3}";
                }
            }

            for (int i = 0; i < RawData_DownProfile.Count; i++)
            {
                for (int j = 0; j < RawData_DownProfile[i].Count; j++)
                {
                    var row = dt.Rows.Add();
                    row[0] = $"LD{i}R{j}";
                    row[1] = $"{RawData_DownProfile[i][j].Z:F3}";
                }
            }

            return dt;

        }
    }
}