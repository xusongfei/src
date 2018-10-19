using System;
using System.ComponentModel;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PosXYZ : IPlatformPos
    {
        public static readonly PosXYZ Zero = new PosXYZ();

        public int Index { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public int Flag1 { get; set; }
        public int Flag2 { get; set; }
        public int Flag3 { get; set; }

        public PosXYZ()
        {
        }

        public PosXYZ(double[] pos)
        {
            if (pos == null)
            {
                return;
            }
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
                OffsetX = pos[3];
            }
            else if (pos.Length == 5)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                OffsetX = pos[3];
                OffsetY = pos[4];
            }
            else if (pos.Length == 6)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                OffsetX = pos[3];
                OffsetY = pos[4];
                OffsetZ = pos[5];
            }
        }

        public PosXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static PosXYZ operator +(PosXYZ left, PosXYZ right)
        {
            return new PosXYZ()
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
            };
        }
        public static PosXYZ operator -(PosXYZ left, PosXYZ right)
        {
            return new PosXYZ()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
                Z = left.Z - right.Z,
            };
        }

        public static PosXYZ operator -(PosXYZ right)
        {
            return new PosXYZ()
            {
                X = -right.X,
                Y = -right.Y,
                Z = -right.Z,
            };
        }

        public double[] Data()
        {
            return new[] { X, Y, Z, OffsetX, OffsetY, OffsetZ };
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
                OffsetX = pos[3];
            }
            else if (pos.Length == 5)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                OffsetX = pos[3];
                OffsetY = pos[4];
            }
            else if (pos.Length == 6)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                OffsetX = pos[3];
                OffsetY = pos[4];
                OffsetZ = pos[5];
            }
        }
        public double DistanceTo(IPlatformPos pos)
        {
            if (pos is PosXYZ)
            {
                var xyz = (PosXYZ)pos;
                var p = (this - xyz);
                return Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
            }

            throw new Exception("DistanceTo Pos Type Error");
        }

        public override string ToString()
        {
            return $"{Index},{Name},{X:F3},{Y:F3},{Z:F3},{OffsetX:F3},{OffsetY:F3},{OffsetZ:F3},{Status},{Description},{Flag1},{Flag2},{Flag3}";
        }


        public static explicit operator PosXYZUVW(PosXYZ p)
        {
            return new PosXYZUVW(p.Data())
            {
                Index = p.Index,
                Name = p.Name,
                Description = p.Description,
                OffsetX = p.OffsetX,
                OffsetY = p.OffsetY,
                OffsetZ = p.OffsetZ,
                Flag1 = p.Flag1,
                Flag2 = p.Flag2,
                Flag3 = p.Flag3,
            };
        }


        public static PosXYZ Create(string posStr)
        {
            var data = posStr.Split(',');

            int i = 0;
            var p = new PosXYZ();
            p.Index = int.Parse(data[i++]);
            p.Name = data[i++];
            p.X = double.Parse(data[i++]);
            p.Y = double.Parse(data[i++]);
            p.Z = double.Parse(data[i++]);
            p.OffsetX = double.Parse(data[i++]);
            p.OffsetY = double.Parse(data[i++]);
            p.OffsetZ = double.Parse(data[i++]);
            p.Status = bool.Parse(data[i++]);
            p.Description = data[i++];
            p.Flag1 = int.Parse(data[i++]);
            p.Flag2 = int.Parse(data[i++]);
            p.Flag3 = int.Parse(data[i++]);

            return p;
        }
    }
}