using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementExtensionInterfaces
{
    public interface IDoEx : IElement
    {
        string Name { set; get; }
        EleDoType Type { set; get; }
        string Description { set; get; }


        string Driver { set; get; }

        int Port { set; get; }


        IMotionWrapper DriverCard { get; }
    }
}