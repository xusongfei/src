using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.elementExtension;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementExtensionInterfaces
{
    public interface ICylinderEx : IElement
    {
        string Name { set; get; }
        EleDoType Type { set; get; }
        string Description { set; get; }
        bool Enable { set; get; }
        int Delay { get; set; }


        /// <summary>
        /// Cylinder Di Driver
        /// </summary>
        MotionCardWrapper DriverCard { get; }

        /// <summary>
        /// Cylinder Do Driver
        /// </summary>
        MotionCardWrapper DriverCard2 { get; }

    
        string Driver { set; get; }
        int DiOrg { set; get; }
        int DiWork { set; get; }

        string Driver2 { set; get; }
        int DoOrg { set; get; }
        int DoWork { set; get; }


        IDiEx[] GetDiExs();
        IDoEx[] GetDoExs();
    }
}