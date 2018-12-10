using System.Linq;
using System.Text;
using Lead.Detect.DatabaseHelper;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using Lead.Detect.ThermoAOIProductLib.Thermo2;
using System.Collections;

namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    public static class ThermoProductDatabaseConvertHelper
    {
        public static ProductDataEntity ToEntity(this Thermo1Product data)
        {
            var en = new ProductDataEntity();

            en.StartTime = data.StartTime.ToString("yyyyMMdd-HHmmss.fff");
            en.FinishTime = data.FinishTime.ToString("yyyyMMdd-HHmmss");

            en.Status = data.Status.ToString();
            en.Error = data.Error;
            en.CT = decimal.Parse(data.CT.ToString("F1"));
            en.Barcode = data.Barcode;

            en.ProductType = data.ProductType;
            en.Description = data.Description;


            en.Spcs = string.Join(",", data.SPCItems.Select(f => $"{f.SPC}:{f.Value:F3}"));

            var sb = new StringBuilder();
            sb.Append(string.Join(",", data.RawDataUp.Select(p => $"{p.Z:F3}:{p.Status}")));
            sb.Append(",");
            sb.Append(string.Join(",", data.RawDataDown.Select(p => $"{p.Z:F3}:{p.Status}")));

            en.Raws = sb.ToString();


            return en;
        }


        public static ProductDataEntity ToEntity(this Thermo2ProductA data)
        {
            var en = new ProductDataEntity();

            en.StartTime = data.StartTime.ToString("yyyyMMdd-HHmmss");
            en.FinishTime = data.FinishTime.ToString("yyyyMMdd-HHmmss");

            en.Status = data.Status.ToString();
            en.Error = data.Error;
            en.CT = decimal.Parse(data.CT.ToString("F1"));
            en.Barcode = data.Barcode;

            en.ProductType = data.ProductType;
            en.Description = data.Description;


            en.Spcs = string.Join(",", data.SPCItems.Select(f => $"{f.SPC}:{f.Value:F3}"));

            var sb = new StringBuilder();
            sb.Append(string.Join(",", data.RawData.Select(p => $"{p:F3}")));

            en.Raws = sb.ToString();


            return en;
        }


        public static ProductDataEntity ToEntity(this Thermo2ProductB data)
        {
            var en = new ProductDataEntity();

            en.StartTime = data.StartTime.ToString("yyyyMMdd-HHmmss");
            en.FinishTime = data.FinishTime.ToString("yyyyMMdd-HHmmss");

            en.Status = data.Status.ToString();
            en.Error = data.Error;
            en.CT = decimal.Parse(data.CT.ToString("F1"));
            en.Barcode = data.Barcode;

            en.ProductType = data.ProductType;
            en.Description = data.Description;


            en.Spcs = string.Join(",", data.SPCItems.Select(f => $"{f.SPC}:{f.Value:F3}"));

            var sb = new StringBuilder();
            sb.Append("Camera1,");
            sb.Append(string.Join(",", data.RawData_C1Profile.Select(p => p.Max(v => v.Z))));
            sb.Append("Camera2,");
            sb.Append(string.Join(",", data.RawData_C2Profile.Select(p => p.Max(v => v.Z))));

            sb.Append("Up,");
            sb.Append(string.Join(",", data.RawData_UpProfile.Select(p => p.Max(v => v.Z))));
            sb.Append("Down,");
            sb.Append(string.Join(",", data.RawData_DownProfile.Select(p => p.Max(v => v.Z))));

            en.Raws = sb.ToString();


            return en;
        }

        public static string ToSQL(this Thermo1Product data)
        {
            var en = new StringBuilder();
            en.Append(" Values('" + data.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
            en.Append("'" + data.FinishTime.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            en.Append("'" + data.Barcode + "',");
            en.Append("'" + data.Status.ToString() + "',");
            en.Append("'" + data.MachineName + "',");
            en.Append("'" + data.Error + "',");
            en.Append("'" + data.ProductType + "',");
            en.Append("'" + data.Description + "',");
            en.Append("'" + string.Join(",", data.SPCItems.Select(f => $"{f.SPC}:{f.Value:F3}")) + "',");
            en.Append("'" + decimal.Parse(data.CT.ToString("F1")) + "')");

            return en.ToString();
        }

        public static string ToSQL(this Thermo2ProductA data)
        {
            var en = new StringBuilder();
            en.Append(" Values('" + data.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
            en.Append("'" + data.FinishTime.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            en.Append("'" + data.Barcode + "',");
            en.Append("'" + data.Status.ToString() + "',");
            en.Append("'" + data.MachineName + "',");
            en.Append("'" + data.Error + "',");
            en.Append("'" + data.ProductType + "',");
            en.Append("'" + data.Description + "',");
            en.Append("'" + string.Join(",", data.RawData.Select(p => $"{p:F3}")) + "',");
            en.Append("'" + decimal.Parse(data.CT.ToString("F1")) + "')");

            return en.ToString();
        }

        public static string ToSQL(this Thermo2ProductB data)
        {
            var en = new StringBuilder();
            en.Append(" Values('" + data.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
            en.Append("'" + data.FinishTime.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            en.Append("'" + data.Barcode + "',");
            en.Append("'" + data.Status.ToString() + "',");
            en.Append("'" + data.MachineName + "',");
            en.Append("'" + data.Error + "',");
            en.Append("'" + data.ProductType + "',");
            en.Append("'" + data.Description + "',");
            en.Append("'" + string.Join(",", data.SPCItems.Select(f => $"{f.SPC}:{f.Value:F3}")) + "',");
            en.Append("'" + decimal.Parse(data.CT.ToString("F1")) + "')");

            return en.ToString();
        }
    }
}