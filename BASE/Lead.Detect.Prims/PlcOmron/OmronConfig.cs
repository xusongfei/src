using System;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimPlcOmron
{
    public enum OmronCommMode
    {
        Fins,
        Complet,
        Other = 100
    }

    public class OmronConfig
    {
        public OmronCommMode CommMode { set; get; }
        public string DstIp { set; get; }
        public string DstPort { set; get; }
        public string LocalIp { set; get; }

        public string DataTablePath { set; get; }

        public int CycleTime { set; get; }
        //public  PrimConnState IsConnected { get; set; }//判断PLC是否已经连接，2018/3/2 增加

        #region Override IPrim's Property

        public string Name { set; get; }

        public string PrimTypeName { set; get; }

        public Guid Id { set; get; }

        //Prim子类的类型
        public Type ChildType { set; get; }

        //Prim的硬件、软件类型
        public PrimType HSType { set; get; }

        public PrimManufacture Manu { set; get; }

        public PrimVer Ver { set; get; }

        public PrimConnType ConnType { set; get; }

        public string ConnInfo { set; get; }

        public PrimConnState PrimConnStat { set; get; }

        //public PrimConnState IsConnected { get; set; }//判断PLC是否已经连接，2018/3/2 增加
        public PrimRunState PrimRunStat { set; get; }

        public bool PrimSimulator { set; get; }

        public bool PrimDebug { set; get; }

        public bool PrimEnable { set; get; }

        #endregion

        //public List<PLCParamRow> DgvTable;
        //public List<PLCParamDictionary> QueryList;
    }
}