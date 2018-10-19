using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.FrameworkExtension.elementExtension
{
    public static class ElementExtension
    {

        public static Dictionary<string, IMotionWrapper> MotionWrappers = new Dictionary<string, IMotionWrapper>();


        public static IDiEx Cast(this EleDi ele)
        {
            if (MotionWrappers.ContainsKey(ele.Driver))
            {
                return new DiEx(ele, MotionWrappers[ele.Driver]);
            }
            else
            {
                MotionWrappers.Add(ele.Driver, new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(ele.Driver)));
                return new DiEx(ele, MotionWrappers[ele.Driver]);
            }
        }

        public static IDoEx Cast(this EleDo ele)
        {
            if (MotionWrappers.ContainsKey(ele.Driver))
            {
                return new DoEx(ele, MotionWrappers[ele.Driver]);
            }
            else
            {
                MotionWrappers.Add(ele.Driver, new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(ele.Driver)));
                return new DoEx(ele, MotionWrappers[ele.Driver]);
            }
        }


        public static ICylinderEx Cast(this EleCylinder ele)
        {
            if (MotionWrappers.ContainsKey(ele.Driver))
            {
                return new CylinderEx(ele, MotionWrappers[ele.Driver]);
            }
            else
            {
                MotionWrappers.Add(ele.Driver, new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(ele.Driver)));
                return new CylinderEx(ele, MotionWrappers[ele.Driver]);
            }
        }


        public static IVioEx Cast(this EleVio ele)
        {
            if (MotionWrappers.ContainsKey(ele.Driver))
            {
                return new VioEx(ele, MotionWrappers[ele.Driver]);
            }
            else
            {
                MotionWrappers.Add(ele.Driver, new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(ele.Driver)));
                return new VioEx(ele, MotionWrappers[ele.Driver]);
            }
        }

        public static IAxisEx Cast(this EleAxis ele)
        {
            if (MotionWrappers.ContainsKey(ele.Driver))
            {
                return new AxisEx(ele, MotionWrappers[ele.Driver]);
            }
            else
            {
                MotionWrappers.Add(ele.Driver, new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(ele.Driver)));
                return new AxisEx(ele, MotionWrappers[ele.Driver]);
            }
        }
    }
}