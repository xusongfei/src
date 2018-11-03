using System.Drawing;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MeasureComponents.Thermo2Camera
{
    public class ThermoCameraBSim : ThermoCameraB
    {
        public new string Name { get; set; }
        public new string IP { get; set; }
        public new int Port { get; set; }
        public new string LastError { get; set; }

        public new bool Connect()
        {
            return true;
        }

        public new bool Disconnect()
        {
            return true;
        }

        public new Image GrabOne()
        {
            return null;
        }

        public new bool Trigger(string msg)
        {
            return true;
        }

        public new bool Trigger(string msg, PosXYZ pos)
        {
            return true;
        }

        public new bool SwitchProduct(int product)
        {
            TriggerResult = "SWITCH OK";
            return true;
        }


        public new bool TriggerProduct(int step)
        {
            TriggerResult = "PRODUCT OK";
            return true;
        }
        public new string GetResult(string resultInfo, int timeout = 0)
        {
            return "OK";
        }


    }
}