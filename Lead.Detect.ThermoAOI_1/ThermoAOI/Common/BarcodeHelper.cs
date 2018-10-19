using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.frameworkManage;

namespace Lead.Detect.ThermoAOI.Common
{
    public class BarcodeHelper : SerialPort
    {
        public void Open(string com, int baudrate = 115200, int databit = 8, StopBits stopBits = StopBits.One, Parity parity = Parity.None)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return;
            }

            if (IsOpen)
            {
                Close();
            }

            PortName = com;
            BaudRate = baudrate;
            DataBits = databit;
            StopBits = stopBits;
            Parity = parity;
            Open();
        }

        public string Trigger()
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return "BarcodeTest";
            }

            try
            {
                DiscardInBuffer();
                DtrEnable = true;
                WriteLine("||>trigger on\r\n");
                ReadTimeout = 1500;
                var readBuffer = new byte[2048];

                int readCount = Read(readBuffer, 0, readBuffer.Length);
                var barcode = Encoding.ASCII.GetString(readBuffer, 0, readCount);
                //var barcode = ReadExisting();
                return barcode.Replace("\r\n", string.Empty);

            }
            catch (Exception )
            {
                return string.Empty;
            }

        }
    }
}