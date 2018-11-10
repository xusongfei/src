using System.Collections.Generic;
using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOIProductLib.ThermoDataConvert
{
    public class DataUploadHelper : IDataUpload
    {
        public Dictionary<string, string> DataProps = new Dictionary<string, string>();

        public void SetDataProp(string prop, string value)
        {
            if (DataProps.ContainsKey(prop))
            {
                DataProps[prop] = value;
            }
            else
            {
                DataProps.Add(prop, value);
            }
        }

        public virtual void Upload(ICsvData data)
        {
        }
    }


    public class DataUploadFactory
    {
        private DataUploadFactory()
        {
        }

        public static DataUploadFactory Ins { get; } = new DataUploadFactory();


        public DataUploadHelper Create(string uploadTypeName, DataUploaderSetting uploaderSetting)
        {
            switch (uploadTypeName)
            {
                case "AVC":
                    var avc = new AvcDataUploadHelper();
                    if (uploaderSetting != null)
                    {
                        avc.SetDataProp("IPAddr", uploaderSetting.AVC_FTP);
                        avc.SetDataProp("IPAddr", uploaderSetting.AVC_Part_ID);
                        avc.SetDataProp("IPAddr", uploaderSetting.AVC_Machine_ID);
                        avc.SetDataProp("IPAddr", uploaderSetting.AVC_Operator_ID);
                    }

                    return avc;
                case "CM":
                    var cm = new CMDataUploadHelper();
                    if (uploaderSetting != null)
                    {
                        cm.SetDataProp("CM", uploaderSetting.CM_Setting);
                    }

                    return cm;
                default:
                    return null;
            }
        }
    }
}