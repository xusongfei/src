using System;
using System.Collections.Generic;
using System.Drawing;
using Lead.Detect.MeasureComponents.LaserControl;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System.Runtime.InteropServices;
using System.IO;
using Lead.Detect.FrameworkExtension.frameworkManage;
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


        public string LastError;

        public static void Init()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

            KApiLib.Construct();
            GoSdkLib.Construct();
        }

        public static void Uninit()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

            KApiLib.Instance.Dispose();
            GoSdkLib.Instance.Dispose();
        }

        public bool Connect()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

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
                LastError = "ConnectError:" + ex.Message;
                return false;
            }

            return true;
        }

        public bool Disconnect()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

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
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<List<PosXYZ>> Trigger(string measure)
        {
            LastError = string.Empty;

            if (FrameworkExtenion.IsSimulate)
            {
                return null;
            }

            try
            {
                system.ClearData();
                system.EnableData(true);
                system.Start();
            }
            catch (Exception ex)
            {
                LastError = "TriggerError" + ex.Message;
            }

            return null;
        }


        public void StartRec()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

            if (EnableSaveRec)
            {
                sensor.RecordingEnabled = true;
                sensor.ClearReplayData();
            }
        }


        public void SaveRec(string file)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

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
            if (FrameworkExtenion.IsSimulate)
            {
                return new List<List<PosXYZ>>()
                {
                    new List<PosXYZ>() {new PosXYZ(1, 1, 1), new PosXYZ(1, 1, 1), new PosXYZ(1, 1, 1)},
                    new List<PosXYZ>() {new PosXYZ(2, 2, 2), new PosXYZ(2, 2, 2), new PosXYZ(2, 2, 2)}
                };
            }

            try
            {
                //goOutput = sensor.Output.GetEthernetAt(0);
                //goOutput.ClearAllSources();

                var dataSet = system.ReceiveData(3000000);

                system.Stop();
                system.EnableData(false);

                //parse dataSet
                for (int i = 0; i < dataSet.Count; i++)
                {
                    GoDataMsg dataObj = (GoDataMsg) dataSet.Get(i);

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
                                Marshal.Copy(genericMsg.BufferData, buffer, 0, (int) genericMsg.BufferSize);

                                var gridRawData = FlatnessParser.Parse(buffer);

                                var output = FlatnessParser.GetGridOutput(gridRawData);


                                if (DisplayResultEvent != null)
                                {
                                    DisplayResultEvent.Invoke(FlatnessParser.CreateDisplay(gridRawData));
                                }

                                return output;
                            }
                        }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = $"GetResultError: {ex.Message}";
            }

            return null;
        }

        public void SetJob(string jobname)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

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
}