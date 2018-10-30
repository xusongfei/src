namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public static class RunningStateExtension
    {
        public static string GetState(this RunningState state)
        {
            switch (state)
            {
                case RunningState.WaitReset:
                    return "等待复位";
                case RunningState.Resetting:
                    return "复位中";
                case RunningState.WaitRun:
                    return "等待运行";
                case RunningState.Running:
                    return "运行中";
                case RunningState.Pause:
                    return "暂停";
            }
            return string.Empty;
        }


    }
}