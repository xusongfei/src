using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.Interface.ViewTask;

namespace Lead.Detect.ViewTaskDefault
{
    public class DefaultConfig
    {
        public string ViewTaskName { set; get; }
        public Guid ViewTaskId { set; get; }
        public string ViewTaskFilePath { set; get; }
        public string ViewTaskAssemblyPath { set; get; }
        public bool ViewTaskEnabled { set; get; }

        public EnumViewTaskInitState ViewTaskInitState { set; get; }
        public EnumViewTaskRunState ViewTaskRunState { set; get; }
        public bool IsViewTaskLoadFile { set; get; }
        public bool IsViewTaskLoadAssembly { set; get; }
    }
}