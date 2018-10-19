using System;

namespace Lead.Detect.Base.GlobalCell
{
    public class CellConfig
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
}