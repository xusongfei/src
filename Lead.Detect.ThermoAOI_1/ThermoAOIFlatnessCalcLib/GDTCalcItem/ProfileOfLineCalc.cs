using System;
using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOIProductLib.GDTCalcItem
{
    /// <summary>
    /// 线轮廓度计算
    /// </summary>
    public class ProfileOfLineCalc : GDTCalc
    {
        public ProfileOfLineCalc()
        {
            GDTType = GDTType.ProfileOfLine;
        }
        public ProfileOfLineCalc(object line)
        {
            GDTType = GDTType.ProfileOfLine;
            Datum = line;
        }

        public override void SetDatum(object datum)
        {
            throw new NotImplementedException();
        }

        public override void DoCalc(List<PosXYZ> pos)
        {
            if (Datum == null)
            {
                throw new Exception($"{GDTType} {Name} 基准不存在");
            }

            throw new NotImplementedException();
            
        }
    }
}