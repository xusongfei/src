using System;
using System.Windows.Forms;

namespace Lead.Detect.Prim3DAgl
{
    public partial class PrimConfigControl : UserControl
    {
        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public string AglName
        {
            set { tTaskBoxName.Text = value; }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
        }

        private void btnPathDir_Click(object sender, EventArgs e)
        {
        }

        private void buttonPoints_Click(object sender, EventArgs e)
        {
        }

        private void chkBtn2D_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkBtn3D_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}