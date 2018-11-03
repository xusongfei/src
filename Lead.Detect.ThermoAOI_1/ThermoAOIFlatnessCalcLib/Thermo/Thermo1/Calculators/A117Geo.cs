namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1.Calculators
{
    public enum A117Geo
    {
        /// <summary>
        /// J - 0 (0.04) - pedestal flatness (datum A)    (gt1)
        /// </summary>
        J,
        /// <summary>
        /// M - 6 (+-0.1) - vc down surface profile to datum A    (gt1)
        /// </summary>
        M,



        /// <summary>
        /// K - 10.5 (+-0.2) - vc up surface profile to datum A    (gt)
        /// </summary>
        K,
        /// <summary>
        /// L - 0.26 - parallelism to datum A    (gt)
        /// </summary>
        L,



        /// <summary>
        /// AB - 71.50(+-0.2) - fin up surface profile to datum A    (gt)
        /// </summary>
        AB,



        /// <summary>
        /// O - 0 (+-0.15) - inner standoff profile to datum A    (gt2)
        /// </summary>
        O,
        /// <summary>
        /// N - 5.33 (+-0.15) - outer standoff profile to datum A    (gt2)
        /// </summary>
        N,
    }
}