namespace Lead.Detect.Interfaces.Dev
{
    public interface ISktClient
    {
        event MsgReved OnMsgReved;
        string DstIp { set; get; }

        string Port { set; get; }

        int ReconnectCnt { set; get; }

        SktNotifyMode NotifyMode { set; get; }

        string HeartInfo { set; get; }

        int HeartInfoCycleTime { set; get; }

        bool HeartFlag { set; get; }

        int SendQueueCnt { set; get; }
        int RevQueueCnt { set; get; }

        string NetName { set; get; }
        bool NetConneted { get; }
        int SetSendQueueCnt(int num);
        int SetNotifyMode(SktNotifyMode mode, int num);
        int RegNotifyInfo(string regInfo);

        int SetNetName(string name);

        int SetFilterInfo(string filterInfo);
        int SetHeartInfo(string heart, int cycleTime);

        int SendInfo(string rev, object context);
    }
}