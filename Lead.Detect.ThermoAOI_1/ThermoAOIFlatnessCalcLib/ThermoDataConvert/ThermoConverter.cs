using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Product;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.ThermoDataConvert
{
    public class ThermoConverter
    {

        public static AvcData Convert(ThermoProduct product, string partid, string machineName)
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
        /// <returns></returns>
        public static CMData Convert(ThermoProduct product, string machineName)
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
