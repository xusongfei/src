using System;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalTask;

namespace Lead.Detect.Interface.ViewTask
{
    public delegate int TaskOpLog(string viewTaskName, Guid viewTaskId, object log);

    public delegate int TaskOpStateChanged(string viewTaskName, Guid viewTaskId, object context);

    public delegate int TaskOpDataUpdate(string viewTaskName, Guid viewTaskId, object context);

    public enum EnumViewTaskInitState
    {
        Uninitialized = 0,
        Initialized = 1,
        Other = 100,
    }

    public enum EnumViewTaskRunState
    {
        Idle = 0,
        Running = 1,
        Pause = 2,
        Err = 3,
        Other = 100,
    }

    public interface IViewTask
    {
        event TaskOpLog OnTaskOpLog;
        event TaskOpStateChanged OnTaskOpStateChanged;
        event TaskOpDataUpdate OnTaskOpDataUpdate;

        DevPrimsManager DevPrimsManagerInstance { set; get; }
        TasksManager TasksManagerInsance { set; get; }

        string ViewTaskName { set; get; }
        Guid ViewTaskId { set; get; }
        string ViewTaskFilePath { set; get; }
        string ViewTaskAssemblyPath { set; get; }
        bool ViewTaskEnabled { set; get; }
        bool IsViewTaskLoadFile { set; get; }
        bool IsViewTaskLoadAssembly { set; get; }

        EnumViewTaskInitState ViewTaskInitState { set; get; }
        EnumViewTaskRunState ViewTaskRunState { set; get; }

        Control OutputUI { set; get; }
        Control ConfigUI { set; get; }

        int IViewTaskInit();
        int IViewTaskStop();
        int IViewTaskRun();
        int IViewTaskPause();
        int IViewTaskResume();
        int IViewTaskAbort();
        int IViewTaskDispose();

        int IViewTaskImportConfigXml(XmlNode xmlNode);
        XmlNode IViewTaskExportConfigXml();

        int IViewTaskCommand(string cmd, object info);
    }
}