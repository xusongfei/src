using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.Utility.FittingHelper;

namespace Lead.Detect.UnitTest1
{
    [TestClass]
    public class UnitTest_Base
    {
        [TestMethod]
        public void Basic()
        {
            Console.WriteLine(Convert.ToInt32(1.4));
            Console.WriteLine(Convert.ToInt32(1.5));

            Console.WriteLine(new List<PosXYZ>() {new PosXYZ(1, 2, 3d)}.Max(p => p.Z).ToString("F3"));
        }



        [TestMethod]
        public void TestMethod_FitPlane()
        {
            double[] x = new[] { 1, 0d, -1 };
            double[] y = new[] { 1, 0d, 1 };
            double[] z = new[] { 0, 0d, 0 };


            var plane = PlaneFitHelper.FitPlane(x, y, z);

            Console.WriteLine($"Normal : {plane.Normal.ToString()}");
            Console.WriteLine($"Origin : {plane.Origin.ToString()}");

            Console.WriteLine(PlaneFitHelper.Point2Plane(plane, new[] { 1, 1, 1d }));
        }

        [TestMethod]
        public void TestMethod_Linq()
        {
            var spec = 1.1;

            List<double> data = new List<double>()
            {
                2.1,3.1,4.1,0, -1,-2
            };


            Console.WriteLine($"{data.Max(d => Math.Abs(d - spec)):F3}");

        }


        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine(Math.Truncate(0.111111111111111111111));



            //Dictionary<int, bool> sts = new Dictionary<int, bool>();

            //Console.WriteLine(sts.Any(s => s.Value));
            //Console.WriteLine(sts.Any(s => !s.Value));
            //Console.WriteLine(!sts.Any(s => s.Value));

            //Console.WriteLine(sts.All(s => s.Value));
            //Console.WriteLine(sts.All(s => !s.Value));
            //Console.WriteLine(!sts.All(s => s.Value));

            //MachineSettings.Ins = new MachineSettings();

            //JsonHelper.Save(MachineSettings.Ins, "test.json");
            //MachineSettings.Ins.Save();
        }


        [TestMethod]
        public void TestMethodCast()
        {
            Assembly assembly = Assembly.LoadFrom(@"E:\proj_flatness_detect\src\Lead.Detect.FrameworkExtension\bin\Debug\Lead.Detect.FrameworkExtension.dll");
            Type[] types = assembly.GetExportedTypes();
            foreach (Type type in types)
            {
                if (type.IsClass && type.GetInterface(nameof(IPrimCreator)) != null)
                {
                    var creator = (IPrimCreator)Activator.CreateInstance(type);
                    var primCreator = creator as IPrimCreator;
                    if (primCreator != null)
                    {
                        Console.WriteLine(primCreator.PrimProps);
                    }

                    break;
                }
            }


            //var c = Activator.CreateInstance(typeof(VirtualCardPrimCreator));

            //var ctor = c as IPrimCreator;
            //ctor = (IPrimCreator)c;

            //Console.WriteLine(ctor.PrimProps);
        }
    }
}