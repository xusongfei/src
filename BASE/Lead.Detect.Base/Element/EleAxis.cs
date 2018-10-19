using System;
using System.ComponentModel;

namespace Lead.Detect.Element
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class EleAxis
    {
        public EleAxis()
        {
            Name = "Axis";
            Driver = "Driver";
            Description = "Axis";
        }

        [ReadOnly(true)]
        [Category("Axis")]
        public int Id { set; get; }
        [ReadOnly(true)]
        [Category("Axis")]
        public string Name { set; get; }
        [ReadOnly(true)]
        [Category("Axis")]
        public EleAxisType Type { set; get; }
        [Category("Axis")]
        public string Description { set; get; }
        [Category("Axis")]
        public bool Enable { set; get; }



        [ReadOnly(true)]
        [Category("Axis")]
        public double AxisLead { set; get; }
        [Category("Axis")]
        [ReadOnly(true)]
        public int AxisPlsPerRoll { get; set; }
        [Category("Axis")]
        public double AxisSpeed { set; get; }
        [Category("Axis")]
        [ReadOnly(true)]
        public double AxisAcc { get; set; }


        [ReadOnly(true)]
        [Category("Axis")]
        public string Driver { set; get; }
        [Category("Axis")]
        [ReadOnly(true)]
        public int AxisChannel { set; get; }
        [ReadOnly(true)]
        [Category("Axis")]
        public bool AxisChannelEnable { set; get; }



        [Category(("SOFT LIMIT"))]
        public int PosCheckDI { set; get; }

        [Category(("SOFT LIMIT"))]
        public bool PosCheckDIEnable { set; get; }

        [Category(("SOFT LIMIT"))]
        public int NegCheckDI { set; get; }

        [Category(("SOFT LIMIT"))]
        public bool NegCheckDIEnable { set; get; }

        [Category(("SOFT LIMIT"))]
        public int OriginCheckDI { set; get; }

        [Category(("SOFT LIMIT"))]
        public bool OriginCheckDIEnable { set; get; }



        [Category(("HOME"))]
        public int HomeMode { set; get; }

        [Category(("HOME"))]
        public int HomeDir { set; get; }

        [ReadOnly(true)]
        [Category(("HOME"))]
        public double HomeCurve { set; get; }

        [ReadOnly(true)]
        [Category(("HOME"))]
        public double HomeAcc { set; get; }

        [Category(("HOME"))]
        public double HomeVm { set; get; }

        [Category(("HOME"))]
        public int HomeOrder { get; set; }

        [ReadOnly(true)]
        public string Error { get; set; }



        public double ToMm(int pls)
        {
            return ((float)pls / AxisPlsPerRoll) * AxisLead;
        }
        public int ToPls(double mm)
        {
            return Convert.ToInt32(mm / AxisLead * AxisPlsPerRoll);
        }



        public override string ToString()
        {
            return Name;
        }
    }
}