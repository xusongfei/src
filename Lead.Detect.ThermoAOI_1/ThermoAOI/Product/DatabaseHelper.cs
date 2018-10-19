using System.Linq;
using Lead.Detect.DatabaseHelper;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;

namespace Lead.Detect.ThermoAOI.Product
{
    public static class DatabaseHelper
    {
        public static ProductTestDataEntity ToEntity(this Thermo1Product data)
        {
            var en = new ProductTestDataEntity();

            en.StartTime = data.StartTime.ToString("yyyyMMdd-HHmmss");
            en.FinishTime = data.FinishTime.ToString("yyyyMMdd-HHmmss");

            en.Status = data.Status.ToString();
            en.Error = data.Error;
            en.CT = decimal.Parse(data.CT.ToString("F1"));
            en.Barcode = data.Barcode;

            en.ProductType = data.ProductType;
            en.Description = data.Description;


            en.FAI = string.Join(",", data.SPCItems.Select(f => $"{f.SPC}:{f.Value:F3}"));

            en.RAWUP = string.Join(",", data.RawDataUp.Select(p => $"{p.Z:F3}:{p.Status}"));
            en.RAWDOWN = string.Join(",", data.RawDataDown.Select(p => $"{p.Z:F3}:{p.Status}"));

            return en;
        }
    }
}