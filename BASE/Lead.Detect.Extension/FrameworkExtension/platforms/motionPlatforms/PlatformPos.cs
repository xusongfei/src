using System;
using System.Xml.Serialization;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    public interface IPlatformPos
    {
        int Index { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        double[] Data();
        double DistanceTo(IPlatformPos pos);
        void Update(double[] pos);
    }


    public class PlatformPos : IPlatformPos
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }
        public bool Status { get; set; }

        public string Description { get; set; }
        public int Flag1 { get; set; }
        public int Flag2 { get; set; }
        public int Flag3 { get; set; }
        public double[] Data()
        {
            throw new NotImplementedException();
        }

        public double DistanceTo(IPlatformPos pos)
        {
            throw new NotImplementedException();
        }

        public void Update(double[] pos)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Index},{Name},{X:F3},{Y:F3},{Z:F3},{OffsetX:F3},{OffsetY:F3},{OffsetZ:F3},{Status},{Description},{Flag1},{Flag2},{Flag3}";
        }


        public static explicit operator PosXYZ(PlatformPos p)
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
    }
}