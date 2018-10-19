using System.ComponentModel;

namespace Lead.Detect.Element
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EleDi
    {
        public EleDi()
        {
            Name = "EleDi";
            Driver = "Driver";
            Description = "EleDi";
        }
        [ReadOnly(true)]
        public string Name { set; get; }
        [ReadOnly(true)]
        public EleDiType Type { set; get; }
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