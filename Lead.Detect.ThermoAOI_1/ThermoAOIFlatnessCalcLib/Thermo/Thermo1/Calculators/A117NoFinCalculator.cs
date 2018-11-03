using System.Collections.Generic;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators
{
    /// <summary>
    /// A117 730-00127 V13  VAPOR CHAMBER
    /// </summary>
    public class A117NoFinCalculator : A117WithFinCalculator
    {
        public A117NoFinCalculator()
        {
        }


        public new A117NoFinCalculator Create()
        {
            ProductName = "A117NoFin";

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
                        ExpectValue = ExpectCalcValue.Min,
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
                        ExpectValue = ExpectCalcValue.Min,
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
                        ExpectValue = ExpectCalcValue.Max,
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
                        ExpectValue = ExpectCalcValue.Max,
                    }
                },
            };

            return this;
        }
    }
}