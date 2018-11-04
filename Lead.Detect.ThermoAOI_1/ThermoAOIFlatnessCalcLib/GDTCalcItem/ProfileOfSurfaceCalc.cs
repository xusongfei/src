using System;
using System.Collections.Generic;
using System.Linq;
using g3;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.Utility.FittingHelper;

namespace Lead.Detect.ThermoAOIProductLib.GDTCalcItem
{
    /// <summary>
    /// 面轮廓度计算
    /// </summary>
    public class ProfileOfSurfaceCalc : GDTCalc
    {
        public ProfileOfSurfaceCalc()
        {
            GDTType = GDTType.ProfileOfSurface;
        }
        public ProfileOfSurfaceCalc(OrthogonalPlaneFit3 datumPlane)
        {
            GDTType = GDTType.ProfileOfSurface;
            Datum = datumPlane;
        }

        public override void SetDatum(object datum)
        {
            if (datum is OrthogonalPlaneFit3)
            {
                Datum = datum;
            }
            else
            {
                throw new Exception($"{GDTType} {Name} 基准不存在");
            }
        }

        public override void DoCalc(List<PosXYZ> pos)
        {
            if (Datum == null)
            {
                throw new Exception($"{GDTType} {Name} 基准不存在");
            }

            Data = pos.Select(p => PlaneFitHelper.Point2Plane(Datum as OrthogonalPlaneFit3, p.Data())).ToList();
            Max = Data.Max();
            Min = Data.Min();

            Value = Math.Abs(Max - Spec) > Math.Abs(Min - Spec) ? Max : Min;

            //标记轮廓度超差点
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i] > SpecMax || Data[i] < SpecMin)
                {
                    pos[i].Status = false;
                }
            }

            Success = true;
        }
    }


}