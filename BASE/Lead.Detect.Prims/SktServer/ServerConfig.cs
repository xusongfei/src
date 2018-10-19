using System;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimSktServer
{
    public class ServerConfig
    {
        #region Override IPrim's Property

        public string Name { set; get; }

        public string PrimTypeName { set; get; }

        public Guid Id { set; get; }

        //Prim 子类的类型
        public Type ChildType { set; get; }

        //Prim的硬件、软件类型
        public PrimType HSType { set; get; }

        public PrimManufacture Manu { set; get; }

        public PrimVer Ver { set; get; }

        public PrimConnType ConnType { set; get; }

        public string ConnInfo { set; get; }

        public PrimConnState PrimConnStat { set; get; }

        public PrimRunState PrimRunStat { set; get; }

        public bool PrimSimulator { set; get; }

        public bool PrimDebug { set; get; }

        public bool PrimEnable { set; get; }

        #endregion

        #region Override ISktServer Property

        public string BindIp { set; get; }

        public string Port { set; get; }

        public SktNotifyMode NotifyMode { set; get; }

        public string HeartInfo { set; get; }

        public bool HeartFlag { set; get; }

        public int SendQueueCnt { set; get; }

        public int RevQueueCnt { set; get; }

        public string NetName { set; get; }

        #endregion
    }
}