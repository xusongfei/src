namespace Lead.Detect.Interfaces.Dev
{
    public interface ICamera
    {
        event ImgArrived OnImgArrived;
        int ImgWidth { set; get; }
        int ImgHeight { set; get; }
        int ImgBitDepth { set; get; }

        void SetImgParam(int width, int height, int bitDepth);
        void SetImgExposureTime(int time);

        //SHMode 1:SoftTrigger; 2:HardTrigger
        void SetSHMode(int SHMode);

        //EIMode 1:ExtTrigger; 2:InternalTrigger
        void SetEIMode(int EIMode);

        void SetNotifyEnable(bool falg);

        //mode 1:ImgBytesInfo 2:ImgBitmapInfo
        void SetqQueueCnt(int mode, int qCnt);
        void SetFrame(int fCnt);
        int StartSnap();
        int StartGrab();

        int SnapGrabAbort();

        object GetOneFrameBySoftCmd(int timeout);
    }
}