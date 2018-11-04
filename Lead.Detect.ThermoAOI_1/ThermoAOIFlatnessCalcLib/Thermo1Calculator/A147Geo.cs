namespace Lead.Detect.ThermoAOIProductLib.Thermo1Calculator
{
    public enum A147Geo
    {
        /// <summary>
        /// AW - 71.50(+-0.2) - fin up surface profile to datum A
        /// </summary>
        AW,

        /// <summary>
        /// W - 10.5 (+-0.2) - vc up surface profile to datum A
        /// </summary>
        W,

        /// <summary>
        /// X - 0.26 - parallelism to datum A
        /// </summary>
        X,

        /// <summary>
        /// Y - 0 (0.04) - pedestal1 flatness (datum A)
        /// </summary>
        Y,

        /// <summary>
        /// T - 0 (0.10) - pedestal2 profile to datum A
        /// </summary>
        T,

        /// <summary>
        /// S - 0 (0.04) - pedestal2 parallelism to datum A
        /// </summary>
        S,

        /// <summary>
        /// V - 6 (+-0.10) - vc down surface profile to datum A
        /// </summary>
        V,

        /// <summary>
        /// AG - 0 (+-0.15) - inner standoff profile to datum A
        /// </summary>
        AG,

        /// <summary>
        /// AH - 5.33 (+-0.15) - outer standoff profile to datum A
        /// </summary>
        AH,
    }
}