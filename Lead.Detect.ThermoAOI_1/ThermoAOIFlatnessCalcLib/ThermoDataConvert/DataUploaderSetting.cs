using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Detect.ThermoAOIProductLib.ThermoDataConvert
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DataUploaderSetting
    {

        public bool Enable { get; set; } = false;
        public string UploaderName { get; set; }



        public string AVC_FTP { get; set; } = @"192.168.80.10\TestData";
        public string AVC_Part_ID { get; set; } = "117";
        public string AVC_Machine_ID { get; set; } = "AOI1";
        public string AVC_Operator_ID { get; set; } = "op1";



        public string CM_Setting { get; set; }
        public string CM_Machine_ID { get; set; }

    }
}
