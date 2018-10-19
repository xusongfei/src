namespace Lead.Detect.PrimCameraFocalSpec.FocalSpecCore
{
    public class FSCoreDefines
    {
        /// <summary>
        ///     The width of the FocalSpec sensor in px.
        /// </summary>
        public const int SensorWidth = 2048;

        /// <summary>
        ///     Average width of one pixel in um
        /// </summary>
        public const double PixelWidth = 5.5;

        /// <summary>
        ///     Max. FocalSpec sensor height [px].
        /// </summary>
        public const int MaxSensorHeight = 1088;

        /// <summary>
        ///     Default LED pulse width in µs.
        /// </summary>
        public const int DefaultPulseWidth = 20;

        /// <summary>
        ///     Default max. LED pulse width in µs used by the Automatic Gain Control (AGC) if supported by the HW.
        /// </summary>
        public const int DefaultMaxPulseWidth = 5000;

        /// <summary>
        ///     UI update interval in µs.
        /// </summary>
        public const int UiUpdateInterval = 200;

        /// <summary>
        ///     The maximum number of point clouds in the application reception queue.
        /// </summary>
        public const int QueueMaxLength = 1;

        /// <summary>
        ///     Maximum UI window height in mm.
        /// </summary>
        public const int MinUiWindowSize = 1;

        /// <summary>
        ///     Minimum UI window height in mm.
        /// </summary>
        public const int MaxUiWindowSize = 20;

        /// <summary>
        ///     Default UI window height in mm.
        /// </summary>
        public const int DefaultUiWindowSize = 3;

        /// <summary>
        ///     Default sensor trigger frequency in Hz.
        /// </summary>
        public const int DefaultTriggerFrequency = 300;

        /// <summary>
        ///     Sensor trigger frequency value when using external triggering.
        /// </summary>
        public const int ExternalTriggering = 0;

        /// <summary>
        ///     Path to the application settings file.
        /// </summary>
        public const string ApplicationSettingsFile = "GuiExampleSettings.json";

        /// <summary>
        ///     Micrometer unit.
        /// </summary>
        public const int PeakUnitMicrometer = 1;

        /// <summary>
        ///     Sensor discovery timeout in ms.
        /// </summary>
        public const int SensorDiscoveryTimeout = 2000;

        /// <summary>
        ///     Default IPv4 address of the sensor.
        /// </summary>
        public const string DefaultIpAddress = null;

        /// <summary>
        ///     Default Z calibration file.
        /// </summary>
        public const string DefaultZCalibrationFile = null;

        /// <summary>
        ///     Default X calibration file.
        /// </summary>
        public const string DefaultXCalibrationFile = null;

        /// <summary>
        ///     File externsion for the calibration data files.
        /// </summary>
        public const string CalibrationFileExt = ".dat";

        /// <summary>
        ///     Scale to convert profile data from [µm] to [mm].
        /// </summary>
        public const double ProfileScale = 1.0 / 1000.0;

        /// <summary>
        ///     Min. target intensity for good profile signal. This depends on the target.
        /// </summary>
        public const float TargetIntensityMin = 70.0f;

        /// <summary>
        ///     Max. target intensity for good profile signal. This depends on the target.
        /// </summary>
        public const float TargetIntensityMax = 90.0f;

        /// <summary>
        ///     AGC target grayscale intensity.
        /// </summary>
        public const float DefaultAgcTargetIntensity = 80.0f;

        /// <summary>
        ///     AGC target intensity margin for UI controls.
        /// </summary>
        public const float AgcMargin = 10.0f;

        /// <summary>
        ///     Default number of profile in a batch.
        /// </summary>
        public const int DefaultBatchLength = 5000;

        /// <summary>
        ///     Default sensor trigger mode in batch.
        /// </summary>
        public const TriggerMode DefaultBatchTriggerMode = TriggerMode.External;

        /// <summary>
        ///     Default line speed [m/min] in batch mode when internal sensor triggering is used.
        /// </summary>
        public const double DefaultLineSpeedInBatch = 1.0;

        /// <summary>
        ///     Default scan step length [mm] in batch mode when external sensor triggering is used.
        /// </summary>
        public const double DefaultScanStepLength = 0.5;

        /// <summary>
        ///     Default AGC state.
        /// </summary>
        public const bool DefaultAgcState = false;
    }

    public enum TriggerMode
    {
        /// <summary>
        ///     Sensor sampling frequency is constant.
        /// </summary>
        Internal,

        /// <summary>
        ///     Sensor sampling frequency is triggered by an external device.
        /// </summary>
        External
    }

    public enum ExportLayer
    {
        /// <summary>
        ///     Full data.
        /// </summary>
        All,

        /// <summary>
        ///     Layer with min. distance to the sensor.
        /// </summary>
        Top,

        /// <summary>
        ///     Layer with max. distance to the sensor.
        /// </summary>
        Bottom,

        /// <summary>
        ///     Layer with max intensity.
        /// </summary>
        Brightest
    }
}