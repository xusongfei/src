using System.Collections.Generic;
using Lead.Detect.ThermoAOIProductLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1Calculator
{
    public class X1311Calculator : Thermo1GeometryCalculator
    {
        public X1311Calculator()
        {
        }


        public Thermo1GeometryCalculator Create()
        {
            ProductName = "X1311FullModule";

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