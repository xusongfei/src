using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lead.Detect.ThermoAOIProductLib.Thermo1Calculator
{
    public class Thermo1CalculatorMgr
    {
        #region singleton

        private Thermo1CalculatorMgr()
        {
            Calculators.Add(new A117VcCalculator().Create());
            Calculators.Add(new A117FullModuleCalculator().Create());
            Calculators.Add(new A147VcCalculator().Create());
            Calculators.Add(new A147FullModuleCalculator().Create());
            Calculators.Add(new X1311Calculator().Create());
        }

        public static Thermo1CalculatorMgr Ins { get; } = new Thermo1CalculatorMgr();

        #endregion

        public List<Thermo1GeometryCalculator> Calculators { get; } = new List<Thermo1GeometryCalculator>();

        public Thermo1GeometryCalculator New(string product)
        {
            if (Calculators.Exists(c => c.ProductName == product))
            {
                return Calculators.First(c => c.ProductName == product);
            }
            else
            {
                return null;
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
                    var calc = Thermo1GeometryCalculator.Load(file);
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