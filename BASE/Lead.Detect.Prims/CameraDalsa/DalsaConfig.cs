using System;
using System.Drawing;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimCameraDalsa
{
    public class DalsaConfig
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

        #region Dalsa's Property

        public string DalsaConfigFilePath { set; get; }
        public int TimeOut { get; set; }

        public Size ImageSize { get; set; }
        public PixelFormatType PixelFormat { get; set; }


        public string FileName { get; set; }

        public string ServerName { get; set; }

        public int ResourceIndex { get; set; }

        public int AllObjSize { get; set; }

        public int SingleObjSize { get; set; }

        public int Count { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int PixelDepth { get; set; }

        public bool IsSave { get; set; }

        public bool IsMonitor { get; set; }

        public string SavePath { get; set; }

        public string ImgFormat { get; set; }

        public string LoadPath { get; set; }

        public string ImgFrom { get; set; }

        public string InputType { get; set; }

        public TirgMode TrigMode { get; set; }

        public int DelayTime { get; set; }

        public int BitsPerPixel { get; set; }

        public int Threshold { get; set; }

        public string WaferType { get; set; }

        public int CamEnum { get; set; }

        public int CamIndex { get; set; }

        public int StitchNum { get; set; }

        public string CalibPath { get; set; }

        public string SaveType { get; set; }

        public string TimeAreaMode { get; set; }

        public long TimeD { get; set; }

        public long TimeU { get; set; }

        #endregion
    }

    public enum DataFrom
    {
        Custom,
        FormPrim,
        FormInput
    }

    public enum PixelFormatType
    {
        Mono8,
        Rgb24
    }

    public enum TirgMode
    {
        ExtHardWare,
        SoftWareCmd,
        Other = 100
    }
}