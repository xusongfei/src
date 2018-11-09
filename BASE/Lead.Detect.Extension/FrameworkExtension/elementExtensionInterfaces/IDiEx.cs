using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementExtensionInterfaces
{
    public interface IDiEx : IElement
    {
      
        string Name { set; get; }
        EleDiType Type { set; get; }
        string Description { set; get; }
        bool Enable { set; get; }


        string Driver { set; get; }
        int Port { set; get; }


        MotionCardWrapper DriverCard { get; }
    }
}