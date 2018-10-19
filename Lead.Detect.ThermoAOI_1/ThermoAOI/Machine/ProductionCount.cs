using System.ComponentModel;

namespace Lead.Detect.ThermoAOI.Machine
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProductionCount
    {
        public int TotalCount { set; get; }

        public int OKCount { set; get; }
        public int NGCount { set; get; }

        public void Clear()
        {
            TotalCount = 0;
            OKCount = 0;
            NGCount = 0;
        }


        public string Display()
        {
            return $"ALL:{TotalCount} OK:{OKCount} NG:{NGCount}";
        }


        public override string ToString()
        {
            return $"Total:{TotalCount}\r\nOK:{OKCount}\r\nNG:{NGCount}\r\n";
        }
    }
}