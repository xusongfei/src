using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Base.GlobalTask;

namespace Lead.Detect.Base.GlobalCell
{
    public interface ICell
    {
        event Action<Guid> OnCellOutputUpdate;


        Guid CellID { set; get; }
        string CellName { set; get; }


        List<IPrim> IPrimList { set; get; }
        List<IPrimTask> ITaskList { set; get; }


        Form CellOutputForm { get; }
        Form CellConfigForm { get; }


        int AddPrim();
        int AddPrim(string primName, Guid primId);
        int AddTask();
        int DeletePrim(string primName, Guid primId);
        int DeleteTask(string taskName, Guid taskId);


        XmlNode ExportConfig();

        int ImportConfig(XmlNode xmlNode);
    }
}