using Lead.Detect.FrameworkExtension.stateMachine;

namespace MachineUtilityLib.Utils
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
            while (waitTask.State != TaskState.WaitRun && waitTask.State != TaskState.Running)
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
                task.ThrowException($"{msg ?? "SOME OBJECT"} is NULL");
            }
        }

    }
}
