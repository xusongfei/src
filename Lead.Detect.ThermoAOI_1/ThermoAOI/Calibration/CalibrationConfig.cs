using System;
using System.ComponentModel;
using System.Linq;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.PlatformCalibration.FittingHelper;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;

namespace Lead.Detect.ThermoAOI.Calibration
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CalibrationConfig : UserSettings<CalibrationConfig>
    {
        public CalibrationConfig()
        {
            var props = this.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, Activator.CreateInstance(p.PropertyType));
            }
        }

        #region left platform calib

        /// <summary>
        /// 产品坐标到上平台转换
        /// </summary>
        [Category("Left Up XY Calib ")]
        [ReadOnly(true)]
        public TransformParams LeftUpTransform { get; set; }


        /// <summary>
        /// 下gt1/gt2偏移
        /// </summary>
        [Category("Left Up Down Calib ")]
        [ReadOnly(true)]
        public PosXYZ LeftGtOffset { get; set; }

        /// <summary>
        /// 上下坐标系转换
        /// </summary>
        [Category("Left Up Down Calib ")]
        [ReadOnly(true)]
        public TransformParams LeftTransform { get; set; }

        #endregion

        #region left height calib

        /// <summary>
        /// Up GT 标定高度
        /// </summary>
        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightCalibGtPos { get; set; }

        /// <summary>
        /// GT1 标定高度
        /// </summary>
        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightCalibGt1Pos { get; set; }

        /// <summary>
        /// GT2 标定高度
        /// </summary>
        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightCalibGt2Pos { get; set; }

        /// <summary>
        /// 上标定平面 GT平面
        /// </summary>
        [Category("Left Height Calib ")]
        public PlaneParams LeftUpStandardPlaneGT { get; set; }

        /// <summary>
        /// 下标定平面 GT1 平面
        /// </summary>
        [Category("Left Height Calib ")]
        public PlaneParams LeftDownStandardPlaneGT1 { get; set; }

        /// <summary>
        /// 标定块高度
        /// </summary>
        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightStandard { get; set; }

        /// <summary>
        /// GT2 读数系统误差
        /// </summary>
        [Category("Left Height Calib "), Description("GT2倾斜高度偏差")]
        public PosXYZ LeftGt2ZOffset { get; set; }

        #endregion

        #region right platform calib


        [Category("Right Up XY Calib ")]
        [ReadOnly(true)]
        public TransformParams RightUpTransform { get; set; }

        /// <summary>
        /// 下gt1/gt2偏移
        /// </summary>
        [Category("Right Up Down Calib ")]
        [ReadOnly(true)]
        public PosXYZ RightGtOffset { get; set; }

        /// <summary>
        /// 上下坐标系转换
        /// </summary>
        [Category("Right Up Down Calib ")]
        [ReadOnly(true)]
        public TransformParams RightTransform { get; set; }

        #endregion

        #region right height calib


        /// <summary>
        /// Up GT 标定高度
        /// </summary>
        [Category("Right Height Calib ")]
        public PosXYZ RightHeightCalibGtPos { get; set; }

        /// <summary>
        /// GT1 标定高度
        /// </summary>
        [Category("Right Height Calib ")]
        public PosXYZ RightHeightCalibGt1Pos { get; set; }

        /// <summary>
        /// GT2 标定高度
        /// </summary>
        [Category("Right Height Calib ")]
        public PosXYZ RightHeightCalibGt2Pos { get; set; }



        /// <summary>
        /// 上标定平面 GT平面
        /// </summary>
        [Category("Right Height Calib ")]
        public PlaneParams RightUpStandardPlaneGT { get; set; }


        /// <summary>
        /// 下标定平面 GT1 平面
        /// </summary>
        [Category("Right Height Calib ")]
        public PlaneParams RightDownStandardPlaneGT1 { get; set; }


        /// <summary>
        /// 标定块高度
        /// </summary>
        [Category("Right Height Calib ")]
        public PosXYZ RightHeightStandard { get; set; }

        /// <summary>
        /// GT2 读数系统误差
        /// </summary>

        [Category("Right Height Calib "), Description("GT2倾斜高度偏差")]
        public PosXYZ RightGt2ZOffset { get; set; }

        #endregion
      
        public override bool CheckIfNormal()
        {
            var prop = this.GetType().GetProperties();

            var nullCount = from p in prop where p == null select p;
            if (nullCount.Any())
            {
                return false;
            }

            if (LeftGtOffset.DistanceTo(new PosXYZ()) > 30 || RightGtOffset.DistanceTo(new PosXYZ()) > 30)
            {
                return false;
            }
            return true;
        }
    }
}