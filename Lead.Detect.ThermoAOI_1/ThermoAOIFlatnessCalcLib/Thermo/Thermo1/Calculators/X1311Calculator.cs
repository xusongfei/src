using System.Collections.Generic;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalculator;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators
{
    public class X1311Calculator : GeometryCalculator
    {
        public X1311Calculator()
        {
        }


        public GeometryCalculator Create()
        {
            ProductName = "X1311WithFin";

            GeoCalcs = new List<GDTCalc>()
            {
                {
                    new FlatnessCalc()
                    {
                        Name = X1311Geo.D.ToString(),
                        IsDatum = true
                    }
                },
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = X1311Geo.H.ToString(),
                        DatumName = X1311Geo.D.ToString()
                    }
                },
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = X1311Geo.I.ToString(),
                        DatumName = X1311Geo.D.ToString()
                    }
                },
            };
            return this;
        }
    }
}