using System;

namespace Lead.Detect.FrameworkExtension.platforms.safeCheckObjects
{
    [Flags]
    public enum SafeCheckType
    {
        Always = 0X01,
        AutoHome = 0X02,
        Auto = 0X04,
        ManualHome = 0X08,
        Manual = 0X10,
    }
}