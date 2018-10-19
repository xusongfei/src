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
    }
}