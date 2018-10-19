using System;
using System.Windows.Forms;
using System.Xml;

namespace Lead.Detect.Base.GlobalStation
{
    public interface IStation
    {
        Guid StationId { get; set; }

        string Name { set; get; }

        Control ConfigControl { get; set; }


        void Initialize();

        void Terminate();

        //void StationTaskAutoRun();

        int ImportConfig(XmlNode xmlNode);
        XmlNode ExportConfig();
    }
}