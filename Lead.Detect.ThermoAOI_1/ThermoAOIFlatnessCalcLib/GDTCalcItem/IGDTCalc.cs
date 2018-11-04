using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOIProductLib.GDTCalcItem
{
    public interface IGDTCalc
    {
        GDTType GDTType { get; }

        bool IsDatum { get; }

        object Datum { get; }
        double Value { get; }
        double Max { get; }
        double Min { get; }
        bool Success { get; }

        List<double> Data { get; }

        void Clear();

        void SetDatum(object datum);

        void DoCalc(List<PosXYZ> pos);
    }
}