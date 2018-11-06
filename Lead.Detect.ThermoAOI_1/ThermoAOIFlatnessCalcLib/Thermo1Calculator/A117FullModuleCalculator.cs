using System.Collections.Generic;
using Lead.Detect.ThermoAOIProductLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1Calculator
{
    /// <summary>
    /// A117 730-00127 V13  FULL THERMAL MODULE
    /// </summary>
    public class A117FullModuleCalculator : Thermo1GeometryCalculator
    {
        public A117FullModuleCalculator()
        {
        }


        public A117FullModuleCalculator Create()
        {
            ProductName = "A117FullModule";


            GeoCalcs = new List<GDTCalc>()
            {
                // J - 0 (0.04) - pedestal flatness (datum A)
                {
                    new FlatnessCalc()
                    {
                        Name = A117Geo.J.ToString(),
                        Description = "pedestal flatness (datum A)",
                        IsDatum = true,
                        SourcePos = "ped1",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },
                // M - 6 (+-0.1) - vc down surface profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A117Geo.M.ToString(),
                        Description = "vc down surface profile to datum A",
                        DatumName = A117Geo.J.ToString(),
                        SourcePos = "down",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },

                //AB - 71.50(+-0.2) - fin up surface profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A117Geo.AB.ToString(),
                        Description = "fin up surface profile to datum A",
                        DatumName = A117Geo.J.ToString(),
                        SourcePos = "up",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },

                // O - 0 (+-0.15) - inner standoff profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A117Geo.O.ToString(),
                        Description = "inner standoff profile to datum A",
                        DatumName = A117Geo.J.ToString(),
                        SourcePos = "inner",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },
                // N - 5.33 (+-0.15) - outer standoff profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A117Geo.N.ToString(),
                        Description = "outer standoff profile to datum A",
                        DatumName = A117Geo.J.ToString(),
                        SourcePos = "outer",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },
            };

            return this;
        }
    }
}