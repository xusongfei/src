using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MeasureComponents.LMILaser
{
    public class FlatnessParser
    {
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


        public static List<List<PosXYZ>> GetGridOutput(List<PosXYZ> gridRawPoints)
        {
            var output = new List<List<PosXYZ>>();
            {
                var rowy = gridRawPoints.First().Y;


                //get cols
                var rows = 0;
                var cols = 0;
                while (true)
                {
                    if (Math.Abs(gridRawPoints[cols].Y - rowy) > 4.5)
                    {
                        break;
                    }
                    cols++;
                }

                rows = gridRawPoints.Count / cols;
                //cols = results.Count / rows;


                for (int c = 0; c < cols; c++)
                {
                    output.Add(new List<PosXYZ>());
                    for (int r = 0; r < rows; r++)
                    {
                        output[c].Add(new PosXYZ());
                    }
                }


                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        var index = row * cols + col;
                        if (index > gridRawPoints.Count - 1)
                        {
                            break;
                        }
                        var r = gridRawPoints[index];
                        output[col][row] = (new PosXYZ(r.X, r.Y, r.Z) { OffsetZ = r.OffsetZ });
                    }
                }
            }


            {
                //parse all grid nodes to rows and cols
                var ResultColCount = 1;
                var ResultRowCount = gridRawPoints.Count;
                if (gridRawPoints.Count >= ResultRowCount * ResultColCount)
                {
                    for (var col = 0; col < ResultColCount; col++)
                    {
                        var rowData = new List<PosXYZ>();
                        for (var row = 0; row < ResultRowCount; row++)
                        {
                            rowData.Add(gridRawPoints[col * ResultRowCount + row]);
                        }
                        output.Add(rowData);
                    }
                }
            }


            //{
            //    //order gird by x 
            //    var resultsOrdered = results.OrderBy(p => p.X).ToList();
            //    var n = 0;
            //    while (n < resultsOrdered.Count)
            //    {
            //        var colx = resultsOrdered[n].X;
            //        var colPos = resultsOrdered.FindAll(p => Math.Abs(p.X - colx) < 2).ToList();
            //        output.Add(colPos);
            //        n += colPos.Count;
            //    }
            //}


            return output;



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

            if (width > height)
            {
                var img = new Bitmap((int)width + 1, (int)height + 1, PixelFormat.Format24bppRgb);

                for (int i = 0; i < pos.Count; i++)
                {
                    var gray = (int)((pos[i].Z - zMin) / range * 255);
                    int w = (int)((int)(pos[i].X - xMin) % width);
                    int h = (int)((int)(pos[i].Y - yMin) % height);
                    img.SetPixel(w, h, Color.FromArgb(gray, gray, gray));
                }
                return img;

            }
            else
            {
                var temp = width;
                width = height;
                height = temp;

                var img = new Bitmap((int)width + 1, (int)height + 1, PixelFormat.Format24bppRgb);

                for (int i = 0; i < pos.Count; i++)
                {
                    var gray = (int)((pos[i].Z - zMin) / range * 255);
                    int h = (int)((int)(pos[i].X - xMin) % height);
                    int w = (int)((int)(pos[i].Y - yMin) % width);
                    img.SetPixel(w, h, Color.FromArgb(gray, gray, gray));
                }
                return img;
            }




        }
    }
}