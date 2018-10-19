using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lead.Detect.PrimCameraFocalSpec.FocalSpecCore
{
    public class FSCoreConfig
    {
        public FSCoreConfig()
        {
            LedPulseWidth = FSCoreDefines.DefaultPulseWidth;
            MaxLedPulseWidth = FSCoreDefines.DefaultMaxPulseWidth;
            Freq = FSCoreDefines.DefaultTriggerFrequency;
            IpAddress = FSCoreDefines.DefaultIpAddress;
            ZCalibrationFile = FSCoreDefines.DefaultZCalibrationFile;
            XCalibrationFile = FSCoreDefines.DefaultXCalibrationFile;
            UiWindowHeight = FSCoreDefines.DefaultUiWindowSize;
            IsAgcEnabled = FSCoreDefines.DefaultAgcState;
            AgcTargetIntensity = FSCoreDefines.DefaultAgcTargetIntensity;
            SelectedLayerConfig = ExportLayer.All;
            TrigInterval = 0.1F;
            IsXAxisTrige = false;
            IsExternalPulsingEnabled = false;
        }

        private bool GetCalibrationProperties(string calibrationFile, int varMin, int varMax, out double calibMin, out double calibMax, out double avgGain)
        {
            calibMin = double.MaxValue;
            calibMax = double.MinValue;
            try
            {
                var lines = File.ReadAllLines(calibrationFile);
                var gain = new List<double>();

                foreach (var line in lines)
                {
                    var coeffs = line.Split(';').Skip(1).Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToList();

                    gain.Add(coeffs[1]);

                    var minCandidate = Math.Min(Horner(coeffs, varMin), Horner(coeffs, varMax));
                    var maxCandidate = Math.Max(Horner(coeffs, varMin), Horner(coeffs, varMax));

                    calibMin = minCandidate < calibMin ? minCandidate : calibMin;
                    calibMax = maxCandidate > calibMax ? maxCandidate : calibMax;
                }

                avgGain = gain.Average();
            }
            catch (Exception )
            {
                avgGain = 0.0;
                return false;
            }

            return true;
        }

        private double Horner(List<double> coeffs, double x)
        {
            double s = 0.0f;

            for (var i = coeffs.Count - 1; i >= 0; i--) s = s * x + coeffs[i];
            return s;
        }

        #region Private Members

        private string xCalibrationFile;
        private string zCalibrationFile;

        #endregion

        #region Public Property

        public int LedPulseWidth { get; set; }
        public int MaxLedPulseWidth { get; set; }
        public int Freq { get; set; }
        public bool IsExternalPulsingEnabled { get; set; } //是否启用外部触发
        public string IpAddress { get; set; }
        public bool IsAgcEnabled { get; set; }
        public bool IsXAxisTrige { get; set; }
        public int UiWindowHeight { get; set; }
        public float AgcTargetIntensity { get; set; }
        public float TrigInterval { get; set; }
        public ExportLayer SelectedLayerConfig { get; set; }

        public float RegPulseWidthFloat { get; set; }
        public int PeakThreshold { get; set; }
        public int PeakFirLength { get; set; }
        public int RegPulseDivider { get; set; }
        public double Gain { get; set; }
        public int ImageHeight { get; set; }
        public int ImageOffsetX { get; set; }
        public int ImageOffsetY { get; set; }
        public int ImageHeightZeroPosition { set; get; }
        public float FlipXX0 { get; set; }
        public int FlipXEnabled { get; set; }
        public int Reordering { get; set; }
        public float DetectMissingFirstLayer { get; set; }
        public float FillGapXMax { get; set; }
        public float AverageZFilterSize { get; set; }
        public float AverageIntensityFilterSize { get; set; }
        public float ResampleLineXResolution { get; set; } //No Param

        public string ZCalibrationFile
        {
            get { return zCalibrationFile; }
            set
            {
                if (value != null)
                {
                    double min, max, avgGain;

                    // Due to nature of the optics, we need to calculate average pixel width [mm].
                    var CalibZresult = GetCalibrationProperties(value, 0, FSCoreDefines.SensorWidth - 1, out min, out max, out avgGain);
                    if (CalibZresult)
                    {
                        AveragePixelHeight = avgGain;
                        zCalibrationFile = value;
                    }
                    else
                    {
                        zCalibrationFile = null;
                    }
                }
                else
                {
                    zCalibrationFile = value;
                }
            }
        }

        public string XCalibrationFile
        {
            get { return xCalibrationFile; }
            set
            {
                if (value != null)
                {
                    double min, max, avgGain;

                    // Due to nature of the optics, we need to calculate actual min. X [mm], max. X [mm] and average pixel width [mm] from the calibration data of the sensor at hand.
                    var CalibXresult = GetCalibrationProperties(value, 0, FSCoreDefines.SensorWidth - 1, out min, out max, out avgGain);
                    if (CalibXresult)
                    {
                        OpticalProfileMinX = min;
                        OpticalProfileMaxX = max;
                        AveragePixelWidth = avgGain;
                        xCalibrationFile = value;
                    }
                    else
                    {
                        xCalibrationFile = null;
                    }
                }
                else
                {
                    xCalibrationFile = value;
                }
            }
        }

        #endregion

        #region Private Property

        public bool IsZCalibrationDataSet => !string.IsNullOrEmpty(ZCalibrationFile);

        public bool IsXCalibrationDataSet => !string.IsNullOrEmpty(XCalibrationFile);

        public double AveragePixelWidth { get; set; }

        public double AveragePixelHeight { get; set; }

        public double OpticalProfileMinX { get; set; }

        public double OpticalProfileMaxX { get; set; }

        #endregion
    }
}