using System.Collections.Generic;
using Lead.Detect.ThermoAOIProductLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1Calculator
{
    /// <summary>
    /// A117 730-00127 V13  VAPOR CHAMBER
    /// </summary>
    public class A117VcCalculator : A117FullModuleCalculator
    {
        public A117VcCalculator()
        {
        }


        public new A117VcCalculator Create()
        {
            ProductName = "A117VC";

            GeoCalcs = new List<GDTCalc>()
            {
                //calc h1
                //new FlatnessCalc()
                //{
                //    Name = "UP",
                //    Description = "up flatness (datum A)",
                //    IsDatum = true,
                //    SourcePos = "up",
                //      ExpectValue = ExpectCalcValue.Normal,
                //},
                //new ProfileOfSurfaceCalc()
                //{
                //    Name = "H1",
                //    Description = "vc up surface profile to datum A",
                //    DatumName = "UP",
                //    SourcePos = "ped1",
                //    ExpectValue = ExpectCalcValue.Max,
                //},


                //------------------------------------------------------------------- up
                // K - 10.5 (+-0.2) - vc up surface profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A117Geo.K.ToString(),
                        Description = "vc up surface profile to datum A",
                        DatumName = A117Geo.J.ToString(),
                        SourcePos = "up",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },
                // L - 0.26 - parallelism to datum A
                {
                    new ParallelismCalc()
                    {
                        Name = A117Geo.L.ToString(),
                        Description = "parallelism to datum A",
                        DatumName = A117Geo.J.ToString(),
                        SourcePos = "up",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },

                //------------------------------------------------------------------- down
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