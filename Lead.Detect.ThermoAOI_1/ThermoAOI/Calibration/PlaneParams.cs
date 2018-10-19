using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using g3;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOI.Calibration
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PlaneParams
    {
        public Vector3d Normal { get; set; }

        public Vector3d Origin { get; set; }


        public static PlaneParams FitPlane(List<PosXYZ> pos)
        {
            var plane = new OrthogonalPlaneFit3(pos.Select(p => new Vector3d(p.X, p.Y, p.Z)));
            var ret = new PlaneParams() { Normal = plane.Normal, Origin = plane.Origin };
            if (ret.Normal.z < 0)
            {
                ret.Normal = -ret.Normal;
            }
            return ret;
        }
        public double Point2Plane(PosXYZ pos)
        {
            var v1 = new Vector3d(pos.X, pos.Y, pos.Z);
            return (v1 - Origin).Dot(Normal);
        }

        public double CalcZ(double x, double y)
        {
            return -(((x - Origin.x) * Normal.x) + ((y - Origin.y) * Normal.y)) / Normal.z + Origin.z;
        }

        public override string ToString()
        {
            return $"{Normal.ToString()},{Origin.ToString()}";
        }
    }
}