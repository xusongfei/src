using System;
using System.Collections.Generic;

namespace Lead.Detect.DefaultCell
{
    public class BaseCellConfig
    {
        public string CellName { set; get; }

        public Guid CellID { set; get; }

        public List<SingleCellPrimConfig> PrimConfigList { set; get; }
    }

    public class SingleCellPrimConfig
    {
        public string PrimName { set; get; }
        public Guid PrimGuid { set; get; }

        public bool OutputUIDis { set; get; }
        public bool ConfigUIDis { set; get; }
        public bool DebugUIDis { set; get; }

        public int TabIndex { set; get; }
        public int ControlIndex { set; get; }

        public TabDisMode TabType { set; get; }
    }

    public enum TabDisMode
    {
        Default = 0,
        SingleTab = 1,
        MultiTab = 2,
        Other = 100
    }
}