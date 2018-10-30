using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    public class PlatformXyzu : PlatformEx
    {
        public PlatformXyzu()
        {
            Axis = new IAxisEx[4];

        }

        public PlatformXyzu(string name, IAxisEx[] axis, StationTask task, List<PosXYZ> positions) : base(name, axis, task, positions.Cast<IPlatformPos>().ToList())
        {

        }
        public PlatformXyzu(string name, IAxisEx[] axis, StationTask task, List<IPlatformPos> positions) : base(name, axis, task, positions)
        {

        }
        public override Type PosType
        {
            get { return typeof(PosXYZU); }
        }

        public override void TeachPos(string name)
        {
            var curpos = new PosXYZU(CurPos);

            if (Positions.Exists(p => p.Name == name))
            {
                var teachpos = Positions.FirstOrDefault(p => p.Name == name);
                teachpos?.Update(curpos.Data());
            }
            else
            {
                curpos.Name = name;

                Positions.Add(curpos);
            }
        }
    }
}
