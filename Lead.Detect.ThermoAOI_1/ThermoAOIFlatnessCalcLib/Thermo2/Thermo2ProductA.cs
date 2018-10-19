﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
    public class Thermo2ProductA : ThermoProduct
    {
        [Description("测试点")]
        public List<PosXYZ> CapturePos { get; set; } = new List<PosXYZ>();


        public override string CsvHeaders()
        {
            return base.CsvHeaders();
        }

        public override string CsvValues()
        {
            return base.CsvValues();
        }


        public override DataTable ToDataTable()
        {
            return base.ToDataTable();
        }
    }
}