using System;
using System.Collections.Generic;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimCameraFocalSpec
{
    public class FocalSpecConfig
    {
        public string BasicConfigPath { set; get; }
        public string CamIdxStr { set; get; }

        public bool IsBind { set; get; }

        public List<string> ListTrigPosFilePath { get; set; }

        public int EachparagraphNum1 { set; get; }

        public int EachparagraphNum2 { set; get; }

        public int EachparagraphNum3 { set; get; }

        public int EachparagraphNum4 { set; get; }

        public int EachparagraphNum5 { set; get; }

        public int EachparagraphNum6 { set; get; }

        public int EachparagraphNum7 { set; get; }

        public int EachparagraphNum8 { set; get; }

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
    }
}