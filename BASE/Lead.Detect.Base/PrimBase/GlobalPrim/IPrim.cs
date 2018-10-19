using System;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Base.GlobalPrim
{
    public interface IPrim
    {
        string Name { set; get; }

        string PrimTypeName { set; get; }

        Guid PrimId { set; get; }

        //Prim 子类的类型
        Type ChildType { set; get; }

        //Prim的硬件、软件类型
        PrimType HSType { set; get; }

        PrimManufacture Manu { set; get; }

        PrimVer Ver { set; get; }

        PrimConnType ConnType { set; get; }

        string ConnInfo { set; get; }

        PrimConnState PrimConnStat { set; get; }

        PrimRunState PrimRunStat { set; get; }

        bool PrimSimulator { set; get; }

        bool PrimDebug { set; get; }

        bool PrimEnable { set; get; }

        IDataArea DataArea { set; get; }

        Control PrimDebugUI { get; }

        Control PrimConfigUI { get; }

        Control PrimOutputUI { get; }

        int IPrimConnect(ref string result);

        int IPrimDisConnect(ref string result);

        int IPrimDispose();


        
        XmlNode ExportConfig();

        XmlNode ExportDataConfig();

        int ImportConfig(XmlNode xmlNode);

        int ImportDataConfig(XmlNode xmlNode);



        int IPrimInit();

        int IPrimResume();

        object IPrimSetCommand(PrimCmdType cmdType, string cmd, object info);

        int IPrimStart();

        int IPrimStop();

        int IPrimSuspend();


        event PrimDataRefresh OnPrimDataRefresh;

        event PrimOpLog OnPrimOpLog;

        event PrimStateChanged OnPrimStateChanged;
      
    }
}