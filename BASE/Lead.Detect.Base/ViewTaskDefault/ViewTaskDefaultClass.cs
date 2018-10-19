using System;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Base.GlobalTask;
using Lead.Detect.Interface.ViewTask;

namespace Lead.Detect.ViewTaskDefault
{
    public class ViewTaskDefaultClass : IViewTask
    {
        private readonly DefaultConfig _defaultConfig = null;

        public ViewTaskDefaultClass()
        {
        }

        public ViewTaskDefaultClass(XmlNode configNode)
        {
        }

        public event TaskOpLog OnTaskOpLog;
        public event TaskOpStateChanged OnTaskOpStateChanged;
        public event TaskOpDataUpdate OnTaskOpDataUpdate;

        public DevPrimsManager DevPrimsManagerInstance { set; get; }

        public TasksManager TasksManagerInsance { set; get; }

        public string ViewTaskName
        {
            set { _defaultConfig.ViewTaskName = value; }
            get { return _defaultConfig.ViewTaskName; }
        }

        public Guid ViewTaskId
        {
            set { _defaultConfig.ViewTaskId = value; }
            get { return _defaultConfig.ViewTaskId; }
        }

        public string ViewTaskFilePath
        {
            set { _defaultConfig.ViewTaskFilePath = value; }
            get { return _defaultConfig.ViewTaskFilePath; }
        }

        public string ViewTaskAssemblyPath
        {
            set { _defaultConfig.ViewTaskAssemblyPath = value; }
            get { return _defaultConfig.ViewTaskAssemblyPath; }
        }

        public bool ViewTaskEnabled
        {
            set { _defaultConfig.ViewTaskEnabled = value; }
            get { return _defaultConfig.ViewTaskEnabled; }
        }

        public EnumViewTaskInitState ViewTaskInitState
        {
            set { _defaultConfig.ViewTaskInitState = value; }
            get { return _defaultConfig.ViewTaskInitState; }
        }

        public EnumViewTaskRunState ViewTaskRunState
        {
            set { _defaultConfig.ViewTaskRunState = value; }
            get { return _defaultConfig.ViewTaskRunState; }
        }

        public bool IsViewTaskLoadFile
        {
            set { _defaultConfig.IsViewTaskLoadFile = value; }
            get { return _defaultConfig.IsViewTaskLoadFile; }
        }

        public bool IsViewTaskLoadAssembly
        {
            set { _defaultConfig.IsViewTaskLoadAssembly = value; }
            get { return _defaultConfig.IsViewTaskLoadAssembly; }
        }

        public Control OutputUI { set; get; }
        public Control ConfigUI { set; get; }


        public int IViewTaskInit()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskStop()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskRun()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskPause()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskResume()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskAbort()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskDispose()
        {
            var iRet = 0;

            return iRet;
        }

        public int IViewTaskImportConfigXml(XmlNode xmlNode)
        {
            var iRet = 0;

            return iRet;
        }

        public XmlNode IViewTaskExportConfigXml()
        {
            XmlNode node = null;

            return node;
        }

        public int IViewTaskCommand(string cmd, object info)
        {
            var iRet = 0;

            return iRet;
        }

        protected virtual int OnOnTaskOpLog(string viewtaskname, Guid viewtaskid, object log)
        {
            OnTaskOpLog?.Invoke(viewtaskname, viewtaskid, log);
            return 0;
        }

        protected virtual int OnOnTaskOpStateChanged(string viewtaskname, Guid viewtaskid, object context)
        {
            OnTaskOpStateChanged?.Invoke(viewtaskname, viewtaskid, context);
            return 0;
        }

        protected virtual int OnOnTaskOpDataUpdate(string viewtaskname, Guid viewtaskid, object context)
        {
            OnTaskOpDataUpdate?.Invoke(viewtaskname, viewtaskid, context);
            return 0;
        }
    }
}