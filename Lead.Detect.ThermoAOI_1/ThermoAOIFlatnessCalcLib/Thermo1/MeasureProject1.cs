using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOITrajectoryLib;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1
{
    public class MeasureProject1 : MeasureProject
    {




        [Category("测试"), Description("上工站测试点")]
        public List<PosXYZ> UpTestPositions { get; set; } = new List<PosXYZ>();

        [Category("测试"), Description("下工站测试点")]
        public List<PosXYZ> DownTestPositions { get; set; } = new List<PosXYZ>();



        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(base.ToString());


            sb.AppendLine("Up:");
            foreach (var p in UpTestPositions)
            {
                sb.AppendLine($"{p}");
            }

            sb.AppendLine("Down:");
            foreach (var p in DownTestPositions)
            {
                sb.AppendLine($"{p}");
            }
            return sb.ToString();
        }

    }



    public class MeasureProject1OrderOptimizeMethod
    {

        public List<PosXYZ> Optimize(List<PosXYZ> pos)
        {
            if (pos.Count > 20)
            {
                var ped1pos = pos.FindAll(p => p.Name == "ped1");
                var ped2pos = pos.FindAll(p => p.Name == "ped2");

                var orderpos = new List<PosXYZ>();
                orderpos.AddRange(pos);
                orderpos.RemoveAll(p => p.Name.StartsWith("ped"));
                orderpos.Add(ped1pos.Last());
                orderpos.Add(ped2pos.Last());

                var trajDp = new DpTsp(new PosGraph(orderpos));
                trajDp.RunTsp();

                var order = trajDp.Order;

                pos.Clear();
                for (int i = 0; i < orderpos.Count; i++)
                {
                    if (orderpos[order[i] - 1].Name == "ped1")
                    {
                        pos.AddRange(ped1pos);
                    }
                    else if (orderpos[order[i] - 1].Name == "ped2")
                    {
                        pos.AddRange(ped2pos);
                    }
                    else
                    {
                        pos.Add(orderpos[order[i] - 1]);
                    }
                }
            }
            else
            {
                var orderpos = new List<PosXYZ>();
                orderpos.AddRange(pos);
                var trajDp = new DpTsp(new PosGraph(orderpos));
                trajDp.RunTsp();

                var order = trajDp.Order;

                pos.Clear();
                for (int i = 0; i < orderpos.Count; i++)
                {

                    pos.Add(orderpos[order[i] - 1]);
                }
            }

            return pos;

        }
    }
}
