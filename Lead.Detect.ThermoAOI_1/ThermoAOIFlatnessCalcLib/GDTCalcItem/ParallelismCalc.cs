using System;
using System.Collections.Generic;
using System.Linq;
using g3;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.PlatformCalibration;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem
{
    /// <summary>
    /// 平行度计算
    /// </summary>
    public class ParallelismCalc : GDTCalc
    {
        public ParallelismCalc()
        {
            GDTType = GDTType.Parallelism;
        }

        public ParallelismCalc(OrthogonalPlaneFit3 datum)
        {
            Datum = datum;
            GDTType = GDTType.Parallelism;
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

            //todo test new parallelism calc
            //convert pos to pos on fit plane
            var plane = PlaneFitHelper.FitPlane(pos.Select(p => p.X).ToArray(), pos.Select(p => p.Y).ToArray(), pos.Select(p => p.Z).ToArray());
            var newPos = new List<PosXYZ>();
            foreach (var p in pos)
            {
                var z = PlaneFitHelper.CalcZ(plane, p.X, p.Y);
                newPos.Add(new PosXYZ(p.X, p.Y, z));
            }

            //calc plane pos to datum
            Data = newPos.Select(p => PlaneFitHelper.Point2Plane(Datum as OrthogonalPlaneFit3, p.Data())).ToList();
            Max = Data.Max();
            Min = Data.Min();

            Value = Max - Min;


            //标记平行度超差点
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