using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using FocalSpec.FsApiNet.Model;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimCameraFocalSpec.FocalSpecCore
{
    public class FSCoreModel
    {
        public FSCoreModel(FSCoreConfig fsCoreConfig)
        {
            _fsCoreConfig = fsCoreConfig;
        }

        public int CurBatchCount => _batchCount;

        private void AppendToPointCloudFile(string file, List<FSPoint> points, double batchStepLength, int index, bool isSaveAll)
        {
            var str = new StringBuilder();
            if (isSaveAll) //列排布4个数保存
            {
                //   int nYCount = 0;
                foreach (var point in points)
                {
                    string row;
                    //   if(nYCount % 3 == 0)
                    {
                        row = string.Format(CultureInfo.InvariantCulture, "{0:0.000000}, {1:0.000000}, {2:0.000000},{3:0.000000}", point.X, point.Y, point.Z,
                            Color.FromArgb(0, point.Intensity, point.Intensity, point.Intensity).ToArgb());
                        str.AppendLine(row);
                    }
                    //       nYCount++;
                }

                File.AppendAllText(file, str.ToString());
            }
            else //每一行批处理结果（散点）就每一行数据
            {
                var row = "";
                //     int nYCount = 0;
                foreach (var point in points)
                    //            if(nYCount%3==0)
                    row += string.Format(CultureInfo.InvariantCulture, "{0:0.000000}, {1:0.000000}, {2:0.000000},", point.X, point.Y, point.Z);
                //          nYCount++;
                if (!string.IsNullOrEmpty(row))
                {
                    row = row.Remove(row.Length - 1, 1);
                    str.AppendLine(row);
                    File.AppendAllText(file, str.ToString());
                }
            }
        }

        public void ClearBatch(int index)
        {
            //if(listBatch !=null && listBatch.Count > 0)
            //{
            //    listBatch.Clear();
            //    listBatch = null;
            //    GC.Collect();
            //}
            //_listBatch = new List<List<FSPoint>>();


            switch (index)
            {
                case 1:
                    _listBatch1.Clear();
                    break;
                case 2:
                    _listBatch2.Clear();
                    break;
                case 3:
                    _listBatch3.Clear();
                    break;
                case 4:
                    _listBatch4.Clear();
                    break;
                case 5:
                    _listBatch5.Clear();
                    break;
                case 6:
                    _listBatch6.Clear();
                    break;
                case 7:
                    _listBatch7.Clear();
                    break;
                case 8:
                    _listBatch8.Clear();
                    _lastLocation = -1;
                    _FirstLocation = -1;
                    _batchCount = 0;
                    break;
                case 99:
                    _listBatch1.Clear();
                    _listBatch2.Clear();
                    _listBatch3.Clear();
                    _listBatch4.Clear();
                    _listBatch5.Clear();
                    _listBatch6.Clear();
                    _listBatch7.Clear();
                    _listBatch8.Clear();
                    _lastLocation = -1;
                    _FirstLocation = -1;
                    _batchCount = 0;
                    break;
            }
        }

        public void Close()
        {
            _dataRequest = false;
            _lastLocation = -1;
            _batchCount = 0;

            if (string.IsNullOrEmpty(_cameraId)) return;

            _fsCore.StopGrabbing(_cameraId);

            Thread.Sleep(500);

            _fsCore.Close();
        }

        private List<FSPoint> CreatePoints(int num, double xStep, int location, int Count)
        {
            var Points = new List<FSPoint>();

            for (var i = 0; i < num; i++)
            {
                var temp = new FSPoint();
                temp.Location = location;
                temp.Z = 999999 * FSCoreDefines.ProfileScale;

                if (_fsCoreConfig.IsXAxisTrige)
                {
                    temp.X = -(XStartPos + Count * _inervalTemp);
                    temp.Y = -YStartPos + i * xStep * FSCoreDefines.ProfileScale;
                }
                else
                {
                    temp.X = XStartPos + i * xStep * FSCoreDefines.ProfileScale;
                    temp.Y = YStartPos + Count * _inervalTemp;
                }

                Points.Add(temp);
            }

            return Points;
        }

        public void Flush()
        {
            const double timeout = 3000;
            var stopwatch = Stopwatch.StartNew();
            while (!IsCameraBufferEmpty())
                if (stopwatch.Elapsed.TotalMilliseconds > timeout)
                    break;
        }

        public Bitmap GetImage(List<List<FSPoint>> profiles, double pixelWidth)
        {
            pixelWidth *= 1000.0d;

            if (profiles.Count <= 0) return null;

            var imageSize = profiles.Count * FSCoreDefines.SensorWidth;
            var profileIndex = 0;

            var imageR = new byte[imageSize];
            var imageG = new byte[imageSize];
            var imageB = new byte[imageSize];

            double minZ, maxZ;

            GetProfilesMinAndMaxZ(profiles, out minZ, out maxZ);

            foreach (var profile in profiles)
            {
                for (var i = 0; i < profile.Count; i++)
                {
                    var pixelIndex = profileIndex * FSCoreDefines.SensorWidth + Convert.ToInt32(Math.Round(profile[i].Y / pixelWidth));

                    if (pixelIndex < 0 || pixelIndex >= imageSize) continue;

                    var z = (float) profile[i].Z;

                    HsvToRgb((1 - (z - minZ) / (maxZ - minZ)) * byte.MaxValue, out imageR[pixelIndex], out imageG[pixelIndex], out imageB[pixelIndex]);
                }

                profileIndex++;
            }

            return SetImage(imageR, imageG, imageB, FSCoreDefines.SensorWidth, profiles.Count, pixelWidth, 0.1 * 1000);
        }

        public List<int> GetIndexV(float[] zValues, float[] intensityValues, int lineLength, double xStep, FsApi.Header header, int Count)
        {
            var Points = new List<int>();

            for (var i = 0; i < lineLength; i++) Points.Add(Convert.ToInt32(header.Index));
            return Points;
        }

        private List<FSPoint> GetPoints(float[] zValues, float[] intensityValues, int lineLength, double xStep, FsApi.Header header, int Count)
        {
            var Points = new List<FSPoint>();
            var no_meas = FsApi.NoMeasurement - 1;
            //for (int i = 0; i < lineLength; i = i + 3)
            for (var i = 0; i < lineLength; i++)
            {
                if (zValues[i] > no_meas) zValues[i] = 999998;

                var temp = new FSPoint();

                if (_fsCoreConfig.IsXAxisTrige)
                {
                    temp.X = -(XStartPos + Count * _inervalTemp);
                    //should be changed by TriggerTable
                    temp.Y = -YStartPos + i * xStep * FSCoreDefines.ProfileScale;
                }
                else
                {
                    if (_fsCoreConfig.FlipXEnabled == 0)
                    {
                        //正常
                        temp.Y = -(XStartPos + Count * _inervalTemp);
                        temp.X = -YStartPos + i * xStep * FSCoreDefines.ProfileScale;
                    }
                    else if (_fsCoreConfig.FlipXEnabled == 1)
                    {
                        //正常
                        temp.Y = -(XStartPos + Count * _inervalTemp);
                        temp.X = -YStartPos + (2047 - i) * xStep * FSCoreDefines.ProfileScale;
                    }

                    //temp.X = XStartPos + i * xStep * FSCoreDefines.ProfileScale;
                    //temp.Y = YStartPos + _listBatch.Count * _inervalTemp;
                }

                temp.Z = zValues[i] * FSCoreDefines.ProfileScale;
                temp.Intensity = (int) intensityValues[i];
                //temp.Location = header.Location;
                //   temp.Location = _batchCount * 2;
                temp.Location = header.Location;
                temp.index = header.Index;
                Points.Add(temp);
            }

            return Points;
        }

        private void GetProfilesMinAndMaxZ(List<List<FSPoint>> profiles, out double minZ, out double maxZ)
        {
            minZ = double.NaN;
            maxZ = double.NaN;


            if (profiles.Count <= 0) return;

            var min = double.MaxValue;
            var max = double.MinValue;

            for (var profileIndex = 0; profileIndex < profiles.Count; profileIndex++)
            {
                var profile = profiles[profileIndex];
                for (var i = 0; i < profile.Count; i++)
                {
                    var profilePoint = profile[i];
                    if (profilePoint.Z < min)
                        min = profilePoint.Z;
                    if (profilePoint.Z > max)
                        max = profilePoint.Z;
                }
            }

            minZ = min;
            maxZ = max;
        }

        public void HsvToRgb(double h, out byte r, out byte g, out byte b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            var H = h;
            while (H < 0) H += 360;
            ;
            while (H >= 360) H -= 360;
            ;
            double R, G, B;

            {
                var hf = H / 60.0;
                var i = (int) Math.Floor(hf);
                var f = hf - i;
                double pv = 0;
                var qv = 1 - f;
                var tv = f;
                switch (i)
                {
                    // Red is the dominant color

                    case 0:
                        R = 1;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = 1;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = 1;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = 1;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = 1;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = 1;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = 1;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = 1;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = 1; // Just pretend its black/white
                        break;
                }
            }
            r = (byte) (R * 255.0);
            g = (byte) (G * 255.0);
            b = (byte) (B * 255.0);
        }

        public bool InitAndConnect(string camId, ref string errorCode)
        {
            errorCode = "";

            if (!_fsCoreConfig.IsZCalibrationDataSet || !_fsCoreConfig.IsXCalibrationDataSet) return false;

            var result = InitAndConnectCamera(camId);
            if (result != CameraStatusCode.Ok)
            {
                errorCode = "FSConfig Init Fail :" + result;
                return false;
                //           return false; ;
            }

            SetCallBackFuncByExportLayer(_fsCoreConfig.SelectedLayerConfig);
            //IsAgcEnabled = _fsCoreConfig.IsAgcEnabled;
            //fTarIntensity = _fsCoreConfig.AgcTargetIntensity;
            //nMaxLedPulseWidth = _fsCoreConfig.MaxLedPulseWidth;
            //nLedPulseWidth = _fsCoreConfig.LedPulseWidth;
            //IsExTrigger = _fsCoreConfig.IsExternalPulsingEnabled;

            //IsXTrigger = _fsCoreConfig.IsXAxisTrige;
            //Interval = _fsCoreConfig.TrigInterval;

            XStartPos = 0;
            YStartPos = 0;
            ZStartPos = 0;
            //Fre = _fsCoreConfig.Freq;
            return true;
        }

        private CameraStatusCode InitAndConnectCamera(string camId)
        {
            //int cameraCount;
            //List<string> cameraIds;

            _header.ReceptionQueueSize = 0;

            CameraStatusCode cameraStatus;

            //_fsCore = new FsApi();

            //var cameraStatus = _fsCore.Open(out cameraCount, out cameraIds, FSCoreDefines.SensorDiscoveryTimeout);
            //if (cameraStatus != CameraStatusCode.Ok)
            //{
            //    return cameraStatus;
            //}

            //if (cameraCount == 0)
            //{
            //    _fsCore.Close();
            //    return CameraStatusCode.NotConnected;
            //}

            //_cameraId = cameraIds.First();
            //cameraStatus = _fsCore.Connect(_cameraId, _fsCoreConfig.IpAddress);

            _cameraId = camId;
            cameraStatus = _fsCore.Connect(camId, _fsCoreConfig.IpAddress);

            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetProfileCallback(_cameraId, ProfileReceptionCallback);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.ZCalibrationFile, _fsCoreConfig.ZCalibrationFile);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.PeakYUnit, FSCoreDefines.PeakUnitMicrometer);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.XCalibrationFile, _fsCoreConfig.XCalibrationFile);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.PeakXUnit, FSCoreDefines.PeakUnitMicrometer);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.PeakThreshold, 15);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.FirLength, 16);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.Mtu, 9014);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            // Set Interpacket Delay, if needed. By default, it's 20.
            cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.Ifg, 20);
            if (cameraStatus != CameraStatusCode.Ok)
                return cameraStatus;

            cameraStatus = SetAgcEnable(_fsCoreConfig.IsAgcEnabled);
            switch (cameraStatus)
            {
                case CameraStatusCode.CameraErrorHwNotSupported:
                    IsAgcSupported = false;
                    break;
                case CameraStatusCode.Ok:
                    cameraStatus = SetAgcTargetIntensity(_fsCoreConfig.AgcTargetIntensity);
                    if (cameraStatus != CameraStatusCode.Ok) return cameraStatus;
                    IsAgcSupported = true;
                    break;
                default:
                    return cameraStatus;
            }

            cameraStatus = _fsCore.StartGrabbing(_cameraId);
            _dataRequest = false;

            return cameraStatus;
        }

        public bool IsCameraBufferEmpty()
        {
            return _header.ReceptionQueueSize == 0;
        }

        public void Limitvolution(int[] index)
        {
            _listBatchNumber1 = index[0];
            _listBatchNumber2 = index[1];
            _listBatchNumber3 = index[2];
            _listBatchNumber4 = index[3];
            _listBatchNumber5 = index[4];
            _listBatchNumber6 = index[5];
            _listBatchNumber7 = index[6];
            _listBatchNumber8 = index[7];
        }

        private void LineCallback(int layerId, float[] zValues, float[] intensityValues, int lineLength, double xStep, FsApi.Header headerIn)
        {
            if (!_dataRequest) return;

            if (layerId > 0) return;

            _inervalTemp = _fsCoreConfig.TrigInterval;
            if (!_fsCoreConfig.IsExternalPulsingEnabled) _inervalTemp = 1.0f / _fsCoreConfig.Freq;
            if (Math.Abs(headerIn.Location - _lastLocation) != 2) WriteSwLog(headerIn.Location - _lastLocation + "," + headerIn.Location + "," + _lastLocation);
            //如果_lastLocation == -1，说明当前帧是第一帧，直接接收
            //如果_lastLocation ！= -1，说明已有帧接收，根据当前Location和_lastLocation判断中间是否丢失帧，如果丢帧则补充
            var curLocation = headerIn.Location;
            if (_FirstLocation == -1) _FirstLocation = headerIn.Location;
            //if (_lastLocation != -1)
            //{
            //    int deltaLocation = curLocation - _lastLocation;
            //    if (deltaLocation > 2)
            //    {
            //        int numLocation = deltaLocation / 2 - 1;

            //        for (int i = 0; i < numLocation; i++)
            //        {
            //            int newLocation = _lastLocation + i * 2 + 2;
            //            List<FSPoint> points = CreatePoints(zValues.Length, xStep, newLocation, (numLocation- _FirstLocation)/2);


            //            ListBatchAdd(newLocation, points);
            //          //  _listBatch.Add(points);
            //            _batchCount++;
            //        }
            //    }
            //}

            //_header = headerIn;

            var processed = GetPoints(zValues, intensityValues, lineLength, xStep, headerIn, headerIn.Location - _FirstLocation);
            var processedIndex = GetIndexV(zValues, intensityValues, lineLength, xStep, headerIn, headerIn.Location - _FirstLocation);
            //保存当前的Location
            _lastLocation = curLocation;

            //this._averageIntensity = processed.Any() ? processed.Average(p => p.Intensity) : 0.0f;

            //_queue.Enqueue(processed);

            //if (_queue.Count > FSCoreDefines.QueueMaxLength)
            //{
            //    List<FSPoint> overflow;
            //    _queue.TryDequeue(out overflow);
            //}

            if (headerIn.Location / 2 - _FirstLocation / 2 < 50000)
            {
                ListBatchAdd(headerIn.Location / 2 - _FirstLocation / 2, new List<FSPoint>(processed), processedIndex);
                // _listBatch.Add(new List<FSPoint>(processed));
                _batchCount++;
            }

            if (IsAgcSupported && _fsCoreConfig.IsAgcEnabled) _agcAdjustedLedPulseWidth = (int) _header.PulseWidth;
        }

        public void ListBatchAdd(int CountNum, List<FSPoint> PointTemp, List<int> IndexTemp)
        {
            var LIMIT = new int[9];
            LIMIT[0] = 0;
            LIMIT[1] = _listBatchNumber1;
            LIMIT[2] = LIMIT[1] + _listBatchNumber2;
            LIMIT[3] = LIMIT[2] + _listBatchNumber3;
            LIMIT[4] = LIMIT[3] + _listBatchNumber4;
            LIMIT[5] = LIMIT[4] + _listBatchNumber5;
            LIMIT[6] = LIMIT[5] + _listBatchNumber6;
            LIMIT[7] = LIMIT[6] + _listBatchNumber7;
            LIMIT[8] = LIMIT[7] + _listBatchNumber8;
            if (CountNum >= LIMIT[0] && CountNum < LIMIT[1]) _listBatch1.Add(PointTemp);
            if (CountNum >= LIMIT[1] && CountNum < LIMIT[2]) _listBatch2.Add(PointTemp);
            if (CountNum >= LIMIT[2] && CountNum < LIMIT[3]) _listBatch3.Add(PointTemp);
            if (CountNum >= LIMIT[3] && CountNum < LIMIT[4]) _listBatch4.Add(PointTemp);
            if (CountNum >= LIMIT[4] && CountNum < LIMIT[5]) _listBatch5.Add(PointTemp);
            if (CountNum >= LIMIT[5] && CountNum < LIMIT[6]) _listBatch6.Add(PointTemp);
            if (CountNum >= LIMIT[6] && CountNum < LIMIT[7]) _listBatch7.Add(PointTemp);
            if (CountNum >= LIMIT[7] && CountNum < LIMIT[8]) _listBatch8.Add(PointTemp);
        }

        /*
        public CameraStatusCode EnableAgc(bool isEnabled)
        {
            CameraStatusCode status;
            lock (_lockObj)
            {
                StopGrabbing();

                status = _fsCore.SetParameter(_cameraId, SensorParameter.AgcEnabled, isEnabled ? 1 : 0);

                if (status == CameraStatusCode.Ok)
                {
                    //IsAgcEnabled = isEnabled;
                    //_fsCoreConfig.IsAgcEnabled = isEnabled;
                }

                StartGrabbing();
            }
            return status;
        }
        */
        /*
        public CameraStatusCode SetAgcTargetIntensity(float targetIntensity)
        {
            CameraStatusCode status;
            lock (_lockObj)
            {
                StopGrabbing();
                //_fsCoreConfig.AgcTargetIntensity = targetIntensity;
                status = _fsCore.SetParameter(_cameraId, SensorParameter.AgcTarget, _fsCoreConfig.AgcTargetIntensity);
                StartGrabbing();
            }
            return status;
        }
        */
        public CameraStatusCode LoadCameraList(Dictionary<string, bool> list)
        {
            CameraStatusCode cameraStatus;

            if (list == null) return CameraStatusCode.NotConnected;

            if (list.Count != 0) return CameraStatusCode.Ok;

            int cameraCount;
            List<string> cameraIds;

            _fsCore = new FsApi();

            cameraStatus = _fsCore.Open(out cameraCount, out cameraIds, FSCoreDefines.SensorDiscoveryTimeout);
            if (cameraStatus != CameraStatusCode.Ok) return cameraStatus;

            if (cameraCount == 0)
            {
                _fsCore.Close();
                return CameraStatusCode.NotConnected;
            }

            foreach (var camId in cameraIds) list.Add(camId, false);

            return cameraStatus;
        }

        private void ProfileReceptionCallback(IList<FsApi.Point> profile, FsApi.Header headerIn)
        {
            if (!_dataRequest) return;

            _header = headerIn;
            _averageIntensity = profile.Any() ? profile.Average(p => p.Intensity) : 0.0f;

            try
            {
                var processed = new List<FSPoint>();

                _inervalTemp = _fsCoreConfig.TrigInterval;
                if (!_fsCoreConfig.IsExternalPulsingEnabled) _inervalTemp = 1.0f / _fsCoreConfig.Freq;

                //for (int i = 0; i < profile.Count; i = i + 3)
                for (var i = 0; i < profile.Count; i++)
                {
                    var temp = new FSPoint();
                    if (_fsCoreConfig.IsXAxisTrige)
                    {
                        temp.X = -(XStartPos + (headerIn.Location = _FirstLocation) / 2 * _inervalTemp);
                        temp.Y = -YStartPos + profile[i].X * FSCoreDefines.ProfileScale;
                    }
                    else
                    {
                        temp.X = XStartPos + profile[i].X * FSCoreDefines.ProfileScale;
                        temp.Y = YStartPos + (headerIn.Location = _FirstLocation) / 2 * _inervalTemp;
                    }

                    temp.Z = ZStartPos + profile[i].Y * FSCoreDefines.ProfileScale;
                    temp.Intensity = (int) profile[i].Intensity;
                    temp.index = headerIn.Index;
                    processed.Add(temp);
                }

                //_queue.Enqueue(processed);

                //if (_queue.Count > FSCoreDefines.QueueMaxLength)
                //{
                //    List<FSPoint> overflow;
                //    _queue.TryDequeue(out overflow);
                //    overflow.Clear();
                //}

                if ((headerIn.Location = _FirstLocation) / 2 < 5000)
                {
                    //_listBatch.Add(new List<FSPoint>(processed));
                    //  _listBatch.Add(processed);
                }

                if (IsAgcSupported && _fsCoreConfig.IsAgcEnabled) _agcAdjustedLedPulseWidth = (int) _header.PulseWidth;
            }
            catch (Exception)
            {
            }
        }

        private void SaveToBitmapFile(string file, List<List<FSPoint>> profiles, double batchStepLength)
        {
            if (File.Exists(file)) File.Delete(file);

            var rawImage = new byte[FSCoreDefines.SensorWidth * profiles.Count];
            Array.Clear(rawImage, 0, rawImage.Length);

            uint lineStart = 0;


            lineStart = profiles[0][0].index;


            foreach (var profile in profiles)
            foreach (var point in profile)
            {
                var column = Convert.ToInt32(point.X * 1000 / FSCoreDefines.PixelWidth);
                if (column >= 0 && column < FSCoreDefines.SensorWidth)
                {
                    var line = profile[0].index - lineStart;
                    if (line < profiles.Count) rawImage[line * FSCoreDefines.SensorWidth + column] = (byte) point.Intensity;
                }
            }

            var image = new Bitmap(
                FSCoreDefines.SensorWidth,
                profiles.Count,
                FSCoreDefines.SensorWidth,
                PixelFormat.Format8bppIndexed,
                Marshal.UnsafeAddrOfPinnedArrayElement(rawImage, 0));

            // Create grayscale entries
            var palette = image.Palette;
            for (var i = 0; i < palette.Entries.Length; i++) palette.Entries[i] = Color.FromArgb(255, i, i, i);
            image.Palette = palette;

            // set resolution as dots per inch
            image.SetResolution((float) (1000.0 / FSCoreDefines.PixelWidth * 25.4), (float) (1000.0 / batchStepLength * 25.4));

            image.Save(file, ImageFormat.Bmp);
            //if (File.Exists(file))
            //{
            //    File.Delete(file);
            //}

            //Bitmap bmp = GetImage(profiles, _fsCoreConfig.AveragePixelWidth);

            //bmp.Save(file);
        }

        //保存数据
        public void SaveToPointCloudFile(string file, List<List<FSPoint>> profiles, double batchStepLength, bool isSaveAll)
        {
            var index = 0;

            if (File.Exists(file)) File.Delete(file);

            if (file.ToLower().EndsWith(".bmp"))
            {
                SaveToBitmapFile(file, profiles, batchStepLength);
                return;
            }

            foreach (var profile in profiles) AppendToPointCloudFile(file, profile, batchStepLength, index++, isSaveAll);
        }

        public CameraStatusCode SetAgcEnable(bool enableAgc)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.AgcEnabled, enableAgc ? 1 : 0);

            if (status == CameraStatusCode.Ok)
                IsAgcSupported = enableAgc;

            StartGrabbing(20);

            return status;
        }

        public CameraStatusCode SetAgcTargetIntensity(float agcTargetIntensity)
        {
            CameraStatusCode status;

            StopGrabbing(30);
            status = _fsCore.SetParameter(_cameraId, SensorParameter.AgcTarget, agcTargetIntensity);
            StartGrabbing(20);

            return status;
        }

        public CameraStatusCode SetAverageIntensityFilterSize(float averageIntensityFilterSize)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.AverageIntensityFilterSize, averageIntensityFilterSize);

            return status;
        }

        public CameraStatusCode SetAverageZFilterSize(float averageZFilterSize)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.AverageZFilterSize, averageZFilterSize);

            return status;
        }

        public int SetCallBackFuncByExportLayer(ExportLayer layer)
        {
            var iRet = 0;

            if (_fsCore == null) return iRet;

            switch (layer)
            {
                case ExportLayer.All:
                    _fsCore.StopGrabbing(_cameraId);

                    _fsCore.RemoveLineCallback(_cameraId);
                    _fsCore.SetProfileCallback(_cameraId, ProfileReceptionCallback);

                    _fsCore.StartGrabbing(_cameraId);
                    break;

                case ExportLayer.Top:
                    _fsCore.StopGrabbing(_cameraId);
                    _fsCore.RemoveProfileCallback(_cameraId);

                    _fsCore.SetLineSortingOrder(_cameraId, SortingOrder.FromTopToBottom);

                    _fsCore.SetLineCallback(_cameraId, 0, LineCallback);
                    _fsCore.StartGrabbing(_cameraId);
                    break;

                case ExportLayer.Bottom:
                    _fsCore.StopGrabbing(_cameraId);
                    _fsCore.RemoveProfileCallback(_cameraId);

                    _fsCore.SetLineSortingOrder(_cameraId, SortingOrder.FromBottomToTop);

                    _fsCore.SetLineCallback(_cameraId, 0, LineCallback);
                    _fsCore.StartGrabbing(_cameraId);
                    break;

                case ExportLayer.Brightest:
                    _fsCore.StopGrabbing(_cameraId);
                    _fsCore.RemoveProfileCallback(_cameraId);

                    _fsCore.SetLineSortingOrder(_cameraId, SortingOrder.FromMaxIntensityToLower);

                    _fsCore.SetLineCallback(_cameraId, 0, LineCallback);
                    _fsCore.StartGrabbing(_cameraId);
                    break;

                default:
                    break;
            }

            return iRet;
        }

        public CameraStatusCode SetDetectMissingFirstLayer(float detectMissingFirstLayer)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.DetectMissingFirstLayer, detectMissingFirstLayer);

            return status;
        }

        public CameraStatusCode SetFillGapXMax(float fillGapXMax)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.FillGapXmax, fillGapXMax);

            return status;
        }

        public CameraStatusCode SetFlipXEnabled(int flipXEnabled)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.FlipXEnabled, flipXEnabled);

            return status;
        }

        public CameraStatusCode SetFlipXX0(float flipXX0)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.FlipX0, flipXX0);

            return status;
        }

        public CameraStatusCode SetFreqAndExternalPulseEnable(int freq, bool isExternalPulsingEnabled)
        {
            CameraStatusCode cameraStatus;
            lock (_lockObj)
            {
                StopGrabbing();

                var height = (int) (47.5 / 129 * (1e6 / freq - 1548 / 47.5));

                // Safety margin.
                height -= (int) (0.2 * height);

                if (height > FSCoreDefines.MaxSensorHeight)
                    height = FSCoreDefines.MaxSensorHeight;

                var offset = (FSCoreDefines.MaxSensorHeight - height) / 2;

                cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.Height, height);
                if (cameraStatus != CameraStatusCode.Ok)
                    return cameraStatus;

                cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.OffsetY, offset);
                if (cameraStatus != CameraStatusCode.Ok)
                    return cameraStatus;

                cameraStatus = _fsCore.SetParameter(_cameraId, SensorParameter.PulseFrequency, isExternalPulsingEnabled ? 0 : freq);
                StartGrabbing();
            }

            return cameraStatus;
        }

        public CameraStatusCode SetGain(double gain)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.Gain, gain);

            return status;
        }

        private Bitmap SetImage(byte[] abyRed, byte[] abyGreen, byte[] abyBlue, int iWidth, int iHeight, double resolutionX, double resolutionY)
        {
            var bmp = new Bitmap(iHeight, iWidth, PixelFormat.Format32bppArgb);

            for (var i = 0; i < iHeight; i++)
            for (var j = 0; j < iWidth; j++)
                bmp.SetPixel(i, j, Color.FromArgb(abyRed[i * iWidth + j], abyGreen[i * iWidth + j], abyBlue[i * iWidth + j]));
            return bmp;
        }

        public CameraStatusCode SetImageHeight(int imageHeight)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.Height, imageHeight);

            return status;
        }

        public CameraStatusCode SetImageHeightZeroPosition(int imageHeightZeroPosition)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.ImageHeightZeroPosition, imageHeightZeroPosition);

            return status;
        }

        public CameraStatusCode SetImageOffsetX(int imageOffsetX)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.OffsetX, imageOffsetX);

            return status;
        }

        public CameraStatusCode SetImageOffsetY(int imageOffsetY)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.OffsetY, imageOffsetY);

            return status;
        }

        public CameraStatusCode SetLedPulseWidthAndMaxLimit(int us, int? maxUs)
        {
            CameraStatusCode status;

            StopGrabbing(30);

            if (maxUs.HasValue && IsAgcSupported)
            {
                if (us > maxUs.Value)
                    throw new ArgumentException("LED pulse start reference must be smaller than LED pulse upper limit (max).");

                status = _fsCore.SetParameter(_cameraId, SensorParameter.AgcWiLimit, maxUs.Value);

                if (status != CameraStatusCode.Ok)
                {
                    StartGrabbing(20);
                    return status;
                }
            }

            status = _fsCore.SetParameter(_cameraId, SensorParameter.LedDuration, us);
            StartGrabbing(20);
            return status;
        }

        public CameraStatusCode SetPeakFirLength(int peakFirLength)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.FirLength, peakFirLength);

            return status;
        }

        public CameraStatusCode SetPeakThreshold(int peakThreshold)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.PeakThreshold, peakThreshold);

            return status;
        }

        public CameraStatusCode SetRegPulseDivider(int regPulseDivider)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.PulseDivider, regPulseDivider);

            return status;
        }

        public CameraStatusCode SetRegPulseWidthFloat(float regPulseWidthFloat)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.PulseWidthFloat, regPulseWidthFloat);

            return status;
        }

        public CameraStatusCode SetReordering(int reordering)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.Reordering, reordering);

            return status;
        }

        public CameraStatusCode SetResampleLineXResolution(float resampleLineXResolution)
        {
            CameraStatusCode status;

            status = _fsCore.SetParameter(_cameraId, SensorParameter.ResampleLineXResolution, resampleLineXResolution);

            return status;
        }

        public bool StartGrabbing()
        {
            //_fsCore.SetParameter(_cameraId, SensorParameter.Reordering, 1);
            return _fsCore.StartGrabbing(_cameraId) == CameraStatusCode.Ok;
        }

        public bool StartGrabbing(int sleepTime)
        {
            var status = _fsCore.StartGrabbing(_cameraId) == CameraStatusCode.Ok;
            Thread.Sleep(sleepTime);
            return status;
        }

        public void StartRequest()
        {
            _dataRequest = true;
            _FirstLocation = -1;
            _lastLocation = -1;
            _batchCount = 0;
        }

        public bool StopGrabbing()
        {
            var status = _fsCore.StopGrabbing(_cameraId) == CameraStatusCode.Ok;
            Thread.Sleep(500);
            return status;
        }

        public bool StopGrabbing(int sleepTime)
        {
            var status = _fsCore.StopGrabbing(_cameraId) == CameraStatusCode.Ok;
            Thread.Sleep(sleepTime);
            return status;
        }


        public void StopRequest()
        {
            _dataRequest = false;
            _lastLocation = -1;
            _batchCount = 0;
        }

        public static bool WriteSwLog(string Remarks)
        {
            try
            {
                string RemarkString;
                var LogPath = new string[3];
                LogPath[0] = @"D:\Data\Log\focalLog\" + DateTime.Now.ToString("yyyy-MM-dd");
                LogPath[1] = LogPath[0] + @"\" + DateTime.Now.ToString("HH") + "log.txt";
                if (!Directory.Exists(LogPath[0])) Directory.CreateDirectory(LogPath[0]); //创建文件夹
                RemarkString = "消息：" + Remarks;
                WriteTxtFile(LogPath[1], RemarkString);
            }
            catch
            {
            }

            return true;
        }

        public static void WriteTxtFile(string FilePath, string Data)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine("Time:" + DateTime.Now.ToString("HH:mm:ss"));
                sw.WriteLine(Data);
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                sw.Close();
                fs.Close();
            }
        }

        #region Private Members

        public FSCoreConfig _fsCoreConfig;

        //private ExportLayer _selectedLayer;

        private readonly List<List<FSPoint>> _listBatch1 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch2 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch3 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch4 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch5 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch6 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch7 = new List<List<FSPoint>>();
        private readonly List<List<FSPoint>> _listBatch8 = new List<List<FSPoint>>();


        public int _listBatchNumber1;
        public int _listBatchNumber2;
        public int _listBatchNumber3;
        public int _listBatchNumber4;
        public int _listBatchNumber5;
        public int _listBatchNumber6;
        public int _listBatchNumber7;
        public int _listBatchNumber8;

        /// <summary>
        ///     Last received profile header.
        /// </summary>
        private FsApi.Header _header;

        private FsApi _fsCore;

        private string _cameraId = "";

        private readonly object _lockObj = new object();

        private float _inervalTemp;

        private bool _dataRequest; //数据接收标志

        public ConcurrentQueue<List<FSPoint>> _queue = new ConcurrentQueue<List<FSPoint>>(); //队列

        private int _agcAdjustedLedPulseWidth;

        public double _averageIntensity;

        public int _lastLocation = -1;

        public int _FirstLocation = -1;
        public int _batchCount;

        #endregion

        #region Private Property

        //Defined by Tony.Liu
        public int BatchCount { set; get; }
        public bool NotifyFalg { set; get; }
        public int BatchTimeOut { set; get; }
        public int CountLimitDown { set; get; }
        public int CountLimitUp { set; get; }

        //Defined by Demo
        public bool IsAgcSupported { get; private set; } //是否支持AGC
        public float XStartPos { get; set; }
        public float YStartPos { get; set; }

        public float ZStartPos { get; set; }
        //public bool IsStartBatch { get; set; }
        //public bool IsAgcEnabled { get; set; }     //AGC是否启用
        //public float fTarIntensity { get; set; }
        //public int nMaxLedPulseWidth { get; set; }
        //public int nLedPulseWidth { get; set; }
        //public float Interval { get; set; }
        //public int Fre { get; set; }
        //public bool IsXTrigger { get; set; }
        //public bool IsExTrigger { get; set; }


        public List<List<FSPoint>> DataBatch(ref int index)
        {
            switch (index)
            {
                case 1:
                    return _listBatch1;
                case 2:
                    return _listBatch2;
                case 3:
                    return _listBatch3;
                case 4:
                    return _listBatch4;
                case 5:
                    return _listBatch5;
                case 6:
                    return _listBatch6;
                case 7:
                    return _listBatch7;
                case 8:
                    return _listBatch8;
            }

            return null;
        }


        //public ExportLayer SelectedLayer
        //{
        //    get
        //    {
        //        return this._selectedLayer;
        //    }

        //    set
        //    {
        //        this._selectedLayer = value;
        //    }
        //}

        #endregion
    }
}