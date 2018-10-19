using System;
using System.Collections.Generic;

namespace Lead.Detect.MeasureComponents.LaserControl
{
    public class LineLaserSim : ILineLaserEx
    {
        public string Name { get; set; }
        public string IpStr { get; set; }
        public int Port { get; set; }

        public bool Connect()
        {
            return true;
        }

        public bool Disconnect()
        {
            return true;
        }

        public List<double[]> Trigger(string measureType)
        {
            return new List<double[]>()
            {
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
            };
        }

        public List<double[]> GetResult()
        {

            return new List<double[]>()
            {
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
                new []{0,0,1d},
            };
        }
    }
}