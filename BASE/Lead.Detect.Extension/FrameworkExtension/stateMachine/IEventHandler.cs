namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public interface IEventHandler
    {
        void HandleEvent(UserEvent e);
    }
}