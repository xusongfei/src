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
        public IAxisEx AX => Axis[0];
        public IAxisEx AY => Axis[1];
        public IAxisEx AZ => Axis[2];
        public IAxisEx AU => Axis[3];

        public AxisLimit LimitX { get; set; }
        public AxisLimit LimitY { get; set; }
        public AxisLimit LimitZ { get; set; }
        public AxisLimit LimitU { get; set; }

        public PlatformXyzu()
        {
            Axis = new IAxisEx[3];

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
