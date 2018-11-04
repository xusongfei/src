using System;
using System.Collections.Generic;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.Utility.Transformation
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

            return new Tuple<double[,], double>(ret.Item1, ret.Item2);
        }

        public static PosXYZ AffineTransform(PosXYZ pos, TransformParams trans)
        {
            return new PosXYZ(RigidAlign.AffineTransform(pos.Data(), trans.ToDoubles())) { Z = pos.Z };
        }

        public static PosXYZ AffineInverseTransform(TransformParams trans, PosXYZ pos)
        {
            return new PosXYZ(RigidAlign.AffineInverseTransform(pos.Data(), trans.ToDoubles())) { Z = pos.Z };
        }



        public static Tuple<double[,], double> CalcAlignTransform(List<PosXYZ> platform1, List<PosXYZ> platform2)
        {
            var ret = RigidAlign.Align(
                platform1.Select(p => p.X).ToArray(),
                platform1.Select(p => p.Y).ToArray(),
                platform2.Select(p => p.X).ToArray(),
                platform2.Select(p => p.Y).ToArray()
            );

            return new Tuple<double[,], double>(ret.Item1, ret.Item2);
        }

    }
}