using System.Collections.Generic;
using System.Data;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Product;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1
{
    public class Thermo1Product : ThermoProduct
    {
        /// <summary>
        /// 上测试点数据 X: 产品X， Y: 产品Y， Z: GT读数， OffsetX: GT读数备份， OffsetZ: GT工作高度
        /// </summary>
        public List<PosXYZ> RawDataUp { get; set; } = new List<PosXYZ>();

        /// <summary>
        /// 下测试点数据 X: 产品X， Y: 产品Y， Z: GT读数， OffsetX: GT读数备份， OffsetZ: GT工作高度
        /// </summary>
        public List<PosXYZ> RawDataDown { get; set; } = new List<PosXYZ>();
      
     


        public override string CsvHeaders()
        {
            var sb = new StringBuilder();

            sb.Append($"{base.CsvHeaders()},");


            //spc value
            sb.Append("SPC,");
            foreach (var s in SPCItems)
            {
                sb.Append($"{s.SPC}-{s.Description},");
            }
            sb.Append("SPCMIN,");
            foreach (var s in SPCItems)
            {
                sb.Append($"{s.SPC}-MIN,");
            }
            sb.Append("SPCMAX,");
            foreach (var s in SPCItems)
            {
                sb.Append($"{s.SPC}-MAX,");
            }

            //spec description
            sb.Append("SPEC,");
            foreach (var fai in SPCItems)
            {
                sb.Append($"SPEC-{fai.SPC},UpLimit-{fai.SPC},DownLimit-{fai.SPC},");
            }

            //Z GT value overwrite by coord convert
            sb.Append("RawUp,");
            foreach (var gt in RawDataUp)
            {
                sb.Append(gt.Name); sb.Append(",");
            }

            sb.Append("RawDown,");
            foreach (var gt in RawDataDown)
            {
                sb.Append(gt.Name); sb.Append(",");
            }
            sb.Append("RawUpSTATUS,");
            foreach (var gt in RawDataUp)
            {
                sb.Append(gt.Name); sb.Append(",");
            }

            sb.Append("RawDownSTATUS,");
            foreach (var gt in RawDataDown)
            {
                sb.Append(gt.Name); sb.Append(",");
            }


            //Z GT raw value backup
            sb.Append("RawUpGT,");
            foreach (var gt in RawDataUp)
            {
                sb.Append(gt.Name); sb.Append(",");
            }

            sb.Append("RawDownGT,");
            foreach (var gt in RawDataDown)
            {
                sb.Append(gt.Name); sb.Append(",");
            }


            return sb.ToString();
        }

        public override string CsvValues()
        {
            var sb = new StringBuilder();
            sb.Append(base.CsvValues()); sb.Append(",");


            sb.Append("SPC,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.Value.ToString("F3"));
                sb.Append(",");
            }
            sb.Append("SPCMIN,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.ValueMin.ToString("F3"));
                sb.Append(",");
            }
            sb.Append("SPCMAX,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.ValueMax.ToString("F3"));
                sb.Append(",");
            }

            sb.Append("SPEC,");
            foreach (var s in SPCItems)
            {
                sb.Append($"{s.SPEC:F2},{s.UpLimit:F3},{s.DownLimit:F2},");
            }

            //Z GT value overwrite by coord convert
            sb.Append("RawUp,");
            foreach (var gt in RawDataUp)
            {
                sb.Append(gt.Z.ToString("F3")); sb.Append(",");
            }
            sb.Append("RawDown,");
            foreach (var gt in RawDataDown)
            {
                sb.Append(gt.Z.ToString("F3")); sb.Append(",");
            }

            sb.Append("RawUpSTATUS,");
            foreach (var gt in RawDataUp)
            {
                sb.Append(gt.Status); sb.Append(",");
            }

            sb.Append("RawDownSTATUS,");
            foreach (var gt in RawDataDown)
            {
                sb.Append(gt.Status); sb.Append(",");
            }

            //Z GT value backup
            sb.Append("RawUpGT,");
            foreach (var gt in RawDataUp)
            {
                sb.Append(gt.OffsetX.ToString("F3")); sb.Append(",");
            }

            sb.Append("RawDownGT,");
            foreach (var gt in RawDataDown)
            {
                sb.Append(gt.OffsetX.ToString("F3")); sb.Append(",");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{base.ToString()},");

            foreach (var spc in SPCItems)
            {
                sb.Append(spc.ToString() + ",");
            }

            return sb.ToString();
        }

        public override DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("测试项");
            dt.Columns.Add("测试值");

            {
                var row = dt.Rows.Add();
                row[0] = "开始时间";
                row[1] = StartTime.ToString("yyyyMMdd-HHmmss");
            }
            {
                var row = dt.Rows.Add();
                row[0] = "结束时间";
                row[1] = FinishTime.ToString("yyyyMMdd-HHmmss");
            }
            {
                var row = dt.Rows.Add();
                row[0] = "测试结果";
                row[1] = Status;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "测试异常";
                row[1] = Error;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "测试时间";
                row[1] = CT.ToString("F2");
            }
            {
                var row = dt.Rows.Add();
                row[0] = "条码";
                row[1] = Barcode;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "产品类型";
                row[1] = ProductType;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "产品描述";
                row[1] = Description;
            }


            foreach (var s in SPCItems)
            {
                var row = dt.Rows.Add();
                row[0] = $"SPC-{s.Name}";
                row[1] = s.ToString();
            }

            foreach (var gt in RawDataUp)
            {
                var row = dt.Rows.Add();
                row[0] = $"RAW-{gt.Index}-{gt.Name}-{gt.Description}";
                row[1] = gt.Z.ToString("F3");
            }

            foreach (var gt in RawDataDown)
            {
                var row = dt.Rows.Add();
                row[0] = $"RAW-{gt.Index}-{gt.Name}-{gt.Description}";
                row[1] = gt.Z.ToString("F3");
            }

            foreach (var gt in RawDataUp)
            {
                var row = dt.Rows.Add();
                row[0] = $"RAW-{gt.Index}-{gt.Name}-{gt.Description}";
                row[1] = gt.OffsetX.ToString("F3");
            }

            foreach (var gt in RawDataDown)
            {
                var row = dt.Rows.Add();
                row[0] = $"RAW-{gt.Index}-{gt.Name}-{gt.Description}";
                row[1] = gt.OffsetX.ToString("F3");
            }
            return dt;

        }
    }
}