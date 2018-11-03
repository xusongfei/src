using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using g3;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.PlatformCalibration.FittingHelper
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LineParams
    {
        public Vector3d Direction { get; set; }

        public Vector3d Origin { get; set; }


        public double OX { get { return Origin.x; } }

        public double OY { get { return Origin.y; } }


        public double Distance(PosXYZ pos)
        {
            var offset = new Vector3d(pos.X, pos.Y, pos.Z) - Origin;

            return offset.Cross(Direction).Length;
        }



        public static LineParams FitLine(List<PosXYZ> pos)
        {
            var points = pos.Select(p => new Vector3d(p.X, p.Y, p.Z));

            var Origin = Vector3d.Zero;
            int num1 = 0;
            foreach (Vector3d point in points)
            {
                Origin += point;
                ++num1;
            }
            double num2 = 1.0 / num1;
            Origin *= num2;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            foreach (Vector3d point in points)
            {
                Vector3d vector3d = point - Origin;
                num3 += vector3d[0] * vector3d[0];

                num4 += vector3d[0] * vector3d[1];
                num5 += vector3d[0] * vector3d[2];
                num6 += vector3d[1] * vector3d[1];
                num7 += vector3d[1] * vector3d[2];
                num8 += vector3d[2] * vector3d[2];
            }
            double num9 = num3 * num2;
            double num10 = num4 * num2;
            double num11 = num5 * num2;
            double num12 = num6 * num2;
            double num13 = num7 * num2;
            double num14 = num8 * num2;
            double[] input = new double[9]
            {
                num9, num10, num11,
                num10, num12, num13,
                num11, num13, num14
            };
            SymmetricEigenSolver symmetricEigenSolver = new SymmetricEigenSolver(3, 4096);
            int num15 = symmetricEigenSolver.Solve(input, SymmetricEigenSolver.SortType.Decreasing);
            var ResultValid = num15 > 0 && num15 < int.MaxValue;
            var Normal = new Vector3d(symmetricEigenSolver.GetEigenvector(0));

            return new LineParams() { Direction = Normal, Origin = Origin };
        }
    }
}