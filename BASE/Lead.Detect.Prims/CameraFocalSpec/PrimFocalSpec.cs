using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CommonStruct;
using CommonStruct.LC3D;
using FocalSpec.FsApiNet.Model;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimCameraFocalSpec.FocalSpecCore;

namespace Lead.Detect.PrimCameraFocalSpec
{
    public class PrimFocalSpec : IPrim, ISensorFocalSpec
    {
        #region Override ISensorFocalSpec's Property

        public event FocalSpecBatchArrived OnFocalSpecBatchArrived;

        #endregion

        #region Override IPrim's Property

        public event PrimDataRefresh OnPrimDataRefresh;

        public event PrimStateChanged OnPrimStateChanged;

        public event PrimOpLog OnPrimOpLog;
        protected virtual int OnOnPrimDataRefresh(string devname, object context)
        {
            OnPrimDataRefresh?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log);
            return 0;
        }

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }
        public string Name
        {
            set
            {
                _config.Name = value;
                ((PrimConfigControl) PrimConfigUI).FocalSpaceName = value;
            }
            get { return _config.Name; }
        }

        public string PrimTypeName
        {
            set { _config.PrimTypeName = value; }
            get { return _config.PrimTypeName; }
        }

        public Guid PrimId
        {
            set { _config.Id = value; }
            get { return _config.Id; }
        }

        //Prim 子类的类型
        public Type ChildType
        {
            set { _config.ChildType = value; }
            get { return _config.ChildType; }
        }

        //Prim的硬件、软件类型
        public PrimType HSType
        {
            set { _config.HSType = value; }
            get { return _config.HSType; }
        }

        public PrimManufacture Manu
        {
            set { _config.Manu = value; }
            get { return _config.Manu; }
        }

        public PrimVer Ver
        {
            set { _config.Ver = value; }
            get { return _config.Ver; }
        }

        public PrimConnType ConnType
        {
            set { _config.ConnType = value; }
            get { return _config.ConnType; }
        }

        public string ConnInfo
        {
            set { _config.ConnInfo = value; }
            get { return _config.ConnInfo; }
        }

        public PrimConnState PrimConnStat
        {
            set { _config.PrimConnStat = value; }
            get { return _config.PrimConnStat; }
        }

        public PrimRunState PrimRunStat
        {
            set { _config.PrimRunStat = value; }
            get { return _config.PrimRunStat; }
        }

        public bool PrimSimulator
        {
            set { _config.PrimSimulator = value; }
            get { return _config.PrimSimulator; }
        }

        public bool PrimDebug
        {
            set { _config.PrimDebug = value; }
            get { return _config.PrimDebug; }
        }

        public bool PrimEnable
        {
            set { _config.PrimEnable = value; }
            get { return _config.PrimEnable; }
        }

        public IDataArea DataArea { set; get; }

        public Control PrimDebugUI { get; }

        public Control PrimConfigUI { get; }

        public Control PrimOutputUI { get; }

        #endregion

        #region Private Members

        private FocalSpecConfig _config;

        private FSCoreConfig _fsCoreConfig;

        private readonly FSCoreModel _fsCoreModel;

        public Dictionary<string, bool> _fsCameraList;

        public string _curCamId = "";

        public List<List<double>> _listTrigPos = new List<List<double>>();


        public List<int> _listTirgNum = new List<int>();

        #endregion

        #region Override IPrim's Function

        public int IPrimInit()
        {
            var iRet = 0;

            if (_config == null)
            {
                iRet = -1;
                return iRet;
            }

            if (_fsCoreConfig == null)
            {
                iRet = -2;
                return iRet;
            }

            if (_fsCoreModel == null)
            {
                iRet = -3;
                return iRet;
            }

            if (_fsCameraList == null)
            {
                _fsCameraList = new Dictionary<string, bool>();
                _fsCameraList.Clear();
                var camStatus = _fsCoreModel.LoadCameraList(_fsCameraList);

                if (camStatus != CameraStatusCode.Ok)
                {
                    iRet = -4;
                    return iRet;
                }

                //Update ConfigUI
                ((PrimConfigControl) PrimConfigUI).UpdateCameraList();
            }

            var errCode = "";

            if (string.IsNullOrEmpty(_curCamId) && !string.IsNullOrEmpty(_config.CamIdxStr)) _curCamId = _config.CamIdxStr;

            var bRet = _fsCoreModel.InitAndConnect(_curCamId, ref errCode);

            if (!bRet) return -5;

            PrimConnStat = PrimConnState.Connected;
            PrimRunStat = PrimRunState.Running;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            var iRet = 0;

            if (PrimConnStat == PrimConnState.Connected) return iRet;

            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;

            //if (PrimRunStat == PrimRunState.Running)
            //{ return iRet; }

            _fsCoreModel.Flush();

            //Set LedPulseWidth and MaxLedPulseWidth
            iRet = SetLedPulseWidthAndMaxLimit(_fsCoreConfig.LedPulseWidth, _fsCoreConfig.MaxLedPulseWidth);

            ////Set Frequency and IsExternalPulsingEnabled
            iRet = SetFreqAndExternalPulseEnable(_fsCoreConfig.Freq, _fsCoreConfig.IsExternalPulsingEnabled);

            iRet = SetAgcEnable(_fsCoreConfig.IsAgcEnabled);

            iRet = SetAgcTargetIntensity(_fsCoreConfig.AgcTargetIntensity);

            StopGrabbing(20);
            iRet = SetRegPulseWidthFloat(_fsCoreConfig.RegPulseWidthFloat);
            iRet = SetPeakThreshold(_fsCoreConfig.PeakThreshold);
            iRet = SetPeakFirLength(_fsCoreConfig.PeakFirLength);
            iRet = SetRegPulseDivider(_fsCoreConfig.RegPulseDivider);
            iRet = SetGain(_fsCoreConfig.Gain);
            iRet = SetImageHeight(_fsCoreConfig.ImageHeight);
            //SetImageOffsetX(_fsCoreConfig.ImageOffsetX);
            iRet = SetImageOffsetY(_fsCoreConfig.ImageOffsetY);
            iRet = SetImageHeightZeroPosition(_fsCoreConfig.ImageHeightZeroPosition);
            //iRet = SetFlipXX0(_fsCoreConfig.FlipXX0);
            //iRet = SetFlipXEnabled(_fsCoreConfig.FlipXEnabled);
            iRet = SetReordering(_fsCoreConfig.Reordering);
            iRet = SetDetectMissingFirstLayer(_fsCoreConfig.DetectMissingFirstLayer);
            iRet = SetFillGapXMax(_fsCoreConfig.FillGapXMax);
            iRet = SetAverageZFilterSize(_fsCoreConfig.AverageZFilterSize);
            iRet = SetAverageIntensityFilterSize(_fsCoreConfig.AverageIntensityFilterSize);
            iRet = SetResampleLineXResolution(_fsCoreConfig.ResampleLineXResolution);
            StartGrabbing(20);
            _fsCoreModel.StartRequest();
            ClearBatch(99);
            return iRet;
        }

        public int IPrimStop()
        {
            var iRet = 0;

            _fsCoreModel.StopRequest();

            return iRet;
        }

        public int IPrimDisConnect(ref string result)
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimSuspend()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimResume()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimDispose()
        {
            var iRet = 0;

            _fsCoreModel.Close();

            return iRet;
        }

        public int ImportDataConfig(XmlNode xmlNode)
        {
            var iRet = 0;

            return iRet;
        }

        public XmlNode ExportDataConfig()
        {
            return null;
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var iRet = 0;
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(FocalSpecConfig)) as FocalSpecConfig;
            else
                return -1;
            return iRet;
        }

        public XmlNode ExportConfig()
        {
            //config turn to xmlNode
            if (_config == null) return null;

            var node = XMLHelper.ObjectToXML(_config);

            return node;
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            var iRet = 0;
            return iRet;
        }

        #endregion

        #region Override ISensorFocalSpec's Function

        public List<List<FSPoint>> GetBatch(int index)
        {
            return _fsCoreModel.DataBatch(ref index);
        }

        public float GetBatchInterval()
        {
            return _fsCoreConfig.TrigInterval;
        }

        public int GetBatchCount()
        {
            return _fsCoreModel.CurBatchCount;
        }

        public int SetBatchCount(int cnt)
        {
            var iRet = 0;
            _fsCoreModel.BatchCount = cnt;
            return iRet;
        }

        public int SetNotifyFlag(bool flag)
        {
            var iRet = 0;
            _fsCoreModel.NotifyFalg = flag;
            return iRet;
        }

        public int SetBatchTimeOut(int tms)
        {
            var iRet = 0;
            _fsCoreModel.BatchTimeOut = tms;
            return iRet;
        }

        public int SetBatchCountLimit(int limitD, int limitU)
        {
            var iRet = 0;
            _fsCoreModel.CountLimitDown = limitD;
            _fsCoreModel.CountLimitUp = limitU;
            return iRet;
        }

        public int SetFreqAndExternalPulseEnable(int freq, bool isExternalPulsingEnabled)
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            var cameraStatus = _fsCoreModel.SetFreqAndExternalPulseEnable(freq, isExternalPulsingEnabled);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetLedPulseWidthAndMaxLimit(int ledPulseWidth, int? maxLedPulseWidth)
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            var cameraStatus = _fsCoreModel.SetLedPulseWidthAndMaxLimit(ledPulseWidth, maxLedPulseWidth);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetAgcEnable(bool enableAgc)
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            var cameraStatus = _fsCoreModel.SetAgcEnable(enableAgc);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetAgcTargetIntensity(float? agcTargetIntensity)
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            if (agcTargetIntensity.HasValue)
            {
                var cameraStatus = _fsCoreModel.SetAgcTargetIntensity(agcTargetIntensity.Value);
                if (cameraStatus != CameraStatusCode.Ok) iRet = -1;
            }

            return iRet;
        }

        public int SetRegPulseWidthFloat(float regPulseWidthFloat)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetRegPulseWidthFloat(regPulseWidthFloat);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetPeakThreshold(int peakThreshold)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetPeakThreshold(peakThreshold);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetPeakFirLength(int peakFirLength)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetPeakFirLength(peakFirLength);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetRegPulseDivider(int regPulseDivider)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetRegPulseDivider(regPulseDivider);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetGain(double gain)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetGain(gain);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetImageHeight(int imageHeight)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetImageHeight(imageHeight);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetImageOffsetX(int imageOffsetX)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetImageOffsetX(imageOffsetX);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetImageOffsetY(int imageOffsetY)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetImageOffsetY(imageOffsetY);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetImageHeightZeroPosition(int imageHeightZeroPosition)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetImageHeightZeroPosition(imageHeightZeroPosition);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetFlipXX0(float flipXX0)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetFlipXX0(flipXX0);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetFlipXEnabled(int flipXEnabled)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetFlipXEnabled(flipXEnabled);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetReordering(int reordering)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetReordering(reordering);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetDetectMissingFirstLayer(float detectMissingFirstLayer)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetDetectMissingFirstLayer(detectMissingFirstLayer);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetFillGapXMax(float fillGapXMax)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetFillGapXMax(fillGapXMax);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetAverageZFilterSize(float averageZFilterSize)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetAverageZFilterSize(averageZFilterSize);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetAverageIntensityFilterSize(float averageIntensityFilterSize)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetAverageIntensityFilterSize(averageIntensityFilterSize);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public int SetResampleLineXResolution(float resampleLineXResolution)
        {
            var iRet = 0;

            var cameraStatus = _fsCoreModel.SetResampleLineXResolution(resampleLineXResolution);
            if (cameraStatus != CameraStatusCode.Ok) iRet = -1;

            return iRet;
        }

        public void StopGrabbing(int time)
        {
            _fsCoreModel.StopGrabbing(time);
        }

        public void StartGrabbing(int time)
        {
            _fsCoreModel.StartGrabbing(time);
        }

        public int ClearBatch(int Index)
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            _fsCoreModel.ClearBatch(Index);

            return iRet;
        }

        public void Limitvolution(int[] index)
        {
            _fsCoreModel.Limitvolution(index);
        }

        public int Close()
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            _fsCoreModel.Close();

            return iRet;
        }

        public int StartRequest()
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            _fsCoreModel.StartRequest();

            return iRet;
        }

        public int StopRequest()
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            _fsCoreModel.StopRequest();

            return iRet;
        }

        #endregion

        #region Constructor

        public PrimFocalSpec()
        {
            _config = new FocalSpecConfig();

            if (!string.IsNullOrEmpty(_config.BasicConfigPath))
            {
                var iRet = ImportFSCoreConfig(_config.BasicConfigPath);
            }
            else
            {
                _fsCoreConfig = new FSCoreConfig();
            }

            _fsCoreModel = new FSCoreModel(_fsCoreConfig);

            PrimConfigUI = new PrimConfigControl(_config, _fsCoreConfig, this);
            PrimDebugUI = new PrimDebugControl();
            PrimOutputUI = new PrimOutputControl();

            ((PrimConfigControl) PrimConfigUI)._fsCoreConfig = _fsCoreConfig;
            _fsCoreModel._fsCoreConfig = _fsCoreConfig;

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        public PrimFocalSpec(XmlNode configNode)
        {
            if (configNode != null)
                _config = XMLHelper.XMLToObject(configNode, typeof(FocalSpecConfig)) as FocalSpecConfig;
            else
                return;

            if (!string.IsNullOrEmpty(_config.BasicConfigPath))
            {
                var iRet = ImportFSCoreConfig(_config.BasicConfigPath);
            }
            else
            {
                _fsCoreConfig = new FSCoreConfig();
            }

            _fsCoreModel = new FSCoreModel(_fsCoreConfig);

            PrimConfigUI = new PrimConfigControl(_config, _fsCoreConfig, this);
            PrimDebugUI = new PrimDebugControl();
            PrimOutputUI = new PrimOutputControl();

            ((PrimConfigControl) PrimConfigUI)._fsCoreConfig = _fsCoreConfig;
            _fsCoreModel._fsCoreConfig = _fsCoreConfig;

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        #endregion

        #region PrimFocalSpec's Function

        public int ImportFSCoreConfig(string path)
        {
            var iRet = 0;

            if (!File.Exists(path))
                _fsCoreConfig = new FSCoreConfig();
            else
                _fsCoreConfig = GlobalFunc.DeserializeFromXml<FSCoreConfig>(path);

            return iRet;
        }

        public int SaveToPointCloudBmpFile(string file, List<List<PointFS>> profiles, double batchStepLength, bool isSaveAll)
        {
            var iRet = 0;

            return iRet;
        }

        public int ExportFSCoreConfig(string path)
        {
            var iRet = 0;

            var bRet = GlobalFunc.SerializeToXml(path, _fsCoreConfig);

            return iRet;
        }

        public int SetFSCoreCallBackFuncByExportLayer(ExportLayer layer)
        {
            var iRet = 0;

            if (_fsCoreModel == null) return iRet;

            _fsCoreModel.SetCallBackFuncByExportLayer(layer);

            return iRet;
        }

        public int LoadCameraList()
        {
            var iRet = 0;

            if (_fsCameraList == null) _fsCameraList = new Dictionary<string, bool>();

            if (_fsCameraList.Count > 0) return iRet;

            _fsCoreModel.LoadCameraList(_fsCameraList);

            return iRet;
        }

        public int SetCameraId(string idStr)
        {
            var iRet = 0;

            if (!_fsCameraList.ContainsKey(idStr)) return -1;

            if (!_fsCameraList[idStr]) _curCamId = idStr;

            return iRet;
        }

        public int LoadTrigPos()
        {
            var num1 = 0;
            if (_listTrigPos == null) _listTrigPos = new List<List<double>>();
            _listTrigPos.Clear();

            if (_listTirgNum == null) _listTirgNum = new List<int>();
            _listTirgNum.Clear();

            try
            {
                foreach (var path in _config.ListTrigPosFilePath)
                {
                    var reader = new StreamReader(path, Encoding.UTF8, false);
                    reader.Peek();
                    var num = 0;
                    var D1 = new List<double>();
                    while (reader.Peek() > 0)
                    {
                        var str = reader.ReadLine();
                        var split = str.Split(',');

                        if (!string.IsNullOrEmpty(split[0]))
                        {
                            double d = -1;
                            double.TryParse(split[0], out d);
                            D1.Add(d);
                            num++;
                            num1++;
                        }
                    }

                    _listTrigPos.Add(D1);
                    _listTirgNum.Add(num);
                }
            }
            catch (Exception)
            {
                return -2;
            }

            return num1;
        }

        public int SaveToPointCloudFile(string file, List<List<FSPoint>> points, double batchStepLength, bool isSaveAll)
        {
            var iRet = 0;
            if (_fsCoreModel == null) return -1;

            _fsCoreModel.SaveToPointCloudFile(file, points, batchStepLength, isSaveAll);

            return iRet;
        }

        #endregion

        protected virtual void OnOnFocalSpecBatchArrived(List<List<FSPoint>> batch, int count)
        {
            OnFocalSpecBatchArrived?.Invoke(batch, count);
        }
    }
}