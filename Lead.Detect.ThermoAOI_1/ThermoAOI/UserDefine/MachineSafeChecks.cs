using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.platforms.safeCheckObjects;

namespace Lead.Detect.ThermoAOI.Machine1.UserDefine
{
    public class CarrierSafeCheck : SafeCheckObject
    {
        public PlatformEx Up { get; }
        public PlatformEx Down { get; }
        public ICylinderEx DownGT { get; }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Description
        {
            get { return @"CarrierSafeCheck"; }
        }


        public CarrierSafeCheck(PlatformEx up, PlatformEx down, ICylinderEx downGT, SafeCheckType target)
        {
            Target = target;
            Up = up;
            Down = down;
            DownGT = downGT;
        }

        public override bool Check(PlatformEx platform, int i)
        {
            switch (Target)
            {
                case SafeCheckType.AutoHome:
                case SafeCheckType.Auto:
                    if (!Up.LocateInPos("Wait", 2))
                    {
                        Error = $"CarrierSafeCheck {Up.Name} 不在等待位置";
                        return false;
                    }
                    else if (!Down.LocateInPos("Wait", 2))
                    {
                        Error = $"CarrierSafeCheck {Down.Name} 不在等待位置";
                        return false;
                    }

                    return true;
                case SafeCheckType.ManualHome:
                case SafeCheckType.Manual:
                    if (!Up.LocateInPos("Wait", 2))
                    {
                        Error = $"CarrierSafeCheck {Up.Name} Z 不在等待位置";
                        return false;
                    }
                    else if (!Down.LocateInPos("Wait", 2))
                    {
                        Error = $"CarrierSafeCheck {Down.Name} 不在等待位置";
                        return false;
                    }

                    return true;

                default:
                    return true;
            }
        }
    }


    public class UpSafeCheck : SafeCheckObject
    {
        public PlatformEx Carrier { get; }

        public UpSafeCheck(PlatformEx carrier, SafeCheckType target)
        {
            Target = target;
            Carrier = carrier;
        }

        public override bool Check(PlatformEx up, int i)
        {
            switch (Target)
            {
                case SafeCheckType.ManualHome:
                case SafeCheckType.Manual:
                    if (i < 2)
                    {
                        if (Carrier.LocateInPos("Wait"))
                        {
                            return true;
                        }
                        else if (Carrier.LocateInPos("Work"))
                        {
                            return true;
                        }
                        else
                        {
                            if (!up.LocateInPos("Wait", 2))
                            {
                                Error = $"UpSafeCheck {up.Name} Z 不在等待位";
                                return false;
                            }
                            return true;
                        }
                    }
                    return true;
                default:
                    return true;
            }
        }
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Description
        {
            get
            {
                return @"UpSafeCheck";

            }
        }
    }

    public class DownSafeCheck : SafeCheckObject
    {
        public PlatformEx Carrier { get; }
        public ICylinderEx DownGt { get; }

        public DownSafeCheck(PlatformEx carrier, ICylinderEx downGt, SafeCheckType target)
        {
            Carrier = carrier;
            DownGt = downGt;
            Target = target;
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Description
        {
            get
            {
                return @"DownSafeCheck";
            }
        }

        public override bool Check(PlatformEx down, int i)
        {
            switch (Target)
            {
                case SafeCheckType.ManualHome:
                case SafeCheckType.Manual:
                    if (Carrier.LocateInPos("Wait"))
                    {
                        return true;
                    }
                    else if (Carrier.LocateInPos("Work"))
                    {
                        if (i < 2)
                        {
                            if (!down.LocateInPos("Wait", 2))
                            {
                                Error = $"DownSafeCheck {down.Name} Z 不在等待位";
                                return false;
                            }
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        if (i < 2)
                        {
                            if (!down.LocateInPos("Wait", 2))
                            {
                                Error = $"DownSafeCheck {down.Name} Z 不在等待位";
                                return false;
                            }
                        }
                        return true;
                    }
                default:
                    return true;
            }
        }
    }
}