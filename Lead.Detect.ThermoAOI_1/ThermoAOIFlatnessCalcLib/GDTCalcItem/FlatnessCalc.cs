using System.Collections.Generic;
using System.Linq;
using g3;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.PlatformCalibration;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem
{
    /// <summary>
    /// 平面度计算
    /// </summary>
    public class FlatnessCalc : GDTCalc
    {
        public FlatnessCalc()
        {
            GDTType = GDTType.Flatness;

            IsDatum = true;
        }


        public override void DoCalc(List<PosXYZ> pos)
        {
            Datum = PlaneFitHelper.FitPlane(pos.Select(p => p.X).ToArray(), pos.Select(p => p.Y).ToArray(), pos.Select(p => p.Z).ToArray());

            Data = pos.Select(p => PlaneFitHelper.Point2Plane(Datum as OrthogonalPlaneFit3, p.Data())).ToList();

            Max = Data.Max();
            Min = Data.Min();


            Value = Max - Min;

            //标记平面度超差点
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i] > SpecMax / 2 || Data[i] < -SpecMax / 2)
                {
                    pos[i].Status = false;
                }
            }

            Success = true;
        }
    }
}
