using System;
using System.ComponentModel;
using System.Linq;
using g3;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.platforms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOI.Machine;
using Lead.Detect.ThermoAOI.Machine.Common;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;

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

        [Obsolete]
        [Category("Left Up XY Calib ")]
        [ReadOnly(true)]
        public PosXYZ LeftOrigin { get; set; }

        /// <summary>
        /// 产品坐标到上平台转换
        /// </summary>
        [Category("Left Up XY Calib ")]
        [ReadOnly(true)]
        public TransformParams LeftUpTransform { get; set; }

        [Obsolete]
        [Category("Left Up XY Calib ")]
        public bool LeftUpTransformEnable { get; set; } = false;


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

        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightCalibGtPos { get; set; }

        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightCalibGt1Pos { get; set; }

        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightCalibGt2Pos { get; set; }


        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightGt { get; set; }

        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightGt1 { get; set; }

        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightGt2 { get; set; }


        [Category("Left Height Calib ")]
        public PlaneParams LeftUpStandardPlaneGT { get; set; }

        [Category("Left Height Calib ")]
        public PlaneParams LeftDownStandardPlaneGT1 { get; set; }


        [Category("Left Height Calib ")]
        public PosXYZ LeftHeightStandard { get; set; }


        [Category("Left Height Calib "), Description("GT2倾斜高度偏差")]
        public PosXYZ LeftGt2ZOffset { get; set; }

        #endregion


        #region right platform calib

        [Obsolete]
        [Category("Right Up XY Calib ")]
        [ReadOnly(true)]
        public PosXYZ RightOrigin { get; set; }

        [Category("Right Up XY Calib ")]
        [ReadOnly(true)]
        public TransformParams RightUpTransform { get; set; }

        [Obsolete]
        [Category("Right Up XY Calib ")]
        public bool RightUpTransformEnable { get; set; } = false;

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

        [Category("Right Height Calib ")]
        public PosXYZ RightHeightCalibGtPos { get; set; }

        [Category("Right Height Calib ")]
        public PosXYZ RightHeightCalibGt1Pos { get; set; }

        [Category("Right Height Calib ")]
        public PosXYZ RightHeightCalibGt2Pos { get; set; }


        [Category("Right Height Calib ")]
        public PosXYZ RightHeightGt { get; set; }

        [Category("Right Height Calib ")]
        public PosXYZ RightHeightGt1 { get; set; }

        [Category("Right Height Calib ")]
        public PosXYZ RightHeightGt2 { get; set; }


        [Category("Right Height Calib ")]
        public PlaneParams RightUpStandardPlaneGT { get; set; }

        [Category("Right Height Calib ")]
        public PlaneParams RightDownStandardPlaneGT1 { get; set; }


        [Category("Right Height Calib ")]
        public PosXYZ RightHeightStandard { get; set; }


        [Category("Right Height Calib "), Description("GT2倾斜高度偏差")]
        public PosXYZ RightGt2ZOffset { get; set; }

        #endregion


        /// <summary>
        /// 转换 治具坐标系点 到 上平台 xy 坐标系 点
        /// </summary>
        /// <param name="calib"></param>
        /// <param name="platform"></param>
        /// <param name="productpos"></param>
        /// <param name="gtWorkZ"></param>
        /// <param name="gt">下gt索引</param>
        /// <returns></returns>
        public static PosXYZ TransformToPlatformPos(CalibrationConfig calib, PlatformType platform, PosXYZ productpos, double gtWorkZ, int gt = 1)
        {
            switch (platform)
            {
                case PlatformType.LUp:
                    {
                        var pos = productpos;
                        pos = XyzPlarformCalibration.AffineTransform(pos, calib.LeftUpTransform);
                        //if (calib.LeftUpTransformEnable)
                        //{
                        //    pos = XyzPlarformCalibration.AffineTransform(pos, calib.LeftUpTransform);
                        //}
                        //else
                        //{
                        //    pos = productpos + calib.LeftOrigin;
                        //}
                        pos.Z = gtWorkZ;
                        return pos;
                    }
                case PlatformType.LDown:
                    {
                        //trans to up
                        var pos = productpos;
                        pos = XyzPlarformCalibration.AffineTransform(pos, calib.LeftUpTransform);

                        //trans to down
                        if (gt == 1)
                        {
                            var transPos = XyzPlarformCalibration.AffineTransform(pos, calib.LeftTransform);
                            transPos.Z = gtWorkZ;
                            return transPos;
                        }
                        else if (gt == 2)
                        {
                            var transPos = XyzPlarformCalibration.AffineTransform(pos, calib.LeftTransform) + calib.LeftGtOffset;
                            transPos.Z = gtWorkZ;
                            return transPos;
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                case PlatformType.RUp:
                    {
                        var pos = productpos;
                        pos = XyzPlarformCalibration.AffineTransform(pos, calib.RightUpTransform);
                        //if (calib.RightUpTransformEnable)
                        //{
                        //    pos = XyzPlarformCalibration.AffineTransform(pos, calib.RightUpTransform);
                        //}
                        //else
                        //{
                        //    pos = productpos + calib.RightOrigin;
                        //}
                        pos.Z = gtWorkZ;
                        return pos;
                    }
                case PlatformType.RDown:
                    {
                        //trans to up
                        var pos = productpos;
                        pos = XyzPlarformCalibration.AffineTransform(pos, calib.RightUpTransform);

                        //trans to down
                        if (gt == 1)
                        {
                            var transPos = XyzPlarformCalibration.AffineTransform(pos, calib.RightTransform);
                            transPos.Z = gtWorkZ;
                            return transPos;
                        }
                        else if (gt == 2)
                        {
                            var transPos = XyzPlarformCalibration.AffineTransform(pos, calib.RightTransform) + calib.RightGtOffset;
                            transPos.Z = gtWorkZ;
                            return transPos;
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
            }

            throw new Exception("Platform not supported");
        }


        [Obsolete]
        public static double TransformUpDownHeight(PlatformType platform, CalibrationConfig calib, double upGtWorkPos, double downGtWorkPos, double upGtRawHeight, double downGtRawHeight, int gt = 1)
        {
            PosXYZ calibGtPos;
            PosXYZ calibDownGtPos;

            PosXYZ calibUpGt;
            PosXYZ calibDownGt;

            PosXYZ calibHeightStandard;


            if (platform == PlatformType.LTrans)
            {
                calibGtPos = calib.LeftHeightCalibGtPos;
                calibDownGtPos = calib.LeftHeightCalibGt1Pos;

                calibUpGt = calib.LeftHeightGt;
                calibDownGt = calib.LeftHeightGt1;

                calibHeightStandard = calib.LeftHeightStandard;
            }
            else if (platform == PlatformType.RTrans)
            {
                calibGtPos = calib.RightHeightCalibGtPos;
                calibDownGtPos = calib.RightHeightCalibGt1Pos;

                calibUpGt = calib.RightHeightGt;
                calibDownGt = calib.RightHeightGt1;

                calibHeightStandard = calib.RightHeightStandard;
            }
            else
            {
                throw new Exception("Platform Error");
            }

            var upoffset = upGtWorkPos - calibGtPos.Z;
            var downoffset = downGtWorkPos - calibDownGtPos.Z;
            var gtUpOffset = upGtRawHeight - calibUpGt.Z;
            var gtDownOffset = downGtRawHeight - calibDownGt.Z;
            return calibHeightStandard.Z - upoffset - downoffset + gtUpOffset + gtDownOffset;
        }


        [Obsolete]
        public static double TransformUpDownHeight(PlatformType platform, CalibrationConfig calib, double upGtWorkPos, double downGtWorkPos, PosXYZ upGtRaw, OrthogonalPlaneFit3 pedestalPlane,
            int gt = 1)
        {
            PosXYZ calibUpGtPos;
            PosXYZ calibDownGtPos;

            PlaneParams calibUpGtPlane;
            PlaneParams calibDownGtPlane;

            PosXYZ calibHeightStandard;


            if (platform == PlatformType.LTrans)
            {
                calibUpGtPos = calib.LeftHeightCalibGtPos;
                calibDownGtPos = calib.LeftHeightCalibGt1Pos;

                calibUpGtPlane = calib.LeftUpStandardPlaneGT;
                calibDownGtPlane = calib.LeftDownStandardPlaneGT1;

                calibHeightStandard = calib.LeftHeightStandard;
            }
            else if (platform == PlatformType.RTrans)
            {
                calibUpGtPos = calib.RightHeightCalibGtPos;
                calibDownGtPos = calib.RightHeightCalibGt1Pos;

                calibUpGtPlane = calib.RightUpStandardPlaneGT;
                calibDownGtPlane = calib.RightDownStandardPlaneGT1;

                calibHeightStandard = calib.RightHeightStandard;
            }
            else
            {
                throw new Exception("Platform Error");
            }

            var upWorkOffset = upGtWorkPos - calibUpGtPos.Z;
            var downWorkOffset = downGtWorkPos - calibDownGtPos.Z;

            var gtUpOffset = upGtRaw.Z - calibUpGtPlane.CalcZ(upGtRaw.X, upGtRaw.Y);
            var gtDownOffset = calibUpGtPlane.CalcZ(upGtRaw.X, upGtRaw.Y) - calibDownGtPlane.CalcZ(upGtRaw.X, upGtRaw.Y);

            var height = new Vector3d(0, 0, calibHeightStandard.Z - upWorkOffset - downWorkOffset + gtUpOffset + gtDownOffset);
            return height.Dot(pedestalPlane.Normal);
        }


        public static double TransGtRaw(PlatformType platform, CalibrationConfig calib, double gtWork, double gtRaw, string gtDesc, double gtWorkX, double gtWorkY)
        {
            var gt = 0;
            switch (gtDesc)
            {
                case "GT":
                    gt = 0;
                    break;
                case "GT1":
                    gt = 1;
                    break;
                case "GT2":
                    gt = 2;
                    break;
                default:
                    throw new Exception("gt Error");
            }

            var gtCalibWork = 0d;
            var gtCalibRaw = 0d;
            var gtHeight = 0d;
            var gtDirection = false;

            if (platform == PlatformType.LTrans)
            {
                if (gt == 0)
                {
                    gtCalibWork = calib.LeftHeightCalibGtPos.Z;
                    //gtCalibRaw = calib.LeftHeightCalibGtPos.OffsetZ;
                    //gtHeight = calib.LeftHeightStandard.OffsetZ;
                    gtCalibRaw = calib.LeftUpStandardPlaneGT.CalcZ(gtWorkX, gtWorkY);
                    gtHeight = calib.LeftHeightStandard.Z;
                    gtDirection = false;
                }
                else if (gt == 1)
                {
                    gtCalibWork = calib.LeftHeightCalibGt1Pos.Z;
                    //gtCalibRaw = calib.LeftHeightCalibGt1Pos.OffsetZ;
                    gtCalibRaw = calib.LeftDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtHeight = 0;
                    gtDirection = true;
                }
                else if (gt == 2)
                {
                    gtHeight = 0;
                    gtDirection = true;
                    var gt2Raw = calib.LeftHeightCalibGt1Pos.OffsetZ + GTTransform.TransGT2ToGT1(gtWork, gtRaw, calib.LeftHeightCalibGt2Pos.Z, calib.LeftHeightCalibGt2Pos.OffsetZ, gtHeight, gtDirection);

                    gtCalibWork = calib.LeftHeightCalibGt1Pos.Z;
                    gtCalibRaw = calib.LeftDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtHeight = 0;
                    gtDirection = true;


                    gtWork = calib.LeftHeightCalibGt1Pos.Z;
                    gtRaw = gt2Raw;

                    return GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork, gtCalibRaw, gtHeight, gtDirection) + calib.LeftGt2ZOffset.OffsetZ;
                }
            }
            else if (platform == PlatformType.RTrans)
            {
                if (gt == 0)
                {
                    gtCalibWork = calib.RightHeightCalibGtPos.Z;
                    //gtCalibRaw = calib.RightHeightCalibGtPos.OffsetZ;
                    //gtHeight = calib.RightHeightStandard.OffsetZ;
                    gtCalibRaw = calib.RightUpStandardPlaneGT.CalcZ(gtWorkX, gtWorkY);
                    gtHeight = calib.RightHeightStandard.Z;
                    gtDirection = false;
                }
                else if (gt == 1)
                {
                    gtCalibWork = calib.RightHeightCalibGt1Pos.Z;
                    //gtCalibRaw = calib.RightHeightCalibGt1Pos.OffsetZ;
                    gtCalibRaw = calib.RightDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtHeight = 0;
                    gtDirection = true;
                }
                else if (gt == 2)
                {
                    gtHeight = 0;
                    gtDirection = true;
                    var gt2Raw = calib.RightHeightCalibGt1Pos.OffsetZ + GTTransform.TransGT2ToGT1(gtWork, gtRaw, calib.RightHeightCalibGt2Pos.Z, calib.RightHeightCalibGt2Pos.OffsetZ, gtHeight, gtDirection);

                    gtCalibWork = calib.RightHeightCalibGt1Pos.Z;
                    gtCalibRaw = calib.RightDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtHeight = 0;
                    gtDirection = true;


                    gtWork = calib.RightHeightCalibGt1Pos.Z;
                    gtRaw = gt2Raw;

                    return GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork, gtCalibRaw, gtHeight, gtDirection) + calib.RightGt2ZOffset.OffsetZ;
                }
            }
            else
            {
                throw new Exception("Platform Error");
            }

            return GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork, gtCalibRaw, gtHeight, gtDirection);
        }

        public static void TransformRawData(PlatformType platformType, CalibrationConfig cfgCalib, Thermo1Product productTestData)
        {
            foreach (var p in productTestData.RawDataUp)
            {
                p.Z = CalibrationConfig.TransGtRaw(platformType, cfgCalib, p.OffsetZ, p.Z, p.Description, p.X, p.Y);
            }

            foreach (var p in productTestData.RawDataDown)
            {
                p.Z = CalibrationConfig.TransGtRaw(platformType, cfgCalib, p.OffsetZ, p.Z, p.Description, p.X, p.Y);
            }
        }

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