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
                        Description =  "ped flatness",
                        IsDatum = true,
                        SourcePos = "ped"
                    }
                },
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = X1311Geo.H.ToString(),
                        Description =  "standoff h",
                        DatumName = X1311Geo.D.ToString(),
                        SourcePos = "h",
                    }
                },
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = X1311Geo.I.ToString(),
                        Description =  "standoff i",
                        DatumName = X1311Geo.D.ToString(),
                        SourcePos = "i",

                    }
                },
                {
                    new ProfileOfSurfaceCalc()
                    {
                        Name = X1311Geo.FAI3.ToString(),
                        DatumName = X1311Geo.D.ToString(),
                        SourcePos = "up",
                    }
                },
            };
            return this;
        }
    }
}