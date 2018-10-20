using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimVirtualCard.motionWrapper;
using Timer = System.Threading.Timer;
using Lead.Detect.Helper;

namespace Lead.Detect.PrimVirtualCard
{
    [Serializable]
    public class VirtualCard : IPrim, IMotionCard
    {
        #region  origin impletions

        public readonly CardAxisMotion[] CardAxisMotion;


        [XmlIgnore]
        public int[] Di { get; }

        [XmlIgnore]
        public int[] Do { get; }


        private Timer _timer;

        public VirtualCard()
        {
            Name = "VC";
            Di = new int[32];
            Do = new int[32];

            CardAxisMotion = new CardAxisMotion[16];
            for (int i = 0; i < CardAxisMotion.Length; i++)
            {
                CardAxisMotion[i].Mdn = true;
            }


            _timer = new Timer(OnTimer, null, 0, 100);

            //prim ctor
            PrimConfigUI = new ConfigControl(this);
        }

        public VirtualCard(XmlNode node)
        {
            Name = "VC";
            Di = new int[32];
            Do = new int[32];
            CardAxisMotion = new CardAxisMotion[16];
            for (int i = 0; i < CardAxisMotion.Length; i++)
            {
                CardAxisMotion[i].Mdn = true;
            }

            _timer = new Timer(OnTimer, null, 0, 100);

            //prim ctor
            PrimConfigUI = new ConfigControl(this);
            ImportConfig(node);
        }


        private void OnTimer(object state)
        {
            lock (this)
            {
                //motion simulate update
                for (var i = 0; i < CardAxisMotion.Length; i++)
                {
                    CardAxisMotion[i].Update();
                }
            }
        }


        public int InitialCard(int cardNum, string configFileName)
        {
            return 0;
        }

        public int ReleaseCard(int cardNum)
        {
            return 0;
        }

        public int LoadParams(string path)
        {
            return 0;
        }

        public void ClrSts(int cardId, int axis)
        {
        }

        public int ClearServoAlarm(int id, int axis)
        {
            lock (this)
            {
                CardAxisMotion[axis].Alarm = false;
                CardAxisMotion[axis].Servo = false;
                return 0;
            }
        }

        public int SetServo(int id, int axis, bool sts)
        {
            lock (this)
            {
                CardAxisMotion[axis].Servo = sts;
                return 0;
            }
        }

        #region axis move

        public int SetAxisBand(int id, int axis, int band_Pulse, int time_ms)
        {
            return 0;
        }

        public int SetCommandPos(int id, int axis, int pos)
        {
            lock (this)
            {
                CardAxisMotion[axis].CommandPos = pos;
                return 0;
            }
        }

        public int GetCommandPos(int id, int axis, ref int pos)
        {
            lock (this)
            {
                pos = CardAxisMotion[axis].CommandPos;
                return 0;
            }
        }

        public int SetEncPos(int id, int axis, int encpos)
        {
            lock (this)
            {
                CardAxisMotion[axis].CurPos = encpos;
                return 0;
            }
        }

        public int GetEncPos(int id, int axis, ref int pos)
        {
            lock (this)
            {
                pos = CardAxisMotion[axis].CurPos;
                return 0;
            }
        }


        public int GetMotionIO(int id, int axis, ref int sts)
        {
            lock (this)
            {
                sts = 0;

                if (CardAxisMotion[axis].Alarm)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_ALM);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_ALM);
                }

                if (CardAxisMotion[axis].Pel)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_PEL);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_PEL);
                }

                if (CardAxisMotion[axis].Mel)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_MEL);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_MEL);
                }

                if (CardAxisMotion[axis].Org)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_ORG);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_ORG);
                }

                if (CardAxisMotion[axis].Astp)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_EMG);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_EMG);
                }

                if (CardAxisMotion[axis].Servo)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_SVON);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_SVON);
                }

                return 0;
            }
        }

        public int GetMotionSts(int id, int axis, ref int sts)
        {
            lock (this)
            {
                sts = 0;

                if (!CardAxisMotion[axis].Mdn)
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_STS_MDN);
                }
                else
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_STS_MDN);
                }

                //if (_cardAxisMotion[axis].Astp || _cardAxisMotion[axis].Mel || _cardAxisMotion[axis].Pel || !_cardAxisMotion[axis].Servo)
                //{
                //    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_STS_ASTP);
                //}
                //else
                //{
                //    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_STS_ASTP);
                //}

                return 0;
            }
        }

        public int SetMoveParm(int cardId, int axis, double Acc, double Dec)
        {
            CardAxisMotion[axis].Acc = (int)Acc;
            CardAxisMotion[axis].Dec = (int)Dec;
            return 0;
        }

        public int ABSMove(int id, int axis, int pos, int vel)
        {
            lock (this)
            {
                if (CardAxisMotion[axis].IsMove)
                {
                    return 0;
                }

                CardAxisMotion[axis].Mdn = false;
                CardAxisMotion[axis].CommandPos = pos;
                CardAxisMotion[axis].Vel = vel * Math.Sign(CardAxisMotion[axis].CommandPos - CardAxisMotion[axis].CurPos);

                CardAxisMotion[axis].IsMove = true;
                return 0;
            }
        }

        public int RELMove(int id, int axis, int pos, int vel)
        {
            lock (this)
            {
                if (CardAxisMotion[axis].IsMove)
                {
                    return 0;
                }

                CardAxisMotion[axis].Mdn = false;
                CardAxisMotion[axis].CommandPos = CardAxisMotion[axis].CurPos + pos;
                CardAxisMotion[axis].Vel = vel * Math.Sign(pos);

                CardAxisMotion[axis].IsMove = true;
                return 0;
            }
        }

        public int ZeroPos(int id, int axis)
        {
            lock (this)
            {
                CardAxisMotion[axis].CurPos = 0;
                CardAxisMotion[axis].CommandPos = 0;
            }

            return 0;
        }

        public int EMGStop(int id, int axis)
        {
            lock (this)
            {
                CardAxisMotion[axis].IsMove = false;
                CardAxisMotion[axis].Mdn = true;
                return 0;
            }
        }

        public int StopAxis(int id, int axis)
        {
            lock (this)
            {
                CardAxisMotion[axis].IsMove = false;
                CardAxisMotion[axis].Mdn = true;
                return 0;
            }
        }

        #endregion

        #region home

        public int HomeMove(int id, int axis, int vel)
        {
            return 0;
        }

        public int SetCaptureModeHome(int id, int axis)
        {
            return 0;
        }

        public int GetCaptureHome(int id, int axis, ref int CaptureSts, ref int CapturePos)
        {
            CaptureSts = 1;
            CapturePos = 0;
            return 0;
        }

        public int SetCaptureModeIndex(int id, int axis)
        {
            return 0;
        }

        public int GetCaptureIndex(int id, int axis, ref int CaptureSts, ref int CapturePos)
        {
            CaptureSts = 1;
            CapturePos = 0;
            return 0;
        }

        public int SearchForHome(int id, int axis, int length, double vel)
        {
            lock (this)
            {
                CardAxisMotion[axis].Mel = false;
                CardAxisMotion[axis].Pel = false;
                CardAxisMotion[axis].Org = true;
                return 0;
            }
        }

        public int SearchForIndex(int id, int axis, int length, double vel)
        {
            lock (this)
            {
                CardAxisMotion[axis].Mel = false;
                CardAxisMotion[axis].Pel = false;
                CardAxisMotion[axis].Org = true;
                return 0;
            }
        }

        public int SearchForLimit(int id, int axis, int length, double vel)
        {
            lock (this)
            {
                if (length > 0)
                {
                    CardAxisMotion[axis].Mel = false;
                    CardAxisMotion[axis].Pel = true;
                    CardAxisMotion[axis].Org = false;
                }
                else
                {
                    CardAxisMotion[axis].Mel = true;
                    CardAxisMotion[axis].Pel = false;
                    CardAxisMotion[axis].Org = false;
                }

                return 0;
            }
        }

        #endregion

        #region di do

        public int GetDi(int id, int index, ref int status)
        {
            status = Di[index];
            return 0;
        }

        public int SetDi(int id, int index, int status)
        {
            Di[index] = status;
            return 0;
        }

        public int SetDo(int id, int index, int status)
        {
            Do[index] = status;
            return 0;
        }

        public int GetDo(int id, int index, ref int status)
        {
            status = Do[index];
            return 0;
        }

        public int GetAD(int id, int index, ref double adValue)
        {
            return 0;
        }

        public int GetDA(int id, int index, ref double adValue)
        {
            return 0;
        }

        #endregion

        #endregion


        //#region  IMotionCardWrapper


        //public void Init(string file)
        //{
        //}

        //public void Uninit()
        //{
        //}

        //public void GetDi(int port, out int status)
        //{
        //    status = 0;
        //    GetDi(Id, port, ref status);
        //}

        //public void SetDo(int port, int status)
        //{
        //    SetDo(Id, port, status);
        //}

        //public int GetCommandPos(int axis, ref int pos)
        //{
        //    GetCommandPos(Id, axis, ref pos);
        //    return 0;
        //}

        //public int GetEncPos(int axis, ref int pos)
        //{
        //    GetEncPos(Id, axis, ref pos);
        //    return 0;
        //}

        //public int SetCommandPos(int axis, int pos)
        //{
        //    SetCommandPos(Id, axis, pos);
        //    return 0;
        //}

        //public int SetEncPos(int axis, int pos)
        //{
        //    SetEncPos(Id, axis, pos);
        //    return 0;
        //}

        //public void ServoEnable(int axis, bool enable)
        //{
        //    _cardAxisMotion[axis].Servo = enable;
        //}

        //public void Home(int axis, int vel)
        //{
        //    _cardAxisMotion[axis].Hmv = true;
        //}

        //public void MoveAbs(int axis, int pos, int vel)
        //{
        //    ABSMove(Id, axis, pos, vel);
        //}

        //public void MoveRel(int axis, int step, int vel)
        //{
        //    RELMove(Id, axis, step, vel);
        //}

        //public void MoveStop(int axis)
        //{
        //    _cardAxisMotion[axis].IsMove = false;

        //    _cardAxisMotion[axis].CurPos = _cardAxisMotion[axis].CommandPos;

        //    _cardAxisMotion[axis].Mdn = true;
        //}

        //public bool CheckHomeDone(int axis)
        //{
        //    return _cardAxisMotion[axis].Hmv;
        //}

        //public bool CheckMoveDone(int axis)
        //{
        //    return _cardAxisMotion[axis].Astp;
        //}

        //public bool GetAxisEnable(int axis)
        //{
        //    return _cardAxisMotion[axis].Servo;
        //}

        //public bool GetAxisAlarm(int axis)
        //{
        //    return _cardAxisMotion[axis].Astp;
        //}

        //public bool GetAxisEmg(int axis)
        //{
        //    return _cardAxisMotion[axis].Astp;
        //}

        //public bool GetAxisDone(int axis)
        //{
        //    return _cardAxisMotion[axis].Astp;
        //}


        //#endregion


        #region IPrim

        public string PrimTypeName { get; set; } = nameof(VirtualCard);
        public Guid PrimId { get; set; } = Guid.NewGuid();
        public Type ChildType { get; set; }
        public PrimType HSType { get; set; }
        public PrimManufacture Manu { get; set; }
        public PrimVer Ver { get; set; }
        public PrimConnType ConnType { get; set; }
        public string ConnInfo { get; set; }
        public PrimConnState PrimConnStat { get; set; } = PrimConnState.Connected;
        public PrimRunState PrimRunStat { get; set; } = PrimRunState.Running;
        public bool PrimSimulator { get; set; }
        public bool PrimDebug { get; set; }
        public bool PrimEnable { get; set; }

        [XmlIgnore]
        public IDataArea DataArea { get; set; }

        [XmlIgnore]
        public Control PrimDebugUI { get; }

        [XmlIgnore]
        public Control PrimConfigUI { get; }

        [XmlIgnore]
        public Control PrimOutputUI { get; }

        public int IPrimConnect(ref string result)
        {
            return 0;
        }

        public int IPrimDisConnect(ref string result)
        {
            return 0;
        }

        public int IPrimDispose()
        {
            return 0;
        }

        public XmlNode ExportConfig()
        {
            return XMLHelper.ObjectToXML(this);
        }

        public XmlNode ExportDataConfig()
        {
            return XMLHelper.ObjectToXML(this);
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var v = XMLHelper.XMLToObject(xmlNode, typeof(VirtualCard)) as VirtualCard;
            if (v != null)
            {
                Id = v.Id;
                Name = v.Name;

                PrimId = v.PrimId;

                ConfigFilePath = v.ConfigFilePath;

                PrimEnable = v.PrimEnable;
            }

            return 0;
        }

        public int ImportDataConfig(XmlNode xmlNode)
        {
            return 0;
        }

        public int IPrimInit()
        {
            return 0;
        }

        public int IPrimResume()
        {
            return 0;
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            return 0;
        }

        public int IPrimStart()
        {
            return 0;
        }

        public int IPrimStop()
        {
            return 0;
        }

        public int IPrimSuspend()
        {
            return 0;
        }

        public event PrimDataRefresh OnPrimDataRefresh;
        public event PrimOpLog OnPrimOpLog;
        public event PrimStateChanged OnPrimStateChanged;

        #endregion


        #region  IMotionCard

        public string ConfigFilePath { get; set; }
        public int DevIndex { get; set; }
        public bool DIChangedEnable { get; set; }
        public bool DOChangedEnable { get; set; }

        [XmlIgnore]
        public bool ASTP { get; protected set; }

        [XmlIgnore]
        public bool HMV { get; protected set; }

        [XmlIgnore]
        public bool INP { get; protected set; }

        [XmlIgnore]
        public bool PEL { get; protected set; }

        [XmlIgnore]
        public bool MEL { get; protected set; }

        [XmlIgnore]
        public bool ORG { get; protected set; }

        [XmlIgnore]
        public bool SVON { get; protected set; }

        [XmlIgnore]
        public bool EMG { get; protected set; }

        [XmlIgnore]
        public bool ALM { get; protected set; }

        [XmlIgnore]
        public bool MDN { get; protected set; }

        public int Id { get; set; }


        public string Name { get; set; }

        public void LoadConfigFile(string filePath)
        {
        }

        public void LoadConfigFileLocal()
        {
        }

        public int AddCycleReadInputGroup(int boardId, int group)
        {
            return 0;
        }

        public int AddCycleReadInputGroup(int boardId, int[] group)
        {
            return 0;
        }

        public int RemoveCycleReadInput(int boardId, int group)
        {
            return 0;
        }

        public int RemoveCycleReadInput(int boardId, int[] group)
        {
            return 0;
        }

        public int StartCycleReadInput(int boardId, int timeSpace)
        {
            return 0;
        }

        public int EndCycleReadInput(int boardId)
        {
            return 0;
        }

        public int AddCycleReadOnputGroup(int boardId, int group)
        {
            return 0;
        }

        public int AddCycleReadOnputGroup(int boardId, int[] group)
        {
            return 0;
        }

        public int RemoveCycleReadOnput(int boardId, int group)
        {
            return 0;
        }

        public int RemoveCycleReadOnput(int boardId, int[] group)
        {
            return 0;
        }

        public int StartCycleReadOnput(int boardId, int timeSpace)
        {
            return 0;
        }

        public int EndCycleReadOnput(int boardId)
        {
            return 0;
        }

        public int WriteSingleDOutput(int boarId, int group, int idx, int val)
        {
            if (Do != null)
            {
                Do[idx] = val;
            }
            return 0;
        }

        public int ReadSingleDOutput(int boarId, int group, int idx, out int val)
        {
            if (Do != null)
            {
                val = Do[idx];
                return 0;
            }
            else
            {
                val = 0;
                return 0;
            }
        }

        public int ReadSingleDInput(int boarId, int group, int idx, out int val)
        {
            if (Di != null)
            {
                val = Di[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int WriteMultiDOutput(int startIdx, int num, int[] val)
        {
            return 0;
        }

        public int[] ReadMultiDOutput(int startIdx, int num)
        {
            return new[] { 0 };
        }

        public int[] ReadMultiDInput(int startIdx, int num)
        {
            return new[] { 0 };
        }

        public int ReadSingleDOutputFromPrim(int boarId, int group, int idx, out int val)
        {
            if (Do != null)
            {
                val = Do[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int ReadSingleDInputFormPrim(int boarId, int group, int idx, out int val)
        {
            if (Di != null)
            {
                val = Di[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int ReadMultiDOutputFromPrim(int boarId, int group, int idx, out int val)
        {
            if (Do != null)
            {
                val = Do[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int ReadMultiDInputFormPrim(int boarId, int group, int idx, out int val)
        {
            if (Do != null)
            {
                val = Do[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int ReadSingleDOutputFromAPI(int boarId, int group, int idx, ref int val)
        {
            if (Do != null)
            {
                val = Do[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int ReadSingleDInputFormAPI(int boarId, int group, int idx, ref int val)
        {
            if (Di != null)
            {
                val = Di[idx];
                return 0;
            }

            val = 0;
            return 0;
        }

        public int ReadMultiDOutputFromAPI(int boarId, int group, ref int val)
        {
            return 0;
        }

        public int ReadMultiDInputFromAPI(int boarId, int group, ref int val)
        {
            return 0;
        }

        public int RegisterDIChanged(int idx, int mode, int type)
        {
            return 0;
        }

        public int RegisterDOChanged(int idx, int mode)
        {
            return 0;
        }

        public int RemoveDIChanged(int idx)
        {
            return 0;
        }

        public int RemoveDOChanged(int idx)
        {
            return 0;
        }

        public int ResetTriggerCount(int boardId, int trigCh)
        {
            return 0;
        }

        public int SetTriggerChannelEnable(int boardId, string enableStr)
        {
            return 0;
        }

        public int SetTriggerSourceEnable(int boardId, int trigCh, string enableStr)
        {
            return 0;
        }

        public int SetTriggerSouceAndInputSource(int boardId, int trigSrc, int inputSrc)
        {
            return 0;
        }

        public int SetTrigOutputPulseWidth(int boardId, int trigCh, int widthTime)
        {
            return 0;
        }

        public int SetTrigOutputLogic(int boardId, int trigCh, int logic)
        {
            return 0;
        }

        public int SetTrigDir(int boardId, int trigCh, int dir)
        {
            return 0;
        }

        public int StartLinearCompareTrig(int boardId, int trigCh, int trigStart, int freq, int count)
        {
            return 0;
        }

        public int GetTriggerCount(int boardId, int trigCh, ref int count)
        {
            return 0;
        }

        public int SetTriggerTable(int tCmpCh, int[] dataArr, int arrSize)
        {
            return 0;
        }

        public bool AxisIsStop(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Mdn;
        }

        public int GetTriggerCount(int trigCh)
        {
            return 0;
        }

        public bool AxisIsEnble(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Servo;
        }

        public bool AxisIsAlarm(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Alarm;
        }

        public int AxisStopMove(int boardId, int axisIdx)
        {
            CardAxisMotion[axisIdx].IsMove = false;
            CardAxisMotion[axisIdx].Mdn = true;
            return 0;
        }

        public bool LimitZ(int boardId, int axisIdx)
        {
            return false;
        }

        public bool AxisSingalEMG(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Alarm;
        }

        public bool LimitMel(int index, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Mel;
        }

        public bool LimitOrg(int motionDevIndex, int axis)
        {
            return CardAxisMotion[axis].Org;
        }

        public bool AxisAstp(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Astp;
        }

        public bool AxisHMV(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Hmv;
        }

        public int SetAxisPositionOrFeedbackPules(int axisIdx, int position)
        {
            CardAxisMotion[axisIdx].CurPos = position;
            return 0;
        }

        public int GetAxisPositionOrFeedbackPules(int axisIdx, ref int position)
        {
            position = CardAxisMotion[axisIdx].CurPos;
            return 0;
        }

        public int GetAxisMotionIOStatus(int axisIdx)
        {
            return 0;
        }

        public int GetAxisMotionStatus(int axisIdx)
        {
            return 0;
        }

        public int GetAxisPositionF(int axisIdx, ref double pos)
        {
            pos = CardAxisMotion[axisIdx].CurPos;
            return 0;
        }

        public int GetAxisCommandF(int axisIdx, ref double command)
        {
            command = CardAxisMotion[axisIdx].CommandPos;
            return 0;
        }

        public int GetAxisTargetPositionF(int axisIdx, ref double targetPos)
        {
            targetPos = CardAxisMotion[axisIdx].CommandPos;
            return 0;
        }

        public int GetAxisErrPositionF(int axisIdx, ref double errPos)
        {
            return 0;
        }

        public int GetAxisCommandVelocityF(int axisIdx, ref double velocity)
        {
            return 0;
        }

        public int GetAxisFeedbackVelocityF(int axisIdx, ref double velocity)
        {
            return 0;
        }

        public int AddCycleReadAxisPos(int axisIdx)
        {
            return 0;
        }

        public int AddCycleReadAxisPos(int[] axisIdx)
        {
            return 0;
        }

        public int RemoveCycleReadAxisPos(int axisIdx)
        {
            return 0;
        }

        public int RemoveCycleReadAxisPos(int[] axisIdx)
        {
            return 0;
        }

        public int StartCycleReadAxisPos(int timeSpace)
        {
            return 0;
        }

        public int EndCycleReadAxisPos()
        {
            return 0;
        }

        public int GetAxisPositionOrFeedbackPulesFromPrim(int axisIdx, ref int position)
        {
            return 0;
        }

        public int AxisSetEnable(int boardId, int axisId, bool enable)
        {
            CardAxisMotion[axisId].Servo = enable;
            return 0;
        }

        public int AxisSetAcc(int boardId, int axisId, double acc)
        {
            return 0;
        }

        public int AxisSetDec(int boardId, int axisId, double dec)
        {
            return 0;
        }

        public int AxisSetHomeVel(int axis, int vel)
        {
            return 0;
        }

        public int AxisRelMove(int boardId, int axisId, int dis, int maxVel)
        {
            RELMove(boardId, axisId, dis, maxVel);
            return 0;
        }

        public int AxisAbsMove(int boardId, int axisId, int dis, int maxVel)
        {
            ABSMove(boardId, axisId, dis, maxVel);
            return 0;
        }

        public int AxisMotionStatus(int boardId, int axisId)
        {
            return 0;
        }

        public int AxisMotionIOStatus(int boardId, int axisId)
        {
            return 0;
        }

        public int AxisSetHomeMode(int boardId, int axisId, int homeMode)
        {
            return 0;
        }

        public int AxisSetHomeDir(int boardId, int axisId, int homeDir)
        {
            return 0;
        }

        public int AxisSetHomeCurve(int boardId, int axisId, double curve)
        {
            return 0;
        }

        public int AxisSetHomeAcc(int boardId, int axisId, double acc)
        {
            return 0;
        }

        public int AxisSetHomeMaxVel(int boardId, int axisId, double maxVel)
        {
            return 0;
        }

        public int AxisSetHomeVO(int boardId, int axisId, double vo)
        {
            return 0;
        }

        public int AxisSetHomeEZA(int boardId, int axisId, double eza)
        {
            return 0;
        }

        public int AxisSetHomeShift(int boardId, int axisId, double shift)
        {
            return 0;
        }

        public int AxisSetHomePos(int boardId, int axisId, double pos)
        {
            return 0;
        }

        public int AxisHomeMove(int boardId, int axisId)
        {
            CardAxisMotion[axisId].Mdn = true;
            CardAxisMotion[axisId].Hmv = false;
            return 0;
        }

        public int AxisJogStart(int boardId, int axisId, int statOn)
        {
            return 0;
        }

        public int AxisSetJogMode(int boardId, int axisId, int jogMode)
        {
            return 0;
        }

        public int AxisSetJogDir(int boardId, int axisId, int jogDir)
        {
            return 0;
        }

        public int AxisSetJogAcc(int boardId, int axisId, double jogAcc)
        {
            return 0;
        }

        public int AxisSetJogDec(int boardId, int axisId, double jogDec)
        {
            return 0;
        }

        public int AxisSetJogMaxVel(int boardId, int axisId, double jogMaxVel)
        {
            return 0;
        }

        public int PTDisable(int boardId, int pTbId)
        {
            return 0;
        }

        public int PTEnable(int boardId, int pTbId, int dimension, int[] axisArray)
        {
            return 0;
        }

        public int PTSetAbsolute(int boardId, int pTbId)
        {
            return 0;
        }

        public int PTSetTransBuffered(int boardId, int pTbId)
        {
            return 0;
        }

        public int PTSetAcc(int boardId, int pTbId, double acc)
        {
            return 0;
        }

        public int PTSetDec(int boardId, int pTbId, double dec)
        {
            return 0;
        }

        public int PTSetVm(int boardId, int pTbId, double vm)
        {
            return 0;
        }

        public int PTSetVe(int boardId, int pTbId, double ve)
        {
            return 0;
        }

        public int PTStop(int boardId, int pTbId)
        {
            return 0;
        }

        public int PTStart(int boardId, int pTbId)
        {
            return 0;
        }

        public int PTGetStatus(int pTbId, ref PTStatus status)
        {
            return 0;
        }

        public int PTLineSetPos(int pTbId, double[] pos)
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

        public void Stop(MoveProfile profile)
        {
        }

        public void Stop(MoveProfile[] profile)
        {
        }

        public void ReadStatus(MoveProfile profile)
        {
        }

        public int WaitMoveDone(List<MoveProfile> profile, bool bCheckLimit, bool bCheckPos)
        {
            return 0;
        }

        public int WaitMoveDone(MoveProfile[] profile, bool bCheckLimit, bool bCheckPos)
        {
            return 0;
        }

        public int WaitMoveDone(MoveProfile profile, bool bCheckLimit, bool bCheckPos)
        {
            return 0;
        }

        public void ReadStatus(int axis)
        {
            if (CardAxisMotion != null)
            {
                ASTP = CardAxisMotion[axis].Astp;
                MEL = CardAxisMotion[axis].Mel;
                PEL = CardAxisMotion[axis].Pel;
                ORG = CardAxisMotion[axis].Org;
                SVON = CardAxisMotion[axis].Servo;
                MDN = CardAxisMotion[axis].Mdn;
                HMV = CardAxisMotion[axis].Hmv;
            }
        }

        public bool LimitPel(int boardId, int axisIdx)
        {
            return CardAxisMotion[axisIdx].Pel;
        }

        #endregion

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimDataRefresh(string devname, object context)
        {
            OnPrimDataRefresh?.Invoke(devname, context); return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log); return 0;
        }

        public bool AxisInp(int index, int axisChannel)
        {
            return true;
        }
    }
}