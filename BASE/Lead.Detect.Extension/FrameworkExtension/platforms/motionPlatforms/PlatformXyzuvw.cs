using System;
using System.Collections.Generic;
using System.Linq;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    public class PlatformXyzuvw : PlatformEx
    {
        public PlatformXyzuvw()
        {
            Axis = new IAxisEx[6];
        }

        public PlatformXyzuvw(string name, IAxisEx[] axis, StationTask task, List<PosXYZUVW> positions) : base(name, axis, task, positions.Cast<IPlatformPos>().ToList())
        {
        }

        public PlatformXyzuvw(string name, IAxisEx[] axis, StationTask task, List<IPlatformPos> positions) : base(name, axis, task, positions)
        {
        }
        public override Type PosType
        {
            get { return typeof(PosXYZUVW); }
        }

        public override void TeachPos(string name)
        {
            var curpos = new PosXYZUVW(CurPos);

            if (Positions.Exists(p => p.Name == name))
            {
                var teachpos = Positions.First(pos => pos.Name == name);
                teachpos.Update(curpos.Data());
            }
            else
            {
                curpos.Name = name;
                Positions.Add(curpos);
            }
        }

    }
}