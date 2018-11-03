using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalculator;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1
{
    public class CalculatorMgr
    {
        #region singleton

        private CalculatorMgr()
        {
            Calculators.Add(new A117NoFinCalculator().Create());
            Calculators.Add(new A117WithFinCalculator().Create());
            Calculators.Add(new A147NoFinCalculator().Create());
            Calculators.Add(new A147WithFinCalculator().Create());
            Calculators.Add(new X1311Calculator().Create());
        }

        public static CalculatorMgr Ins { get; } = new CalculatorMgr();

        #endregion

        public List<GeometryCalculator> Calculators { get; } = new List<GeometryCalculator>();

        public GeometryCalculator New(string product)
        {
            if (Calculators.Exists(c => c.ProductName == product))
            {
                return Calculators.First(c => c.ProductName == product);
            }
            else
            {
                throw new NotImplementedException(product);
            }
        }


        public void Import(string dir = @".\Config\Calculators")
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                try
                {
                    var calc = GeometryCalculator.Load(file);
                    if (Calculators.Exists(c => c.ProductName == calc.ProductName))
                    {
                        continue;
                    }

                    Calculators.Add(calc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Load Calculators Error:{ex.Message}");
                }
            }
        }

        public void Export(string dir = @".\Config\Calculators")
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            foreach (var calc in Calculators)
            {
                calc.SaveAs(Path.Combine(@".\Config\Calculators", $"{calc.ProductName}.calc"));
            }
        }
    }
}