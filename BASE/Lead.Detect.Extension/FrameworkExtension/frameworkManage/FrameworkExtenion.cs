using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    public static class FrameworkExtenion
    {
        public static bool IsDebugFramework = false;

        public static bool IsSimulate = false;


        public static string FrameworkExceptionHead = "FRAMEWORK EXCEPTION";


        public static void ShowMsgError(string msg)
        {
            if (IsDebugFramework)
            {
                EnvironmentManager.Ins.DebugFramework(msg);
            }

            MessageBox.Show(msg, FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }



    public class FrameworkUserTypeManager
    {
        public static Dictionary<string, Type> TaskTypes = new Dictionary<string, Type>();

        public static void LoadUserTaskTypes(Assembly assembly)
        {
            var taskTypes = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(StationTask)));
            foreach (var t in taskTypes)
            {
                TaskTypes.Add(t.Name, t);
            }

            EnvironmentManager.Ins.DebugFramework($"加载用户定义任务类型：\n{string.Join("\n", TaskTypes.Select(t => t.Key))}");
        }

        public static Dictionary<string, Type> MotionTypes = new Dictionary<string, Type>();

        public static void LoadUserMotionTypes(string folder)
        {

        }

    }



}
