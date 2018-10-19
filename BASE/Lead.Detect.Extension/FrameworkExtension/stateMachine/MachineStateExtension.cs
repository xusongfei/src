namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public static class MachineStateExtension
    {


        public static string GetState(this StationAutoState state)
        {
            switch (state)
            {
                case StationAutoState.WaitReset:
                    return "等待复位";
                case StationAutoState.Resetting:
                    return "复位中";
                case StationAutoState.WaitRun:
                    return "等待运行";
                case StationAutoState.Running:
                    return "运行中";
                case StationAutoState.Pause:
                    return "暂停";
            }
            return string.Empty;
        }


    }
}