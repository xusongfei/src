using System.Collections.Generic;

namespace Lead.Detect.PrimPlcOmron.OmronCompletInfo
{
    public class OmronCompletInfo
    {
        public int LocalPort { set; get; }
        public string PeerAddress { set; get; }
        public string RoutePath { set; get; }
        public bool Active { set; get; }
        public string ConnectionType { set; get; }
        public bool UseRoutePath { set; get; }
        public bool IsConnected { set; get; }
        public string TypeName { set; get; }
        public bool DontFragment { set; get; }

        public List<string> SysVariableList { set; get; }
        public List<string> VariableList { set; get; }
    }
}