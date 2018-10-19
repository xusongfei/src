using System;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    [Serializable]
    public class PosXYZUVW : IPlatformPos
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double U { get; set; }
        public double V { get; set; }
        public double W { get; set; }

        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
        public double OffsetU { get; set; }
        public double OffsetV { get; set; }
        public double OffsetW { get; set; }

        public bool Status { get; set; }

        public string Description { get; set; }
        public int Flag1 { get; set; }
        public int Flag2 { get; set; }
        public int Flag3 { get; set; }
        public PosXYZUVW()
        {
        }

        public PosXYZUVW(double[] pos)
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
            else if (pos.Length == 6)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
                V = pos[4];
                W = pos[5];
            }
        }

        public PosXYZUVW(double x, double y, double z, double u = 0, double v = 0, double w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            U = u;
            V = v;
            W = w;
        }


        public static PosXYZUVW operator +(PosXYZUVW left, PosXYZUVW right)
        {
            return new PosXYZUVW()
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
                U = left.U + right.U,
                V = left.V + right.V,
                W = left.W + right.W,
            };
        }

        public static PosXYZUVW operator -(PosXYZUVW left, PosXYZUVW right)
        {
            return new PosXYZUVW()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
                Z = left.Z - right.Z,
                U = left.U - right.U,
                V = left.V - right.V,
                W = left.W - right.W,
            };
        }

        public static PosXYZUVW operator -(PosXYZUVW right)
        {
            return new PosXYZUVW()
            {
                X = -right.X,
                Y = -right.Y,
                Z = -right.Z,
                U = -right.U,
                V = -right.V,
                W = -right.W,
            };
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
            else if (pos.Length == 6)//pos xyz
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
            }
            else if (pos.Length == 8)//posxyzu
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
            }
            else if (pos.Length == 12)//posxyzuvw
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
                V = pos[4];
                W = pos[5];
            }
        }

        public double[] Data()
        {
            return new[] { X, Y, Z, U, V, W, OffsetX, OffsetY, OffsetZ, OffsetU, OffsetV, OffsetW };
        }
        public double DistanceTo(IPlatformPos pos)
        {
            if (pos is PosXYZUVW)
            {
                var xyz = (PosXYZUVW)pos;
                var p = (this - xyz);
                return Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z + p.U * p.U + p.V * p.V + p.W * p.W);
            }

            throw new Exception("Pos Type Error");
        }

        public override string ToString()
        {
            return $"{Index},{Name},{X:F3},{Y:F3},{Z:F3},{U:F3},{V:F3},{W:F3},{OffsetX:F3},{OffsetY:F3},{OffsetZ:F3},{OffsetU:F3},{OffsetV:F3},{OffsetW:F3},{Status},{Description},{Flag1},{Flag2},{Flag3}";
        }


        public static explicit operator PosXYZ(PosXYZUVW p)
        {
            return new PosXYZ(p.Data())
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


        public static PosXYZUVW Create(string posStr)
        {
            var data = posStr.Split(',');

            int i = 0;
            var p = new PosXYZUVW();
            p.Index = int.Parse(data[i++]);
            p.Name = data[i++];
            p.X = double.Parse(data[i++]);
            p.Y = double.Parse(data[i++]);
            p.Z = double.Parse(data[i++]);
            p.U = double.Parse(data[i++]);
            p.V = double.Parse(data[i++]);
            p.W = double.Parse(data[i++]);
            p.OffsetX = double.Parse(data[i++]);
            p.OffsetY = double.Parse(data[i++]);
            p.OffsetZ = double.Parse(data[i++]);
            p.OffsetU = double.Parse(data[i++]);
            p.OffsetV = double.Parse(data[i++]);
            p.OffsetW = double.Parse(data[i++]);
            p.Status = bool.Parse(data[i++]);
            p.Description = data[i++];
            p.Flag1 = int.Parse(data[i++]);
            p.Flag2 = int.Parse(data[i++]);
            p.Flag3 = int.Parse(data[i++]);

            return p;
        }
    }
}