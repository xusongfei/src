using System;
using System.Collections.Generic;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.PlatformCalibration;

namespace Lead.Detect.FrameworkExtension.platforms
{
    public class XyzPlarformCalibration
    {

        public static Tuple<double[,], double> CalcAffineTransform(List<PosXYZ> platform1, List<PosXYZ> platform2)
        {
            var ret = RigidAlign.AffineAlign(
                platform1.Select(p => p.X).ToArray(),
                platform1.Select(p => p.Y).ToArray(),
                platform2.Select(p => p.X).ToArray(),
                platform2.Select(p => p.Y).ToArray()
            );

            return new Tuple<double[,], double>(ret.Item1, 0);
        }

        public static PosXYZ AffineTransform( PosXYZ pos, TransformParams trans)
        {
            return new PosXYZ(RigidAlign.AffineTransform(pos.Data(), trans.ToDoubles()));
        }

        public static PosXYZ AffineInverseTransform(TransformParams trans, PosXYZ pos)
        {
            return new PosXYZ(RigidAlign.AffineInverseTransform(pos.Data(), trans.ToDoubles()));
        }

    }
}