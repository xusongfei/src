using System.ComponentModel;

namespace Lead.Detect.Element
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EleDo
    {
        public EleDo()
        {
            Name = "EleDo";
            Driver = "Driver";
            Description = "EleDo";
        }

        [ReadOnly(true)]
        public string Name { set; get; }
        [ReadOnly(true)]
        public EleDoType Type { set; get; }
        public string Description { set; get; }
        public string Driver { set; get; }
        public bool Enable { set; get; }
        [ReadOnly(true)]
        public int Port { set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}