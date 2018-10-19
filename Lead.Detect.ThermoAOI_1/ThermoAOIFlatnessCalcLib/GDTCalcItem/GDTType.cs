namespace Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem
{
    public enum GDTType
    {
        None,


        /// <summary>
        /// FORM 直线度
        /// </summary>
        Straightness,
        /// <summary>
        /// FORM 平面度
        /// </summary>
        Flatness,
        /// <summary>
        /// FORM 圆度
        /// </summary>
        Circularity,
        /// <summary>
        /// FORM 圆柱度
        /// </summary>
        Cylindricity,


        /// <summary>
        /// PROFILE 线轮廓度 （SOMTIMES USED A DATUM）
        /// </summary>
        ProfileOfLine,
        /// <summary>
        /// PROFILE 面轮廓度 （SOMTIMES USED A DATUM）
        /// </summary>
        ProfileOfSurface,


        /// <summary>
        /// ORIENTATION 倾斜度 （ALWAYS USED A DATUM）
        /// </summary>
        Angularity,
        /// <summary>
        /// ORIENTATION 垂直度 （ALWAYS USED A DATUM）
        /// </summary>
        Perpendiruclarity,
        /// <summary>
        /// ORIENTATION 平行度 （ALWAYS USED A DATUM）
        /// </summary>
        Parallelism,

        /// <summary>
        /// LOCATION 位置度 （ALWAYS USED A DATUM）
        /// </summary>
        Position,
        /// <summary>
        /// LOCATION 同轴度 （ALWAYS USED A DATUM）
        /// </summary>
        Concentricity,
        /// <summary>
        /// LOCATION 对称度 （ALWAYS USED A DATUM）
        /// </summary>
        Symmetry,


        /// <summary>
        /// RUNOUT 圆跳动 （ALWAYS USED A DATUM）
        /// </summary>
        CircularRunout,
        /// <summary>
        /// RUNOUT 全跳动 （ALWAYS USED A DATUM）
        /// </summary>
        TotalRunout,
    }
}