using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.PlatformCalibration;
using Lead.Detect.PlatformCalibration.FittingHelper;
using Lead.Detect.PlatformCalibration.Transformation;
using Lead.Detect.ThermoAOI.Calibration;
using Lead.Detect.ThermoAOI.Machine;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lead.Detect.UnitTest1.ThermoAOI
{
    [TestClass]
    public class UnitTest_Calibration
    {
        [TestMethod]
        public void Test_FitTest()
        {
            var line = LineParams.FitLine(new List<PosXYZ>()
            {
                new PosXYZ(1d, 1d, 1.1d),
                new PosXYZ(2d, 2d, 2.2d),
                new PosXYZ(3d, 3d, 3.3d),
                new PosXYZ(44d, 44d, 44.4d),
            });

            Console.WriteLine($"{line.Direction},{line.Origin}");

        }


        [TestMethod]
        public void Test_AffineTransform()
        {

            List<PosXYZ> cpuPos = new List<PosXYZ>()
            {
                new PosXYZ(62.69, -64.8, 0.987),
                new PosXYZ(62.69, -96.8, 1.060),
                new PosXYZ(94.69, -96.8, 1.108),
                new PosXYZ(94.69, -64.8, 1.009),
            };


            List<PosXYZ> pins = new List<PosXYZ>()
            {
                new PosXYZ( 46.69,  -48.8, 2.117),
                new PosXYZ( 46.69, -112.8, 2.131),
                new PosXYZ(110.69, -112.8, 2.179),
                new PosXYZ(110.69,  -48.8, 2.051),
            };


            {
                var ret = XyzPlarformCalibration.CalcAffineTransform(cpuPos, pins);

                Console.WriteLine($"CalcAffineTransform:\r\n"
                    + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6},{ret.Item1[0, 3]:F6}\r\n"
                    + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6},{ret.Item1[1, 3]:F6}\r\n"
                    + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6},{ret.Item1[2, 3]:F6}\r\n"
                    + $"{ret.Item1[3, 0]:F6},{ret.Item1[3, 1]:F6},{ret.Item1[3, 2]:F6},{ret.Item1[3, 3]:F6}\r\n");

                var mat = DenseMatrix.OfArray(new double[,]
                {
                    {ret.Item1[0, 0], ret.Item1[0, 1], ret.Item1[0, 3]},
                    {ret.Item1[1, 0], ret.Item1[1, 1], ret.Item1[1, 3]},
                    {0,0,1 }
                }).Inverse();

                var newMat = DenseMatrix.OfArray(new double[,]
                {
                    {mat[0, 0], mat[0, 1], mat[0, 2]},
                    {mat[1, 0], mat[1, 1], mat[1, 2]},
                });

                Console.WriteLine($"InverseMatrix:\r\n{newMat.ToString()}");

            }

            {
                var ret = XyzPlarformCalibration.CalcAffineTransform(pins, cpuPos);

                Console.WriteLine($"CalcAffineTransform:\r\n"
                                  + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6},{ret.Item1[0, 3]:F6}\r\n"
                                  + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6},{ret.Item1[1, 3]:F6}\r\n"
                                  + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6},{ret.Item1[2, 3]:F6}\r\n"
                                  + $"{ret.Item1[3, 0]:F6},{ret.Item1[3, 1]:F6},{ret.Item1[3, 2]:F6},{ret.Item1[3, 3]:F6}\r\n");

            }
        }


        [TestMethod]
        public void Test_AffineTransformWithRot()
        {

            List<PosXYZ> cpuPos = new List<PosXYZ>()
            {
                new PosXYZ(62.69, -64.8, 0.987),
                new PosXYZ(62.69, -96.8, 1.060),
                new PosXYZ(94.69, -96.8, 1.108),
                new PosXYZ(94.69, -64.8, 1.009),
            };

            for (int i = 0; i < cpuPos.Count; i++)
            {
                cpuPos[i] = cpuPos[i].Scale(new PosXYZ(10.3,10.31,1)).RotateAt(new PosXYZ(), 42.2).Translate(new PosXYZ(10, 11.2d, 0));
            }

            List<PosXYZ> oldcpupos = new List<PosXYZ>()
            {
                new PosXYZ(62.69, -64.8, 0.987),
                new PosXYZ(62.69, -96.8, 1.060),
                new PosXYZ(94.69, -96.8, 1.108),
                new PosXYZ(94.69, -64.8, 1.009),
            };


            {
                var ret = XyzPlarformCalibration.CalcAffineTransform(cpuPos, oldcpupos);

                Console.WriteLine($"CalcAffineTransform:\r\n"
                    + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6},{ret.Item1[0, 3]:F6}\r\n"
                    + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6},{ret.Item1[1, 3]:F6}\r\n"
                    + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6},{ret.Item1[2, 3]:F6}\r\n"
                    + $"{ret.Item1[3, 0]:F6},{ret.Item1[3, 1]:F6},{ret.Item1[3, 2]:F6},{ret.Item1[3, 3]:F6}\r\n");

                Console.WriteLine($"Error:{ret.Item2:F6}");



                var mat = DenseMatrix.OfArray(new double[,]
                {
                    {ret.Item1[0, 0], ret.Item1[0, 1], ret.Item1[0, 3]},
                    {ret.Item1[1, 0], ret.Item1[1, 1], ret.Item1[1, 3]},
                    {0,0,1 }
                }).Inverse();

                var newMat = DenseMatrix.OfArray(new double[,]
                {
                    {mat[0, 0], mat[0, 1], mat[0, 2]},
                    {mat[1, 0], mat[1, 1], mat[1, 2]},
                });

                Console.WriteLine($"InverseMatrix:\r\n{newMat.ToString()}");

            }

            {
                var ret = XyzPlarformCalibration.CalcAffineTransform(oldcpupos, cpuPos);

                Console.WriteLine($"CalcAffineTransform Inverse:\r\n"
                                  + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6},{ret.Item1[0, 3]:F6}\r\n"
                                  + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6},{ret.Item1[1, 3]:F6}\r\n"
                                  + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6},{ret.Item1[2, 3]:F6}\r\n"
                                  + $"{ret.Item1[3, 0]:F6},{ret.Item1[3, 1]:F6},{ret.Item1[3, 2]:F6},{ret.Item1[3, 3]:F6}\r\n");

            }


            {

                var ret = XyzPlarformCalibration.CalcAlignTransform(cpuPos, oldcpupos);

                Console.WriteLine($"CalcAlignTransform:\r\n"
                                  + $"{ret.Item1[0, 0]:F6},{ret.Item1[0, 1]:F6},{ret.Item1[0, 2]:F6}\r\n"
                                  + $"{ret.Item1[1, 0]:F6},{ret.Item1[1, 1]:F6},{ret.Item1[1, 2]:F6}\r\n"
                                  + $"{ret.Item1[2, 0]:F6},{ret.Item1[2, 1]:F6},{ret.Item1[2, 2]:F6}\r\n");

                Console.WriteLine($"Error:{ret.Item2:F6}");

            }








        }



        [TestMethod]
        public void Test_GT_HEIGHT()
        {
            var settings = MachineSettings.Load(@".\Config\settings.cfg");

            var project = FlatnessProject.Load(@".\Config\A117WithFinNewCalib.fprj");


            var data = File.ReadAllLines(@".\Config\20180807.csv");


            var calc = CalculatorMgr.Ins.New(project.ProductSettings.ProductName);


            var prodata = new Thermo1Product()
            {
                RawDataUp = project.ProductSettings.UpTestPositions,
                RawDataDown = project.ProductSettings.DownTestPositions,
                SPCItems = project.ProductSettings.SPCItems,
                ProductType = project.ProductSettings.ProductName,
            };


            for (int data1Index = 0; data1Index < data.Length; data1Index++)
            {

                {
                    //
                    //var data1Index = 11;
                    var buffer = data[data1Index].Split(',');

                    var up = 17;
                    for (int i = 0; i < prodata.RawDataUp.Count; i++)
                    {
                        prodata.RawDataUp[i].Z = double.Parse(buffer[up + i]);
                    }
                    var down = 26;
                    for (int i = 0; i < prodata.RawDataDown.Count; i++)
                    {
                        prodata.RawDataDown[i].Z = double.Parse(buffer[down + i]);
                    }

                    //prodata.RawUpGtWorkPos = new PosXYZ(
                    //    prodata.RawDataUp.Select(p => p.X).Average(),
                    //    prodata.RawDataUp.Select(p => p.Y).Average(),
                    //    settings.AxisPos.RUpPlatformPos.Find(p => p.Name == "GtWork").Z - project.ProductSettings.Height);

                    //prodata.RawDownGt1WorkPos = new PosXYZ(
                    //    prodata.RawDataDown.FindAll(p => p.Name == "cpu").Select(p => p.X).Average(),
                    //    prodata.RawDataDown.FindAll(p => p.Name == "cpu").Select(p => p.Y).Average(),
                    //    settings.AxisPos.RDownPlatformPos.Find(p => p.Name == "GtWork1").Z);


                    //prodata.RawDownGt2WorkPos = new PosXYZ(
                    //    prodata.RawDataDown.FindAll(p => p.Name == "pin").Select(p => p.X).Average(),
                    //    prodata.RawDataDown.FindAll(p => p.Name == "pin").Select(p => p.Y).Average(),
                    //    settings.AxisPos.RDownPlatformPos.Find(p => p.Name == "GtWork2").Z);

                    if (data1Index == 9)
                    {
                        data1Index = 9;
                    }

                    calc.Calculate(prodata);

                    //Console.WriteLine("up:\r\n");
                    //Console.WriteLine(string.Join("\r\n", prodata.RawDataUp));
                    //Console.WriteLine("down:\r\n");
                    //Console.WriteLine(string.Join("\r\n", prodata.RawDataDown));

                    //Console.WriteLine($"data1:");

                    var a117wf = calc as A117WithFinCalculator;


                }
            }





        }



        /// <summary>
        /// ok
        /// </summary>
        [TestMethod]
        public void Test_UpGtFlatness()
        {
            var settings = MachineSettings.Load(@".\Config2\settings.cfg");

            var project = FlatnessProject.Load(@".\Config2\A117NoFinNew.fprj");


            var upPos = project.ProductSettings.UpTestPositions.Skip(0).Take(8).ToList();

            int startIndex = 0;

            List<double> leftFlat = new List<double>();
            List<double> leftMin = new List<double>();
            List<double> leftMax = new List<double>();
            List<double> rightFlat = new List<double>();
            List<double> rightMin = new List<double>();
            List<double> rightMax = new List<double>();
            {
                //fit left with 4 pos 
                List<double[]> rawData = new List<double[]>();

                using (var fs = new FileStream(@".\Config2\left.csv", FileMode.Open))
                {
                    using (var sw = new StreamReader(fs))
                    {
                        var dataStr = sw.ReadLine()?.Split(',');
                        while (dataStr != null)
                        {
                            rawData.Add(new double[]
                            {
                                double.Parse(dataStr[13]),
                                double.Parse(dataStr[14]),
                                double.Parse(dataStr[15]),
                                double.Parse(dataStr[16]),
                                double.Parse(dataStr[17]),
                                double.Parse(dataStr[18]),
                                double.Parse(dataStr[19]),
                                double.Parse(dataStr[20]),
                            });
                            dataStr = sw.ReadLine()?.Split(',');
                        }

                    }
                }

                //fit plane
                for (int i = 0; i < rawData.Count; i++)
                {

                    for (int j = 0; j < upPos.Count; j++)
                    {
                        upPos[j].Z = rawData[i][startIndex + j];
                    }

                    var plane = PlaneFitHelper.FitPlane(
                        upPos.Select(p => p.X).ToArray(),
                        upPos.Select(p => p.Y).ToArray(),
                        upPos.Select(p => p.Z).ToArray());

                    var flat1 = upPos.Select(p => PlaneFitHelper.Point2Plane(plane, p.Data())).Min();
                    var flat2 = upPos.Select(p => PlaneFitHelper.Point2Plane(plane, p.Data())).Max();

                    var flatness = flat2 - flat1;

                    leftFlat.Add(flatness);
                    leftMin.Add(upPos.Select(p => p.Z).Min());
                    leftMax.Add(upPos.Select(p => p.Z).Max());
                }
            }

            {
                //fit left with 4 pos 
                List<double[]> rawData = new List<double[]>();

                using (var fs = new FileStream(@".\Config2\right.csv", FileMode.Open))
                {
                    using (var sw = new StreamReader(fs))
                    {
                        var dataStr = sw.ReadLine()?.Split(',');
                        while (dataStr != null)
                        {
                            rawData.Add(new double[]
                            {
                                double.Parse(dataStr[13]),
                                double.Parse(dataStr[14]),
                                double.Parse(dataStr[15]),
                                double.Parse(dataStr[16]),
                                double.Parse(dataStr[17]),
                                double.Parse(dataStr[18]),
                                double.Parse(dataStr[19]),
                                double.Parse(dataStr[20]),
                            });
                            dataStr = sw.ReadLine()?.Split(',');
                        }

                    }
                }

                //fit plane
                for (int i = 0; i < rawData.Count; i++)
                {

                    for (int j = 0; j < upPos.Count; j++)
                    {
                        upPos[j].Z = rawData[i][startIndex + j];
                    }

                    var plane = PlaneFitHelper.FitPlane(
                        upPos.Select(p => p.X).ToArray(),
                        upPos.Select(p => p.Y).ToArray(),
                        upPos.Select(p => p.Z).ToArray());

                    var flat1 = upPos.Select(p => PlaneFitHelper.Point2Plane(plane, p.Data())).Min();
                    var flat2 = upPos.Select(p => PlaneFitHelper.Point2Plane(plane, p.Data())).Max();

                    var flatness = flat2 - flat1;

                    rightFlat.Add(flatness);
                    rightMin.Add(upPos.Select(p => p.Z).Min());
                    rightMax.Add(upPos.Select(p => p.Z).Max());
                }
            }

            Console.WriteLine("flat:\r\n");
            Console.WriteLine(string.Join(",", leftFlat.Select(f => f.ToString("F3"))));
            Console.WriteLine(string.Join(",", rightFlat.Select(f => f.ToString("F3"))));

            Console.WriteLine("zmin:\r\n");
            Console.WriteLine(string.Join(",", leftMin.Select(f => f.ToString("F3"))));
            Console.WriteLine(string.Join(",", rightMin.Select(f => f.ToString("F3"))));

            Console.WriteLine("zmax:\r\n");
            Console.WriteLine(string.Join(",", leftMax.Select(f => f.ToString("F3"))));
            Console.WriteLine(string.Join(",", rightMax.Select(f => f.ToString("F3"))));

        }

        /// <summary>
        /// ok
        /// </summary>
        [TestMethod]
        public void Test_UpDownGtGt1Calib()
        {

            //var platform = PlatformType.LTrans;

            //var calibration = new CalibrationConfig()
            //{
            //    LeftHeightGt = new PosXYZ(0, 0, 2.561),
            //    LeftHeightCalibGtPos = new PosXYZ(0, 0, 96.99),

            //    LeftHeightGt1 = new PosXYZ(0, 0, 2.331),
            //    LeftHeightGt2 = new PosXYZ(0, 0, 3.387),
            //    LeftHeightCalibGt1Pos = new PosXYZ(0, 0, 0),
            //    LeftHeightCalibGt2Pos = new PosXYZ(0, 0, 0),

            //    LeftHeightStandard = new PosXYZ(0, 0, 70.5),
            //};


            //{
            //    //calibration pos
            //    Console.WriteLine("\r\ncalibration pos:\r\n");
            //    var gtWork = new PosXYZ()
            //    {
            //        Z = 96.99,
            //    };
            //    var gt1Work = new PosXYZ()
            //    {
            //        Z = 0,
            //    };
            //    var gt = 2.561;
            //    var gt1 = 2.331;

            //    //var height = CalibrationConfig.TransformUpDownHeight(
            //    //    platform, calibration,
            //    //    gtWork.Z, //gtwork
            //    //    gt1Work.Z, //gt1work
            //    //    gt, //gt
            //    //    gt1 //gt1
            //    //);
            //    //Console.WriteLine($"gtWork:{gtWork.Z:F3} gt1Work:{gt1Work.Z:F3} gt:{gt:F3} gt1:{gt1:F3} calc height:{height:F3}");

            //}
        }


        [TestMethod]
        public void Test_DownGt2Gt1Calib()
        {
            CalibrationConfig calib = new CalibrationConfig()
            {
                LeftHeightCalibGtPos = new PosXYZ(0, 0, 96.99),

                LeftHeightCalibGt1Pos = new PosXYZ(0, 0, 0),
                LeftHeightCalibGt2Pos = new PosXYZ(0, 0, 0),

                LeftHeightStandard = new PosXYZ(0, 0, 70.5),
            };


            List<PosXYZ> cpuPos = new List<PosXYZ>()
            {
                new PosXYZ(62.69, -64.8, 0.987),
                new PosXYZ(62.69, -96.8, 1.060),
                new PosXYZ(94.69, -96.8, 1.108),
                new PosXYZ(94.69, -64.8, 1.009),
                new PosXYZ(78.69, -80.8, 1.058),
            };


            List<PosXYZ> pins = new List<PosXYZ>()
            {
                new PosXYZ( 46.69,  -48.8, 2.117),
                new PosXYZ( 46.69, -112.8, 2.131),
                new PosXYZ(110.69, -112.8, 2.179),
                new PosXYZ(110.69,  -48.8, 2.051),
            };

            var plane = PlaneFitHelper.FitPlane(cpuPos.Select(p => p.X).ToArray(), cpuPos.Select(p => p.Y).ToArray(), cpuPos.Select(p => p.Z).ToArray());




        }
    }
}
