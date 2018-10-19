using System;
using System.Windows.Forms;

namespace Lead.Detect.PrimCameraFocalSpec.Test
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void btnFSPrim2_Click(object sender, EventArgs e)
        {
            var prim = new PrimFocalSpec();
            prim.Name = "FS-2";
            splitContainer2.Panel2.Controls.Add(prim.PrimConfigUI);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var prim = new PrimFocalSpec();
            prim.Name = "FS-1";
            splitContainer2.Panel1.Controls.Add(prim.PrimConfigUI);
        }
    }
}