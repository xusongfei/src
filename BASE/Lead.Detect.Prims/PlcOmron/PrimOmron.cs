using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimPlcOmron.OmronData;
using Lead.Detect.PrimPlcOmron.OmronUdp;

namespace Lead.Detect.PrimPlcOmron
{
    public class PrimOmron : IPrim, IPlc
    {
        #region Override IPlc's Event

        public event PlcVariableChanged OnPlcVariableChanged;

        #endregion

        #region Override IPrim's Event

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
        #endregion

        #region Override IPrim's Property

        public string Name
        {
            set
            {
                _config.Name = value;
                ((PrimConfigControl) PrimConfigUI).PrimOmronName = value;
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

        #region Override IPrim's Function

        public int IPrimInit()
        {
            var iRet = 0;

            //PrimRunStat = PrimRunState.Stop;
            //PrimConnStat = PrimConnState.UnConnected;

            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            var iRet = 0;

            if (PrimConnStat == PrimConnState.Connected)
            {
                result = Name + "PrimConnStat is Connected!";
                return 0;
            }

            if (_config.CommMode == OmronCommMode.Fins)
            {
                if (_omronUdp == null)
                {
                    result = Name + "_omronUdp is null!";
                    return -1;
                }

                if (string.IsNullOrEmpty(_config.DstPort))
                {
                    result = Name + "DstPort is null!";
                    return -2;
                }

                var port = -1;
                var bRet = int.TryParse(_config.DstPort, out port);
                if (!bRet)
                {
                    result = Name + "Int32.TryParse Fail!";
                    return -3;
                }

                bRet = _omronUdp.Init(_config.DstIp, port, _config.LocalIp);
                if (!bRet)
                {
                    result = Name + "Udp.Init Fail!";
                    return -4;
                }

                PrimConnStat = PrimConnState.Connected;
            }
            else if (_config.CommMode == OmronCommMode.Complet)
            {
                PrimConnStat = PrimConnState.Connected;
            }

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;

            if (PrimConnStat != PrimConnState.Connected) return -1;

            if (PrimRunStat == PrimRunState.Running) return 0;

            ((PrimConfigControl) PrimConfigUI).CycleReadTaskStart();

            PrimRunStat = PrimRunState.Running;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStop()
        {
            var iRet = 0;
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
                _config = XMLHelper.XMLToObject(xmlNode, typeof(OmronConfig)) as OmronConfig;
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

        #region Override IPlc's Function

        public object ReadVariableFromName(string valName)
        {
            object obj = null;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (PrimConfigUI == null) return obj;

                obj = ((PrimConfigControl) PrimConfigUI).ReadVariableFromName(valName);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {
            }

            return obj;
        }

        public object ReadVariableFromAddr(string valAddr)
        {
            object obj = null;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (PrimConfigUI == null) return obj;

                obj = ((PrimConfigControl) PrimConfigUI).ReadVariableFromAddr(valAddr);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {
            }

            return obj;
        }

        public int WriteVariableFromName(string valName, object val)
        {
            var iRet = -1;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (PrimConfigUI == null) return iRet;

                ((PrimConfigControl) PrimConfigUI).WriteVariableFromName(valName, val);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {
            }

            return iRet;
        }

        public int WriteVariableFromAddr(string valAddr, object val)
        {
            var iRet = -1;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (PrimConfigUI == null) return iRet;

                ((PrimConfigControl) PrimConfigUI).WriteVariableFromAddr(valAddr, val);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {
            }

            return iRet;
        }

        public int RegVariableName(string varName)
        {
            var iRet = -1;

            if (_regVarName.Contains(varName)) return 0;

            _regVarName.Add(varName);

            return iRet;
        }

        public int RemoveRegVariableName(string varName)
        {
            var iRet = -1;

            if (!_regVarName.Contains(varName)) return 0;

            _regVarName.Remove(varName);

            return iRet;
        }

        public int ClearRegVariableName()
        {
            var iRet = -1;

            _regVarName.Clear();

            return iRet;
        }

        public int RegVariableAddr(string varAddr)
        {
            var iRet = -1;

            if (_regVarAddr.Contains(varAddr)) return 0;

            _regVarAddr.Add(varAddr);

            return iRet;
        }

        public int RemoveRegVariableAddr(string varAddr)
        {
            var iRet = -1;

            if (!_regVarAddr.Contains(varAddr)) return 0;

            _regVarAddr.Remove(varAddr);

            return iRet;
        }

        public int ClearRegVariableAddr()
        {
            var iRet = -1;

            _regVarAddr.Clear();

            return iRet;
        }

        public int SetDataChangedNotifyMode(DataChangedNotify mode)
        {
            var iRet = -1;

            _dataChangedNotifyMode = mode;

            return iRet;
        }

        #endregion

        #region Private Numbers

        public OmronConfig _config;

        public OmronUDP _omronUdp;

        public DataConfigs _dataConfigs = new DataConfigs();
        public OmronCompletInfo.OmronCompletInfo _completInfo = new OmronCompletInfo.OmronCompletInfo();

        public List<string> _regVarName = new List<string>();
        public List<string> _regVarAddr = new List<string>();

        private DataChangedNotify _dataChangedNotifyMode = DataChangedNotify.Other;

        #endregion

        #region Constructor

        public PrimOmron()
        {
            _config = new OmronConfig();
            _dataConfigs.ListDataConfig = new List<DataConfig>();

            if (!string.IsNullOrEmpty(_config.DataTablePath)) ImportDataTable(_config.DataTablePath);

            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            _omronUdp = new OmronUDP();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            ((PrimConfigControl) PrimConfigUI).OnMonitorVariableChanged -= OnMonitorVariableChanged_Handle;
            ((PrimConfigControl) PrimConfigUI).OnMonitorVariableChanged += OnMonitorVariableChanged_Handle;
        }

        public PrimOmron(XmlNode xmlNode)
        {
            //xmlNode turn to _config
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(OmronConfig)) as OmronConfig;
            else
                return;
            _dataConfigs.ListDataConfig = new List<DataConfig>();

            if (!string.IsNullOrEmpty(_config.DataTablePath)) ImportDataTable(_config.DataTablePath);

            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            _omronUdp = new OmronUDP();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            ((PrimConfigControl) PrimConfigUI).OnMonitorVariableChanged -= OnMonitorVariableChanged_Handle;
            ((PrimConfigControl) PrimConfigUI).OnMonitorVariableChanged += OnMonitorVariableChanged_Handle;
        }

        #endregion

        #region Public UDP Function

        public bool WriteWordByName(Dictionary<string, string> dataDictionary, string name, int dataSet) //根据名称 写单个字
        {
            return _omronUdp.WriteWordByName(dataDictionary, name, dataSet);
        }

        public bool WriteBitByName(Dictionary<string, string> dataDictionary, string name, int dataSet) //根据名称 写单个位
        {
            return _omronUdp.WriteBitByName(dataDictionary, name, dataSet);
        }

        public bool ReadWordByName(Dictionary<string, string> dataDictionary, string name, out int dataRead) //根据名称 读单个字
        {
            return _omronUdp.ReadWordByName(dataDictionary, name, out dataRead);
        }

        public bool ReadBitByName(Dictionary<string, string> dataDictionary, string name, out int dataRead) //根据名称 读单个位
        {
            return _omronUdp.ReadBitByName(dataDictionary, name, out dataRead);
        }

        public bool Init(string ip, int port, string localIp) //初始化
        {
            return _omronUdp.Init(ip, port, localIp);
        }

        public bool WriteWord(string posStr, int dataSet) //写单个字
        {
            return _omronUdp.WriteWord(posStr, dataSet);
        }

        public bool WriteBit(string posStr, int index, int dataSet) //写单个位
        {
            return _omronUdp.WriteBit(posStr, index, dataSet);
        }

        public bool ReadWord(string posStr, int readNum, out int[] dataRead) //读单个字或者多个字
        {
            return _omronUdp.ReadWord(posStr, readNum, out dataRead);
        }

        public bool ReadBitChannel(string posStr, out int[] dataRead) //读单个字的16位
        {
            return _omronUdp.ReadBitChannel(posStr, out dataRead);
        }

        public bool ReadBit(string posStr, int index, out int dataRead) //读单个位
        {
            return _omronUdp.ReadBit(posStr, index, out dataRead);
        }

        //发送UDP指令  dataStr十六进制
        public int WriteData(string posStr, int dataSet)
        {
            var iRet = 0;
            return iRet;
        }

        public int ReadData(string posStr, int readNum, out int[] dataRead)
        {
            var iRet = 0;
            dataRead = new int[1];

            return iRet;
        }

        #endregion

        #region Public Other Function

        public int ImportDataTable(string filePath)
        {
            var iRet = 0;

            _dataConfigs.ListDataConfig.Clear();

            if (File.Exists(filePath)) _dataConfigs = (DataConfigs) XmlSerializerHelper.ReadXML(filePath, typeof(DataConfigs));

            return iRet;
        }

        public int ExportDataTable(string filePath)
        {
            var iRet = 0;

            if (_dataConfigs.ListDataConfig.Count > 0)
            {
                var bRet = XmlSerializerHelper.WriteXML(_dataConfigs, filePath, typeof(DataConfigs));

                if (!bRet) return -1;
            }

            return iRet;
        }

        public int AddDataConfig()
        {
            var iRet = 0;

            var dtConfig = new DataConfig();
            _dataConfigs.ListDataConfig.Add(dtConfig);

            return iRet;
        }

        public int RemoveDataConfig(int idx)
        {
            var iRet = 0;

            _dataConfigs.ListDataConfig.RemoveAt(idx);

            return iRet;
        }

        public void OnMonitorVariableChanged_Handle(string valName, string valAddr, string valType, object valLast, object valCur)
        {
            if (_dataChangedNotifyMode == DataChangedNotify.Any)
            {
                if (OnPlcVariableChanged != null) OnPlcVariableChanged(valName, valAddr, valType, valLast, valCur);
            }
            else if (_dataChangedNotifyMode == DataChangedNotify.Registered)
            {
                if (!_regVarName.Contains(valName)) return;

                if (OnPlcVariableChanged != null) OnPlcVariableChanged(valName, valAddr, valType, valLast, valCur);
            }
        }

        #endregion
    }
}