using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOIProductLib.GDTCalcItem
{
    /// <summary>
    /// 位置度计算
    /// </summary>
    public class LocationOfPositionCalc : GDTCalc
    {
        public LocationOfPositionCalc()
        {
            GDTType = GDTType.Position;
        }


        public override void SetDatum(object datum)
        {
            if (datum is PosXYZ)
            {
                Datum = datum;
            }


        }

        public override void DoCalc(List<PosXYZ> pos)
        {
            if (Datum != null)
            {

            }

        }
    }
}
