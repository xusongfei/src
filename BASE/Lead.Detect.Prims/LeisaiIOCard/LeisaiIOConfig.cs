using System;
using System.ComponentModel;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.PrimLeisaiIOCard
{
    public class LeisaiIOConfig
    {
        #region Override IPrim's Property

        [ReadOnly(true)]
        public string Name { set; get; }
        [ReadOnly(true)]
        public string PrimTypeName { set; get; }
        [ReadOnly(true)]
        public Guid PrimId { set; get; }
        //Prim 子类的类型
        [ReadOnly(true)]
        public Type ChildType { set; get; }
        //Prim的硬件、软件类型
        [ReadOnly(true)]
        public PrimType HSType { set; get; } = PrimType.MoionCard;
        [ReadOnly(true)]
        public PrimManufacture Manu { set; get; } = PrimManufacture.Other;
        [ReadOnly(true)]
        public PrimVer Ver { set; get; } = PrimVer.Other;
        [ReadOnly(true)]
        public PrimConnType ConnType { set; get; } = PrimConnType.Other;
        [ReadOnly(true)]
        public string ConnInfo { set; get; }
        [ReadOnly(true)]
        public PrimConnState PrimConnStat { set; get; } = PrimConnState.Connected;
        [ReadOnly(true)]
        public PrimRunState PrimRunStat { set; get; } = PrimRunState.Running;
        [ReadOnly(true)]
        public bool PrimSimulator { set; get; }
        [ReadOnly(true)]
        public bool PrimDebug { set; get; }
        [ReadOnly(true)]
        public bool PrimEnable { set; get; }

        #endregion

        #region Override IMotionCard's Property

        public string ConfigFilePath { set; get; }
        public int DevIndex { set; get; }

        public int Node { get; set; }

        #endregion
    }
}
