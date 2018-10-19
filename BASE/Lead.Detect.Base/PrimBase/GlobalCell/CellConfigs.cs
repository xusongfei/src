using System;
using System.Collections.Generic;

namespace Lead.Detect.Base.GlobalCell
{
    public class CellConfigs
    {
        public string CellName { set; get; }

        public Guid CellID { set; get; }

        public List<CellConfig> PrimConfigList { set; get; }
    }
}