using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.FrameworkExtension
{
    public class MotionRecorderHelper
    {

        private Stopwatch sw = new Stopwatch();
        private List<PosXYZ> MoveLength = new List<PosXYZ>();

        public void InitRecord()
        {
            MoveLength.Clear();
            sw.Stop();
            sw.Reset();
        }

        public void RecordMoveStart(PosXYZ p)
        {
            sw.Restart();
            MoveLength.Add(p);
        }

        public void RecordMoveFinish(PosXYZ p)
        {
            sw.Stop();
            var last = MoveLength.Last();
            last.X = Math.Abs(p.X - last.X);
            last.Y = Math.Abs(p.Y - last.Y);
            last.Z = Math.Abs(p.Z - last.Z);
            last.OffsetX = sw.ElapsedMilliseconds;
        }

        public string DisplatMoveDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine("MoveDetails:\n-----------------------------------------------");
            for (int i = 0; i < MoveLength.Count; i++)
            {
                sb.AppendLine($" {i} {MoveLength[i].ToString()}");
            }
            sb.AppendLine("MoveDetails:\n-----------------------------------------------");
            sb.AppendLine($"X:{MoveLength.Select(m => m.X).Sum():F2} Y:{MoveLength.Select(m => m.Y).Sum():F2} Z:{MoveLength.Select(m => m.Z).Sum():F2} TIME:{MoveLength.Select(m => m.OffsetX).Sum():F2}");
            sb.AppendLine("MoveDetails:\n-----------------------------------------------");

            return sb.ToString();
        }
    }
}
