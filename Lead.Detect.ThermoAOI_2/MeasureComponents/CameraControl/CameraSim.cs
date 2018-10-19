using System;
using System.Drawing;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.Properties;

namespace Lead.Detect.MeasureComponents.CameraControl
{
    public class CameraSim : ICameraEx
    {
        public string Name { get; set; } = "Sim";
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 5555;
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
            return Resources.test;
        }

        public bool Trigger(string msg)
        {
            return true;
        }

        public bool Trigger(string msg, PosXYZ pos)
        {
            return true;
        }

        public string ReadMsg()
        {
            return string.Empty;
        }

        public string GetResult(string resultInfo)
        {
            return "OK";
        }


        public override string ToString()
        {
            return "CameraSim";
        }

        public void Test()
        {
            return;
        }
    }
}