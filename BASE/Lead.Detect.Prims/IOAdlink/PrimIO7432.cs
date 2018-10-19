using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimIOAdlink.ADLink;

namespace Lead.Detect.PrimIOAdlink
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

    public class PrimIO7432 : IPrim, IMotionCard
    {
        public PrimIO7432()
        {
            _config = new ADLinkConfig();
            PrimDebugUI = new PrimDebugControl();
            PrimConfigUI = new PrimConfigControl(this);
            PrimOutputUI = new PrimOutputControl();

            PrimConnStat = PrimConnState.Other;
            PrimRunStat = PrimRunState.Other;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
        }

        public PrimIO7432(XmlNode xmlNode)
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

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
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
                ((PrimConfigControl) PrimConfigUI).ADLinkName = value;
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

        #region Override IMotionCard's Property

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
        public bool ASTP { get; }
        public bool HMV { get; }
        public bool INP { get; }
        public bool PEL { get; }
        public bool MEL { get; }
        public bool ORG { get; }
        public bool SVON { get; }
        public bool EMG { get; }
        public bool ALM { get; }
        public bool MDN { get; }


        public Action<int, bool, int> OnDIStateChanged;
        public Action<int, bool, int> OnDOStateChanged;

        #endregion

        #region Private Numbers

        //private static bool _initFlag = false;

        public ADLinkConfig _config;

        public int _startAxisId = 0;
        public int _totalAxis = 0;
        public int _cardName = 0;
        public bool _isInitialed = false;

        public bool _cycleWaitInterruptFlag;
        public bool _cycleCycleReadIOFlag;
        public bool _cycleReadPosFlag = false;

        //Key InterruptId; Value IOInfo
        public Dictionary<int, IOInterInfo> InterruptList = new Dictionary<int, IOInterInfo>();
        public Dictionary<int, IOInterModeEnum> CycleTrigList = new Dictionary<int, IOInterModeEnum>();

        //默认5组DI输入，每组32路输入
        public int[] groupDIVal = new int[5];
        public int[] groupDOVal = new int[5];

        public int[] _posVal = new int[4];

        private Task taskCycleReadIO;
        //private Task taskCycleReadPos = null;

        #endregion

        #region Private Function

        private void WaitInterrutp(int interruptId)
        {
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
                uint dInput = 0;
                int ret = DASK.DI_ReadPort((ushort) _config.DevIndex, 0, out dInput);
                if (dInput != 0)
                {
                    if (groupDIVal[0] != -1 && CycleTrigList.Count > 0) CycleCmpDI(0, (int) dInput);
                    groupDIVal[0] = (int) dInput;
                }

                uint dOut = 0;
                ret = DASK.DO_ReadPort((ushort) _config.DevIndex, 0, out dOut);
                if (dOut != 0) groupDOVal[0] = (int) dOut;

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

            int ret = DASK.Register_Card(DASK.PCI_7432, (ushort) boardIdInBits);


            for (var i = 0; i < groupDIVal.Length; i++) groupDIVal[i] = -1;

            for (var i = 0; i < groupDOVal.Length; i++) groupDOVal[i] = -1;

            PrimRunStat = PrimRunState.Stop;
            PrimConnStat = PrimConnState.UnConnected;

            ((PrimConfigControl) PrimConfigUI).SetPrimConnState(PrimConnStat);
            ((PrimConfigControl) PrimConfigUI).SetPrimRunState(PrimRunStat);
            _isInitialed = true;
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

            if (taskCycleReadIO == null)
            {
                taskCycleReadIO = new Task(() => CycleReadIO());
                _cycleCycleReadIOFlag = true;
                taskCycleReadIO.Start();
            }

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
            var ret = 0;

            ret = DASK.DO_WriteLine((ushort) _config.DevIndex, (ushort) group, (ushort) idx, (ushort) iVal);
            return ret;
        }

        public int ReadSingleDOutput(int boardId, int group, int idx, out int iVal)
        {
            uint doValue0 = 0;
            var ret = 0;
            iVal = -1;

            ret = DASK.DO_ReadPort((ushort) _config.DevIndex, 0, out doValue0);
            iVal = (int) (doValue0 >> idx) & 1;
            return ret;
        }

        public int ReadSingleDInput(int boardId, int group, int idx, out int iVal)
        {
            uint diValue0 = 0;
            var ret = 0;
            iVal = -1;

            ret = DASK.DI_ReadPort((ushort) _config.DevIndex, 0, out diValue0);
            iVal = (int) (diValue0 >> idx) & 1;
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
        }

        private void RegsterDIFalling(int idx)
        {
        }

        private void RegsterDIChange(int idx)
        {
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

            if (!_cycleWaitInterruptFlag) _cycleWaitInterruptFlag = true;

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
            try
            {
                uint doValue1;
                DASK.DO_ReadPort((ushort) boarId, 0, out doValue1);
                val = (int) doValue1;
            }
            catch
            {
            }

            return 0;
        }

        public int ReadMultiDInputFromAPI(int boarId, int group, ref int val)
        {
            try
            {
                uint diValue0;
                DASK.DI_ReadPort((ushort) boarId, 0, out diValue0);
                val = (int) diValue0;
            }
            catch
            {
            }

            return 0;
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
            var iRet = 0;

            //iRet = APS168.APS_reset_trigger_count(_config.DevIndex, trigCh);

            return iRet;
        }

        //Set Trig CH Enable "0000"
        public int SetTriggerChannelEnable(int boardId, string enableStr)
        {
            var iRet = 0;
            //iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG_EN, Convert.ToInt32(enableStr, 2));

            return iRet;
        }

        //set Trig CH and Trig Source Enable "00000"
        public int SetTriggerSourceEnable(int board, int trigCh, string enableStr)
        {
            var iRet = 0;

            //switch (trigCh)
            //{
            //    case 0:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG0_SRC, Convert.ToInt32(enableStr, 2));
            //        break;
            //    case 1:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG1_SRC, Convert.ToInt32(enableStr, 2));
            //        break;
            //    case 2:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG2_SRC, Convert.ToInt32(enableStr, 2));
            //        break;
            //    case 3:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG3_SRC, Convert.ToInt32(enableStr, 2));
            //        break;
            //}

            return iRet;
        }

        //Set Trigger Source and Input Source
        public int SetTriggerSouceAndInputSource(int board, int trigSrc, int inputSrc)
        {
            var iRet = 0;

            //switch (trigSrc)
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_LCMP0_SRC, inputSrc);
            //        break;
            //    case 2:
            //        APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_LCMP1_SRC, inputSrc);
            //        break;
            //    case 3:
            //        APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP0_SRC, inputSrc);
            //        break;
            //    case 4:
            //        APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP1_SRC, inputSrc);
            //        break;
            //}

            return iRet;
        }

        //Set Trig Channel's Output Pulse Width  pulse width=  value * 0.02 us
        public int SetTrigOutputPulseWidth(int board, int trigCh, int widthTime)
        {
            var iRet = 0;

            //switch (trigCh)
            //{
            //    case 0:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG0_PWD, widthTime); //  pulse width=  value * 0.02 us
            //        break;
            //    case 1:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG1_PWD, widthTime); //  pulse width=  value * 0.02 us
            //        break;
            //    case 2:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG2_PWD, widthTime); //  pulse width=  value * 0.02 us
            //        break;
            //    case 3:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG3_PWD, widthTime); //  pulse width=  value * 0.02 us
            //        break;
            //}

            return iRet;
        }

        //1 logic  0: Not inverse  1:Inverse
        public int SetTrigOutputLogic(int board, int trigCh, int logic)
        {
            var iRet = 0;

            //switch (trigCh)
            //{
            //    case 0:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG0_LOGIC, logic);
            //        break;
            //    case 1:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG1_LOGIC, logic);
            //        break;
            //    case 2:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG2_LOGIC, logic);
            //        break;
            //    case 3:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TRG3_LOGIC, logic);
            //        break;
            //}

            return iRet;
        }

        public int SetTrigDir(int boardId, int trigCh, int dir)
        {
            var iRet = 0;

            //switch (trigCh)
            //{
            //    case 3:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP0_DIR, dir);
            //        break;
            //    case 4:
            //        iRet = APS168.APS_set_trigger_param(_config.DevIndex, (short)APS_Define.TGR_TCMP1_DIR, dir);
            //        break;
            //}

            return iRet;
        }

        //Start Line Trig
        public int StartLinearCompareTrig(int board, int trigCh, int trigStart, int freq, int count)
        {
            var iRet = 0;

            //iRet = APS168.APS_set_trigger_linear(_config.DevIndex, trigCh, trigStart, freq, count);

            return iRet;
        }

        public int SetTriggerTable(int tCmpCh, int[] dataArr, int arrSize)
        {
            var iRet = 0;

            //iRet = APS168.APS_set_trigger_table(_config.DevIndex, tCmpCh, dataArr, arrSize);

            return iRet;
        }

        #endregion

        public int GetTriggerCount(int trigCh)
        {
            var Count = -1;
            //iRet = APS168.APS_get_trigger_count(_config.DevIndex, trigCh, ref Count);
            return Count;
        }

        #region Axis General Move Param And Exe

        public int AxisSetEnable(int boardId, int axisId, bool enable)
        {
            var iRet = 0;

            //APS168.APS_set_servo_on(axisId, Convert.ToInt32(enable));
            return iRet;
        }

        public int AxisSetAcc(int boardId, int axisId, double acc)
        {
            var iRet = 0;

            //iRet = APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_ACC, acc);

            return iRet;
        }

        public int AxisSetDec(int boardId, int axisId, double dec)
        {
            var iRet = 0;

            //iRet = APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_DEC, dec); ;

            return iRet;
        }

        public int AxisSetHomeVel(int axis, int vel)
        {
            throw new NotImplementedException();
        }

        public int AxisRelMove(int boardId, int axisId, int dis, int maxVel)
        {
            var iRet = 0;

            //APS168.APS_relative_move(axisId, dis, maxVel);

            return iRet;
        }

        public int AxisAbsMove(int boardId, int axisId, int dis, int maxVel)
        {
            var iRet = 0;

            //APS168.APS_absolute_move(axisId, dis, maxVel);
            return iRet;
        }

        public int AxisMotionStatus(int boardId, int axisId)
        {
            return 0;
            //return APS168.APS_motion_status(axisId);
        }

        public int AxisMotionIOStatus(int boardId, int axisId)
        {
            return 0;
            //return APS168.APS_motion_io_status(axisId);
        }

        #endregion

        #region Axis State

        //Axis Feedback Pulse Set
        public bool AxisHMV(int boardId, int axisIdx)
        {
            throw new NotImplementedException();
        }

        public int SetAxisPositionOrFeedbackPules(int axisIdx, int position)
        {
            var iRet = 0;


            //iRet = APS168.APS_set_position(axisIdx, position);

            return iRet;
        }

        //private object thisLock15 = new object();
        public bool AxisIsStop(int boardId, int axisIdx)
        {
            //int motionIoStatus1, motionStatus1;
            ////刷新MotionIoStatus
            //motionIoStatus1 = APS168.APS_motion_io_status(axisIdx);
            //motionStatus1 = APS168.APS_motion_status(axisIdx);
            //bool status2 = false;
            //bool status1 = false;
            //status1 = (motionIoStatus1 & (1 << 6)) != 0;
            //status2 = (motionStatus1 & (1 << 5)) != 0;
            //if (status1 && status2)
            //    return true;
            //else
            return false;
        }

        public bool AxisIsEnble(int boardId, int axisIdx)
        {
            //boardId = -1;
            //int motionIoStatus = APS168.APS_motion_io_status(axisIdx);
            //bool status = (motionIoStatus & (1 << 7)) != 0;
            //return status;

            return false;
        }

        public int AxisStopMove(int boardId, int axisIdx)
        {
            var iRet = 0;
            //iRet = APS168.APS_stop_move(axisIdx);
            return iRet;
        }

        public bool AxisIsAlarm(int boardId, int axisIdx)
        {
            //boardId = -1;
            //int motionIoStatus = APS168.APS_motion_io_status(axisIdx);
            //bool status = (motionIoStatus & 1) != 0;
            //return status;
            return false;
        }

        public bool AxisSingalEMG(int boardId, int axisIdx)
        {
            //}
            //int motionStatusMdn = 4;

            //int num2 = (APS168.APS_motion_io_status(axisIdx) & (1 << motionStatusMdn));
            //if (num2 == 0)
            //    return false;
            //else
            return true;
        }

        public bool LimitZ(int boardId, int axisIdx)
        {
            //   lock (this.thisLock15)
            //{
            //    int num2 = (APS168.APS_motion_status(axisIdx) >> 0x10) & 1;
            //    return (num2 == 0);
            //}

            //int motionStatusMdn = 1;

            //int num2 = (APS168.APS_motion_io_status(axisIdx) & (1 << motionStatusMdn));
            //if (num2 == 0)
            //    return false;
            //else
            return true;
        }

        public bool LimitMel(int index, int axisIdx)
        {
            //   lock (this.thisLock15)
            //{
            //    int num2 = (APS168.APS_motion_status(axisIdx) >> 0x10) & 1;
            //    return (num2 == 0);
            //}
            //int motionStatusMdn = 2;

            //int num2 = (APS168.APS_motion_io_status(axisIdx) & (1 << motionStatusMdn));

            //if (num2 == 0)
            //    return false;
            //else
            return true;
        }

        public bool LimitOrg(int motionDevIndex, int axis)
        {
            throw new NotImplementedException();
        }

        public bool AxisAstp(int boardId, int axisIdx)
        {
            throw new NotImplementedException();
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

        public int GetAxisPositionOrFeedbackPulesFromPrim(int axisIdx, ref int position)
        {
            throw new NotImplementedException();
        }

        public int GetTriggerCount(int boardId, int trigCh, ref int count)
        {
            return 0;
            //return APS168.APS_get_trigger_count(boardId, trigCh, ref count);
        }

        public int GetAxisMotionIOStatus(int axisIdx)
        {
            return 0;
            //return APS168.APS_motion_io_status(axisIdx);
        }

        public int GetAxisMotionStatus(int axisIdx)
        {
            return 0;
            //return APS168.APS_motion_status(axisIdx);
        }


        public int GetAxisPositionF(int axisIdx, ref double pos)
        {
            return 0;
            //return APS168.APS_get_position_f(axisIdx, ref pos);
        }

        public int GetAxisCommandF(int axisIdx, ref double command)
        {
            return 0;
            //return APS168.APS_get_command_f(axisIdx, ref command);
        }

        public int GetAxisTargetPositionF(int axisIdx, ref double targetPos)
        {
            return 0;
            //return APS168.APS_get_target_position_f(axisIdx, ref targetPos);
        }

        public int GetAxisErrPositionF(int axisIdx, ref double errPos)
        {
            return 0;
            //return APS168.APS_get_error_position_f(axisIdx, ref errPos);
        }

        public int GetAxisCommandVelocityF(int axisIdx, ref double velocity)
        {
            return 0;
            //return APS168.APS_get_command_velocity_f(axisIdx, ref velocity);
        }

        public int GetAxisFeedbackVelocityF(int axisIdx, ref double velocity)
        {
            return 0;
            //return APS168.APS_get_feedback_velocity_f(axisIdx, ref velocity);
        }

        #endregion

        #region Axis Home Param And Exe

        public int AxisSetHomeMode(int boardId, int axisId, int homeMode)
        {
            //return APS168.APS_set_axis_param(axisId, (Int32)APS_Define.PRA_HOME_MODE, homeMode);
            return 0;
        }

        public int AxisSetHomeDir(int boardId, int axisId, int homeDir)
        {
            //return APS168.APS_set_axis_param(axisId, (Int32)APS_Define.PRA_HOME_DIR, homeDir);
            return 0;
        }

        public int AxisSetHomeCurve(int boardId, int axisId, double curve)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_CURVE, curve);
            return 0;
        }

        public int AxisSetHomeAcc(int boardId, int axisId, double acc)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_ACC, acc);
            return 0;
        }

        public int AxisSetHomeMaxVel(int boardId, int axisId, double maxVel)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_VM, maxVel);
            return 0;
        }

        public int AxisSetHomeVO(int boardId, int axisId, double vo)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_VO, vo);
            return 0;
        }

        public int AxisSetHomeEZA(int boardId, int axisId, double eza)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_EZA, eza);
            return 0;
        }

        public int AxisSetHomeShift(int boardId, int axisId, double shift)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_SHIFT, shift);
            return 0;
        }

        public int AxisSetHomePos(int boardId, int axisId, double pos)
        {
            //return APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_POS, pos);
            return 0;
        }

        public int AxisHomeMove(int boardId, int axisId)
        {
            //return APS168.APS_home_move(axisId);
            return 0;
        }

        #endregion

        #region Axis Jog Param And Exe

        public int AxisJogStart(int boardId, int axisId, int statOn)
        {
            //return APS168.APS_jog_start(axisId, statOn);
            return 0;
        }

        public int AxisSetJogMode(int boardId, int axisId, int jogMode)
        {
            //return APS168.APS_set_axis_param(axisId, (int)APS_Define.PRA_JG_MODE, jogMode);
            return 0;
        }

        public int AxisSetJogDir(int boardId, int axisId, int jogDir)
        {
            //return APS168.APS_set_axis_param(axisId, (int)APS_Define.PRA_JG_DIR, jogDir);
            return 0;
        }

        public int AxisSetJogAcc(int boardId, int axisId, double jogAcc)
        {
            //return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_JG_ACC, jogAcc);
            return 0;
        }

        public int AxisSetJogDec(int boardId, int axisId, double jogDec)
        {
            //return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_JG_DEC, jogDec);
            return 0;
        }

        public int AxisSetJogMaxVel(int boardId, int axisId, double jogMaxVel)
        {
            //return APS168.APS_set_axis_param_f(axisId, (int)APS_Define.PRA_JG_VM, jogMaxVel);
            return 0;
        }

        #endregion

        #region PT Move Param And Exe

        public int PTDisable(int boardId, int pTbId)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_disable(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTEnable(int boardId, int pTbId, int dimension, int[] axisArray)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_enable(_config.DevIndex, pTbId, dimension, axisArray);
            return iRet;
        }

        public int PTSetAbsolute(int boardId, int pTbId)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_set_absolute(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTSetTransBuffered(int boardId, int pTbId)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_set_trans_buffered(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTSetAcc(int boardId, int pTbId, double acc)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_set_acc(_config.DevIndex, pTbId, acc); //Set acc
            return iRet;
        }

        public int PTSetDec(int boardId, int pTbId, double dec)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_set_dec(_config.DevIndex, pTbId, dec); //Set dec
            return iRet;
        }

        public int PTSetVm(int boardId, int pTbId, double vm)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_set_vm(_config.DevIndex, pTbId, vm);
            return iRet;
        }

        public int PTSetVe(int boardId, int pTbId, double ve)
        {
            var iRet = -1;
            //iRet = APS168.APS_pt_set_ve(_config.DevIndex, pTbId, ve);
            return iRet;
        }

        public int PTStop(int boardId, int pTbId)
        {
            var iRet = 0;
            //iRet = APS168.APS_pt_stop(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTStart(int boardId, int pTbId)
        {
            var iRet = 0;
            //iRet = APS168.APS_pt_start(_config.DevIndex, pTbId);
            return iRet;
        }

        public int PTGetStatus(int pTbId, ref PTStatus status)
        {
            var iRet = 0;

            //PTSTS Status = new PTSTS();
            //iRet = APS168.APS_get_pt_status(_config.DevIndex, pTbId, ref Status);

            //status.BitSts = Status.BitSts;
            //status.PntBufFreeSpace = Status.PntBufFreeSpace;
            //status.PntBufUsageSpace = Status.PntBufUsageSpace;
            //status.RunningCnt = Status.RunningCnt;

            return iRet;
        }

        #endregion

        #region PT Line Move

        public int PTLineSetPos(int pTbId, double[] pos)
        {
            var iRet = 0;
            //Point table id 0
            //int cnt = pos.Length;

            //PTSTS status = new PTSTS();
            //PTLINE line = new PTLINE();
            //line.Dim = cnt;
            //line.Pos = new double[cnt];

            //for (int i = 0; i < cnt; i++)
            //{
            //    line.Pos[i] = pos[i];
            //}
            //APS168.APS_pt_line(_config.DevIndex, pTbId, ref line, ref status);
            return iRet;
        }

        public int MoveAbs(MoveProfile profile)
        {
            return 0;
        }

        public int MoveAbs(MoveProfile[] profile)
        {
            return 0;
        }

        public int MoveAbs(List<MoveProfile> profile)
        {
            return 0;
        }

        public int Home(MoveProfile profile)
        {
            return 0;
        }

        public int Home(MoveProfile[] profile)
        {
            return 0;
        }

        public int Home(List<MoveProfile> profile)
        {
            return 0;
        }

        public void Stop(MoveProfile profile)
        {
        }

        public void Stop(MoveProfile[] profile)
        {
        }

        public int WaitMoveDone(MoveProfile[] profile, bool bCheckLimit, bool bCheckPos)
        {
            return 0;
        }

        public int WaitMoveDone(List<MoveProfile> profile, bool bCheckLimit, bool bCheckPos)
        {
            return 0;
        }

        public int WaitMoveDone(MoveProfile profile, bool bCheckLimit, bool bCheckPos)
        {
            return 0;
        }

        public void ReadStatus(int axis)
        {
            throw new NotImplementedException();
        }

        public bool LimitPel(int boardId, int axisIdx)
        {
            throw new NotImplementedException();
        }

        public void ReadStatus(MoveProfile profile)
        {
        }

        #endregion

        #endregion
    }
}