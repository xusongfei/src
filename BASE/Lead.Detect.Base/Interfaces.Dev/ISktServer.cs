namespace Lead.Detect.Interfaces.Dev
{
    public interface ISktServer
    {
        event MsgReved OnMsgReved;
        string BindIp { set; get; }

        string Port { set; get; }

        SktNotifyMode NotifyMode { set; get; }

        string HeartInfo { set; get; }

        bool HeartFlag { set; get; }

        int SendQueueCnt { set; get; }
        int RevQueueCnt { set; get; }

        string NetName { set; get; }

        int SetSendQueueCnt(int num);
        int SetNotifyMode(SktNotifyMode mode, int num);
        int RegNotifyInfo(string regInfo);

        int SetNetName(string name);

        int SetFilterInfo(string filterInfo);

        int SetHeartInfo(string heart);

        int SendInfoByNetName(string revNetName, object context);

        int SendInfoByPoint(string revPoint, object context);
    }
}