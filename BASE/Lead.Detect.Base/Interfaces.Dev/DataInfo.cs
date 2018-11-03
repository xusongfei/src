namespace Lead.Detect.Interfaces.Dev
{
    public class DataInfo
    {
        public string DataName { get; set; }

        public DataType DataType { get; set; }

        public string DataSrc { get; set; }

        public string DataAddr { get; set; }

        public int DataUseState { get; set; }

        public object DataVal { get; set; }

        public TCType DataGetType { get; set; }

        public string DataGetInfo { get; set; }

        public DataRWProp DataRWProp { get; set; }

        public DataKeep DataKeep { get; set; }

        public bool DataUpdateFlag { get; set; } = false;
    }
}