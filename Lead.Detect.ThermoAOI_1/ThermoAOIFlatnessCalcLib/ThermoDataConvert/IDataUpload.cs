using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOIProductLib.ThermoDataConvert
{
    public interface IDataUpload
    {
        void SetDataProp(string prop, string value);
        void Upload(ICsvData data);
    }
}