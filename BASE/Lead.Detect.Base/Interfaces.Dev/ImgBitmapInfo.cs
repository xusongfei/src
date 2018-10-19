using System.Drawing;

namespace Lead.Detect.Interfaces.Dev
{
    public class ImgBitmapInfo
    {
        public Bitmap _bitmap = null;
        public int _imgWidth = -1;
        public int _imgHeight = -1;
        public int _imgBitDepth = -1;
        public int _frameIdx = -1;
        public long _imgTicks = 0;
    }
}