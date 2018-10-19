namespace Lead.Detect.FrameworkExtension.motionDriver
{
    public interface IGioWrapper
    {
        void Init(string file);
        void Uninit();
        void GetDi(int port, out int status);
        void SetDi(int port, int status);
        void SetDo(int port, int status);
        void GetDo(int port, out int status);
    }
}