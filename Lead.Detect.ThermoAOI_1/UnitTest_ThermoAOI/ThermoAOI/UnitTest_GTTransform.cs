using System;
using Lead.Detect.ThermoAOI.Calibration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lead.Detect.UnitTest1.ThermoAOI
{

    [TestClass]
    public class UnitTest_GTTransform
    {


        [TestMethod]
        public void Test_GT()
        {
            //gt1work, gt1raw, gt1calib, gt1calibraw
            Console.WriteLine("GT1 Transform:");
            var gt = new Tuple<double, double, double, double>(100, 1.1, 105, 1.1d);


            var newgt = GTTransform.TransGT(gt.Item1, gt.Item2, gt.Item3, gt.Item4);
            Console.WriteLine($"{gt}:{newgt:F3}");


            var newgt2 = GTTransform.TransGT2ToGT1(gt.Item1, gt.Item2, gt.Item3, gt.Item4);
            Console.WriteLine($"{gt}:{newgt2:F3}");

        }


        [TestMethod]
        public void Test_GT2GT1()
        {
            Console.WriteLine("Same Direction:");
            {
                var gt = new Tuple<double, double, double, double, double, double>(100, 1.1, 105, 1.1d, 800, 2.2);

                var newgt = GTTransform.TransGT2ToGT1(gt.Item1, gt.Item2, gt.Item3, gt.Item4, 0, true);
                Console.WriteLine($"{gt}:{newgt:F3}");
            }
            {
                var gt = new Tuple<double, double, double, double, double, double>(105, 5.1, 105, 1.1d, 800, 2.2);
                var newgt = GTTransform.TransGT2ToGT1(gt.Item1, gt.Item2, gt.Item3, gt.Item4, 0, true);
                Console.WriteLine($"{gt}:{newgt:F3}");
            }

            Console.WriteLine("Inverse Direction:");
            {
                //gt2work, gt2raw, gt2calib, gt2calibraw, gt1calib, gt1calibraw
                var gt = new Tuple<double, double, double, double, double, double>(100, 1.1, 105, 1.1d, 800, 2.2);

                var newgt1 = GTTransform.TransGT2ToGT1(gt.Item1, gt.Item2, gt.Item3, gt.Item4, 70, false);
                Console.WriteLine($"{gt.Item1:F2},{gt.Item2:F2},{gt.Item3:F2},{gt.Item4:F2},{gt.Item5:F2},{gt.Item6:F2}:{newgt1:F3}");

                Console.WriteLine("GT1 Transform:");
                var gt1 = new Tuple<double, double, double, double>(800, newgt1, 800, 2.2d);
                var newgt2 = GTTransform.TransGT2ToGT1(gt1.Item1, gt1.Item2, gt1.Item3, gt1.Item4, 0);
                Console.WriteLine($"{gt1}:{newgt2:F3}");
            }
        }
    }
}
