﻿using System.Drawing;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MeasureComponents.CameraControl
{
    public interface ICameraEx
    {
        string Name { get; set; }
        string IP { get; set; }
        int Port { get; set; }

        string LastError { get; set; }

        bool Connect();
        bool Disconnect();
        Image GrabOne();


        void Test();

        bool Trigger(string msg);
        bool Trigger(string msg, PosXYZ pos);

        string ReadMsg();
        string GetResult(string resultInfo);
    }
}
