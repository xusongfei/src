using System.Collections.Generic;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators
{
    /// <summary>
    /// A147 730-00129 V10 FULL THERMAL MODULE
    /// </summary>
    public class A147WithFinCalculator : A117WithFinCalculator
    {
        public A147WithFinCalculator()
        {
        }

        public new A147WithFinCalculator Create()
        {
            ProductName = "A147WithFin";


            GeoCalcs = new List<GDTCalc>()
            {
                //AW - 71.50(+-0.2) - fin up surface profile to datum A
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = A147Geo.AW.ToString(),
                        Description = "fin up surface profile to datum A",
                        DatumName = A147Geo.Y.ToString(),
                        SourcePos = "up",
                        ExpectValue = ExpectCalcValue.Min,
                    }
                },


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