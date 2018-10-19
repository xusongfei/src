using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace MachineUtilityLib.Utils
{
    public class DualCarrierSensorCheck
    {

        public IDiEx DISensorCheck1;
        public IDiEx DISensorCheck2;

        public string ErrorMsg = "定位传感器检测异常";

        public bool Check(StationTask task)
        {
            //检查定位传感器
            if (!DISensorCheck1.GetDiSts() || !DISensorCheck2.GetDiSts())
            {
                task.Station.Machine.Beep();
                task.Log($"{DISensorCheck1?.Description} {DISensorCheck2?.Description} {ErrorMsg}", LogLevel.Warning);
                return false;
            }
            return true;
        }
    }
}