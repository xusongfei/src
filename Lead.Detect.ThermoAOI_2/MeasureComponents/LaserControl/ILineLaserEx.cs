using System;
using System.Collections.Generic;
using System.Drawing;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MeasureComponents.LaserControl
{
    public interface ILineLaserEx
    {

        event Action<Bitmap> DisplayResultEvent;

        string Name { get; set; }
        string IpStr { get; set; }
        int Port { get; set; }


        bool Connect();
        bool Disconnect();


        List<List<PosXYZ>> Trigger(string measureType);
        List<List<PosXYZ>> GetResult();
    }
}
