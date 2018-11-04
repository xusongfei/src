using System;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;

namespace Lead.Detect.ThermoAOI.Calibration
{
    public class GTTransform
    {
        /// <summary>
        /// 单个GT坐标系转换<para/> 
        /// |--gtRaw--|----gtWork--|<para/> 
        /// 转换gt读数到标定点为原点的坐标系<para/> 
        /// </summary>
        /// <param name="calibWorkPos"></param>
        /// <param name="calibRaw"></param>
        /// <param name="gtWork"></param>
        /// <param name="gtRaw"></param>
        /// <param name="calibOrigin"></param>
        /// <returns></returns>
        public static double TransGT(double gtWork, double gtRaw, double calibWorkPos, double calibRaw, double calibOrigin = 0d)
        {
            return calibOrigin + (gtRaw - calibRaw) - (gtWork - calibWorkPos);
        }


        ///  <summary>
        ///  两个GT间坐标系转换<para/> 
        /// 
        ///  同向<para/>
        ///   |----gt2Work--|--gt2Raw--|<para/> 
        ///   |----gt1Work--|----gt1Raw--|<para/> 
        ///  反向<para/> 
        ///   |---gt2Work--|--gt2Raw--|--------inner-------------|--gt1Raw--|----gt1Work--|<para/> 
        ///  
        ///   |---gt2Raw--|--gt2Work--|--------outer-------------|--gt1Work--|----gt1Raw--|<para/> 
        ///  </summary>
        ///  <param name="gtWork"></param>
        ///  <param name="gtRaw"></param>
        ///  <param name="gtCalibWork"></param>
        ///  <param name="gtCalibRaw"></param>
        ///  <param name="calibWork1Pos"></param>
        ///  <param name="calib1Raw"></param>
        ///  <param name="calibOrigin"></param>
        ///  <param name="dir"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static double TransGT2ToGT1(
            double gtWork, double gtRaw,
            double gtCalibWork, double gtCalibRaw,
            double calibOrigin = 0, bool dir = true, bool inner = true)
        {
            if (dir)
            {
                //gt1、gt2同向
                if (inner)
                {
                    //增尺寸 gt读数大 尺寸大
                    //gt1、gt2同向 坐标转换
                    // calibHeight = -gt2CalibWork + gt2CalibRaw - (-gt1CalibWork + gt1CalibRaw) + offset
                    // newHeight = -gt2Work + gt2Raw - (-gt1CalibWork + gt1CalibRaw) + offset
                    var work2Offset = gtWork - gtCalibWork;
                    var raw2Offset = gtRaw - gtCalibRaw;

                    return calibOrigin + raw2Offset - work2Offset;
                }
                else
                {
                    //减尺寸 gt读数大 尺寸小
                    // calibHeight = gt1CalibWork - gt1CalibRaw + gt2CalibWork - gt2CalibRaw + offset
                    var work2Offset = gtWork - gtCalibWork;
                    var raw2Offset = gtRaw - gtCalibRaw;

                    return calibOrigin - raw2Offset + work2Offset;
                }
            }
            else
            {
                //gt1、gt2反向  gt2坐标转换
                if (inner)
                {
                    // calibHeight = gt1CalibRaw - gt1CalibWork + gt2CalibRaw - gt2CalibWork + offset
                    // newHeight = gt1CalibRaw - gt1CalibWork + gt2Raw - gt2Work + offset
                    var work2Offset = gtWork - gtCalibWork;
                    var raw2Offset = gtRaw - gtCalibRaw;

                    return (calibOrigin + raw2Offset - work2Offset) * -1;
                }
                else
                {
                    // calibHeight = gt1CalibWork - gt1CalibRaw + gt2CalibWork - gt2CalibRaw + offset

                    var work2Offset = gtWork - gtCalibWork;
                    var raw2Offset = gtRaw - gtCalibRaw;

                    return (calibOrigin - raw2Offset + work2Offset) * -1;
                }
            }
        }


        /// <summary>
        /// 转换当前点gt到产品坐标系
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="calib"></param>
        /// <param name="gtPos"></param>
        /// <returns></returns>
        public static double TransGtRaw(string platform, CalibrationConfig calib, PosXYZ gtPos)
        {
            var gtWork = gtPos.OffsetZ;
            var gtRaw = gtPos.Z;
            var gtDesc = gtPos.Description;
            var gtWorkX = gtPos.X;
            var gtWorkY = gtPos.Y;

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
            var gtStandardHeight = 0d;
            var gtDirection = false;

            if (platform == "LeftStation")
            {
                if (gt == 0)
                {
                    gtCalibWork = calib.LeftHeightCalibGtPos.Z;
                    gtCalibRaw = calib.LeftUpStandardPlaneGT.CalcZ(gtWorkX, gtWorkY);
                    gtStandardHeight = calib.LeftHeightStandard.Z;
                    gtDirection = false;
                }
                else if (gt == 1)
                {
                    gtCalibWork = calib.LeftHeightCalibGt1Pos.Z;
                    gtCalibRaw = calib.LeftDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtStandardHeight = 0;
                    gtDirection = true;
                }
                else if (gt == 2)
                {
                    //gt2 convert
                    gtStandardHeight = 0;
                    var gtCalibWork2 = calib.LeftHeightCalibGt2Pos.Z;
                    var gtCalibRaw2 = calib.LeftDownStandardPlaneGT2.CalcZ(gtWorkX, gtWorkY);
                    var gt1OffsetZ = calib.LeftDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY); ;
                    gtDirection = true;
                    var gt2Raw = GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork2, gtCalibRaw2, gtStandardHeight, gtDirection) + gt1OffsetZ;

                    //gt1 convert
                    gtCalibWork = calib.LeftHeightCalibGt1Pos.Z;
                    gtCalibRaw = calib.LeftDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtStandardHeight = 0;

                    gtWork = calib.LeftHeightCalibGt1Pos.Z;
                    gtRaw = gt2Raw;

                    return GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork, gtCalibRaw, gtStandardHeight, gtDirection) + calib.LeftGt2ZOffset.OffsetZ;
                }
            }
            else if (platform == "RightStation")
            {
                if (gt == 0)
                {
                    gtCalibWork = calib.RightHeightCalibGtPos.Z;
                    gtCalibRaw = calib.RightUpStandardPlaneGT.CalcZ(gtWorkX, gtWorkY);
                    gtStandardHeight = calib.RightHeightStandard.Z;
                    gtDirection = false;
                }
                else if (gt == 1)
                {
                    gtCalibWork = calib.RightHeightCalibGt1Pos.Z;
                    gtCalibRaw = calib.RightDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtStandardHeight = 0;
                    gtDirection = true;
                }
                else if (gt == 2)
                {
                    //gtStandardHeight = 0;
                    //gtDirection = true;
                    //var gt2Raw = calib.RightHeightCalibGt1Pos.OffsetZ +
                    //             GTTransform.TransGT2ToGT1(gtWork, gtRaw, calib.RightHeightCalibGt2Pos.Z, calib.RightHeightCalibGt2Pos.OffsetZ, gtStandardHeight, gtDirection);

                    gtStandardHeight = 0;
                    var gtCalibWork2 = calib.RightHeightCalibGt2Pos.Z;
                    var gtCalibRaw2 = calib.RightDownStandardPlaneGT2.CalcZ(gtWorkX, gtWorkY);
                    var gt1OffsetZ = calib.RightDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY); ;
                    gtDirection = true;
                    var gt2Raw = GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork2, gtCalibRaw2, gtStandardHeight, gtDirection) + gt1OffsetZ;
                  

                    gtCalibWork = calib.RightHeightCalibGt1Pos.Z;
                    gtCalibRaw = calib.RightDownStandardPlaneGT1.CalcZ(gtWorkX, gtWorkY);
                    gtStandardHeight = 0;


                    gtWork = calib.RightHeightCalibGt1Pos.Z;
                    gtRaw = gt2Raw;

                    return GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork, gtCalibRaw, gtStandardHeight, gtDirection) + calib.RightGt2ZOffset.OffsetZ;
                }
            }
            else
            {
                throw new Exception("Platform Error");
            }

            return GTTransform.TransGT2ToGT1(gtWork, gtRaw, gtCalibWork, gtCalibRaw, gtStandardHeight, gtDirection);
        }


        /// <summary>
        /// 转换GT读数到产品坐标系
        /// </summary>
        /// <param name="station"></param>
        /// <param name="calib"></param>
        /// <param name="productData"></param>
        public static void TransformRawData(string station, CalibrationConfig calib, Thermo1Product productData)
        {
            foreach (var p in productData.RawDataUp)
            {
                p.Z = TransGtRaw(station, calib, p);
            }

            foreach (var p in productData.RawDataDown)
            {
                p.Z = TransGtRaw(station, calib, p);
            }
        }
    }
}