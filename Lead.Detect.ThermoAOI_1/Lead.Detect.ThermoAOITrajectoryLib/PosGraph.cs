using System;
using System.Collections.Generic;
using System.Text;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOITrajectoryLib
{
    public class PosGraph
    {
        public double XVel { get; set; } = 200;
        public double YVel { get; set; } = 200;
        public double ZVel { get; set; } = 60;

        public double[,] Graph;

        public List<PosXYZ> pos;


        public PosGraph(List<PosXYZ> positions)
        {
            pos = new List<PosXYZ>();
            pos.Add(new PosXYZ(0, 0, 0) {Name = "wait"});
            pos.AddRange(positions);
      

            Graph = new double[pos.Count, pos.Count];


            for (int i = 0; i < pos.Count; i++)
            {
                for (int j = 0; j < pos.Count; j++)
                {
                    if (pos[i].Name == "outer" || pos[j].Name == "outer")
                    {
                        Graph[i, j] = GetEdge(pos[i], pos[j], 38);
                    }
                    else
                    {
                        Graph[i, j] = GetEdge(pos[i], pos[j], 8);
                    }
                }
            }
        }

        public double GetEdge(PosXYZ p1, PosXYZ p2, double jh)
        {
            var x = Math.Abs(p2.X - p1.X) / XVel;
            var y = Math.Abs(p2.Y - p1.Y) / YVel;
            if (x < float.Epsilon && y < float.Epsilon)
            {
                return 0;
            }


            var z = Math.Abs(p2.Z - p1.Z) + 2 * jh;
            z = z / ZVel;

            return x + y + z;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("--------------------------");
            for (int j = 0; j < Graph.GetLength(1); j++)
            {
                sb.Append(pos[j].Name + ",");
            }

            sb.Append("\n");

            for (int i = 0; i < Graph.GetLength(0); i++)
            {
                sb.Append(pos[i].Name + ",");
                for (int j = 0; j < Graph.GetLength(1); j++)
                {
                    sb.Append(Graph[i, j].ToString("F2") + ", ");
                }

                sb.Append("\n");
            }

            sb.AppendLine("--------------------------");
            return sb.ToString();
        }
    }
}