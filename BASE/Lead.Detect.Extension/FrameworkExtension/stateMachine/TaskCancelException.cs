using System;

namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public class TaskCancelException : Exception
    {
        public TaskCancelException(StationTask task, string msg) : base(msg)
        {
        }
    }
}