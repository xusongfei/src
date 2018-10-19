using System.Collections.Generic;

namespace Lead.Detect.Interfaces.Dev
{
    public interface IMotionCard : IP2PMotionCard, ITriggerMotionCard, IInterpMotionCard
    {
        #region Config Load

        //Load config
        void LoadConfigFileLocal();

        #endregion

        #region Digital IO

        //Cycle Read Input
        int AddCycleReadInputGroup(int boardId, int group);
        int AddCycleReadInputGroup(int boardId, int[] group);
        int RemoveCycleReadInput(int boardId, int group);
        int RemoveCycleReadInput(int boardId, int[] group);
        int StartCycleReadInput(int boardId, int timeSpace);
        int EndCycleReadInput(int boardId);

        //Cycle Read Output
        int AddCycleReadOnputGroup(int boardId, int group);
        int AddCycleReadOnputGroup(int boardId, int[] group);
        int RemoveCycleReadOnput(int boardId, int group);
        int RemoveCycleReadOnput(int boardId, int[] group);
        int StartCycleReadOnput(int boardId, int timeSpace);
        int EndCycleReadOnput(int boardId);


        //RW Multi DOI
        int WriteMultiDOutput(int startIdx, int num, int[] val);
        int[] ReadMultiDOutput(int startIdx, int num);
        int[] ReadMultiDInput(int startIdx, int num);

        //R Single DOI From PrimData
        int ReadSingleDOutputFromPrim(int boarId, int group, int idx, out int val);
        int ReadSingleDInputFormPrim(int boarId, int group, int idx, out int val);

        //R Multi DOI From PrimData
        int ReadMultiDOutputFromPrim(int boarId, int group, int idx, out int val);
        int ReadMultiDInputFormPrim(int boarId, int group, int idx, out int val);

        //R Single DOI From API
        int ReadSingleDOutputFromAPI(int boarId, int group, int idx, ref int val);
        int ReadSingleDInputFormAPI(int boarId, int group, int idx, ref int val);

        //R Multi DOI From API
        int ReadMultiDOutputFromAPI(int boarId, int group, ref int val);
        int ReadMultiDInputFromAPI(int boarId, int group, ref int val);

        //mode 1:Rising Edge; 2:Down Edge; 3:Changed
        //type 1:Trig Interrupt 2:Cycle Read
        int RegisterDIChanged(int idx, int mode, int type);
        int RegisterDOChanged(int idx, int mode);

        int RemoveDIChanged(int idx);
        int RemoveDOChanged(int idx);

        #endregion

        #region Trigger

        //Clear Trigger Count
        int ResetTriggerCount(int boardId, int trigCh);

        //Set Trig CH Enable "0000"
        int SetTriggerChannelEnable(int boardId, string enableStr);

        //set Trig CH and Trig Source Enable "000000"
        int SetTriggerSourceEnable(int boardId, int trigCh, string enableStr);

        //Set Trigger Source and Input Source
        int SetTriggerSouceAndInputSource(int boardId, int trigSrc, int inputSrc);

        //Set Trig Channel's Output Pulse Width  pulse width=  value * 0.02 us
        int SetTrigOutputPulseWidth(int boardId, int trigCh, int widthTime);

        //1 logic  0: Not inverse  1:Inverse
        int SetTrigOutputLogic(int boardId, int trigCh, int logic);

        //
        int SetTrigDir(int boardId, int trigCh, int dir);

        //Start Line Trig
        int StartLinearCompareTrig(int boardId, int trigCh, int trigStart, int freq, int count);

        //Get Trigger Count
        int GetTriggerCount(int boardId, int trigCh, ref int count);

        //Set Trigger Table
        int SetTriggerTable(int tCmpCh, int[] dataArr, int arrSize);

        #endregion

        #region AxisState

        int GetTriggerCount(int trigCh);
        bool LimitZ(int boardId, int axisIdx);

        //Axis Feedback Pulse Set

        int GetAxisPositionOrFeedbackPules(int axisIdx, ref int position);

        int GetAxisMotionIOStatus(int axisIdx);
        int GetAxisMotionStatus(int axisIdx);

        int GetAxisTargetPositionF(int axisIdx, ref double targetPos);
        int GetAxisErrPositionF(int axisIdx, ref double errPos);
        int GetAxisCommandVelocityF(int axisIdx, ref double velocity);
        int GetAxisFeedbackVelocityF(int axisIdx, ref double velocity);

        int AddCycleReadAxisPos(int axisIdx);
        int AddCycleReadAxisPos(int[] axisIdx);
        int RemoveCycleReadAxisPos(int axisIdx);
        int RemoveCycleReadAxisPos(int[] axisIdx);
        int StartCycleReadAxisPos(int timeSpace);
        int EndCycleReadAxisPos();

        //Get Feedback Pulse From PrimData
        int GetAxisPositionOrFeedbackPulesFromPrim(int axisIdx, ref int position);

        #endregion

        #region Axis General Move Param And Exe

        int AxisMotionStatus(int boardId, int axisId);

        int AxisMotionIOStatus(int boardId, int axisId);

        #endregion

        #region Axis Home Param And Exe

        int AxisSetHomeMode(int boardId, int axisId, int homeMode); // Set home mode
        int AxisSetHomeDir(int boardId, int axisId, int homeDir); // Set home direction
        int AxisSetHomeCurve(int boardId, int axisId, double curve); // Set acceleration paten (T-curve)
        int AxisSetHomeAcc(int boardId, int axisId, double acc); // Set homing acceleration rate
        int AxisSetHomeMaxVel(int boardId, int axisId, double maxVel); // Set homing maximum velocity.
        int AxisSetHomeVO(int boardId, int axisId, double vo); // Set homing VO speed
        int AxisSetHomeEZA(int boardId, int axisId, double eza); // Set EZ signal alignment (yes or no)
        int AxisSetHomeShift(int boardId, int axisId, double shift); // Set home position shfit distance. 
        int AxisSetHomePos(int boardId, int axisId, double pos); // Set final home position. 

        #endregion

        #region Axis Jog Param And Exe

        int AxisJogStart(int boardId, int axisId, int statOn);
        int AxisSetJogMode(int boardId, int axisId, int jogMode);
        int AxisSetJogDir(int boardId, int axisId, int jogDir);
        int AxisSetJogAcc(int boardId, int axisId, double jogAcc);
        int AxisSetJogDec(int boardId, int axisId, double jogDec);
        int AxisSetJogMaxVel(int boardId, int axisId, double jogMaxVel);

        #endregion

        #region PT Move Param And Exe

        int PTDisable(int boardId, int pTbId);
        int PTEnable(int boardId, int pTbId, int dimension, int[] axisArray);
        int PTSetAbsolute(int boardId, int pTbId);
        int PTSetTransBuffered(int boardId, int pTbId);
        int PTSetAcc(int boardId, int pTbId, double acc);
        int PTSetDec(int boardId, int pTbId, double dec);
        int PTSetVm(int boardId, int pTbId, double vm);
        int PTSetVe(int boardId, int pTbId, double ve);
        int PTStop(int boardId, int pTbId);

        int PTStart(int boardId, int pTbId);
        int PTGetStatus(int pTbId, ref PTStatus status);

        #endregion

        #region PT Line Move

        int PTLineSetPos(int pTbId, double[] pos);

        #endregion

        #region  Move


        int Home(MoveProfile profile);
        int Home(MoveProfile[] profile);

        int Home(List<MoveProfile> profile);


        int MoveAbs(MoveProfile profile);
        int MoveAbs(MoveProfile[] profile);

        int MoveAbs(List<MoveProfile> profile);

        void Stop(MoveProfile profile);
        void Stop(MoveProfile[] profile);

        void ReadStatus(MoveProfile profile);


        int WaitMoveDone(List<MoveProfile> profile, bool bCheckLimit, bool bCheckPos);
        int WaitMoveDone(MoveProfile[] profile, bool bCheckLimit, bool bCheckPos);
        int WaitMoveDone(MoveProfile profile, bool bCheckLimit, bool bCheckPos);

        #endregion

        void ReadStatus(int axis);
    }


    /// <summary>
    /// 点位运动接口
    /// </summary>
    public interface IP2PMotionCard
    {
        int DevIndex { get; set; }

        string ConfigFilePath { get; set; }
        void LoadConfigFile(string file);


        int WriteSingleDOutput(int index, int i, int port, int status);
        int ReadSingleDOutput(int index, int i, int port, out int status);
        int ReadSingleDInput(int index, int i, int port, out int status);


        int GetAxisPositionF(int axis, ref double d);
        int SetAxisPositionOrFeedbackPules(int axis, int pos);
        int GetAxisCommandF(int axis, ref double d);


        int AxisSetEnable(int index, int axis, bool enable);
        int AxisAbsMove(int index, int axis, int pos, int vel);
        int AxisRelMove(int index, int axis, int step, int vel);
        bool AxisIsStop(int index, int axis);
        int AxisStopMove(int index, int axis);
        int AxisHomeMove(int index, int axis);


        bool AxisHMV(int motionDevIndex, int axis);
        bool AxisIsEnble(int index, int axis);
        bool AxisIsAlarm(int index, int axis);
        bool AxisSingalEMG(int index, int axis);
        bool LimitMel(int index, int axis);
        bool LimitPel(int index, int axis);
        bool LimitOrg(int index, int axis);
        bool AxisAstp(int index, int axisChannel);


        int AxisSetAcc(int index, int axisId, double acc);
        int AxisSetDec(int index, int axisId, double dec);

        int AxisSetHomeVel(int axis, int vel);
    }



    /// <summary>
    /// 触发接口
    /// </summary>
    public interface ITriggerMotionCard
    {

    }


    /// <summary>
    /// 插补接口
    /// </summary>

    public interface IInterpMotionCard
    {

    }






}