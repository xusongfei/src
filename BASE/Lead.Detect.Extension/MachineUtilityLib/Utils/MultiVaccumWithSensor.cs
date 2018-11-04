using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.MachineUtilityLib.Utils
{
    public class MultiVaccumWithSensor
    {
        public List<IDoEx> VaccumDoExs = new List<IDoEx>();

        public List<IDiEx> VaccumSensors = new List<IDiEx>();



        public void SetVaccum(StationTask task, bool status, int delay = 0, int timeout = 1000, bool checkSensor = false, bool showError = false)
        {

            foreach (var vaccumDoEx in VaccumDoExs)
            {
                vaccumDoEx.SetDo(status);
            }

            if (delay > 0)
            {
                Thread.Sleep(delay);
            }

            if (VaccumSensors.Count > 0)
            {
                var ret = VaccumSensors.ToArray().WaitDi(task, status, timeout);
                if (!ret)
                {
                    task.Log($"{string.Join(",", VaccumDoExs.Select(v => v.Description))} 信号异常", showError ? LogLevel.Error : LogLevel.Info);
                }
                else
                {
                    task.Log($"{string.Join(",", VaccumDoExs.Select(v => v.Description))} SetVaccum {status} success");
                }
            }

        }
    }
}
