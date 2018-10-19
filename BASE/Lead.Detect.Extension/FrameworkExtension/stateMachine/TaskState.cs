namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public enum TaskState
    {
        None,
        WaitReset,
        Resetting,

        WaitRun,
        Running,

        Pause,
    }
}