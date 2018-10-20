using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimMotionADLink.ADLink;

namespace Lead.Detect.PrimMotionADLink
{
    public enum IOInterModeEnum
    {
        Rising = 1,
        Falling = 2,
        Change = 3
    }

    public class IOInterInfo
    {
        public int IOInterIdx { set; get; }
        public IOInterModeEnum IOInterMode { set; get; }
    }

    public class PrimADLink : IPrim, IMotionCard
    {
        private readonly Stopwatch sw = new Stopwatch();
        private int motionIO;
        private int motionSts;

        public PrimADLink()
        {
            _config = new ADLinkConfig();
            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl)PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        public PrimADLink(XmlNode xmlNode)
        {
            //xmlNode turn to _config
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(ADLinkConfig)) as ADLinkConfig;
            else
                return;

            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl)PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        public double CurrentPos { get; private set; }

        public double CommandPos { get; private set; }

        public bool INP => XConvert.BitEnable(motionIO, XAPS_Define.MIO_INP);

        public bool PEL => XConvert.BitEnable(motionIO, XAPS_Define.MIO_PEL);

        public bool MEL => XConvert.BitEnable(motionIO, XAPS_Define.MIO_MEL);

        public bool ORG => XConvert.BitEnable(motionIO, XAPS_Define.MIO_ORG);

        public bool SVON => XConvert.BitEnable(motionIO, XAPS_Define.MIO_SVON);

        public bool EMG => XConvert.BitEnable(motionIO, XAPS_Define.MIO_EMG);

        public bool ALM => XConvert.BitEnable(motionIO, XAPS_Define.MIO_ALM);

        public bool MDN => XConvert.BitEnable(motionSts, XAPS_Define.MTS_MDN);

        public bool HMV => XConvert.BitEnable(motionSts, XAPS_Define.MTS_HMV);

        public bool ASTP => XConvert.BitEnable(motionSts, XAPS_Define.MTS_ASTP);


        public int MoveAbs(MoveProfile profile)
        {
            SetAxisAccAndDec(profile.AxisId, XConvert.MM2PULS(profile.Acceleration, profile.AxisLead),
                XConvert.MM2PULS(profile.Deceleration, profile.AxisLead));
            var ret = APS168.APS_absolute_move(profile.AxisId, XConvert.MM2PULS(profile.Distance, profile.AxisLead),
                XConvert.MM2PULS(profile.Speed, profile.AxisLead));
            return ret;
        }

        public int MoveAbs(MoveProfile[] profile)
        {
            foreach (var mfile in profile)
            {
                SetAxisAccAndDec(mfile.AxisId, XConvert.MM2PULS(mfile.Acceleration, mfile.AxisLead),
                    XConvert.MM2PULS(mfile.Deceleration, mfile.AxisLead));
                var ret = APS168.APS_absolute_move(mfile.AxisId, XConvert.MM2PULS(mfile.Distance, mfile.AxisLead),
                    XConvert.MM2PULS(mfile.Speed, mfile.AxisLead));
                if (ret != 0)
                {
                    return ret;
                }
            }

            return 0;
        }

        public int MoveAbs(List<MoveProfile> profile)
        {
            foreach (var mfile in profile)
            {
                SetAxisAccAndDec(mfile.AxisId, XConvert.MM2PULS(mfile.Acceleration, mfile.AxisLead),
                    XConvert.MM2PULS(mfile.Deceleration, mfile.AxisLead));
                var ret = APS168.APS_absolute_move(mfile.AxisId, XConvert.MM2PULS(mfile.Distance, mfile.AxisLead),
                    XConvert.MM2PULS(mfile.Speed, mfile.AxisLead));
                if (ret != 0)
                {
                    return ret;
                }
            }

            return 0;
        }

        public int Home(MoveProfile profile)
        {
            lock (this)
            {
                var ret = APS168.APS_home_move(profile.AxisId);
                return ret;
            }
        }

        public int Home(MoveProfile[] profile)
        {
            foreach (var mfile in profile)
            {
                var ret = APS168.APS_home_move(mfile.AxisId);
                if (ret != 0)
                {
                    return ret;
                }
            }

            return 0;
        }

        public int Home(List<MoveProfile> profile)
        {
            foreach (var mfile in profile)
            {
                var ret = APS168.APS_home_move(mfile.AxisId);
                if (ret != 0)
                {
                    return ret;
                }
            }

            return 0;
        }

        public void Stop(MoveProfile profile)
        {
            APS168.APS_stop_move(profile.AxisId);
            APS168.APS_jog_start(profile.AxisId, 0);
        }

        public void Stop(MoveProfile[] profile)
        {
            foreach (var mfile in profile)
            {
                APS168.APS_stop_move(mfile.AxisId);
                APS168.APS_jog_start(mfile.AxisId, 0);
            }
        }

        public int WaitMoveDone(List<MoveProfile> profile, bool bCheckLimit, bool bCheckPos)
        {
            var timeoutMS = 30000;
            bool flag;
            while (true)
            {
                Thread.Sleep(100);
                flag = true;
                if (sw.ElapsedMilliseconds > timeoutMS)
                {
                    sw.Reset();
                    return -1;
                }

                foreach (var mfile in profile)
                {
                    ReadStatus(mfile);
                    if (bCheckLimit)
                    {
                        if (PEL)
                        {
                            flag = false;
                            return -3;
                        }

                        if (MEL)
                        {
                            flag = false;
                            return -4;
                        }
                    }

                    if (ASTP)
                    {
                        flag = false;
                        return -2;
                    }

                    if (MDN == false)
                    {
                        flag = false;
                        break;
                    }

                    if (bCheckPos)
                        if (Math.Abs(CommandPos - CurrentPos) > 0.01)
                        {
                            flag = false;
                            return -5;
                        }
                }

                if (flag)
                {
                    sw.Reset();
                    break;
                }
            }

            return 0;
        }

        public int WaitMoveDone(MoveProfile[] profile, bool bCheckLimit, bool bCheckPos)
        {
            var timeoutMS = 30000;
            bool flag;
            while (true)
            {
                Thread.Sleep(100);
                flag = true;
                if (sw.ElapsedMilliseconds > timeoutMS)
                {
                    sw.Reset();
                    return -1;
                }

                foreach (var mfile in profile)
                {
                    ReadStatus(mfile);
                    if (bCheckLimit)
                    {
                        if (PEL)
                        {
                            flag = false;
                            return -3;
                        }

                        if (MEL)
                        {
                            flag = false;
                            return -4;
                        }
                    }

                    if (ASTP)
                    {
                        flag = false;
                        return -2;
                    }

                    if (MDN == false)
                    {
                        flag = false;
                        break;
                    }

                    if (bCheckPos)
                        if (Math.Abs(CommandPos - CurrentPos) > 0.01)
                        {
                            flag = false;
                            return -5;
                        }
                }

                if (flag)
                {
                    sw.Reset();
                    break;
                }
            }

            return 0;
        }

        public int WaitMoveDone(MoveProfile profile, bool bCheckLimit, bool bCheckPos)
        {
            var timeoutMS = 30000;
            bool flag;
            while (true)
            {
                Thread.Sleep(100);
                flag = true;
                if (sw.ElapsedMilliseconds > timeoutMS)
                {
                    sw.Reset();
                    return -1;
                }

                ReadStatus(profile);
                if (bCheckLimit)
                {
                    if (PEL)
                    {
                        flag = false;
                        return -3;
                    }

                    if (MEL)
                    {
                        flag = false;
                        return -4;
                    }
                }

                if (ASTP)
                {
                    flag = false;
                    return -2;
                }

                if (MDN == false)
                {
                    flag = false;
                    ;
                }
                else
                {
                    if (bCheckPos)
                        if (Math.Abs(CommandPos - CurrentPos) > 0.01)
                        {
                            flag = false;
                            return -5;
                        }
                }

                if (flag)
                {
                    sw.Reset();
                    break;
                }
            }

            return 0;
        }

        public void ReadStatus(MoveProfile profile)
        {
            try
            {
                var sts = 0;
                var sts1 = 0.0;
                GetMotionIo(profile.AxisId, ref sts);
                motionIO = sts;
                GetMotionSts(profile.AxisId, ref sts);
                motionSts = sts;
                GetMotionPos(profile.AxisId, profile.AxisLead, ref sts1);
                CurrentPos = sts1;
                GetCommandPos(profile.AxisId, profile.AxisLead, ref sts1);
                CommandPos = sts1;
            }
            catch
            {
            }
        }

        public void ReadStatus(int axis)
        {
            var sts = 0;
            var pos = 0.0;
            GetMotionIo(axis, ref sts);
            motionIO = sts;
            GetMotionSts(axis, ref sts);
            motionSts = sts;
            GetMotionPos(axis, 1, ref pos);
            CurrentPos = pos * 10000;
            GetCommandPos(axis, 1, ref pos);
            CommandPos = pos * 10000;
        }

        private int GetCommandPos(int axisId, double lead, ref double pos)
        {
            var cpos = 0;
            APS168.APS_get_command(axisId, ref cpos);
            pos = XConvert.PULS2MM(cpos, lead);
            return 0;
        }

        private int GetMotionIo(int axisId, ref int sts)
        {
            int _sts, _rSts;
            _sts = APS168.APS_motion_io_status(axisId);
            if (_sts < 0) return _sts;
            _rSts = 0;
            if (XConvert.BitEnable(_sts, 0x01 << 0)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_ALM);
            if (XConvert.BitEnable(_sts, 0x01 << 1)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_PEL);
            if (XConvert.BitEnable(_sts, 0x01 << 2)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_MEL);
            if (XConvert.BitEnable(_sts, 0x01 << 3)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_ORG);
            if (XConvert.BitEnable(_sts, 0x01 << 4)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_EMG);
            if (XConvert.BitEnable(_sts, 0x01 << 6)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_INP);
            if (XConvert.BitEnable(_sts, 0x01 << 7)) XConvert.SetBits(ref _rSts, XAPS_Define.MIO_SVON);
            sts = _rSts;
            return 0;
        }

        private int GetMotionPos(int axisId, double lead, ref double pos)
        {
            var mpos = 0;
            APS168.APS_get_position(axisId, ref mpos);
            pos = XConvert.PULS2MM(mpos, lead);
            return 0;
        }

        private int GetMotionSts(int axisId, ref int sts)
        {
            int _sts, _rSts;
            _sts = APS168.APS_motion_status(axisId);
            if (_sts < 0) return _sts;
            _rSts = 0;
            if (XConvert.BitEnable(_sts, 0x01 << 5)) XConvert.SetBits(ref _rSts, XAPS_Define.MTS_MDN);
            if (XConvert.BitEnable(_sts, 0x01 << 6)) XConvert.SetBits(ref _rSts, XAPS_Define.MTS_HMV);
            if (XConvert.BitEnable(_sts, 0x01 << 16)) XConvert.SetBits(ref _rSts, XAPS_Define.MTS_ASTP);
            sts = _rSts;
            return 0;
        }

        private void SetAxisAccAndDec(int axisId, double acc, double dec)
        {
            APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_ACC, acc);
            APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_DEC, dec);
        }

        public void Stop(List<MoveProfile> profile)
        {
            foreach (var mfile in profile)
            {
                APS168.APS_stop_move(mfile.AxisId);
                APS168.APS_jog_start(mfile.AxisId, 0);
            }
        }

        #region Override IPrim's Event

        public event PrimDataRefresh OnPrimDataRefresh;
        public event PrimStateChanged OnPrimStateChanged;
        public event PrimOpLog OnPrimOpLog;

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
        public string Name
        {
            set
            {
                _config.Name = value;
                ((PrimConfigControl)PrimConfigUI).ADLinkName = value;
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
            set { _config.PrimId = value; }
            get { return _config.PrimId; }
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

        #region Override IMotionCard's Event

        public event Action<int, bool, int> OnDIStateChanged;
        public event Action<int, bool, int> OnDOStateChanged;


        public string ConfigFilePath
        {
            set { _config.ConfigFilePath = value; }
            get { return _config.ConfigFilePath; }
        }

        public int DevIndex
        {
            set { _config.DevIndex = value; }
            get { return _config.DevIndex; }
        }

        public bool DIChangedEnable { set; get; }
        public bool DOChangedEnable { set; get; }

        #endregion

        #region Private Numbers

        private static bool _initFlag;

        public ADLinkConfig _config;

        public int _startAxisId;
        public int _totalAxis;
        public int _cardName;
        public bool _isInitialed;

        public bool _cycleWaitInterruptFlag;
        public bool _cycleCycleReadIOFlag;
        public bool _cycleReadPosFlag;

        //Key InterruptId; Value IOInfo
        public Dictionary<int, IOInterInfo> InterruptList = new Dictionary<int, IOInterInfo>();
        public Dictionary<int, IOInterModeEnum> CycleTrigList = new Dictionary<int, IOInterModeEnum>();

        //默认5组DI输入，每组32路输入
        public int[] groupDIVal = new int[5];
        public int[] groupDOVal = new int[5];

        public int[] _posVal = new int[4];

        private Task taskCycleReadIO;
        private Task taskCycleReadPos;

        #endregion

        #region Private Function

        private void WaitInterrutp(int interruptId)
        {
            // 阻塞等触发
            while (_cycleWaitInterruptFlag)
            {
                if (APS168.APS_wait_single_int(interruptId, -1) == 0) //Wait interrupt
                {
                    APS168.APS_reset_int(interruptId);

                    if (InterruptList.ContainsKey(interruptId))
                    {
                        var idx = InterruptList[interruptId].IOInterIdx;
                        var mode = 0;
                        var val = false;
                        switch (InterruptList[interruptId].IOInterMode)
                        {
                            case IOInterModeEnum.Rising:
                                mode = 1;
                                val = true;
                                break;
                            case IOInterModeEnum.Falling:
                                val = false;
                                mode = 2;
                                break;
                            case IOInterModeEnum.Change:
                                mode = 3;
                                break;
                            default:
                                mode = 0;
                                break;
                        }

                        if (OnDIStateChanged != null) OnDIStateChanged(idx, val, mode);
                    }
                }

                Thread.Sleep(2);
            }
        }

        private int cnt;

        private void CycleCmpDI(int group, int val)
        {
            var dtDI = groupDIVal[0];

            if (dtDI == val) return;

            //异或 变化的位都置 1
            var cmpRet = dtDI ^ val;
            for (var i = 0; i < 32; i++)
            {
                var cmpTmp = 1 << i;
                var cmp0 = cmpTmp & cmpRet;
                if (cmp0 > 0)
                {
                    //该位为1
                    var cmp1 = cmpTmp & val;

                    if (cmp1 > 0)
                    {
                        //上升沿
                        if (CycleTrigList.ContainsKey(i))
                        {
                            if (CycleTrigList[i] == IOInterModeEnum.Rising)
                            {
                                if (OnDIStateChanged != null)
                                {
                                    OnDIStateChanged(i, true, 1);
                                    cnt++;
                                }
                            }
                            else if (CycleTrigList[i] == IOInterModeEnum.Change)
                            {
                                if (OnDIStateChanged != null)
                                {
                                    OnDIStateChanged(i, true, 3);
                                    cnt++;
                                }
                            }
                        }
                    }
                    else
                    {
                        //下降沿
                        if (CycleTrigList.ContainsKey(i))
                        {
                            if (CycleTrigList[i] == IOInterModeEnum.Falling)
                            {
                                if (OnDIStateChanged != null)
                                {
                                    OnDIStateChanged(i, false, 2);
                                    cnt++;
                                }
                            }
                            else if (CycleTrigList[i] == IOInterModeEnum.Change)
                            {
                                if (OnDIStateChanged != null)
                                {
                                    OnDIStateChanged(i, false, 3);
                                    cnt++;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CycleReadIO()
        {
            // 阻塞等触发
            while (_cycleCycleReadIOFlag)
            {
                var dInput = -1;
                var ret = APS168.APS_read_d_input(_config.DevIndex, 0, ref dInput);

                if (dInput != -1)
                {
                    if (groupDIVal[0] != -1 && CycleTrigList.Count > 0) CycleCmpDI(0, dInput);
                    groupDIVal[0] = dInput;
                }

                //Int32 dOut = -1;
                //ret = APS168.APS_read_d_output(_config.DevIndex, 0, ref dOut);

                //if (dOut != -1)
                //{
                //    groupDOVal[0] = dOut;
                //}

                Thread.Sleep(5);
            }
        }

        private void CycleReadPos()
        {
            // 阻塞等触发
            while (_cycleReadPosFlag)
            {
                var iRet = 0;

                for (var i = 0; i < 2; i++) iRet = APS168.APS_get_position(i, ref _posVal[i]);

                Thread.Sleep(5);
            }
        }

        #endregion

        #region Override IPrim's Function

        public int IPrimInit()
        {
            if (_isInitialed) return 0;

            var iRet = 0;

            var boardIdInBits = _config.DevIndex;
            //var boardIdInBits = 0;
            var mode = 1;
            // Card(Board) initial,mode bit0(0:By system assigned, 1:By dip switch)  
            //int ret = APS168.APS_initial(ref boardIdInBits, mode);
            var ret = 0;
            if (!_initFlag)
            {
                ret = APS168.APS_initial(ref boardIdInBits, mode);
                _initFlag = true;
            }

            //APS168.APS_load_param_from_file
            if (ret >= 0)
            {
                _isInitialed = true;
                APS168.APS_get_first_axisId(_config.DevIndex, ref _startAxisId, ref _totalAxis);
                APS168.APS_get_card_name(_config.DevIndex, ref _cardName);
                if (_cardName != (int)APS_Define.DEVICE_NAME_PCI_825458 &&
                    _cardName != (int)APS_Define.DEVICE_NAME_AMP_20408C)
                    return -1;
            }

            for (var i = 0; i < groupDIVal.Length; i++) groupDIVal[i] = -1;

            for (var i = 0; i < groupDOVal.Length; i++) groupDOVal[i] = -1;

            PrimRunStat = PrimRunState.Stop;
            PrimConnStat = PrimConnState.UnConnected;

            ((PrimConfigControl)PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            if (PrimConnStat == PrimConnState.Connected)
            {
                result = Name + "Load XML File Success!";
                return 0;
            }

            var iRet = 0;

            if (!_isInitialed)
            {
                result = Name + "Is't Initialed!";
                return -1;
            }

            if (string.IsNullOrEmpty(_config.ConfigFilePath))
            {
                result = Name + "No File Path Found!";
                return -1;
            }

            iRet = APS168.APS_load_param_from_file(_config.ConfigFilePath);

            if (iRet == 0)

            {
                PrimConnStat = PrimConnState.Connected;
                result = Name + "Load XML File Success!";
            }
            else
            {
                PrimConnStat = PrimConnState.UnConnected;
                result = Name + "Load XML File Failed!";
            }

            ((PrimConfigControl)PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)PrimConfigUI).SetPrimRunState(PrimRunStat);

            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;

            if (PrimConnStat != PrimConnState.Connected) return -1;

            if (PrimRunStat != PrimRunState.Stop) return 0;

            if (taskCycleReadIO == null)
            {
                taskCycleReadIO = new Task(() => CycleReadIO());
                _cycleCycleReadIOFlag = true;
                taskCycleReadIO.Start();
            }

            if (taskCycleReadPos == null)
            {
                taskCycleReadPos = new Task(() => CycleReadPos());
                _cycleReadPosFlag = true;
                taskCycleReadPos.Start();
            }

            PrimRunStat = PrimRunState.Running;

            ((PrimConfigControl)PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl)PrimConfigUI).SetPrimRunState(PrimRunStat);

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
                _config = XMLHelper.XMLToObject(xmlNode, typeof(ADLinkConfig)) as ADLinkConfig;
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


        #region Override IMotionCard's Function

        //Load config

        #region  Config Load

        public void LoadConfigFile(string filePath)
        {
        }

        public void LoadConfigFileLocal()
        {
        }

        #endregion

        #region Digital IO

        //RW Single DOI
        public int WriteSingleDOutput(int boardId, int group, int idx, int iVal)
        {
            return APS168.APS_write_d_channel_output(_config.DevIndex, group, idx, iVal);
        }

        public int ReadSingleDOutput(int boardId, int group, int idx, out int iVal)
        {
            var val = 0;
            var ret = APS168.APS_read_d_channel_output(_config.DevIndex, group, idx, ref val);
            iVal = val;
            return ret;
        }

        public int ReadSingleDInput(int boardId, int group, int idx, out int val)
        {
            var ret = 0;
            var value = 0;
            ret = APS168.APS_read_d_input(_config.DevIndex, 0, ref value);
            val = (value >> idx) & 1;
            return ret;
        }

        //RW Multi DOI
        public int WriteMultiDOutput(int startIdx, int num, int[] val)
        {
            var ret = 0;
            return ret;
        }

        public int[] ReadMultiDOutput(int startIdx, int num)
        {
            int[] iRets = null;
            var groupNum = 0;

            if (num % 32 > 0)
                groupNum = num / 32 + 1;
            else
                groupNum = num / 32;

            iRets = new int[groupNum];

            for (var i = 0; i < groupNum; i++) iRets[i] = groupDOVal[i];

            return iRets;
        }

        public int[] ReadMultiDInput(int startIdx, int num)
        {
            int[] iRets = null;

            var groupNum = 0;

            if (num % 32 > 0)
                groupNum = num / 32 + 1;
            else
                groupNum = num / 32;

            iRets = new int[groupNum];

            for (var i = 0; i < groupNum; i++) iRets[i] = groupDIVal[i];

            return iRets;
        }

        private void RegsterDIRising(int idx)
        {
            var info = new IOInterInfo();
            info.IOInterMode = IOInterModeEnum.Rising;
            info.IOInterIdx = idx;

            var interruptId = APS168.APS_set_int_factor(_config.DevIndex, 9, idx, 1);

            InterruptList.Add(interruptId, info);

            //Task taskCycleWaitInterrupt = new Task(() => this.WaitInterrutp(interruptId));
            //taskCycleWaitInterrupt.Start();
        }

        private void RegsterDIFalling(int idx)
        {
            var info = new IOInterInfo();
            info.IOInterMode = IOInterModeEnum.Falling;
            info.IOInterIdx = idx;

            var interruptId = APS168.APS_set_int_factor(_config.DevIndex, 10, idx, 1);

            InterruptList.Add(interruptId, info);

            //Task taskCycleWaitInterrupt = new Task(() => this.WaitInterrutp(interruptId));
            //taskCycleWaitInterrupt.Start();
        }

        private void RegsterDIChange(int idx)
        {
            var info = new IOInterInfo();
            info.IOInterMode = IOInterModeEnum.Change;
            info.IOInterIdx = idx;

            var interruptId1 = APS168.APS_set_int_factor(_config.DevIndex, 10, idx, 1);

            var interruptId2 = APS168.APS_set_int_factor(_config.DevIndex, 10, idx, 1);

            InterruptList.Add(interruptId1, info);
            InterruptList.Add(interruptId2, info);

            //Task taskCycleWaitInterrupt1 = new Task(() => this.WaitInterrutp(interruptId1));
            //taskCycleWaitInterrupt1.Start();

            //Task taskCycleWaitInterrupt2 = new Task(() => this.WaitInterrutp(interruptId2));
            //taskCycleWaitInterrupt2.Start();
        }

        private int RegisterDIChangedCycleTrig(int idx, int mode)
        {
            var ret = 0;

            if (CycleTrigList.ContainsKey(idx)) return ret;

            switch (mode)
            {
                case 1:
                    CycleTrigList.Add(idx, IOInterModeEnum.Rising);
                    break;
                case 2:
                    CycleTrigList.Add(idx, IOInterModeEnum.Falling);
                    break;
                case 3:
                    CycleTrigList.Add(idx, IOInterModeEnum.Change);
                    break;
            }

            return ret;
        }

        private int RegisterDIChangedInterrpt(int idx, int mode)
        {
            var ret = 0;

            foreach (var inter in InterruptList)
                if (inter.Value.IOInterIdx == idx)
                    return ret;

            switch (mode)
            {
                case 1:
                    RegsterDIRising(idx);
                    break;
                case 2:
                    RegsterDIFalling(idx);
                    break;
                case 3:
                    RegsterDIChange(idx);
                    break;
            }

            if (!_cycleWaitInterruptFlag)
            {
                _cycleWaitInterruptFlag = true;

                // Step 2: set interrupt main switch 
                ret = APS168.APS_int_enable(_config.DevIndex, 1); // Enable the interrupt main switch
            }

            return ret;
        }

        //1:Rising Edge; 2:Down Edge; 3:Changed
        //type 1:Trig Interrupt 2:Cycle Read
        public int RegisterDIChanged(int idx, int mode, int type)
        {
            var ret = 0;

            if (type == 1)
                ret = RegisterDIChangedInterrpt(idx, mode);
            else if (type == 2) ret = RegisterDIChangedCycleTrig(idx, mode);

            return ret;
        }

        public int RegisterDOChanged(int idx, int mode)
        {
            var ret = 0;
            return ret;
        }

        public int RemoveDIChanged(int idx)
        {
            var ret = 0;

            if (CycleTrigList.ContainsKey(idx)) CycleTrigList.Remove(idx);

            foreach (var inter in InterruptList)
                if (inter.Value.IOInterIdx == idx)
                {
                    var interrptId = inter.Key;
                    InterruptList.Remove(interrptId);
                }

            return ret;
        }

        public int RemoveDOChanged(int idx)
        {
            var ret = 0;
            return ret;
        }

        public int AddCycleReadInputGroup(int boardId, int group)
        {
            throw new NotImplementedException();
        }

        public int AddCycleReadInputGroup(int boardId, int[] group)
        {
            throw new NotImplementedException();
        }

        public int RemoveCycleReadInput(int boardId, int group)
        {
            throw new NotImplementedException();
        }

        public int RemoveCycleReadInput(int boardId, int[] group)
        {
            throw new NotImplementedException();
        }

        public int StartCycleReadInput(int boardId, int timeSpace)
        {
            throw new NotImplementedException();
        }

        public int EndCycleReadInput(int boardId)
        {
            throw new NotImplementedException();
        }

        public int AddCycleReadOnputGroup(int boardId, int group)
        {
            throw new NotImplementedException();
        }

        public int AddCycleReadOnputGroup(int boardId, int[] group)
        {
            throw new NotImplementedException();
        }

        public int RemoveCycleReadOnput(int boardId, int group)
        {
            throw new NotImplementedException();
        }

        public int RemoveCycleReadOnput(int boardId, int[] group)
        {
            throw new NotImplementedException();
        }

        public int StartCycleReadOnput(int boardId, int timeSpace)
        {
            throw new NotImplementedException();
        }

        public int EndCycleReadOnput(int boardId)
        {
            throw new NotImplementedException();
        }

        public int ReadSingleDOutputFromPrim(int boarId, int group, int idx, out int val)
        {
            throw new NotImplementedException();
        }

        public int ReadSingleDInputFormPrim(int boarId, int group, int idx, out int val)
        {
            throw new NotImplementedException();
        }

        public int ReadMultiDOutputFromPrim(int boarId, int group, int idx, out int val)
        {
            throw new NotImplementedException();
        }

        public int ReadMultiDInputFormPrim(int boarId, int group, int idx, out int val)
        {
            throw new NotImplementedException();
        }

        public int ReadSingleDOutputFromAPI(int boarId, int group, int idx, ref int val)
        {
            throw new NotImplementedException();
        }

        public int ReadSingleDInputFormAPI(int boarId, int group, int idx, ref int val)
        {
            throw new NotImplementedException();
        }

        public int ReadMultiDOutputFromAPI(int boarId, int group, ref int val)
        {
            return APS168.APS_read_d_output(_config.DevIndex, group, ref val);
        }

        public int ReadMultiDInputFromAPI(int boarId, int group, ref int val)
        {
            return APS168.APS_read_d_input(_config.DevIndex, group, ref val);
        }

        public int GetAxisPositionOrFeedbackPules(int axisIdx, ref int position)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Trigger

        //Clear Trigger Count
        public int ResetTriggerCount(int boardId, int trigCh)
        {
            return APS168.APS_reset_trigger_count(_config.DevIndex, trigCh);
        }

        //Set Trig CH Enable "0000"
        public int SetTriggerChannelEnable(int boardId, string enableStr)
        {
            return APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG_EN,
                Convert.ToInt32(enableStr, 2));
        }

        //set Trig CH and Trig Source Enable "00000"
        public int SetTriggerSourceEnable(int board, int trigCh, string enableStr)
        {
            var iRet = 0;

            switch (trigCh)
            {
                case 0:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG0_SRC,
                        Convert.ToInt32(enableStr, 2));
                    break;
                case 1:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG1_SRC,
                        Convert.ToInt32(enableStr, 2));
                    break;
                case 2:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG2_SRC,
                        Convert.ToInt32(enableStr, 2));
                    break;
                case 3:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG3_SRC,
                        Convert.ToInt32(enableStr, 2));
                    break;
            }

            return iRet;
        }

        //Set Trigger Source and Input Source
        public int SetTriggerSouceAndInputSource(int board, int trigSrc, int inputSrc)
        {
            var iRet = 0;

            switch (trigSrc)
            {
                case 0:
                    break;
                case 1:
                    APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_LCMP0_SRC, inputSrc);
                    break;
                case 2:
                    APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_LCMP1_SRC, inputSrc);
                    break;
                case 3:
                    APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP0_SRC, inputSrc);
                    break;
                case 4:
                    APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP1_SRC, inputSrc);
                    break;
            }

            return iRet;
        }

        //Set Trig Channel's Output Pulse Width  pulse width=  value * 0.02 us
        public int SetTrigOutputPulseWidth(int board, int trigCh, int widthTime)
        {
            var iRet = 0;

            switch (trigCh)
            {
                case 0:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG0_PWD,
                        widthTime); //  pulse width=  value * 0.02 us
                    break;
                case 1:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG1_PWD,
                        widthTime); //  pulse width=  value * 0.02 us
                    break;
                case 2:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG2_PWD,
                        widthTime); //  pulse width=  value * 0.02 us
                    break;
                case 3:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG3_PWD,
                        widthTime); //  pulse width=  value * 0.02 us
                    break;
            }

            return iRet;
        }

        //1 logic  0: Not inverse  1:Inverse
        public int SetTrigOutputLogic(int board, int trigCh, int logic)
        {
            var iRet = 0;

            switch (trigCh)
            {
                case 0:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG0_LOGIC, logic);
                    break;
                case 1:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG1_LOGIC, logic);
                    break;
                case 2:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG2_LOGIC, logic);
                    break;
                case 3:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG3_LOGIC, logic);
                    break;
            }

            return iRet;
        }

        public int SetTrigDir(int boardId, int trigCh, int dir)
        {
            var iRet = 0;

            switch (trigCh)
            {
                case 3:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP0_DIR, dir);
                    break;
                case 4:
                    iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP1_DIR, dir);
                    break;
            }

            return iRet;
        }

        //Start Line Trig
        public int StartLinearCompareTrig(int board, int trigCh, int trigStart, int freq, int count)
        {
            var iRet = 0;

            iRet = APS168.APS_set_trigger_linear(_config.DevIndex, trigCh, trigStart, freq, count);

            return iRet;
        }

        public int SetTriggerTable(int tCmpCh, int[] dataArr, int arrSize)
        {
            var iRet = 0;

            iRet = APS168.APS_set_trigger_table(_config.DevIndex, tCmpCh, dataArr, arrSize);

            return iRet;
        }

        #endregion

        public int GetTriggerCount(int trigCh)
        {
            var count = -1;
            APS168.APS_get_trigger_count(_config.DevIndex, trigCh, ref count);
            return count;
        }

        #region Axis General Move Param And Exe

        public int AxisSetEnable(int boardId, int axisId, bool enable)
        {
            return APS168.APS_set_servo_on(axisId, Convert.ToInt32(enable));
        }

        public int AxisSetAcc(int boardId, int axisId, double acc)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_ACC, acc);
        }

        public int AxisSetDec(int boardId, int axisId, double dec)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_DEC, dec);
        }

        public int AxisSetHomeVel(int axis, int vel)
        {
            return 0;
        }

        public int AxisRelMove(int boardId, int axisId, int dis, int maxVel)
        {
            return APS168.APS_relative_move(axisId, dis, maxVel);
        }

        public int AxisAbsMove(int boardId, int axisId, int dis, int maxVel)
        {
            return APS168.APS_absolute_move(axisId, dis, maxVel);
        }

        public int AxisMotionStatus(int boardId, int axisId)
        {
            return APS168.APS_motion_status(axisId);
        }

        public int AxisMotionIOStatus(int boardId, int axisId)
        {
            return APS168.APS_motion_io_status(axisId);
        }

        #endregion

        #region Axis State

        //Axis Feedback Pulse Set
        public int SetAxisPositionOrFeedbackPules(int axisIdx, int position)
        {
            return APS168.APS_set_position_f(axisIdx, position);
        }

        //private object thisLock15 = new object();
        public bool AxisIsStop(int boardId, int axisIdx)
        {
            //var motionio = APS168.APS_motion_io_status(axisIdx);
            var motionStatus1 = APS168.APS_motion_status(axisIdx);
            var mdn = (motionStatus1 & (1 << 5)) != 0;
            var astp = (motionStatus1 & (1 << 16)) == 0;
            //var inp = (motionio & (1 << 6)) == 0;
            if (astp && mdn)
                return true;
            return false;
        }

        public bool AxisIsEnble(int boardId, int axisIdx)
        {
            var motionio = APS168.APS_motion_io_status(axisIdx);
            var ret = (motionio & (1 << 7)) != 0;
            return ret;
        }

        public int AxisStopMove(int boardId, int axisIdx)
        {
            return APS168.APS_stop_move(axisIdx);
        }

        public bool AxisIsAlarm(int boardId, int axisIdx)
        {
            var motionio = APS168.APS_motion_io_status(axisIdx);
            return (motionio & 1) != 0;
        }

        public bool AxisSingalEMG(int boardId, int axisIdx)
        {
            var motionio = APS168.APS_motion_io_status(axisIdx);
            return (motionio & (1 << 4)) != 0;
        }

        public bool LimitZ(int boardId, int axisIdx)
        {
            var motionio = APS168.APS_motion_io_status(axisIdx);
            return (motionio & (1 << 5)) != 0;
        }

        public bool LimitMel(int index, int axisIdx)
        {
            //MEL
            var motionio = APS168.APS_motion_io_status(axisIdx);
            return (motionio & (1 << 2)) != 0;
        }
        public bool LimitPel(int boardId, int axisIdx)
        {
            //PEL
            var motionio = APS168.APS_motion_io_status(axisIdx);
            return (motionio & (1 << 1)) != 0;
        }

        public bool LimitOrg(int motionDevIndex, int axis)
        {
            var motionio = APS168.APS_motion_io_status(axis);
            return (motionio & (1 << 3)) != 0;
        }

        public bool AxisAstp(int boardId, int axisIdx)
        {
            //Astp
            var motionio = APS168.APS_motion_status(axisIdx);
            return (motionio & (1 << 16)) != 0;
        }

        public bool AxisHMV(int boardId, int axisIdx)
        {
            var motionio = APS168.APS_motion_status(axisIdx);
            return (motionio & (1 << 6)) != 0;
        }


        public int AddCycleReadAxisPos(int axisIdx)
        {
            throw new NotImplementedException();
        }

        public int AddCycleReadAxisPos(int[] axisIdx)
        {
            throw new NotImplementedException();
        }

        public int RemoveCycleReadAxisPos(int axisIdx)
        {
            throw new NotImplementedException();
        }

        public int RemoveCycleReadAxisPos(int[] axisIdx)
        {
            throw new NotImplementedException();
        }

        public int StartCycleReadAxisPos(int timeSpace)
        {
            throw new NotImplementedException();
        }

        public int EndCycleReadAxisPos()
        {
            throw new NotImplementedException();
        }

        public int GetAxisPositionOrFeedbackPulesFromPrim(int axisIdx, ref int pos)
        {
            double p = 0;
            var ret = APS168.APS_get_position_f(axisIdx, ref p);
            pos = (int)p;
            return ret;
        }

        public int GetTriggerCount(int boardId, int trigCh, ref int count)
        {
            return APS168.APS_get_trigger_count(boardId, trigCh, ref count);
        }

        public int GetAxisMotionIOStatus(int axisIdx)
        {
            return APS168.APS_motion_io_status(axisIdx);
        }

        public int GetAxisMotionStatus(int axisIdx)
        {
            return APS168.APS_motion_status(axisIdx);
        }


        public int GetAxisPositionF(int axisIdx, ref double pos)
        {
            return APS168.APS_get_position_f(axisIdx, ref pos);
        }

        public int GetAxisCommandF(int axisIdx, ref double command)
        {
            return APS168.APS_get_command_f(axisIdx, ref command);
        }

        public int GetAxisTargetPositionF(int axisIdx, ref double targetPos)
        {
            return APS168.APS_get_target_position_f(axisIdx, ref targetPos);
        }

        public int GetAxisErrPositionF(int axisIdx, ref double errPos)
        {
            return APS168.APS_get_error_position_f(axisIdx, ref errPos);
        }

        public int GetAxisCommandVelocityF(int axisIdx, ref double velocity)
        {
            return APS168.APS_get_command_velocity_f(axisIdx, ref velocity);
        }

        public int GetAxisFeedbackVelocityF(int axisIdx, ref double velocity)
        {
            return APS168.APS_get_feedback_velocity_f(axisIdx, ref velocity);
        }

        #endregion

        #region Axis Home Param And Exe

        public int AxisSetHomeMode(int boardId, int axisId, int homeMode)
        {
            return APS168.APS_set_axis_param(axisId, (int)APS_Define.PRA_HOME_MODE, homeMode);
        }

        public int AxisSetHomeDir(int boardId, int axisId, int homeDir)
        {
            return APS168.APS_set_axis_param(axisId, (int)APS_Define.PRA_HOME_DIR, homeDir);
        }

        public int AxisSetHomeCurve(int boardId, int axisId, double curve)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_CURVE, curve);
        }

        public int AxisSetHomeAcc(int boardId, int axisId, double acc)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_ACC, acc);
        }

        public int AxisSetHomeMaxVel(int boardId, int axisId, double maxVel)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_VM, maxVel);
        }

        public int AxisSetHomeVO(int boardId, int axisId, double vo)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_VO, vo);
        }

        public int AxisSetHomeEZA(int boardId, int axisId, double eza)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_EZA, eza);
        }

        public int AxisSetHomeShift(int boardId, int axisId, double shift)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_SHIFT, shift);
        }

        public int AxisSetHomePos(int boardId, int axisId, double pos)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_HOME_POS, pos);
        }

        public int AxisHomeMove(int boardId, int axisId)
        {
            return APS168.APS_home_move(axisId);
        }

        #endregion

        #region Axis Jog Param And Exe

        public int AxisJogStart(int boardId, int axisId, int statOn)
        {
            return APS168.APS_jog_start(axisId, statOn);
        }

        public int AxisSetJogMode(int boardId, int axisId, int jogMode)
        {
            return APS168.APS_set_axis_param(axisId, (int)APS_Define.PRA_JG_MODE, jogMode);
        }

        public int AxisSetJogDir(int boardId, int axisId, int jogDir)
        {
            return APS168.APS_set_axis_param(axisId, (int)APS_Define.PRA_JG_DIR, jogDir);
        }

        public int AxisSetJogAcc(int boardId, int axisId, double jogAcc)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_JG_ACC, jogAcc);
        }

        public int AxisSetJogDec(int boardId, int axisId, double jogDec)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_JG_DEC, jogDec);
        }

        public int AxisSetJogMaxVel(int boardId, int axisId, double jogMaxVel)
        {
            return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_JG_VM, jogMaxVel);
        }

        #endregion

        #region PT Move Param And Exe

        public int PTDisable(int boardId, int pTbId)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_disable(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTEnable(int boardId, int pTbId, int dimension, int[] axisArray)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_enable(_config.DevIndex, pTbId, dimension, axisArray);
            return iRet;
        }

        public int PTSetAbsolute(int boardId, int pTbId)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_set_absolute(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTSetTransBuffered(int boardId, int pTbId)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_set_trans_buffered(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTSetAcc(int boardId, int pTbId, double acc)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_set_acc(_config.DevIndex, pTbId, acc); //Set acc
            return iRet;
        }

        public int PTSetDec(int boardId, int pTbId, double dec)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_set_dec(_config.DevIndex, pTbId, dec); //Set dec
            return iRet;
        }

        public int PTSetVm(int boardId, int pTbId, double vm)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_set_vm(_config.DevIndex, pTbId, vm);
            return iRet;
        }

        public int PTSetVe(int boardId, int pTbId, double ve)
        {
            var iRet = -1;
            iRet = APS168.APS_pt_set_ve(_config.DevIndex, pTbId, ve);
            return iRet;
        }

        public int PTStop(int boardId, int pTbId)
        {
            var iRet = 0;
            iRet = APS168.APS_pt_stop(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTStart(int boardId, int pTbId)
        {
            var iRet = 0;
            iRet = APS168.APS_pt_start(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTGetStatus(int pTbId, ref PTStatus status)
        {
            var iRet = 0;

            var Status = new PTSTS();
            iRet = APS168.APS_get_pt_status(_config.DevIndex, pTbId, ref Status);

            status.BitSts = Status.BitSts;
            status.PntBufFreeSpace = Status.PntBufFreeSpace;
            status.PntBufUsageSpace = Status.PntBufUsageSpace;
            status.RunningCnt = Status.RunningCnt;

            return iRet;
        }

        #endregion

        #region PT Line Move

        public int PTLineSetPos(int pTbId, double[] pos)
        {
            var iRet = 0;
            //Point table id 0
            var cnt = pos.Length;

            var status = new PTSTS();
            var line = new PTLINE();
            line.Dim = cnt;
            line.Pos = new double[cnt];

            for (var i = 0; i < cnt; i++) line.Pos[i] = pos[i];
            APS168.APS_pt_line(_config.DevIndex, pTbId, ref line, ref status);
            return iRet;
        }

        #endregion

        #endregion

        protected virtual void OnOnDoStateChanged(int arg1, bool arg2, int arg3)
        {
            OnDOStateChanged?.Invoke(arg1, arg2, arg3);
        }

        public bool AxisInp(int index, int axisChannel)
        {
            var motionio = APS168.APS_motion_io_status(axisChannel);
            return (motionio & (1 << 6)) != 0;
        }
    }
}