using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.MachineUtilityLib.UtilsFramework
{
    public static class StationTaskExtension
    {
        public static bool WaitResetFinish(this StationTask waitTask, StationTask curTask)
        {
            if (waitTask == null)
            {
                curTask.ThrowException($"Task is NULL");
            }

            //wait measure task
            while (waitTask.RunningState != RunningState.WaitRun && waitTask.RunningState != RunningState.Running)
            {
                curTask.AbortIfCancel("cancel wait tasks");
                System.Threading.Thread.Sleep(1);
            }

            return true;
        }



        public static void AssertNoNull(this object obj, StationTask task, string msg = null)
        {
            if (obj == null)
            {
                task.ThrowException($"{obj.GetType().Name.ToString()}{msg ?? "SOME OBJECT"} is NULL");
            }
        }

    }
}
