using System.ComponentModel;

namespace Lead.Detect.Element
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EleVio
    {
        public EleVio()
        {
            Name = "EleVio";
            Driver = "Driver";
            Description = "EleVio";
        }

        [ReadOnly(true)]
        public string Name { set; get; }
        [ReadOnly(true)]
        public EleVioType Type { set; get; }
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