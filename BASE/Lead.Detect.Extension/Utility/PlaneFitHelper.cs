using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using g3;
using MathNet.Numerics;

namespace Lead.Detect.PlatformCalibration
{
    public class PlaneFitHelper
    {
        public static OrthogonalPlaneFit3 FitPlane(double[] x, double[] y, double[] z)
        {
            List<Vector3d> pos = new List<Vector3d>();

            for (int i = 0; i < x.Length; i++)
            {
                pos.Add(new Vector3d(x[i], y[i], z[i]));
            }

            var fit = new OrthogonalPlaneFit3(pos);

            if (fit.Normal.z < 0)
            {
                fit.Normal = -fit.Normal;
            }
            return fit;
        }


        public static double Point2Plane(OrthogonalPlaneFit3 plane, double[] pos)
        {
            var v1 = new Vector3d(pos[0], pos[1], pos[2]);
            return (v1 - plane.Origin).Dot(plane.Normal);
        }



        public static double CalcZ(OrthogonalPlaneFit3 plane, double x, double y)
        {
            return -(((x - plane.Origin.x) * plane.Normal.x) + ((y - plane.Origin.y) * plane.Normal.y)) / plane.Normal.z + plane.Origin.z;
        }

        public static double CalcZ(Vector3d normal, Vector3d origin, double x, double y)
        {
            return -(((x - origin.x) * normal.x) + ((y - origin.y) * normal.y)) / normal.z + origin.z;
        }

    }
}
