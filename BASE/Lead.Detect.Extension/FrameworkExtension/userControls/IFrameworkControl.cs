namespace Lead.Detect.FrameworkExtension.userControls
{
    public interface IFrameworkControl
    {
        void LoadFramework();

        void FrameworkActivate();

        void FrameworkDeactivate();

        void FrameworkUpdate();
    }
}