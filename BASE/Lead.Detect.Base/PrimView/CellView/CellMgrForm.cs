using System;
using System.Windows.Forms;
using System.Xml;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalCell;
using Lead.Detect.Base.GlobalPrim;
using WeifenLuo.WinFormsUI.Docking;
using CellConfig = Lead.Detect.Base.GlobalCell.CellConfig;

namespace Lead.Detect.View.CellView
{
    public partial class CellMgrForm : DockContent
    {
        public event CellConfigUIVisibleEventHandler CellConfigUIVisibleEvent;
        public event CellOutputUIVisibleEventHandler CellOutputUIVisibleEvent;
        public event CellDisplayChangeNameEventHandler CellDisplayChangeNameEvent;
        public event DelCellDisplayEventHandler DelCellDisplayEvent;

        public CellMgrForm()
        {
            InitializeComponent();
        }

        public ICell CreateCell(XmlNode node)
        {
            ICell cell = CellsFactory.Instance.CreateCell(node);
            cell.OnCellOutputUpdate += (OnCellOutputUpdateEvent);

            if (cell != null)
            {
                CellsManager.Instance.Cells.Add(cell);

                CellDisplay cellDis = new CellDisplay();
                cellDis.CellName = cell.CellName;
                cellDis.CellGuid = cell.CellID;
                cellDis.Dock = DockStyle.Top;
                cellDis.CellConfigUIVisibleEvent += new CellConfigUIVisibleEventHandler(OnCellConfigUIVisibleEvent);
                cellDis.CellOutputUIVisibleEvent += new CellOutputUIVisibleEventHandler(OnCellOutputUIVisibleEvent);
                cellDis.CellDisplayChangeNameEvent += new CellDisplayChangeNameEventHandler(OnCellDisplayChangeNameEvent);
                cellDis.DelCellDisplayEvent += new DelCellDisplayEventHandler(OnDelCellDisplayEvent);

                pnlCell.Controls.Add(cellDis);
            }

            return cell;
        }

        private void btnCellAdd_Click(object sender, EventArgs e)
        {
            ICell cell = CellsFactory.Instance.CreateCell();
            cell.OnCellOutputUpdate += (OnCellOutputUpdateEvent);

            if (cell != null)
            {
                CellsManager.Instance.Cells.Add(cell);

                CellDisplay cellDis = new CellDisplay();
                cellDis.CellName = cell.CellName;
                cellDis.CellGuid = cell.CellID;
                cellDis.Dock = DockStyle.Top;
                cellDis.CellConfigUIVisibleEvent += new CellConfigUIVisibleEventHandler(OnCellConfigUIVisibleEvent);
                cellDis.CellOutputUIVisibleEvent += new CellOutputUIVisibleEventHandler(OnCellOutputUIVisibleEvent);
                cellDis.CellDisplayChangeNameEvent += new CellDisplayChangeNameEventHandler(OnCellDisplayChangeNameEvent);
                cellDis.DelCellDisplayEvent += new DelCellDisplayEventHandler(OnDelCellDisplayEvent);

                pnlCell.Controls.Add(cellDis);
            }
        }

        public void OnCellOutputUpdateEvent(Guid cellGuid)
        {
            ICell cell = CellsManager.Instance.GetDefCellByGuid(cellGuid);
            if (cell == null)
            {
                return;
            }

            Cell dCell = (Cell) cell;

            if (dCell.CellOutputForm == null)
            {
                return;
            }

            foreach (CellConfig outConfig in dCell.CellConfigs.PrimConfigList)
            {
                if (outConfig.PrimGuid == null)
                {
                    continue;
                }

                IPrim prim = DevPrimsManager.Instance.GetPrimByGUID(outConfig.PrimGuid);

                if (prim == null)
                {
                    return;
                }

                dCell.CellOutputForm.Controls.Clear();

                if (outConfig.OutputUIDis)
                {
                    dCell.CellOutputForm.Controls.Add(prim.PrimOutputUI);
                }

                if (outConfig.ConfigUIDis)
                {
                    dCell.CellOutputForm.Controls.Add(prim.PrimConfigUI);
                }

                if (outConfig.DebugUIDis)
                {
                    dCell.CellOutputForm.Controls.Add(prim.PrimDebugUI);
                }
            }
        }

        private void OnCellConfigUIVisibleEvent(Guid cellGuid, bool visible)
        {
            if (CellConfigUIVisibleEvent != null)
            {
                CellConfigUIVisibleEvent(cellGuid, visible);
            }
        }

        private void OnCellOutputUIVisibleEvent(Guid cellGuid, bool visible)
        {
            if (CellOutputUIVisibleEvent != null)
            {
                CellOutputUIVisibleEvent(cellGuid, visible);
            }
        }

        private void OnCellDisplayChangeNameEvent(Guid cellGuid, string name)
        {
            if (CellDisplayChangeNameEvent != null)
            {
                CellDisplayChangeNameEvent(cellGuid, name);
            }
        }

        private void OnDelCellDisplayEvent(Guid cellGuid)
        {
            if (DelCellDisplayEvent != null)
            {
                DelCellDisplayEvent(cellGuid);
            }
        }
    }
}