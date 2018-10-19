using System.ComponentModel;

namespace Lead.Detect.Element
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EleCylinder
    {
        public EleCylinder()
        {
            Name = "EleCylinder";
            Driver = "Driver";
            Description = "EleCylinder";
        }

        [ReadOnly(true)]
        public string Name { get; set; }

        [ReadOnly(true)]
        public EleDoType Type { get; set; }

        public string Description { get; set; }
        public string Driver { get; set; }

        public string Driver2 { get; set; }

        public bool Enable { get; set; }


        public int Delay { get; set; }


        [Category("Cylinder")]
        [ReadOnly(true)]
        public int DiOrg { get; set; }

        [Category("Cylinder")]
        public bool DiOrgEnable { get; set; }

        [Category("Cylinder")]
        [ReadOnly(true)]
        public int DiWork { get; set; }

        [Category("Cylinder")]
        public bool DiWorkEnable { get; set; }

        [Category("Cylinder")]
        [ReadOnly(true)]
        public int DoOrg { get; set; }

        [Category("Cylinder")]
        public bool DoOrgEnable { get; set; }

        [Category("Cylinder")]
        [ReadOnly(true)]
        public int DoWork { get; set; }

        [Category("Cylinder")]
        public bool DoWorkEnable { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}