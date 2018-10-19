using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimSktServer
{
    public class PrimServer : IPrim, ISktServer
    {
        public ServerConfig _config;
        public event PrimDataRefresh OnPrimDataRefresh;
        public event PrimOpLog OnPrimOpLog;
        public event PrimStateChanged OnPrimStateChanged;

        public event MsgReved OnMsgReved;

        #region Private Feilds

        private Socket _socketListen;
        private IPAddress _ip;
        private IPEndPoint _port;

        private Task _sendTask;
        private Task _revTask = null;
        private Task _listenTask;

        private Queue<ServerMsgInfo> _sendQueue;
        private readonly object _sendQueueMutex = new object();
        private bool _sendFlag;

        private Queue<ServerMsgInfo> _revQueue;
        private readonly object _revQueueMutex = new object();
        private bool _revFlag = false;

        private readonly Dictionary<string, Socket> _dicPointSocket = new Dictionary<string, Socket>();
        private readonly Dictionary<string, string> _dicPointName = new Dictionary<string, string>();

        private readonly List<Thread> _listRevThread = new List<Thread>();

        #endregion

        #region Override IPrim Property

        public Type ChildType
        {
            get { return _config.ChildType; }

            set { _config.ChildType = value; }
        }

        public string ConnInfo
        {
            get { return _config.ConnInfo; }

            set { _config.ConnInfo = value; }
        }

        public PrimConnType ConnType
        {
            get { return _config.ConnType; }

            set { _config.ConnType = value; }
        }

        public IDataArea DataArea { set; get; }

        public PrimType HSType
        {
            get { return _config.HSType; }

            set { _config.HSType = value; }
        }

        public Guid Id
        {
            get { return _config.Id; }

            set { _config.Id = value; }
        }

        public PrimManufacture Manu
        {
            get { return _config.Manu; }

            set { _config.Manu = value; }
        }

        public string Name
        {
            get { return _config.Name; }

            set { _config.Name = value; }
        }

        public Control PrimConfigUI { get; }

        public PrimConnState PrimConnStat
        {
            get { return _config.PrimConnStat; }

            set { _config.PrimConnStat = value; }
        }

        public bool PrimDebug
        {
            get { return _config.PrimDebug; }

            set { _config.PrimDebug = value; }
        }

        public Control PrimDebugUI { get; }

        public bool PrimEnable
        {
            get { return _config.PrimEnable; }

            set { _config.PrimEnable = value; }
        }

        public Control PrimOutputUI { get; }

        public PrimRunState PrimRunStat
        {
            get { return _config.PrimRunStat; }

            set { _config.PrimRunStat = value; }
        }

        public bool PrimSimulator
        {
            get { return _config.PrimSimulator; }

            set { _config.PrimSimulator = value; }
        }

        public string PrimTypeName
        {
            get { return _config.PrimTypeName; }

            set { _config.PrimTypeName = value; }
        }

        public PrimVer Ver
        {
            get { return _config.Ver; }

            set { _config.Ver = value; }
        }

        #endregion

        #region Override IPrim Function

        public int IPrimInit()
        {
            var iRet = 0;

            if (string.IsNullOrEmpty(_config.BindIp)) return -1;

            _ip = IPAddress.Parse(_config.BindIp);
            //IPAddress ip = IPAddress.Any;

            if (string.IsNullOrEmpty(_config.Port)) return -1;
            _port = new IPEndPoint(_ip, int.Parse(_config.Port));

            if (SendQueueCnt <= 0)
                _sendQueue = new Queue<ServerMsgInfo>();
            else
                _sendQueue = new Queue<ServerMsgInfo>(SendQueueCnt);


            if (_config.NotifyMode == SktNotifyMode.DataQueue)
            {
                if (RevQueueCnt <= 0)
                    _revQueue = new Queue<ServerMsgInfo>();
                else
                    _revQueue = new Queue<ServerMsgInfo>(RevQueueCnt);
            }

            if (_socketListen == null) _socketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            PrimRunStat = PrimRunState.Stop;
            PrimConnStat = PrimConnState.UnConnected;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            var iRet = 0;
            if (PrimConnStat == PrimConnState.Connected)
            {
                result = Name + "Connected";
                return 0;
            }

            //socket监听哪个端口

            _socketListen.Bind(_port);

            //同一个时间点过来10个客户端，排队

            _socketListen.Listen(10);

            ((PrimConfigControl) PrimConfigUI).ShowRevMsg("服务器开始监听");

            PrimConnStat = PrimConnState.Connected;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;

            if (PrimConnStat != PrimConnState.Connected) return -1;

            if (PrimRunStat != PrimRunState.Stop) return 0;

            if (_sendTask == null)
            {
                _sendTask = new Task(() => CycleSend());
                _sendFlag = true;
                _sendTask.Start();
            }

            if (_listenTask == null)
            {
                _listenTask = new Task(() => CycleListen());
                _listenTask.Start();
            }

            PrimRunStat = PrimRunState.Running;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimDisConnect(ref string result)
        {
            throw new NotImplementedException();
        }

        public int IPrimDispose()
        {
            throw new NotImplementedException();
        }

        public XmlNode ExportConfig()
        {
            //config turn to xmlNode
            if (_config == null) return null;

            var node = XMLHelper.ObjectToXML(_config);

            return node;
        }

        public XmlNode ExportDataConfig()
        {
            throw new NotImplementedException();
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var iRet = 0;
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(ServerConfig)) as ServerConfig;
            else
                return -1;
            return iRet;
        }

        public int ImportDataConfig(XmlNode xmlNode)
        {
            throw new NotImplementedException();
        }

        public int IPrimResume()
        {
            throw new NotImplementedException();
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            throw new NotImplementedException();
        }

        public int IPrimStop()
        {
            throw new NotImplementedException();
        }

        public int IPrimSuspend()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overide ISkt Property

        public string BindIp
        {
            get { return _config.BindIp; }

            set
            {
                _config.BindIp = value;
                ;
            }
        }

        public string Port
        {
            get { return _config.Port; }

            set { _config.Port = value; }
        }

        public SktNotifyMode NotifyMode
        {
            get { return _config.NotifyMode; }
            set { _config.NotifyMode = value; }
        }

        public string HeartInfo
        {
            set { _config.HeartInfo = value; }
            get { return _config.HeartInfo; }
        }

        public bool HeartFlag
        {
            set { _config.HeartFlag = value; }
            get { return _config.HeartFlag; }
        }

        public int SendQueueCnt
        {
            set { _config.SendQueueCnt = value; }
            get { return _config.SendQueueCnt; }
        }

        public int RevQueueCnt
        {
            set { _config.RevQueueCnt = value; }
            get { return _config.RevQueueCnt; }
        }

        public string NetName
        {
            set { _config.NetName = value; }
            get { return _config.NetName; }
        }

        #endregion

        #region Overide ISkt Function

        public int SetSendQueueCnt(int cnt)
        {
            var iRet = 0;

            SendQueueCnt = cnt;

            return iRet;
        }

        public int RegNotifyInfo(string regInfo)
        {
            throw new NotImplementedException();
        }

        public int SendInfoByNetName(string revNetName, object context)
        {
            var iRet = 0;

            lock (_sendQueueMutex)
            {
                var msg = new ServerMsgInfo();
                msg.NetName = revNetName;
                msg.Context = context;
                _sendQueue.Enqueue(msg);
            }

            return iRet;
        }

        public int SendInfoByPoint(string revPoint, object context)
        {
            var iRet = 0;

            lock (_sendQueueMutex)
            {
                var msg = new ServerMsgInfo();
                msg.Point = revPoint;
                msg.Context = context;
                _sendQueue.Enqueue(msg);
            }

            return iRet;
        }


        public int SetFilterInfo(string filterInfo)
        {
            throw new NotImplementedException();
        }

        public int SetHeartInfo(string heart)
        {
            var iRet = 0;

            HeartInfo = heart;

            return iRet;
        }

        public int SetNetName(string name)
        {
            throw new NotImplementedException();
        }

        public int SetNotifyMode(SktNotifyMode mode, int cnt)
        {
            var iRet = 0;

            NotifyMode = mode;
            RevQueueCnt = cnt;
            return iRet;
        }

        #endregion

        #region Constructor

        public PrimServer()
        {
            _config = new ServerConfig();
            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        public PrimServer(XmlNode xmlNode)
        {
            //xmlNode turn to _config
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(ServerConfig)) as ServerConfig;
            else
                return;

            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        #endregion

        #region Private Function

        private void CycleSend()
        {
            while (true)
            {
                if (PrimConnStat == PrimConnState.UnConnected)
                {
                    Thread.Sleep(10);
                    continue;
                }

                if (!_sendFlag)
                {
                    Thread.Sleep(2);
                    continue;
                }

                if (_sendQueue.Count < 1)
                {
                    Thread.Sleep(2);
                    continue;
                }

                ServerMsgInfo sendMsgInfo = null;

                lock (_sendQueueMutex)
                {
                    sendMsgInfo = _sendQueue.Dequeue();
                }

                if (sendMsgInfo == null)
                {
                    Thread.Sleep(2);
                    continue;
                }

                try
                {
                    if (_dicPointSocket.ContainsKey(sendMsgInfo.Point))
                    {
                        var sendSocket = _dicPointSocket[sendMsgInfo.Point];
                        var buffer = Encoding.UTF8.GetBytes((string) sendMsgInfo.Context);
                        sendSocket.Send(buffer);
                    }
                }
                catch (Exception ex)
                {
                    PrimConnStat = PrimConnState.UnConnected;
                }

                Thread.Sleep(2);
            }
        }

        private void CycleRev(object socket)
        {
            while (true)
            {
                var revSocket = socket as Socket;
                try
                {
                    //定义byte数组存放从客户端接收过来的数据
                    var buffer = new byte[1024];

                    //将接收过来的数据放到buffer中，并返回实际接受数据的长度
                    var n = revSocket.Receive(buffer);

                    //将字节转换成字符串
                    var words = Encoding.UTF8.GetString(buffer, 0, n);
                    var point = revSocket.RemoteEndPoint.ToString();

                    ServerMsgInfo msg = null;
                    if (_dicPointName.ContainsKey(point))
                    {
                        msg = new ServerMsgInfo();
                        msg.Point = point;
                        msg.NetName = _dicPointName[point];
                        msg.Context = words;
                    }
                    else if (_dicPointSocket.ContainsKey(point))
                    {
                        msg = new ServerMsgInfo();
                        msg.Point = point;
                        msg.Context = words;
                    }

                    if (msg == null) continue;

                    if (PrimDebug) ((PrimConfigControl) PrimConfigUI).ShowRevMsg(msg.Point + " - " + msg.NetName + ":" + msg.Context);

                    if (NotifyMode == SktNotifyMode.DataEvent && OnMsgReved != null) OnMsgReved(msg.Point, msg.Context);

                    if (NotifyMode == SktNotifyMode.DataQueue)
                        lock (_revQueueMutex)
                        {
                            _revQueue.Enqueue(msg);
                        }
                }
                catch (Exception ex)
                {
                    //ShowMsg(ex.Message);
                    break;
                }

                Thread.Sleep(2);
            }
        }

        private void CycleListen()
        {
            while (true)
                try
                {
                    //创建通信用的Socket
                    var tSocket = _socketListen.Accept();

                    var point = tSocket.RemoteEndPoint.ToString();

                    _dicPointSocket.Add(point, tSocket);

                    ((PrimConfigControl) PrimConfigUI).AddPoint(point, 1);

                    var revThreas = new Thread(CycleRev);
                    _listRevThread.Add(revThreas);

                    revThreas.IsBackground = true;
                    revThreas.Start(tSocket);
                }
                catch (Exception ex)
                {
                    break;
                }
        }

        #endregion

        #region Public Function

        #endregion
    }
}