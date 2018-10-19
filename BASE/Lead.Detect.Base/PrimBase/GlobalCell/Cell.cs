using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Base.GlobalTask;
using Lead.Detect.Helper;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.Base.GlobalCell
{
    public class Cell : ICell
    {
        private DockContent _cellConfigForm;
        private DockContent _cellOutputForm;

        public CellConfigs CellConfigs = new CellConfigs();

        public Cell()
        {
            CellID = Guid.NewGuid();
            CellName = "Default";
            CellConfigs.PrimConfigList = new List<CellConfig>();

            _cellOutputForm = new CellOutputForm(this);
            _cellConfigForm = new CellConfigForm(this, CellConfigs);
        }

        public Cell(XmlNode xmlNode)
        {
            if (xmlNode != null)
                CellConfigs = XMLHelper.XMLToObject(xmlNode, typeof(CellConfigs)) as CellConfigs;
            else
                return;

            CellID = Guid.NewGuid();
            CellName = "Default";

            _cellOutputForm = new CellOutputForm(this);
            _cellConfigForm = new CellConfigForm(this, CellConfigs);
        }

        public event Action<Guid> OnCellOutputUpdate;

        public string CellName
        {
            set
            {
                CellConfigs.CellName = value;
                _cellOutputForm.TabText = CellConfigs.CellName + " Output";
                _cellConfigForm.TabText = CellConfigs.CellName + " Config";
            }
            get { return CellConfigs.CellName; }
        }

        public Guid CellID
        {
            set { CellConfigs.CellID = value; }
            get { return CellConfigs.CellID; }
        }

        public List<IPrim> IPrimList { set; get; }
        public List<IPrimTask> ITaskList { get; set; }

        public Form CellOutputForm
        {
            get
            {
                if (_cellOutputForm == null) _cellOutputForm = new CellOutputForm(this);

                return _cellOutputForm;
            }
        }

        public Form CellConfigForm
        {
            get
            {
                if (_cellConfigForm == null) _cellConfigForm = new CellConfigForm(this, CellConfigs);

                return _cellConfigForm;
            }
        }

        public int AddPrim()
        {
            return -1;
        }

        public int AddPrim(string primName, Guid primId)
        {
            if (CellConfigs == null) return -1;

            if (CellConfigs.PrimConfigList == null) return -1;

            foreach (var item in CellConfigs.PrimConfigList)
                if (item.PrimGuid == primId)
                    return -1;

            var sConfig = new CellConfig();
            sConfig.PrimName = primName;
            sConfig.PrimGuid = primId;
            sConfig.TabType = TabDisMode.Default;
            sConfig.ControlIndex = 0;
            sConfig.TabIndex = 0;

            CellConfigs.PrimConfigList.Add(sConfig);

            return 0;
        }

        public int DeletePrim(string primName, Guid primId)
        {
            return -1;
        }

        public int AddTask()
        {
            return -1;
        }

        public int DeleteTask(string taskName, Guid taskId)
        {
            return -1;
        }

        public int ImportConfig(XmlNode xmlNode)
        {
            var iRet = 0;
            if (xmlNode != null)
                CellConfigs = XMLHelper.XMLToObject(xmlNode, typeof(CellConfigs)) as CellConfigs;
            else
                return -1;

            return iRet;
        }

        public XmlNode ExportConfig()
        {
            if (CellConfigs == null) return null;

            var node = XMLHelper.ObjectToXML(CellConfigs);

            return node;
        }

        public void CellUpdateOutputUI()
        {
            OnCellOutputUpdate?.Invoke(CellID);
        }
    }
}