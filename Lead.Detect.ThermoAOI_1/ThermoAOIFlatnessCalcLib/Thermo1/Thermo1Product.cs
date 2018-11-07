using System.Collections.Generic;
using System.Data;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIProductLib.Thermo;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1
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
            var dt = base.ToDataTable();

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