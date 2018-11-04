using System.Collections.Generic;
using System.Linq;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.MachineUtilityLib.Utils
{
    public class MultiDiSensorCheck
    {

        public List<IDiEx> DISensors = new List<IDiEx>();

        public string ErrorMsg = "定位传感器检测异常";


        /// <summary>
        /// 检查所有传感器为触发状态
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool Check(StationTask task)
        {

            if (DISensors == null || DISensors.Count <= 0)
            {
                task.Log($"{ErrorMsg} - NO DI SENSOR CHECK", LogLevel.Warning);
                return false;
            }

            //检查定位传感器
            if (DISensors.Any(s => !s.GetDiSts()))
            {
                task.Station.Machine.Beep();
                task.Log($"{ErrorMsg} - {string.Join(",", DISensors.Select(s => s.Description))} ", LogLevel.Info);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 按statusPattern检查传感器是否在触发状态
        /// </summary>
        /// <param name="task"></param>
        /// <param name="statusPattern"></param>
        /// <returns></returns>
        public bool CheckByPattern(StationTask task, bool[] statusPattern)
        {
            if (DISensors == null || DISensors.Count <= 0)
            {
                task.Log($"{ErrorMsg} - NO DI SENSOR CHECK", LogLevel.Warning);
                return false;
            }


            if (DISensors.Count != statusPattern.Length)
            {
                task.Log($"{ErrorMsg} - DI SENSOR CHECK PATTERN ERROR ", LogLevel.Warning);
                return false;
            }


            var ret = true;
            for (int i = 0; i < statusPattern.Length; i++)
            {
                if (!DISensors[i].GetDiSts(statusPattern[i]))
                {
                    ret = false;
                }
            }

            if (!ret)
            {
                task.Log($"{ErrorMsg} - {string.Join(",", DISensors.Select(s => s.Description))} ", LogLevel.Info);
                return false;
            }
            return true;
        }
    }
}