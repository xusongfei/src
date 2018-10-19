using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementExtensionInterfaces
{
    public interface IAxisEx : IElement
    {
        string Name { get; set; }
        EleAxisType Type { get; set; }
        string Description { get; set; }
        bool Enable { get; set; }

        string Driver { get; set; }
        IMotionWrapper DriverCard { get; }
        double AxisLead { get; set; }
        int AxisPlsPerRoll { get; set; }
        double AxisSpeed { get; set; }
        double AxisAcc { get; set; }
        int AxisChannel { get; set; }
        bool AxisChannelEnable { get; set; }


        int PosCheckDI { get; set; }
        bool PosCheckDIEnable { get; set; }
        int NegCheckDI { get; set; }
        bool NegCheckDIEnable { get; set; }
        int OriginCheckDI { get; set; }
        bool OriginCheckDIEnable { get; set; }



        int HomeOrder { get; set; }
        int HomeMode { get; set; }
        int HomeDir { get; set; }
        double HomeCurve { get; set; }
        double HomeAcc { get; set; }
        double HomeVm { get; set; }


        string Error { get; set; }


        double ToMm(int pls);
        int ToPls(double mm);
        string ToString();
    }
}