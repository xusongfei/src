using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Lead.Detect.MeasureComponents.LaserControl;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.GoSdk.Outputs;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;

namespace Lead.Detect.MeasureComponents.LMILaser
{
    public class LmiLaser : ILineLaserEx
    {
        public string Name { get; set; }
        public int Port { get; set; }
        public string IpStr { get; set; } = "192.168.1.10";


        public string JobName = "test";


        private GoSystem system;
        private GoSensor sensor;
        private GoEthernet ethernetOutput;

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

                KIpAddress ipAddress = KIpAddress.Parse(IpStr);
                sensor = system.FindSensorByIpAddress(ipAddress);
                sensor.Connect();
                sensor.DefaultJob = JobName;
      
                //retrieve setup handle
                GoSetup setup = sensor.Setup;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LMI laser connect fail:{ex.Message}");
                return false;
            }
            return true;
        }

        public bool Disconnect()
        {

            if(sensor == null)
            {
                return false;
            }

            sensor.Disconnect();
            sensor.Dispose();
            sensor = null;

            system.Stop();
            system.EnableData(false);
            system.Destroy();
            system.Dispose();

            return true;
        }

        public List<double[]> Trigger(string measure)
        {
            var res = new List<double[]>();

        
            system.ClearData();
            system.EnableData(true);
            system.Start();

            return res;
        }


        public List<double[]> GetResult()
        {
            var res = new List<double[]>();
            try
            {
                system.Stop();
                system.EnableData(false);


                ethernetOutput = sensor.Output.GetEthernetAt(0);
                ethernetOutput.ClearAllSources();
                ethernetOutput.AddSource(GoOutputSource.Measurement, 0);
                var dataSet = system.ReceiveData(3000000);

                for (int i = 0; i < dataSet.Count; i++)
                {
                    GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);

                    switch (dataObj.MessageType)
                    {
                        case GoDataMessageType.Measurement:
                            {
                                GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                                for (UInt32 k = 0; k < measurementMsg.Count; ++k)
                                {
                                    GoMeasurementData measurementData = measurementMsg.Get(k);
                                    Console.WriteLine("ID: {0}", measurementMsg.Id);
                                    Console.WriteLine("Value: {0}", measurementData.Value);
                                    Console.WriteLine("Decision: {0}", measurementData.Decision);

                                    res.Add(new[] { measurementMsg.Id, measurementData.Value, measurementData.Decision, measurementMsg.Count });
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception)
            {

            }
         

            return res;
        }
    }
}
