using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using Lead.Detect.Interfaces;
using Lead.Detect.XmlHelper;

using OMRON.Compolet.CIPCompolet64;
using System.Threading;

namespace Lead.Detect.PrimPlcOmron
{
    public class PrimOmron : IPrim, IPlc
    {
        #region Override IPrim's Event
        public event PrimDataRefresh OnPrimDataRefresh;
        public event PrimStateChanged OnPrimStateChanged;
        public event PrimOpLog OnPrimOpLog;
        #endregion

        #region Override IPlc's Event
        public event PlcVariableChanged OnPlcVariableChanged;
        #endregion

        #region Override IPrim's Property
        public string Name
        {
            set
            {
                _config.Name = value;
                ((PrimConfigControl)_primConfigUI).PrimOmronName = value;
            }
            get { return _config.Name; }
        }

        public string PrimTypeName
        {
            set { _config.PrimTypeName = value; }
            get { return _config.PrimTypeName; }
        }

        public Guid Id
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

        public IDataArea DataArea
        { set; get; }

        public Control PrimDebugUI
        {
            get { return _primDebugUI; }
        }

        public Control PrimConfigUI
        {
            get { return _primConfigUI; }
        }

        public Control PrimOutputUI
        {
            get { return _primOutputUI; }
        }
        #endregion

        #region Override IPrim's Function
        public int IPrimInit()
        {
            int iRet = 0;

            //PrimRunStat = PrimRunState.Stop;
            //PrimConnStat = PrimConnState.UnConnected;

            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            int iRet = 0;

            if (PrimConnStat == PrimConnState.Connected)
            {
                result = Name + "PrimConnStat is Connected!";
                return 0;
            }

            if(_config.CommMode == OmronCommMode.Fins)
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

                int port = -1;
                bool bRet = Int32.TryParse(_config.DstPort, out port);
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
            else if(_config.CommMode == OmronCommMode.Complet)
            {
                PrimConnStat = PrimConnState.Connected;
            }

            ((PrimConfigControl)_primConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)_primConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStart()
        {
            int iRet = 0;

            if (PrimConnStat != PrimConnState.Connected)
            {
                return -1;
            }

            if ((PrimRunStat == PrimRunState.Running))
            {
                return 0;
            }

            ((PrimConfigControl)_primConfigUI).CycleReadTaskStart();

            PrimRunStat = PrimRunState.Running;

            ((PrimConfigControl)_primConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)_primConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStop()
        {
            int iRet = 0;
            return iRet;
        }

        public int IPrimDisConnect(ref string result)
        {
            int iRet = 0;
            return iRet;
        }

        public int IPrimSuspend()
        {
            int iRet = 0;
            return iRet;
        }

        public int IPrimResume()
        {
            int iRet = 0;
            return iRet;
        }

        public int IPrimDispose()
        {
            int iRet = 0;
            return iRet;
        }

        public int IPrimImportDataConfigXml(XmlNode xmlNode)
        {
            int iRet = 0;

            return iRet;
        }

        public XmlNode IPrimExportDataConfigXml()
        {
            return null;
        }

        public int IPrimImportConfigXml(XmlNode xmlNode)
        {
            int iRet = 0;
            if (xmlNode != null)
            {
                _config = XMLHelper.XMLToObject(xmlNode, typeof(OmronConfig)) as OmronConfig;
            }
            else { return -1; }
            return iRet;
        }

        public XmlNode IPrimExportConfigXml()
        {
            //config turn to xmlNode
            if (_config == null) { return null; }

            XmlNode node = XMLHelper.ObjectToXML(_config);

            return node;
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            int iRet = 0;
            return iRet;
        }

        #endregion

        #region Override IPlc's Function
        public object ReadVariableFromName(string valName)
        {
            object obj = null;
            if(_config.CommMode == OmronCommMode.Complet)
            {
                if(_primConfigUI == null) { return obj; }

                obj = ((PrimConfigControl)_primConfigUI).ReadVariableFromName(valName);
            }
            else if(_config.CommMode == OmronCommMode.Fins)
            {

            }

            return obj;
        }
        public object ReadVariableFromAddr(string valAddr)
        {
            object obj = null;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (_primConfigUI == null) { return obj; }

                obj = ((PrimConfigControl)_primConfigUI).ReadVariableFromAddr(valAddr);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {

            }

            return obj;
        }

        public int WriteVariableFromName(string valName, object val)
        {
            int iRet = -1;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (_primConfigUI == null) { return iRet; }

                ((PrimConfigControl)_primConfigUI).WriteVariableFromName(valName, val);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {

            }

            return iRet;
        }

        public int WriteVariableFromAddr(string valAddr, object val)
        {
            int iRet = -1;
            if (_config.CommMode == OmronCommMode.Complet)
            {
                if (_primConfigUI == null) { return iRet; }

                ((PrimConfigControl)_primConfigUI).WriteVariableFromAddr(valAddr, val);
            }
            else if (_config.CommMode == OmronCommMode.Fins)
            {

            }

            return iRet;
        }

        public int RegVariableName(string varName)
        {
            int iRet = -1;

            if (_regVarName.Contains(varName)) { return 0; }

            _regVarName.Add(varName);

            return iRet;
        }

        public int RemoveRegVariableName(string varName)
        {
            int iRet = -1;

            if (!_regVarName.Contains(varName)) { return 0; }

            _regVarName.Remove(varName);

            return iRet;
        }

        public int ClearRegVariableName()
        {
            int iRet = -1;

            _regVarName.Clear();

            return iRet;
        }

        public int RegVariableAddr(string varAddr)
        {
            int iRet = -1;

            if (_regVarAddr.Contains(varAddr)) { return 0; }

            _regVarAddr.Add(varAddr);

            return iRet;
        }

        public int RemoveRegVariableAddr(string varAddr)
        {
            int iRet = -1;

            if (!_regVarAddr.Contains(varAddr)) { return 0; }

            _regVarAddr.Remove(varAddr);

            return iRet;
        }

        public int ClearRegVariableAddr()
        {
            int iRet = -1;

            _regVarAddr.Clear();

            return iRet;
        }

        public int SetDataChangedNotifyMode(DataChangedNotify mode)
        {
            int iRet = -1;

            _dataChangedNotifyMode = mode;

            return iRet;
        }

        #endregion

        #region Private Numbers
        private Control _primDebugUI = null;
        private Control _primConfigUI = null;
        private Control _primOutputUI = null;

        public OmronConfig _config = null;

        public OmronUDP _omronUdp = null;

        public DataConfigs _dataConfigs = new DataConfigs();
        public OmronCompletInfo _completInfo = new OmronCompletInfo();

        public List<string> _regVarName = new List<string>();
        public List<string> _regVarAddr = new List<string>();

        private DataChangedNotify _dataChangedNotifyMode = DataChangedNotify.Other;

        #endregion

        #region Constructor
        public PrimOmron()
        {
            _config = new OmronConfig();
            _dataConfigs.ListDataConfig = new List<DataConfig>();

            if(!string.IsNullOrEmpty(_config.DataTablePath))
            {
                ImportDataTable(_config.DataTablePath);
            }

            _primDebugUI = new PrimDebugControl();
            _primConfigUI = new PrimConfigControl(this);
            _primOutputUI = new PrimOutputControl();

            _omronUdp = new OmronUDP();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl)_primConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)_primConfigUI).SetPrimRunState(PrimRunStat);

            ((PrimConfigControl)_primConfigUI).OnMonitorVariableChanged -= new MonitorVariableChanged(OnMonitorVariableChanged_Handle);
            ((PrimConfigControl)_primConfigUI).OnMonitorVariableChanged += new MonitorVariableChanged(OnMonitorVariableChanged_Handle);
        }
        public PrimOmron(XmlNode xmlNode)
        {
            //xmlNode turn to _config
            if (xmlNode != null)
            {
                _config = XMLHelper.XMLToObject(xmlNode, typeof(OmronConfig)) as OmronConfig;
            }
            else { return; }
            _dataConfigs.ListDataConfig = new List<DataConfig>();

            if (!string.IsNullOrEmpty(_config.DataTablePath))
            {
                ImportDataTable(_config.DataTablePath);
            }

            _primDebugUI = new PrimDebugControl();
            _primConfigUI = new PrimConfigControl(this);
            _primOutputUI = new PrimOutputControl();

            _omronUdp = new OmronUDP();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl)_primConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)_primConfigUI).SetPrimRunState(PrimRunStat);

            ((PrimConfigControl)_primConfigUI).OnMonitorVariableChanged -= new MonitorVariableChanged(OnMonitorVariableChanged_Handle);
            ((PrimConfigControl)_primConfigUI).OnMonitorVariableChanged += new MonitorVariableChanged(OnMonitorVariableChanged_Handle);
        }
        #endregion

        #region Public UDP Function
        public bool WriteWordByName(Dictionary<string, string> dataDictionary, string name, int dataSet)  //根据名称 写单个字
        {
            return _omronUdp.WriteWordByName(dataDictionary, name, dataSet);
        }
        public bool WriteBitByName(Dictionary<string, string> dataDictionary, string name, int dataSet) //根据名称 写单个位
        {
            return _omronUdp.WriteBitByName(dataDictionary, name, dataSet);
        }
        public bool ReadWordByName(Dictionary<string, string> dataDictionary, string name, out int dataRead)//根据名称 读单个字
        {
            return _omronUdp.ReadWordByName(dataDictionary, name, out dataRead);
        }
        public bool ReadBitByName(Dictionary<string, string> dataDictionary, string name, out int dataRead)//根据名称 读单个位
        {
            return _omronUdp.ReadBitByName(dataDictionary, name, out dataRead);
        }

        public bool Init(string ip, int port, string localIp) //初始化
        {
            return _omronUdp.Init(ip, port, localIp);
        }
        public bool WriteWord(string posStr, int dataSet)  //写单个字
        {
            return _omronUdp.WriteWord(posStr, dataSet);
        }
        public bool WriteBit(string posStr, int index, int dataSet)//写单个位
        {
            return _omronUdp.WriteBit(posStr, index, dataSet);
        }
        public bool ReadWord(string posStr, int readNum, out int[] dataRead)//读单个字或者多个字
        {
            return _omronUdp.ReadWord(posStr, readNum, out dataRead);
        }
        public bool ReadBitChannel(string posStr, out int[] dataRead)//读单个字的16位
        {
            return _omronUdp.ReadBitChannel(posStr, out dataRead);
        }
        public bool ReadBit(string posStr, int index, out int dataRead)//读单个位
        {
            return _omronUdp.ReadBit(posStr, index, out dataRead);
        }

        //发送UDP指令  dataStr十六进制
        public int WriteData(string posStr, int dataSet)
        {
            int iRet = 0;
            return iRet;
        }

        public int ReadData(string posStr, int readNum, out int[] dataRead)
        {
            int iRet = 0;
            dataRead = new int[1];

            return iRet;
        }
        #endregion

        #region Public Other Function
        public int ImportDataTable(string filePath)
        {
            int iRet = 0;

            _dataConfigs.ListDataConfig.Clear();

            if (File.Exists(filePath))
            {
                _dataConfigs =  (DataConfigs)XmlSerializerHelper.ReadXML(filePath, typeof(DataConfigs));
            }

            return iRet;
        }

        public int ExportDataTable(string filePath)
        {
            int iRet = 0;

            if(_dataConfigs.ListDataConfig.Count > 0)
            {
                bool bRet = XmlSerializerHelper.WriteXML(_dataConfigs, filePath, typeof(DataConfigs));

                if (!bRet) return -1;
            }

            return iRet;
        }

        public int AddDataConfig()
        {
            int iRet = 0;

            DataConfig dtConfig = new DataConfig();
            _dataConfigs.ListDataConfig.Add(dtConfig);

            return iRet;
        }

        public int RemoveDataConfig(int idx)
        {
            int iRet = 0;

            _dataConfigs.ListDataConfig.RemoveAt(idx);

            return iRet;
        }

        public void OnMonitorVariableChanged_Handle(string valName, string valAddr, string valType, object valLast, object valCur)
        {
            if(_dataChangedNotifyMode == DataChangedNotify.Any)
            {
                if (OnPlcVariableChanged != null)
                {
                    OnPlcVariableChanged(valName, valAddr, valType, valLast, valCur);
                }
            }else if(_dataChangedNotifyMode == DataChangedNotify.Registered)
            {
                if(!_regVarName.Contains(valName))
                {
                    return;
                }

                if (OnPlcVariableChanged != null)
                {
                    OnPlcVariableChanged(valName, valAddr, valType, valLast, valCur);
                }
            }

        }
        #endregion

    }
}
