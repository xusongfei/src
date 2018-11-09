using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.Utility.Transformation;

namespace Lead.Detect.MeasureComponents.LMILaser
{
    public enum GridParseMethod
    {
        RigidAlignAndOrderByX,

        ColCountParseByY,
    }

    public class FlatnessParser
    {
        public static GridParseMethod GridParseMethod = GridParseMethod.RigidAlignAndOrderByX;


        public static List<PosXYZ> Parse(byte[] buffer)
        {
            var results = new List<PosXYZ>();
            using (var ms = new MemoryStream(buffer))
            {
                using (var br = new BinaryReader(ms))
                {
                    var status = (int)br.ReadUInt16();
                    var flatnessNum = (int)br.ReadUInt32();
                    var version = (int)br.ReadUInt32();

                    switch (version)
                    {
                        case 1:
                            break;
                        case 2:
                            List<double> flatnessData = new List<double>();
                            for (int j = 0; j < flatnessNum; j++)
                            {
                                flatnessData.Add(br.ReadDouble());
                                flatnessData.Add(br.ReadDouble());
                                flatnessData.Add(br.ReadDouble());
                            }

                            var nodeNum = (int)br.ReadUInt32();
                            List<double> nodeData = new List<double>(nodeNum * 3);
                            for (int j = 0; j < nodeNum; j++)
                            {
                                nodeData.Add(br.ReadDouble());
                                nodeData.Add(br.ReadDouble());
                                nodeData.Add(br.ReadDouble());
                            }

                            double a, b, c, d;
                            if (nodeNum > 3)
                            {
                                a = br.ReadDouble();
                                b = br.ReadDouble();
                                c = br.ReadDouble();
                                d = br.ReadDouble();

                                var p = Math.Sqrt(a * a + b * b + c * c);

                                //calculate node to plane heights
                                for (var j = 0; j < nodeNum; j++)
                                {
                                    var index = j * 3;
                                    var nx = nodeData[index];
                                    var ny = nodeData[index + 1];
                                    var nz = nodeData[index + 2];

                                    var dist = (nx * a + ny * b + nz * c + d) / p;
                                    results.Add(new PosXYZ(nx, ny, dist) { OffsetZ = nz });
                                }
                            }
                            else
                            {
                                results.Clear();
                            }

                            break;
                    }
                }
            }

            return results;
        }


        public static List<List<PosXYZ>> GetGridOutput(List<PosXYZ> gridNodes)
        {
            switch (GridParseMethod)
            {
                case GridParseMethod.RigidAlignAndOrderByX:
                    return ParseByRigidAlign(gridNodes);
                case GridParseMethod.ColCountParseByY:
                    return ParseByColumnCount(gridNodes);
            }

            return null;
        }

        private static List<List<PosXYZ>> ParseByColumnCount(List<PosXYZ> gridNodes)
        {
            {
                var output = new List<List<PosXYZ>>();
                {
                    var rowy = gridNodes.First().Y;

                    //get column count
                    var gridRowCount = 0;
                    var gridColumnCount = 0;
                    while (true)
                    {
                        if (Math.Abs(gridNodes[gridColumnCount].Y - rowy) > 4.5)
                        {
                            break;
                        }

                        gridColumnCount++;
                    }

                    //get row count
                    gridRowCount = gridNodes.Count / gridColumnCount;


                    //init output
                    for (int c = 0; c < gridColumnCount; c++)
                    {
                        output.Add(new List<PosXYZ>());
                        for (int r = 0; r < gridRowCount; r++)
                        {
                            output[c].Add(new PosXYZ());
                        }
                    }


                    //put grid data to rows and cols
                    for (int row = 0; row < gridRowCount; row++)
                    {
                        for (int col = 0; col < gridColumnCount; col++)
                        {
                            var index = row * gridColumnCount + col;
                            if (index > gridNodes.Count - 1)
                            {
                                //some point loss
                                break;
                            }

                            var node = gridNodes[index];
                            output[col][row] = (new PosXYZ(node.X, node.Y, node.Z) { OffsetZ = node.OffsetZ });
                        }
                    }
                }

                {
                    //add all raw grid nodes
                    var resultColCount = 1;
                    var resultRowCount = gridNodes.Count;
                    if (gridNodes.Count >= resultRowCount * resultColCount)
                    {
                        for (var col = 0; col < resultColCount; col++)
                        {
                            var rowData = new List<PosXYZ>();
                            for (var row = 0; row < resultRowCount; row++)
                            {
                                rowData.Add(gridNodes[col * resultRowCount + row]);
                            }

                            output.Add(rowData);
                        }
                    }
                }

                return output;
            }
        }

        private static List<List<PosXYZ>> ParseByRigidAlign(List<PosXYZ> gridNodes)
        {
            var p1 = gridNodes[0];
            var p2 = gridNodes[1];

            var p1g = new PosXYZ();
            var p2g = new PosXYZ(new PosXYZ(p1.X, p1.Y, 0).DistanceTo(new PosXYZ(p2.X, p2.Y, 0)), 0, 0);

            var rigidTrans = XyzPlarformCalibration.CalcAlignTransform(new List<PosXYZ>() { p1, p2 }, new List<PosXYZ>() { p1g, p2g });


            var transGridNodes = gridNodes.Select(g => XyzPlarformCalibration.AlignTransform(g, rigidTrans.Item1)).ToList();

            {
                var output = new List<List<PosXYZ>>();
                //order gird by x 
                var orderedNodes = transGridNodes.OrderBy(p => p.X).ToList();
                var n = 0;
                while (n < orderedNodes.Count)
                {
                    var colx = orderedNodes[n].X;
                    var colPos = orderedNodes.FindAll(p => Math.Abs(p.X - colx) < 2).ToList();
                    output.Add(colPos);
                    n += colPos.Count;
                }

                output.Add(transGridNodes);

                return output;
            }
        }


        public static Bitmap CreateDisplay(List<PosXYZ> pos)
        {
            var xMax = pos.Max(p => p.X);
            var yMax = pos.Max(p => p.Y);
            var zMax = pos.Max(p => p.Z);

            var xMin = pos.Min(p => p.X);
            var yMin = pos.Min(p => p.Y);
            var zMin = pos.Min(p => p.Z);

            var width = xMax - xMin;
            var height = yMax - yMin;
            var range = zMax - zMin;

            var w_margin = width * 0.2;
            var h_margin = height * 0.2;
            width = width + w_margin;
            height = height + h_margin;


            if (width > height)
            {
                var img = new Bitmap((int)width + 1, (int)height + 1, PixelFormat.Format24bppRgb);

                for (int i = 0; i < pos.Count; i++)
                {
                    var gray = (int)((pos[i].Z - zMin) / range * 255);
                    int w = (int)((int)(pos[i].X - xMin) % width) + (int)w_margin / 2;
                    int h = (int)((int)(pos[i].Y - yMin) % height) + (int)h_margin / 2;
                    img.SetPixel(w, h, Color.FromArgb(gray, gray, gray));
                }

                return img;
            }
            else
            {
                //rotate 90 degree
                var temp = width;
                width = height;
                height = temp;

                var img = new Bitmap((int)width + 1, (int)height + 1, PixelFormat.Format24bppRgb);

                for (int i = 0; i < pos.Count; i++)
                {
                    var gray = (int)((pos[i].Z - zMin) / range * 255);
                    int w = (int)((int)(pos[i].Y - yMin) % width) + (int)h_margin / 2;
                    int h = (int)((int)(pos[i].X - xMin) % height) + (int)w_margin / 2;
                    img.SetPixel(w, h, Color.FromArgb(gray, gray, gray));
                }

                return img;
            }
        }
    }
}