using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DALSA.SaperaLT.SapClassBasic;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Helper;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.PrimCameraDalsa.Dalsa;

namespace Lead.Detect.PrimCameraDalsa
{
    public class PrimDalsa : IPrim, ICamera
    {
        public PrimDalsa()
        {
            _config = new DalsaConfig();

            if (_core == null)
            {
                _core = new DalsaCore(_config, Name);
                _core.DalsaImageArrivedHandle += _core_DalsaImageArrivedHandle;
                _core.ObjSize = _config.Width * _config.Height;
                _core.Online = true;
            }

            PrimConfigUI = new PrimConfigControl();
            ((PrimConfigControl) PrimConfigUI).SetDalsaCore(_core);
            ((PrimConfigControl) PrimConfigUI).SetDalsaConfig(_config);

            PrimDebugUI = new PrimDebugControl();
            PrimOutputUI = new PrimOutputControl();
        }

        public PrimDalsa(XmlNode xmlNode)
        {
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(DalsaConfig)) as DalsaConfig;
            else
                return;

            if (_core == null)
            {
                _core = new DalsaCore(_config, Name);
                _core.DalsaImageArrivedHandle += _core_DalsaImageArrivedHandle;
                _core.ObjSize = _config.Width * _config.Height;
                _core.Online = true;
            }

            PrimConfigUI = new PrimConfigControl();
            ((PrimConfigControl) PrimConfigUI).SetDalsaCore(_core);
            ((PrimConfigControl) PrimConfigUI).SetDalsaConfig(_config);

            PrimDebugUI = new PrimDebugControl();
            PrimOutputUI = new PrimOutputControl();
        }

        public event ImgArrived OnImgArrived;

        //mode 1:ImgBytesInfo 2:ImgBitmapInfo
        private void _core_DalsaImageArrivedHandle(ImgBytesInfo img)
        {
            if (_imgInfoType == 1)
            {
                img._imgWidth = ImgWidth;
                img._imgHeight = ImgHeight;
                img._imgBitDepth = ImgBitDepth;

                //ImgBytesInfoQ.Enqueue(img);
                if (_notifyFlag && OnImgArrived != null) OnImgArrived(img);
            }
            else if (_imgInfoType == 2)
            {
                //Bytes turn to Bitmap
                var imgBitmap = new ImgBitmapInfo();
                imgBitmap._imgWidth = ImgWidth;
                imgBitmap._imgHeight = ImgHeight;
                imgBitmap._imgBitDepth = ImgBitDepth;
                imgBitmap._imgTicks = img._imgTicks;

                var size = new Size();
                size.Width = ImgWidth;
                size.Height = ImgHeight;

                imgBitmap._bitmap = ImgCommonHelper.MemoryShareToImage(img._imgBytes, size, ImgBitDepth);

                //ImgBitmapInfoQ.Enqueue(imgBitmap);
                if (_notifyFlag && OnImgArrived != null) OnImgArrived(imgBitmap);
            }
        }

        #region Override IPrim's Poperty

        public event PrimDataRefresh OnPrimDataRefresh;

        public event PrimStateChanged OnPrimStateChanged;

        /// <inheritdoc />
        public event PrimOpLog OnPrimOpLog;
        protected virtual int OnOnPrimDataRefresh(string devname, object context)
        {
            OnPrimDataRefresh?.Invoke(devname, context);
            return 0;
        }

        protected virtual int OnOnPrimOpLog(string devname, object log)
        {
            OnPrimOpLog?.Invoke(devname, log);
            return 0;
        }

        protected virtual int OnOnPrimStateChanged(string devname, object context)
        {
            OnPrimStateChanged?.Invoke(devname, context);
            return 0;
        }
        public string Name
        {
            set { _config.Name = value; }
            get { return _config.Name; }
        }

        public string PrimTypeName
        {
            set { _config.PrimTypeName = value; }
            get { return _config.PrimTypeName; }
        }

        public Guid PrimId
        {
            set { _config.Id = value; }
            get { return _config.Id; }
        }

        //Prim 子类的类型
        public Type ChildType
        {
            set { _config.ChildType = value; }
            get { return _config.ChildType; }
        }

        //Prim的硬件、软件类型
        public PrimType HSType
        {
            set { _config.HSType = value; }
            get { return _config.HSType; }
        }

        public PrimManufacture Manu
        {
            set { _config.Manu = value; }
            get { return _config.Manu; }
        }

        public PrimVer Ver
        {
            set { _config.Ver = value; }
            get { return _config.Ver; }
        }

        public PrimConnType ConnType
        {
            set { _config.ConnType = value; }
            get { return _config.ConnType; }
        }

        public string ConnInfo
        {
            set { _config.ConnInfo = value; }
            get { return _config.ConnInfo; }
        }

        public PrimConnState PrimConnStat
        {
            set { _config.PrimConnStat = value; }
            get { return _config.PrimConnStat; }
        }

        public PrimRunState PrimRunStat
        {
            set { _config.PrimRunStat = value; }
            get { return _config.PrimRunStat; }
        }

        public bool PrimSimulator
        {
            set { _config.PrimSimulator = value; }
            get { return _config.PrimSimulator; }
        }

        public bool PrimDebug
        {
            set { _config.PrimDebug = value; }
            get { return _config.PrimDebug; }
        }

        public bool PrimEnable
        {
            set { _config.PrimEnable = value; }
            get { return _config.PrimEnable; }
        }

        public IDataArea DataArea { set; get; }

        public Control PrimDebugUI { get; }

        public Control PrimConfigUI { get; }

        public Control PrimOutputUI { get; }

        #endregion

        #region Override ICamera's Property

        public int ImgWidth
        {
            set { _config.Width = value; }
            get { return _config.Width; }
        }

        public int ImgHeight
        {
            set { _config.Height = value; }
            get { return _config.Height; }
        }

        public int ImgBitDepth
        {
            set { _config.BitsPerPixel = value; }
            get { return _config.BitsPerPixel; }
        }

        #endregion

        #region Private Numbers

        private DalsaConfig _config;
        private readonly DalsaCore _core;

        private Queue<ImgBitmapInfo> ImgBitmapInfoQ;
        private Queue<ImgBytesInfo> ImgBytesInfoQ;
        private int _imgInfoType;
        private bool _notifyFlag = true;

        #endregion

        #region Override IPrim's Function

        public int IPrimInit()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimConnect(ref string result)
        {
            var iRet = 0;

            if (!_core.CreateNewObjects(new SapLocation(_config.ServerName, _config.ResourceIndex), _config.FileName, false)) return -1;

            return iRet;
        }

        public int IPrimStart()
        {
            var iRet = 0;

            if (_core.CoreSnap())
            {
                //Debug.WriteLine(FreindlyName + ":" + "Dalsa Extmode Snap success" + "Time：" +
                //DateTime.Now.ToString(CultureInfo.InvariantCulture));
            }

            return iRet;
        }

        public int IPrimStop()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimDisConnect(ref string result)
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimSuspend()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimResume()
        {
            var iRet = 0;
            return iRet;
        }

        public int IPrimDispose()
        {
            var iRet = 0;
            return iRet;
        }

        public int ImportDataConfig(XmlNode xmlNode)
        {
            var iRet = 0;

            return iRet;
        }

        public XmlNode ExportDataConfig()
        {
            return null;
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var iRet = 0;
            if (xmlNode != null)
                _config = XMLHelper.XMLToObject(xmlNode, typeof(DalsaConfig)) as DalsaConfig;
            else
                return -1;
            return iRet;
        }

        public XmlNode ExportConfig()
        {
            //config turn to xmlNode
            if (_config == null) return null;

            var node = XMLHelper.ObjectToXML(_config);

            return node;
        }

        public object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info)
        {
            var iRet = 0;
            return iRet;
        }

        #endregion

        #region Override ICamera's Function

        public void SetImgParam(int width, int height, int bitDepth)
        {
        }

        public void SetImgExposureTime(int time)
        {
        }

        //SHMode 1:SoftTrigger; 2:HardTrigger
        public void SetSHMode(int SHMode)
        {
        }

        //EIMode 1:ExtTrigger; 2:InternalTrigger
        public void SetEIMode(int EIMode)
        {
        }

        public void SetNotifyEnable(bool falg)
        {
            _notifyFlag = falg;
        }

        //mode 1:ImgBytesInfo 2:ImgBitmapInfo 
        public void SetqQueueCnt(int mode, int qCnt)
        {
            _imgInfoType = mode;

            if (mode == 1)
                ImgBytesInfoQ = new Queue<ImgBytesInfo>(qCnt);
            else if (mode == 2) ImgBitmapInfoQ = new Queue<ImgBitmapInfo>(qCnt);
        }

        public void SetFrame(int fCnt)
        {
        }

        public int StartSnap()
        {
            return -1;
        }

        public int StartGrab()
        {
            var iRet = 0;
            if (!_core.CoreGrab()) iRet = -1;
            return iRet;
        }

        public int SnapGrabAbort()
        {
            var iRet = 0;
            if (!_core.CoreAbort()) iRet = -1;
            return iRet;
        }

        public object GetOneFrameBySoftCmd(int timeout)
        {
            return null;
        }

        #endregion

      
    }
}