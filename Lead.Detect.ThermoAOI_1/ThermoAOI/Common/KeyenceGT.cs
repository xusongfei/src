using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.frameworkManage;

namespace Lead.Detect.ThermoAOI.Common
{
    public class KeyenceGT : TcpClient
    {

        public new void Connect(string ip, int port)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }
            if (Connected)
            {
                return;
            }
            base.Connect(ip, port);
        }


        private object _readLock = new object();

        public double[] ReadData()
        {

            lock (_readLock)
            {
                try
                {
                    if (FrameworkExtenion.IsSimulate)
                    {
                        return new[] { 0.1, 0.2, 0.3 };
                    }

                    if (!Connected)
                    {
                        throw new Exception("read when no connection");
                    }

                    var ns = GetStream();
                    byte[] writeBuffer = Encoding.ASCII.GetBytes("M0\r\n");
                    ns.Write(writeBuffer, 0, writeBuffer.Length);


                    byte[] readBuffer = new byte[4096];
                    ns.Read(readBuffer, 0, readBuffer.Length);
                    var data = Encoding.ASCII.GetString(readBuffer);
                    var val = data.Split(',');

                    if (val.Length<4)
                    {
                        throw new Exception($"DATA LEGTH {val.Length} ERROR");
                    }


                    return val.Skip(1).Select(v => double.Parse(v) / 10000).ToArray();
                }
                catch (Exception ex)
                {
                    throw new Exception($"GT {this.Client} READ DATA ERROR: {ex.Message}");
                }
            }
        }

    }
}
