using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOI.Machine1.UserDefine;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using Lead.Detect.Utility.FittingHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lead.Detect.UnitTest1.FlatnessCalcTest
{
    [TestClass]
    public class UnitTest_Calc
    {
        [TestMethod]
        public void TestMethod_Calc_Method20180820()
        {

            //load settings
            var settings = MachineSettings.Load(@".\Config2\Settings.cfg");


            //load raw data
            List<Thermo1Product> rawDatas = new List<Thermo1Product>();
            {

                var fprj = FlatnessProject.Load(@".\Config2\A117NoFin_pf_30.fprj");


                var file = @".\Config2\20180810 LEFT.csv";
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var testData = new Thermo1Product()
                    {
                        ProductType = fprj.ProductSettings.ProductName,
                        SPCItems = fprj.ProductSettings.SPCItems,
                    };
                    var strs = line.Split(',');

                    //raw up
                    {
                        var gtZ = 0;// settings.AxisPos.LUpPlatformPos.First(p => p.Name == "GtWork").Z - fprj.ProductSettings.Height;

                        var strIndex = 13;
                        for (int i = 0; i < 8; i++)
                        {
                            var pos = new PosXYZ(fprj.ProductSettings.UpTestPositions[i].X, fprj.ProductSettings.UpTestPositions[i].Y, double.Parse(strs[i + strIndex]))
                            {
                                Name = "up",
                                Description = "GT",
                                OffsetX = double.Parse(strs[i + strIndex]),
                                OffsetZ = gtZ,
                            };

                            testData.RawDataUp.Add(pos);
                        }
                    }

                    //raw ped1
                    {

                        var gtZ = 0;// settings.AxisPos.LDownPlatformPos.First(p => p.Name == "GtWork1").Z;
                        var strIndex = 22;
                        for (int i = 0; i < 5; i++)
                        {
                            var pos = new PosXYZ(fprj.ProductSettings.DownTestPositions[i].X, fprj.ProductSettings.DownTestPositions[i].Y, double.Parse(strs[i + strIndex]))
                            {
                                Name = "ped1",
                                Description = "GT1",
                                OffsetX = double.Parse(strs[i + strIndex]),
                                OffsetZ = gtZ,
                            };

                            testData.RawDataDown.Add(pos);
                        }

                    }



                    //raw inner standoff
                    {
                        var gtZ = 0;// settings.AxisPos.LDownPlatformPos.First(p => p.Name == "GtWork2").Z;
                        var strIndex = 27;
                        for (int i = 0; i < 4; i++)
                        {
                            var pos = new PosXYZ(fprj.ProductSettings.DownTestPositions[i + 5].X, fprj.ProductSettings.DownTestPositions[i + 5].Y, double.Parse(strs[i + strIndex]))
                            {
                                Name = "inner",
                                Description = "GT2",
                                OffsetX = double.Parse(strs[i + strIndex]),
                                OffsetZ = gtZ,
                            };

                            testData.RawDataDown.Add(pos);
                        }
                    }

                    rawDatas.Add(testData);
                }
            }



        


            {
                var p = rawDatas.Last();
                Console.WriteLine($"{string.Join(",", p.RawDataUp.Select(ps => ps.X.ToString("F3")))}");
                Console.WriteLine($"{string.Join(",", p.RawDataUp.Select(ps => ps.Y.ToString("F3")))}");
                Console.WriteLine($"{string.Join(",", p.RawDataDown.Select(ps => ps.X.ToString("F3")))}");
                Console.WriteLine($"{string.Join(",", p.RawDataDown.Select(ps => ps.Y.ToString("F3")))}");


                foreach (var data in p.RawDataUp)
                {
                    Console.WriteLine($"pos.Add(new PosXYZ({data.X},{data.Y},{data.Z}) {{ Status = {data.Status.ToString().ToLower()} }}); ");
                }
                foreach (var data in p.RawDataDown)
                {
                    Console.WriteLine($"pos.Add(new PosXYZ({data.X},{data.Y},{data.Z}) {{ Status = {data.Status.ToString().ToLower()} }}); ");
                }


            }

        }


        [TestMethod]

        public void TestMethod_LineTest()
        {

            var line = LineParams.FitLine(new List<PosXYZ>()
            {
                new PosXYZ(0,0,1),
                new PosXYZ(0,0,2),
                new PosXYZ(0,0,3),
            });


            Console.WriteLine($"FitLine:{line.Origin} {line.Direction}");

            var dist = line.Distance(new PosXYZ(1, 1, 0));

            Console.WriteLine($"Dist:{dist:F2}");



        }

    }
}
