using System.Collections.Generic;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Base.GlobalTask;
using Lead.Detect.FrameworkExtension.common;

namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    public class DevProject : UserSettings<DevProject>
    {
        public DevProject()
        {
            ProjectName = "default.dev";
        }

        public string ProjectName;

        public List<PrimConfig> Prims = new List<PrimConfig>();
        public List<TaskConfig> Tasks = new List<TaskConfig>();
        public List<CellConfig> Cells = new List<CellConfig>();
        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}