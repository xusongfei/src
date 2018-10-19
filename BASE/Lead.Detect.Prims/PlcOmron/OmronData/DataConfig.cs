using System.Collections.Generic;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimPlcOmron.OmronData
{
    public class DataConfig
    {
        public string DtName { get; set; } //数据名称
        public string DtType { get; set; } //数据类型 :WORD,BOOL
        public OmronDataSection DtSection { get; set; } //数据区域: CIO,MR,D,WR

        public string DtAddress { get; set; } //数据地址

        //public string DtModifyValue { get; set; }//修改值
        // public string DtCurrentValue { get; set; }//实时值
        public DataReadMode DtRdMode { get; set; }
        public bool DtIsNotify { get; set; }
        public DataNotifyMode DtNotifyMode { set; get; }
    }

    public class DataConfigs
    {
        public List<DataConfig> ListDataConfig { set; get; }
    }
}