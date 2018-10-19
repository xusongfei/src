using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementExtensionInterfaces
{
    public interface IVioEx : IElement
    {
        string Name { set; get; }
        EleVioType Type { set; get; }
        string Description { set; get; }
        bool Enable { set; get; }


        string Driver { set; get; }
        int Port { set; get; }


        IMotionWrapper DriverCard { get; }
    }
}