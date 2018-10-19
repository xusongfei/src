using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using Lead.Detect.Interfaces;
using Lead.Detect.XmlHelper;

namespace Lead.Detect.DefaultCell
{
    public class BaseCell : ICell
    {
        private DockContent _cellOutputForm = null;
        private DockContent _cellConfigForm = null;

        public BaseCellConfig CellConfig = new BaseCellConfig();

        public event CellOutputUpdate OnCellOutputUpdate;

        public string CellName
        {
            set
            {
                CellConfig.CellName = value;
                _cellOutputForm.TabText = CellConfig.CellName + " Output";
                _cellConfigForm.TabText = CellConfig.CellName + " Config";
            }
            get { return CellConfig.CellName; }
        }

        public Guid CellID
        {
            set { CellConfig.CellID = value; }
            get { return CellConfig.CellID; }
        }

        public List<IPrim> IPrimList { set; get; }

        public Form CellOutputForm
        {
            get
            {
                if (_cellOutputForm == null)
                {
                    _cellOutputForm = new BaseCellOutputForm((ICell) this);
                }

                return _cellOutputForm;
            }
        }

        public Form CellConfigForm
        {
            get
            {
                if (_cellConfigForm == null)
                {
                    _cellConfigForm = new BaseCellConfigForm((ICell) this, CellConfig);
                }

                return _cellConfigForm;
            }
        }

        public BaseCell()
        {
            CellConfig.PrimConfigList = new List<SingleCellPrimConfig>();
            _cellOutputForm = new BaseCellOutputForm((ICell) this);

            _cellConfigForm = new BaseCellConfigForm((ICell) this, CellConfig);

            CellName = "Default";
            CellID = Guid.NewGuid();
        }

        public BaseCell(XmlNode xmlNode)
        {
            if (xmlNode != null)
            {
                CellConfig = XMLHelper.XMLToObject(xmlNode, typeof(BaseCellConfig)) as BaseCellConfig;
            }
            else
            {
                return;
            }

            //CellName = "Default";
            //CellID = Guid.NewGuid();

            _cellOutputForm = new BaseCellOutputForm((ICell) this);

            _cellConfigForm = new BaseCellConfigForm((ICell) this, CellConfig);

            //_cellConfig.PrimConfigList = new List<SingleCellPrimConfig>();
        }

        public int ICellImportConfigXml(XmlNode xmlNode)
        {
            int iRet = 0;
            if (xmlNode != null)
            {
                CellConfig = XMLHelper.XMLToObject(xmlNode, typeof(BaseCellConfig)) as BaseCellConfig;
            }
            else
            {
                return -1;
            }

            return iRet;
        }

        public XmlNode ICellExprotConfigXml()
        {
            if (CellConfig == null)
            {
                return null;
            }

            XmlNode node = XMLHelper.ObjectToXML(CellConfig);

            return node;
        }

        public int ICellAddPrim()
        {
            return -1;
        }

        public int ICellAddPrim(string primName, Guid primId)
        {
            if (CellConfig == null)
            {
                return -1;
            }

            if (CellConfig.PrimConfigList == null)
            {
                return -1;
            }

            foreach (SingleCellPrimConfig item in CellConfig.PrimConfigList)
            {
                if (item.PrimGuid == primId)
                {
                    return -1;
                }
            }

            SingleCellPrimConfig sConfig = new SingleCellPrimConfig();
            sConfig.PrimName = primName;
            sConfig.PrimGuid = primId;
            sConfig.TabType = TabDisMode.Default;
            sConfig.ControlIndex = 0;
            sConfig.TabIndex = 0;

            CellConfig.PrimConfigList.Add(sConfig);

            return 0;
        }

        public int ICellDelPrim(string primName, Guid primId)
        {
            return -1;
        }

        public int ICellAddTask()
        {
            return -1;
        }

        public int ICellDelTask(string taskName, Guid taskId)
        {
            return -1;
        }

        public void CellUpdateOutputUI()
        {
            if (OnCellOutputUpdate != null)
            {
                OnCellOutputUpdate(CellID);
            }
        }
    }
}