using System;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.Base.PrimView
{
    public partial class PrimDisplay : UserControl
    {
        public event Action<int, string, Guid> PrimDisplayMenuClickEvent;

        public event Action<Guid, string> PrimDisplayPropertyChanged;


        private Image _icon;
        private string _name;

        public PrimDisplay()
        {
            InitializeComponent();
        }

        public Image UIPrimIcon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                pBoxIcon.Image = value;
            }
        }

        public string UIPrimName
        {
            get { return _name; }
            set
            {
                _name = value;
                tBoxName.Text = _name;
            }
        }

        public string UIPrimGroup { get; set; }

        public string UIPrimType { get; set; }

        public Guid UIPrimID { get; set; }

        public IPrim UIPrim { get; set; } = null;

        private void dispalyConfigUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void displayDebugUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void displayOutputUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.HideConfig, UIPrimName, UIPrimID);
        }

        private void hideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.HideOutput, UIPrimName, UIPrimID);
        }

        private void hideToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.HideDebug, UIPrimName, UIPrimID);
        }

        private void pBoxIcon_Click(object sender, EventArgs e)
        {
        }

        private void pBoxIcon_DoubleClick(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.ShowAll, UIPrimName, UIPrimID);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.Remove, UIPrimName, UIPrimID);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null)
            {
                PrimDisplayMenuClickEvent((int) PrimDisplayClick.ShowConfig, UIPrimName, UIPrimID);
            }
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.ShowOutput, UIPrimName, UIPrimID);
        }

        private void showToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (PrimDisplayMenuClickEvent != null) PrimDisplayMenuClickEvent((int) PrimDisplayClick.ShowDebug, UIPrimName, UIPrimID);
        }

        private void tBoxName_Enter(object sender, EventArgs e)
        {
        }

        private void tBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UIPrimName = tBoxName.Text;
                if (UIPrim != null)
                {
                    UIPrim.Name = tBoxName.Text;
                }

                if (PrimDisplayPropertyChanged != null) PrimDisplayPropertyChanged(UIPrimID, UIPrim.Name);
            }
        }
    }


    public enum PrimDisplayClick
    {
        ShowAll,

        ShowConfig,
        HideConfig,

        ShowDebug,
        HideDebug,

        ShowOutput,
        HideOutput,


        Remove
    }
}