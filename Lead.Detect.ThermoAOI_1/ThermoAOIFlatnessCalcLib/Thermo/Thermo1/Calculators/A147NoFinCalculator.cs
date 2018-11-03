using System.Collections.Generic;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators
{
    /// <summary>
    /// A147 730-00129 V10 VAPOR CHAMBER
    /// </summary>
    public class A147NoFinCalculator : A147WithFinCalculator
    {
        public A147NoFinCalculator()
        {
        }

        public new A147NoFinCalculator Create()
        {
            ProductName = "A147NoFin";

            GeoCalcs = new List<GDTCalc>()
            {
                //--------------------------------------------------------------- up
                //W - 10.5 (+-0.2) - vc up surface profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A147Geo.W.ToString(),
                        Description = "vc up surface profile to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "up",
                        ExpectValue = ExpectCalcValue.Min,
                    }
                },
                //X - 0.26 - parallelism to datum A
                {
                    new ParallelismCalc()
                    {
                        Name = A147Geo.X.ToString(),
                        Description = "parallelism to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "up",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },


                //--------------------------------------------------------------- down
                //Y - 0 (0.04) - pedestal1 flatness (datum A)
                {
                    new FlatnessCalc()
                    {
                        Name = A147Geo.Y.ToString(),
                        Description = "pedestal1 flatness (datum A)",
                        IsDatum = true,
                        SourcePos = "ped1",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },
                //T - 0 (0.10) - pedestal2 profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A147Geo.T.ToString(),
                        Description = "pedestal2 profile to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "ped2",
                        ExpectValue = ExpectCalcValue.Max,
                    }
                },
                //S - 0 (0.04) - pedestal2 parallelism to datum A
                {
                    new ParallelismCalc()
                    {
                        Name = A147Geo.S.ToString(),
                        Description = "pedestal2 parallelism to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "ped2",
                        ExpectValue = ExpectCalcValue.Normal,
                    }
                },


                //V - 6 (+-0.10) - vc down surface profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A147Geo.V.ToString(),
                        Description = "vc down surface profile to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "down",
                        ExpectValue = ExpectCalcValue.Min,
                    }
                },


                //AG - 0 (+-0.15) - inner standoff profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A147Geo.AG.ToString(),
                        Description = "inner standoff profile to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "inner",
                        ExpectValue = ExpectCalcValue.Max,
                    }
                },
                //AH - 5.33 (+-0.15) - outer standoff profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A147Geo.AH.ToString(),
                        Description = "outer standoff profile to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "outer",
                        ExpectValue = ExpectCalcValue.Max,
                    }
                },
            };

            return this;
        }
    }
}