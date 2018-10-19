using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Lead.Detect.FrameworkExtension.platforms.safeCheckObjects
{
    public abstract class SafeCheckObject
    {
        public SafeCheckType Target { get; protected set; }
        public bool Enable { get; set; } = true;

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public virtual string Description { get; }

        public string Error { get; protected set; }
        public abstract bool Check(PlatformEx platform, int i);

        public override string ToString()
        {
            return $"{GetType().Name} {Target}";
        }
    }
}