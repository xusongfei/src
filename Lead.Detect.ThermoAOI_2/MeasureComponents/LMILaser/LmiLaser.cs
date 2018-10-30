using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Lead.Detect.MeasureComponents.LaserControl;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MeasureComponents.LMILaser
{
    public class LmiLaser : ILineLaserEx
    {
        public event Action<Bitmap> DisplayResultEvent;

        public string Name { get; set; }
        public int Port { get; set; }
        public string IpStr { get; set; } = "192.168.1.10";


        public bool EnableSaveRec = false;



        public bool EnableAccelerator = false;
        public string AcceleratorIp = "192.168.1.111";


        public string JobName = "test";


        private GoSystem system;
        //private GoAccelerator accelerator;
        private GoSensor sensor;

        public static void Init()
        {
            KApiLib.Construct();
            GoSdkLib.Construct();
        }
        public static void Uninit()
        {
            KApiLib.Instance.Dispose();
            GoSdkLib.Instance.Dispose();
        }

        public bool Connect()
        {
            try
            {
                system = new GoSystem();
                system.Stop();

                if (EnableAccelerator)
                {
                    KIpAddress ipAddress = KIpAddress.Parse(AcceleratorIp);
                    sensor = system.FindSensorByIpAddress(ipAddress);

                    //accelerator = new GoAccelerator();
                    //accelerator.Start();
                    //accelerator.Attach(sensor);
                }
                else
                {
                    KIpAddress ipAddress = KIpAddress.Parse(IpStr);
                    sensor = system.FindSensorByIpAddress(ipAddress);
                }


                sensor.Connect();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool Disconnect()
        {

            if (sensor == null)
            {
                return false;
            }

            if (EnableAccelerator)
            {
                //accelerator.Detach(sensor);
                //accelerator.Stop();
                //accelerator.Dispose();
            }

            try
            {
                //sensor.Disconnect();
                //sensor.Dispose();
                //sensor = null;

                system.Stop();
                system.EnableData(false);
                system.Dispose();
                system = null;

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<List<PosXYZ>> Trigger(string measure)
        {
            system.ClearData();
            system.EnableData(true);
            system.Start();

            return null;
        }


        public int ResultRowCount = 0;
        public int ResultColCount = 0;


        public void StartRec()
        {
            if (EnableSaveRec)
            {
                sensor.RecordingEnabled = true;
                sensor.ClearReplayData();
            }
        }


        public void SaveRec(string file)
        {
            if (EnableSaveRec)
            {

                if (!Directory.Exists(Path.GetDirectoryName(file)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(file));
                }

                sensor.DownloadFile($"_live.rec", file);
                //sensor.Backup(file + ".gs");
                sensor.RecordingEnabled = false;
            }
        }

        public List<List<PosXYZ>> GetResult()
        {
            try
            {
                //goOutput = sensor.Output.GetEthernetAt(0);
                //goOutput.ClearAllSources();

                var dataSet = system.ReceiveData(3000);

                system.Stop();
                system.EnableData(false);

                //parse dataSet
                for (int i = 0; i < dataSet.Count; i++)
                {
                    GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);

                    switch (dataObj.MessageType)
                    {
                        case GoDataMessageType.Measurement:
                            {
                                //GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                                //if (measurementMsg != null)
                                //{
                                //    var output = new List<double[]>();
                                //    for (var k = 0; k < measurementMsg.Count; ++k)
                                //    {
                                //        GoMeasurementData measurementData = measurementMsg.Get(k);
                                //        var data = new double[3];
                                //        data[0] = measurementMsg.Id;
                                //        data[1] = measurementData.Value;
                                //        data[2] = measurementData.Decision;
                                //        output.Add(data);
                                //    }
                                //    return output;
                                //}
                            }
                            break;

                        case GoDataMessageType.Generic:
                            {
                                GoGenericMsg genericMsg = dataObj as GoGenericMsg;
                                if (genericMsg != null)
                                {
                                    byte[] buffer = new byte[genericMsg.BufferSize];
                                    Marshal.Copy(genericMsg.BufferData, buffer, 0, (int)genericMsg.BufferSize);

                                    var results = FlatnessParser.Parse(buffer);


                                    var output = new List<List<PosXYZ>>();
                                    {
                                        var rowy = results.First().Y;
                                        //get cols
                                        var rows = 0;
                                        var cols = 0;
                                        while (true)
                                        {
                                            if (Math.Abs(results[cols].Y - rowy) > 4.5)
                                            {
                                                break;
                                            }
                                            cols++;
                                        }

                                        rows = results.Count / cols;
                                        //cols = results.Count / rows;

                                        for (int c = 0; c < cols; c++)
                                        {
                                            output.Add(new List<PosXYZ>());
                                            for (int r = 0; r < rows; r++)
                                            {
                                                output[c].Add(new PosXYZ());
                                            }
                                        }



                                        for (int col = 0; col < cols; col++)
                                        {
                                            for (int row = 0; row < rows; row++)
                                            {
                                                var index = row * col + row;
                                                if (index > results.Count - 1)
                                                {
                                                    break;
                                                }
                                                var r = results[row * col + row];
                                                output[col][row] = (new PosXYZ(r.X, r.Y, r.Z) { OffsetZ = r.OffsetZ });
                                            }
                                        }
                                    }


                                    {
                                        //parse all grid nodes to rows and cols
                                        ResultColCount = 1;
                                        ResultRowCount = results.Count;
                                        if (results.Count >= ResultRowCount * ResultColCount)
                                        {
                                            for (var col = 0; col < ResultColCount; col++)
                                            {
                                                var rowData = new List<PosXYZ>();
                                                for (var row = 0; row < ResultRowCount; row++)
                                                {
                                                    rowData.Add(results[col * ResultRowCount + row]);
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


                                    if (DisplayResultEvent != null)
                                    {
                                        DisplayResultEvent.Invoke(FlatnessParser.CreateDisplay(results));
                                    }

                                    return output;
                                }

                            }
                            break;
                    }
                }

            }
            catch (Exception)
            {

            }
            return null;
        }

        public void SetJob(string jobname)
        {
            if (sensor != null)
            {
                sensor.DefaultJob = jobname + ".job";

                sensor.CopyFile(sensor.DefaultJob, "_live.job");
                var loadedJob = string.Empty;
                var changed = false;
                sensor.LoadedJob(ref loadedJob, ref changed);
            }
        }
    }


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
