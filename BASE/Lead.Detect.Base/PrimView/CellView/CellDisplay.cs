using System;
using System.Windows.Forms;

namespace Lead.Detect.View.CellView
{
    public delegate void DelCellDisplayEventHandler(Guid cellGuid);

    public delegate void CellDisplayChangeNameEventHandler(Guid cellGuid, string cellNewName);

    public delegate void CellConfigUIVisibleEventHandler(Guid cellGuid, bool visible);

    public delegate void CellOutputUIVisibleEventHandler(Guid cellGuid, bool visible);

    public partial class CellDisplay : UserControl
    {
        private Guid _cellGuid = Guid.Empty;

        public CellConfigUIVisibleEventHandler CellConfigUIVisibleEvent;
        public CellDisplayChangeNameEventHandler CellDisplayChangeNameEvent;
        public CellOutputUIVisibleEventHandler CellOutputUIVisibleEvent;
        public DelCellDisplayEventHandler DelCellDisplayEvent;

        public CellDisplay()
        {
            InitializeComponent();
            CellOutputUIVisible = false;
            CellConfigUIVisible = false;
        }

        public CellDisplay(string name, Guid id)
        {
            InitializeComponent();
            CellName = name;
            CellGuid = id;
        }

        public string CellName
        {
            set { tBoxName.Text = value; }
            get { return tBoxName.Text; }
        }

        public Guid CellGuid
        {
            set
            {
                lbGuid.Text = value.ToString();
                _cellGuid = value;
            }
            get { return _cellGuid; }
        }

        public bool CellConfigUIVisible { set; get; }

        public bool CellOutputUIVisible { set; get; }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (DelCellDisplayEvent != null) DelCellDisplayEvent(CellGuid);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tBoxName.Text))
            {
                MessageBox.Show("Cell Name can't be Null");
                return;
            }

            tBoxName.Enabled = !tBoxName.Enabled;

            if (!tBoxName.Enabled)
                if (CellDisplayChangeNameEvent != null)
                    CellDisplayChangeNameEvent(CellGuid, CellName);
        }

        private void diaplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CellOutputUIVisible) return;

            CellOutputUIVisible = true;
            if (CellOutputUIVisibleEvent != null) CellOutputUIVisibleEvent(CellGuid, CellOutputUIVisible);
        }

        private void dispalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CellConfigUIVisible) return;

            CellConfigUIVisible = true;
            if (CellConfigUIVisibleEvent != null) CellConfigUIVisibleEvent(CellGuid, CellConfigUIVisible);
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CellConfigUIVisible) return;

            CellConfigUIVisible = false;
            if (CellConfigUIVisibleEvent != null) CellConfigUIVisibleEvent(CellGuid, CellConfigUIVisible);
        }

        private void hideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!CellOutputUIVisible) return;

            CellOutputUIVisible = false;
            if (CellOutputUIVisibleEvent != null) CellOutputUIVisibleEvent(CellGuid, CellOutputUIVisible);
        }
    }
}