using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.PrimLeisai
{
    public class LeisaiConfig
    {
        #region Override IPrim's Property

        public string Name { set; get; }

        public string PrimTypeName { set; get; }

        public Guid PrimId { set; get; }

        //Prim 子类的类型
        public Type ChildType { set; get; }

        //Prim的硬件、软件类型
        public PrimType HSType { set; get; } = PrimType.MoionCard;

        public PrimManufacture Manu { set; get; } = PrimManufacture.Other;

        public PrimVer Ver { set; get; } = PrimVer.Other;

        public PrimConnType ConnType { set; get; } = PrimConnType.Other;

        public string ConnInfo { set; get; }

        public PrimConnState PrimConnStat { set; get; } = PrimConnState.Connected;

        public PrimRunState PrimRunStat { set; get; } = PrimRunState.Running;

        public bool PrimSimulator { set; get; }

        public bool PrimDebug { set; get; }

        public bool PrimEnable { set; get; }

        #endregion

        #region Override IMotionCard's Property

        public string ConfigFilePath { set; get; }
        public int DevIndex { set; get; }

        #endregion
    }
}
