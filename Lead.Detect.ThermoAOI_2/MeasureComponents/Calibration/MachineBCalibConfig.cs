﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.common;

namespace Lead.Detect.MeasureComponents.Calibration
{
    public class MachineBCalibConfig : UserSettings<MachineBCalibConfig>
    {
        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}
