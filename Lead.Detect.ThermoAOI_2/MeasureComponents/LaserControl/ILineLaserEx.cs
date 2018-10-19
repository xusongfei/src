using System.Collections.Generic;

namespace Lead.Detect.MeasureComponents.LaserControl
{
    public interface ILineLaserEx
    {
        string Name { get; set; }

        string IpStr { get; set; }

        int Port { get; set; }


        bool Connect();


        bool Disconnect();



        List<double[]> Trigger(string measureType);

        List<double[]> GetResult();
    }
}
