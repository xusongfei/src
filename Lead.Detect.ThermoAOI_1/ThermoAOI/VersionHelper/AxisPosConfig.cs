using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.Helper;
using Lead.Detect.ThermoAOI.Machine;

namespace Lead.Detect.ThermoAOI.VersionHelper
{
    public class AxisPosConfig : UserSettings<AxisPosConfig>
    {
        public AxisPosConfig()
        {
            var props = this.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, Activator.CreateInstance(p.PropertyType));
            }
        }

        [Description("左上平台点位")]
        [Category("Left Work")]
        public List<PosXYZ> LUpPlatformPos { get; set; }

        [Description("左下平台点位")]
        [Category("Left Work")]
        public List<PosXYZ> LDownPlatformPos { get; set; }

        [Description("左载具平移点位")]
        [Category("Left Work")]
        public List<PosXYZ> LTransPos { get; set; }

        [Description("左压料点位")]
        [Category("Left Work")]
        public List<PosXYZ> LPressPos { get; set; }



        [Description("右上平台点位")]
        [Category("Right Work")]
        public List<PosXYZ> RUpPlatformPos { get; set; }
        [Description("右下平台点位")]
        [Category("Right Work")]
        public List<PosXYZ> RDownPlatformPos { get; set; }
        [Description("右载具平移点位")]
        [Category("Right Work")]
        public List<PosXYZ> RTransPos { get; set; }
        [Description("右压料点位")]
        [Category("Right Work")]
        public List<PosXYZ> RPressPos { get; set; }



        [Category("Left UP 2 DOWN XY Align")]
        public List<PosXYZ> LUpPlatformAlignPos { get; set; }
        [Category("Left UP 2 DOWN XY Align")]
        public List<PosXYZ> LDownPlatformAlignPos { get; set; }



        [Category("Left CARRIER 2 UP XY Calib")]
        public List<PosXYZ> LUpStandardPos { get; set; }
        [Category("Left CARRIER 2 UP XY Calib")]
        public List<PosXYZ> LUpPlatformCalibPos { get; set; }



        /// <summary>
        /// height calibration block : product pos
        /// </summary>
        [Category("Left HEIGHT Calib")]
        public List<PosXYZ> LUpHeightCalibPos { get; set; }
        /// <summary>
        /// height calibration block : product pos
        /// </summary> [Category("Left HEIGHT Calib")]
        [Category("Left HEIGHT Calib")]
        public List<PosXYZ> LDownHeightCalibPos { get; set; }



        [Category("Right UP 2 DOWN XY Align")]
        public List<PosXYZ> RUpPlatformAlignPos { get; set; }
        [Category("Right UP 2 DOWN XY Align")]
        public List<PosXYZ> RDownPlatformAlignPos { get; set; }



        [Category("Right CARRIER 2 UP XY Calib")]
        public List<PosXYZ> RUpStandardPos { get; set; }
        [Category("Right CARRIER 2 UP XY Calib")]
        public List<PosXYZ> RUpPlatformCalibPos { get; set; }

        
        /// <summary>
        /// height calibration block : product pos
        /// </summary>
        [Category("Right HEIGHT Calib")]
        public List<PosXYZ> RUpHeightCalibPos { get; set; }
        /// <summary>
        /// height calibration block : product pos
        /// </summary>
        [Category("Right HEIGHT Calib")]
        public List<PosXYZ> RDownHeightCalibPos { get; set; }



        public PlatformType GetPlatformType(string posType)
        {
            var platform = Enum.GetNames(typeof(PlatformType));
            foreach (var p in platform)
            {
                var contains = posType.Contains(p);
                if (contains)
                {
                    return (PlatformType)Enum.Parse(typeof(PlatformType), p);
                }
            }
            return PlatformType.None;
        }


        public EleAxis[] GetTeachAxis(string posType)
        {
            var p = this.GetType().GetProperty(posType);
            if (p != null)
            {
                //if (p.Name.StartsWith(PlatformType.LUp.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.LUp);
                //}
                //else if (p.Name.StartsWith(PlatformType.LDown.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.LDown);
                //}
                //else if (p.Name.StartsWith(PlatformType.LTrans.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.LTrans);
                //}
                //else if (p.Name.StartsWith(PlatformType.LPress.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.LPress);
                //}
                //else if (p.Name.StartsWith(PlatformType.RUp.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.RUp);
                //}
                //else if (p.Name.StartsWith(PlatformType.RDown.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.RDown);
                //}
                //else if (p.Name.StartsWith(PlatformType.RTrans.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.RTrans);
                //}
                //else if (p.Name.StartsWith(PlatformType.RPress.ToString()))
                //{
                //    return Machine.Ins.Settings.Axis.GetPlatformAxis(PlatformType.RPress);
                //}
                //else
                {
                    return null;
                }
            }
            return null;
        }

        public override bool CheckIfNormal()
        {
            return true;
        }


        public override void Save(string environment)
        {

            {
                var Positions = LUpPlatformPos
             .Concat(LUpStandardPos)
             .Concat(LUpPlatformCalibPos)
             .Concat(LUpPlatformAlignPos)
             .Concat(LUpHeightCalibPos).Cast<IPlatformPos>().ToList();
                XmlSerializerHelper.WriteXML(Positions, $@".\Config\LeftUp.pts", Positions.GetType());
            }

            {
                var Positions = LDownPlatformPos
             .Concat(LDownPlatformAlignPos)
             .Concat(LDownHeightCalibPos).Cast<IPlatformPos>().ToList();
                XmlSerializerHelper.WriteXML(Positions, $@".\Config\LeftDown.pts", Positions.GetType());
            }

            {
                var Positions = LTransPos
             .Concat(LPressPos).Cast<IPlatformPos>().ToList();
                XmlSerializerHelper.WriteXML(Positions, $@".\Config\LeftCarrier.pts", Positions.GetType());
            }


            {
                var Positions = RUpPlatformPos
             .Concat(RUpStandardPos)
             .Concat(RUpPlatformCalibPos)
             .Concat(RUpPlatformAlignPos)
             .Concat(RUpHeightCalibPos).Cast<IPlatformPos>().ToList();
                XmlSerializerHelper.WriteXML(Positions, $@".\Config\RightUp.pts", Positions.GetType());
            }

            {
                var Positions = RDownPlatformPos
             .Concat(RDownPlatformAlignPos)
             .Concat(RDownHeightCalibPos).Cast<IPlatformPos>().ToList();
                XmlSerializerHelper.WriteXML(Positions, $@".\Config\RightDown.pts", Positions.GetType());
            }

            {
                var Positions = RTransPos
             .Concat(RPressPos).Cast<IPlatformPos>().ToList();
                XmlSerializerHelper.WriteXML(Positions, $@".\Config\RightCarrier.pts", Positions.GetType());
            }


        }


    }
}