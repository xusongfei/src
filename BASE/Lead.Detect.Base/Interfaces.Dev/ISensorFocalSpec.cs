using System;
using System.Collections.Generic;

namespace Lead.Detect.Interfaces.Dev
{
    public interface ISensorFocalSpec
    {
        event FocalSpecBatchArrived OnFocalSpecBatchArrived;
        List<List<FSPoint>> GetBatch(int index);
        float GetBatchInterval();
        int GetBatchCount();
        int SetBatchCount(int cnt);
        int SetNotifyFlag(bool flag);
        int SetBatchTimeOut(int tms);
        int SetBatchCountLimit(int limitD, int limitU);
        int SetFreqAndExternalPulseEnable(int freq, bool isExternalPulsingEnabled);
        int SetLedPulseWidthAndMaxLimit(int ledPulseWidth, int? maxLedPulseWidth);
        int SetAgcEnable(bool enableAgc);
        int SetAgcTargetIntensity(float? agcTargetIntensity);
        int ClearBatch(int index);
        void Limitvolution(Int32[] index);
        int StartRequest();
        int StopRequest();
        int Close();
        int LoadCameraList();

        int SaveToPointCloudFile(string file, List<List<FSPoint>> points, double batchStepLength, bool isSaveAll);
    }
}