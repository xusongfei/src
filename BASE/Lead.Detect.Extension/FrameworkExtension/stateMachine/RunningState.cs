namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public enum RunningState
    {
        None,

        WaitReset,
        Resetting,

        WaitRun,
        Running,
        Pause,
    }
}