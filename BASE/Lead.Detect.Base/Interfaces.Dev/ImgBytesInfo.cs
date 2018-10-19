using System;

namespace Lead.Detect.Interfaces.Dev
{
    public class ImgBytesInfo
    {
        public byte[] _imgBytes = null;
        public IntPtr _imgSrcPtr;
        public int _imgWidth = -1;
        public int _imgHeight = -1;
        public int _imgBitDepth = -1;
        public int _frameIdx = -1;
        public long _imgTicks = 0;
    }
}