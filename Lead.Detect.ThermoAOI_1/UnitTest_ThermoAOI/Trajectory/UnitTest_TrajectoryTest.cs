using System;
using System.Linq;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;
using Lead.Detect.ThermoAOITrajectoryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lead.Detect.UnitTest1.Trajectory
{
    [TestClass]
    public class UnitTest_TrajectoryTest
    {
        [TestMethod]
        public void TestMethod_GraphTest()
        {

            var fprj = FlatnessProject.Load(@".\Config2\P2\A147NoFinV10.fprj");


            var pos = fprj.ProductSettings.DownTestPositions;

            var ped1 = pos.FindAll(p => p.Name.StartsWith("ped1"));
            var ped2 = pos.FindAll(p => p.Name.StartsWith("ped2"));

            pos.RemoveAll(p => p.Name.StartsWith("ped1"));
            pos.RemoveAll(p => p.Name.StartsWith("ped2"));
            pos.RemoveAll(p => p.Name.StartsWith("down"));

            pos.Add(ped1.Last());
            pos.Add(ped2.Last());

            {
                var graph = new PosGraph(pos);
                Console.WriteLine(graph.ToString());
            }



            {
                Console.WriteLine("------------BackTsp----------------");
                Console.WriteLine(pos.Count);
                Console.WriteLine("----------------------------------");
                var tcp = new BackTsp(new PosGraph(pos));
                Console.WriteLine(tcp.RunTcp());
                Console.WriteLine("----------------------------------");
            }

            {
                Console.WriteLine("------------DpTsp----------------");
                Console.WriteLine(pos.Count);
                Console.WriteLine("----------------------------------");
                var tcp = new DpTsp(new PosGraph(pos));
                Console.WriteLine(tcp.RunTcp());
                Console.WriteLine("----------------------------------");
            }



        }
    }
}
