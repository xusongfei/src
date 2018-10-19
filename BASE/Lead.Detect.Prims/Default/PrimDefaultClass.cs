using System;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimDefault
{
    public class PrimDefaultClass : IPrim
    {
        private DefaultConfig _config;

        public PrimDefaultClass()
        {
            _config = new DefaultConfig();
            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl();
            PrimOutputUI = new PrimOutputControl();
        }

        public PrimDefaultClass(XmlNode xmlNode)
        {
            //xmlNode turn to _config
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(DefaultConfig)) as DefaultConfig;
            else
                return;

            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl();
            PrimOutputUI = new PrimOutputControl();
        }

        public event PrimDataRefresh OnPrimDataRefresh;

        public event PrimStateChanged OnPrimStateChanged;

        public event PrimOpLog OnPrimOpLog;

        public string Name
        {
            set { _config.Name = value; }
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

        public int IPrimInit()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;
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
                _config = XMLHelper.XMLToObject(xmlNode, typeof(DefaultConfig)) as DefaultConfig;
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

        protected virtual int OnOnPrimDataRefresh(string devname, object context)
        {
            OnPrimDataRefresh?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log);
            return 0;
        }
    }
}