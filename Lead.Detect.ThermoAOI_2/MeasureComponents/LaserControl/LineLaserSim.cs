using System;
using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MeasureComponents.LaserControl
{
    public class LineLaserSim : LMILaser.LmiLaser
    {
        public new bool Connect()
        {
            return true;
        }

        public new bool Disconnect()
        {
            return true;
        }

        public new List<double[]> Trigger(string measureType)
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

        public new List<PosXYZ[]> GetResult()
        {

            return new List<PosXYZ[]>()
            {
               new []{new PosXYZ(), new PosXYZ(), new PosXYZ(), }
            };
        }
    }
}