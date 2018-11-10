using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.Thermo;

namespace Lead.Detect.ThermoAOIProductLib.ThermoDataConvert
{
    public class ThermoProductConvertHelper
    {


        public static ICsvData Convert(ThermoProduct product, DataUploaderSetting uploaderSetting)
        {
            switch (uploaderSetting.UploaderName)
            {
                case "AVC":
                    return AvcConvert(product, uploaderSetting.AVC_Part_ID, uploaderSetting.AVC_Machine_ID);
                    break;
                case "CM":
                    return CmConvert(product, uploaderSetting.CM_Machine_ID);

                default:
                    return null;
            }
        }




        public static AvcData AvcConvert(ThermoProduct product, string partid, string machineName)
        {
            var avcData = new AvcData();

            avcData.PartId = partid;
            avcData.TestTime = product.StartTime;
            avcData.PartId = product.Barcode;
            avcData.SpcItems = product.SPCItems;
            avcData.TestResult = product.Status.ToString();
            avcData.Machine = machineName;

            return avcData;
        }


        /// <summary>
        /// todo
        /// </summary>
        /// <param name="product"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static CMData CmConvert(ThermoProduct product, string machineName)
        {
            var avcData = new CMData();

            avcData.PartId = "0";
            avcData.TestTime = product.StartTime;
            avcData.PartId = product.Barcode;
            avcData.SpcItems = product.SPCItems;
            avcData.TestResult = product.Status.ToString();
            avcData.Machine = machineName;

            return avcData;
        }


    }
}
