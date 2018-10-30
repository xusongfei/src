using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PosXYZU : IPlatformPos
    {


        public int Index { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double U { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
        public double OffsetU { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public int Flag1 { get; set; }
        public int Flag2 { get; set; }
        public int Flag3 { get; set; }

        public PosXYZU()
        {
        }

        public PosXYZU(double[] pos)
        {
            if (pos.Length == 1)
            {
                X = pos[0];
            }
            else if (pos.Length == 2)
            {
                X = pos[0];
                Y = pos[1];
            }
            else if (pos.Length == 3)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
            }
            else if (pos.Length == 4)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
            }
            else if (pos.Length == 8)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
                OffsetX = pos[4];
                OffsetY = pos[5];
                OffsetZ = pos[6];
                OffsetU = pos[7];
            }

        }

        public PosXYZU(double x, double y, double z, double u = 0)
        {
            X = x;
            Y = y;
            Z = z;
            U = u;
        }

        public static PosXYZU operator +(PosXYZU left, PosXYZU right)
        {
            return new PosXYZU()
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
                U = left.U + right.U,
            };
        }
        public static PosXYZU operator -(PosXYZU left, PosXYZU right)
        {
            return new PosXYZU()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
                Z = left.Z - right.Z,
                U = left.U - right.U,
            };
        }

        public static PosXYZU operator -(PosXYZU right)
        {
            return new PosXYZU()
            {
                X = -right.X,
                Y = -right.Y,
                Z = -right.Z,
                U = -right.U,
            };
        }

        public double[] Data()
        {
            return new[] { X, Y, Z, U, OffsetX, OffsetY, OffsetZ, OffsetU };
        }

        public void Update(double[] pos)
        {
            if (pos.Length == 1)
            {
                X = pos[0];
            }
            else if (pos.Length == 2)
            {
                X = pos[0];
                Y = pos[1];
            }
            else if (pos.Length == 3)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
            }
            else if (pos.Length == 4)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
            }
            else if (pos.Length == 8)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
                OffsetX = pos[4];
                OffsetY = pos[5];
                OffsetZ = pos[6];
                OffsetU = pos[7];
            }
        }
        public double DistanceTo(IPlatformPos pos)
        {
            if (pos is PosXYZU)
            {
                var oldPos = (PosXYZU)pos;
                var p = (this - oldPos);
                return Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z + p.U * p.U);
            }

            throw new Exception("DistanceTo Pos Type Error");
        }

        public override string ToString()
        {
            return $"{Index},{Name},{X:F3},{Y:F3},{Z:F3},{U:F3},{OffsetX:F3},{OffsetY:F3},{OffsetZ:F3},{OffsetU:F3},{Status},{Description},{Flag1},{Flag2},{Flag3}";
        }


        public static explicit operator PosXYZUVW(PosXYZU p)
        {
            return new PosXYZUVW(p.Data())
            {
                Index = p.Index,
                Name = p.Name,
                Description = p.Description,
                OffsetX = p.OffsetX,
                OffsetY = p.OffsetY,
                OffsetZ = p.OffsetZ,
                OffsetU = p.OffsetU,
                Flag1 = p.Flag1,
                Flag2 = p.Flag2,
                Flag3 = p.Flag3,
            };
        }


        public static PosXYZU Create(string posStr)
        {
            var data = posStr.Split(',');

            int i = 0;
            var p = new PosXYZU();
            p.Index = int.Parse(data[i++]);
            p.Name = data[i++];
            p.X = double.Parse(data[i++]);
            p.Y = double.Parse(data[i++]);
            p.Z = double.Parse(data[i++]);
            p.U = double.Parse(data[i++]);
            p.OffsetX = double.Parse(data[i++]);
            p.OffsetY = double.Parse(data[i++]);
            p.OffsetZ = double.Parse(data[i++]);
            p.OffsetU = double.Parse(data[i++]);
            p.Status = bool.Parse(data[i++]);
            p.Description = data[i++];
            p.Flag1 = int.Parse(data[i++]);
            p.Flag2 = int.Parse(data[i++]);
            p.Flag3 = int.Parse(data[i++]);

            return p;
        }
    }
}
