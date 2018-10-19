using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DALSA.SaperaLT.SapClassBasic;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimCameraDalsa.Dalsa
{
    public class DalsaCore
    {
        public delegate void NotifyDalsaImageArrived(ImgBytesInfo img);

        public event NotifyDalsaImageArrived DalsaImageArrivedHandle;

        private readonly DalsaConfig _config;
        private readonly Guid _id;

        //private readonly Queue<ImgBytesInfo> _imgByteQueue = new Queue<ImgBytesInfo>();
        //private readonly IntPtrQueue _intPtrQueue;
        //private readonly object _mutex = new object();
        private readonly string _name;

        //private byte[] _dst;
        //private bool _getImg;
        //private bool _isOutDataUsed;

        #region Constructors

        public DalsaCore(DalsaConfig config, string name)
        {
            _config = config;
            _id = Guid.NewGuid();
            _name = name;
            //_getImg = false;
            //_intPtrQueue = new IntPtrQueue();
            //_intPtrQueue.IntPtrQueueClear();

            //_imgByteQueue.Clear();
        }

        #endregion

        protected virtual void OnDalsaImageArrivedHandle(ImgBytesInfo img)
        {
            if (DalsaImageArrivedHandle != null) DalsaImageArrivedHandle(img);
        }

        #region Properties

        public SapAcquisition Acquisition { get; set; }

        public SapBuffer Buffers { get; set; }

        public SapAcqToBuf Xfer { get; set; }

        public SapLocation Location { get; set; }

        public bool IsSignalDetected { get; set; }

        public bool Online { get; set; }

        public int ObjSize { get; set; }

        #endregion

        #region Methods

        private void xfer_XferNotify(object sender, SapXferNotifyEventArgs argsNotify)
        {
            try
            {
                var core = argsNotify.Context as DalsaCore;
                if (argsNotify.Trash)
                {
                }
                else
                {
                    //DateTime getStart = DateTime.Now;

                    IntPtr bufferAddress;
                    Buffers.GetAddress(out bufferAddress);
                    Debug.WriteLine(_name + "Dalsa ImgPtr:[" + _id + "]" + bufferAddress + DateTime.Now);

                    var imgInfo = new ImgBytesInfo();
                    imgInfo._imgSrcPtr = bufferAddress;
                    imgInfo._imgBytes = new byte[ObjSize];
                    imgInfo._imgTicks = DateTime.Now.Ticks / 10000;
                    Marshal.Copy(bufferAddress, imgInfo._imgBytes, 0, ObjSize);

                    OnDalsaImageArrivedHandle(imgInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /*
        public bool GetImgByteByIntPtr(byte[] imgData, TirgMode mode)
        {
            IntPtr ptr = IntPtr.Zero;
            //lock (_mutex)
            //{
            if (_config.TrigMode == TirgMode.ExtHardWare)
            {
                //_waitSnap.WaitOne();
                ptr = _intPtrQueue.IntPtrQueuePop();
                while ((ptr == IntPtr.Zero))
                {
                    Thread.Sleep(2);
                    Application.DoEvents();
                    ptr = _intPtrQueue.IntPtrQueuePop();
                }
                Marshal.Copy(ptr, imgData, 0, _objSize);
                return true;
            }
            if (_config.TrigMode == TirgMode.SoftWareCmd)
            {
                Xfer.Snap();
                ptr = _intPtrQueue.IntPtrQueuePop();
                while ((ptr == IntPtr.Zero))
                {
                    Thread.Sleep(2);
                    Application.DoEvents();
                    ptr = _intPtrQueue.IntPtrQueuePop();
                }
                Marshal.Copy(ptr, imgData, 0, _objSize);
                return true;
            }
            return false;
            //}

            return false;
        }
        */
        /*
        public bool GetImgByteByBytes(byte[] imgData, TirgMode mode, bool needArea, long trigT, long deltaT_D,
            long deltaT_U)
        {
            if (_config.TrigMode == TirgMode.ExtHardWare)
            {
                while (true)
                {
                    while (_imgByteQueue.Count < 1)
                    {
                        Thread.Sleep(2);
                        Application.DoEvents();
                    }
                    Debug.WriteLine(_name + "ByteQueueCount:{0}", _imgByteQueue.Count);
                    //ImgBytesInfo imgInfo = _imgByteQueue.Dequeue();
                    ImgBytesInfo imgInfo = _imgByteQueue.Peek();


                    //if (needArea)
                    //{
                    //    long td = trigT + deltaT_D;
                    //    long tu = trigT + deltaT_U;

                    //    //在设定时间区间
                    //    if (imgInfo.ImgTicks >= td && imgInfo.ImgTicks <= tu)
                    //    {
                    //        Array.Copy(imgInfo.ImgBytes, imgData, _objSize);
                    //        _imgByteQueue.Dequeue();
                    //        return true;
                    //    }

                    //    //这次软触发的图片没有
                    //    if (imgInfo.ImgTicks > tu)
                    //    {
                    //        return false;
                    //    }

                    //    //这个图片是上次软触发的，直接丢弃
                    //    if (imgInfo.ImgTicks < td)
                    //    {
                    //        _imgByteQueue.Dequeue();
                    //    }
                    //}
                    //else
                    //{
                    Array.Copy(imgInfo.ImgBytes, imgData, _objSize);
                    _imgByteQueue.Dequeue();
                    return true;
                    //}
                }
            }
            if (_config.TrigMode == TirgMode.SoftWareCmd)
            {
                return true;
            }
            return false;
        }
        */
        public SapView m_View;

        public bool CreateNewObjects(SapLocation location, string fileName, bool Restore)
        {
            if (Online)
            {
                if (!Restore) Location = location;

                if (!SapManager.IsResourceAvailable(location, SapManager.ResourceType.Acq))
                {
                    Debug.WriteLine(fileName + "available");
                    return false;
                }

                Acquisition = new SapAcquisition(Location, fileName);
                if (SapBuffer.IsBufferTypeSupported(Location, SapBuffer.MemoryType.ScatterGather))
                    Buffers = new SapBufferWithTrash(4, Acquisition, SapBuffer.MemoryType.ScatterGather);
                else
                    Buffers = new SapBufferWithTrash(4, Acquisition, SapBuffer.MemoryType.ScatterGatherPhysical);
                Xfer = new SapAcqToBuf(Acquisition, Buffers);
                m_View = new SapView(Buffers);

                Xfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                Xfer.XferNotify += xfer_XferNotify;
                Xfer.XferNotifyContext = this;
            }
            else
            {
                Buffers = new SapBuffer();
            }

            if (!CreateObjects())
            {
                DisposeObjects();
                return false;
            }

            return true;
        }

        // Call Create method  
        public bool CreateObjects()
        {
            // Create acquisition object
            if (Acquisition != null && !Acquisition.Initialized)
                if (Acquisition.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }

            // Create buffer object
            if (Buffers != null && !Buffers.Initialized)
            {
                if (Buffers.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }

                Buffers.Clear();
            }

            // Create Xfer object
            if (Xfer != null && !Xfer.Initialized)
                if (Xfer.Create() == false)
                {
                    DestroyObjects();
                    return false;
                }

            return true;
        }

        //Call Destroy method
        public void DestroyObjects()
        {
            if (Xfer != null && Xfer.Initialized)
                Xfer.Destroy();

            if (Buffers != null && Buffers.Initialized)
                Buffers.Destroy();
            if (Acquisition != null && Acquisition.Initialized)
                Acquisition.Destroy();
        }

        public void DisposeObjects()
        {
            if (Xfer != null)
            {
                Xfer.Dispose();
                Xfer = null;
            }

            if (Buffers != null)
            {
                Buffers.Dispose();
                Buffers = null;
            }

            if (Acquisition != null)
            {
                Acquisition.Dispose();
                Acquisition = null;
            }
        }

        public bool LoadConfig(DalsaConfig config)
        {
            var ret = true;

            DestroyObjects();

            if (string.IsNullOrEmpty(config.ServerName)) return false;

            if (!CreateNewObjects(new SapLocation(config.ServerName, config.ResourceIndex), config.FileName, false))
                if (!CreateNewObjects(null, "", true))
                    ret = false;
            return ret;
        }

        public bool CoreSnap()
        {
            var ret = Xfer.Grab();
            return ret;
        }

        public bool CoreAbort()
        {
            var ret = Xfer.Abort();
            return ret;
        }

        public bool CoreGrab()
        {
            var ret = Xfer.Grab();
            return ret;
        }

        private delegate void DisplayFrameAcquired(int number, bool trash);

        #endregion
    }

    /*
public class IntPtrQueue
{
private static readonly object mutex = new Mutex();
private readonly IntPtr[] ptrs;
private int count;
private int ridx;
private int widx;

public IntPtrQueue()
{
    count = 0;
    widx = 0;
    ridx = 0;
    ptrs = new IntPtr[100];
}

public void IntPtrQueuePush(IntPtr intPtr)
{
    lock (mutex)
    {
        Debug.WriteLine("Dalsa ImgPtr:1" + DateTime.Now);
        if (count >= 100)
        {
            Debug.WriteLine("Dalsa ImgPtr:10" + DateTime.Now);
            return;
        }
        Debug.WriteLine("Dalsa ImgPtr:2" + DateTime.Now);
        ptrs[widx] = intPtr;
        widx = (widx + 1) % 100;
        count++;
        Debug.WriteLine("Dalsa ImgPtr:3" + DateTime.Now);
    }
}

public IntPtr IntPtrQueuePop()
{
    lock (mutex)
    {
        Debug.WriteLine("Dalsa ImgPtr:1" + DateTime.Now);
        IntPtr ptr = IntPtr.Zero;
        Debug.WriteLine("Dalsa ImgPtr:2" + DateTime.Now);
        if (count <= 0)
        {
            Debug.WriteLine("Dalsa ImgPtr:21" + DateTime.Now);
            return ptr;
        }
        ptr = ptrs[ridx];
        Debug.WriteLine("Dalsa ImgPtr:3" + DateTime.Now);
        ridx = (ridx + 1) % 100;
        count--;
        Debug.WriteLine("Dalsa ImgPtr:4" + DateTime.Now);
        return ptr;
    }
}

public void IntPtrQueueClear()
{
    lock (mutex)
    {
        widx = 0;
        ridx = 0;
        count = 0;
    }
}
}
*/
}