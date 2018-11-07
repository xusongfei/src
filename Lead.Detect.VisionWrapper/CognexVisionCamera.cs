using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.CameraControl;

namespace Lead.Detect.VisionWrapper
{
    public class CognexVisionCamera : ICameraEx
    {



        public void Init()
        {

        }
        public void Uninit()
        {

        }











        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string LastError { get; set; }
        public bool Connect()
        {

            return true;
        }

        public bool Disconnect()
        {
            return true;
        }

        public Image GrabOne()
        {
            return null;
        }

        public void Test()
        {
            return;
        }

        public bool Trigger(string msg, int timeout)
        {
            return true;
        }

        public bool Trigger(string msg)
        {
            return true;
        }

        public string ReadMsg(int timeout)
        {
            return null;
        }

        public string GetResult(string resultInfo, int timeout = 0)
        {
            return null;
        }

        public bool Trigger(string msg, PosXYZ pos)
        {
            return true;
        }
    }
}
