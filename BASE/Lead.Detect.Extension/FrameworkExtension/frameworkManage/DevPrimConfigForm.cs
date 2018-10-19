using System;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    public partial class DevPrimConfigForm : Form
    {
        public DevPrimConfigForm()
        {
            InitializeComponent();
        }

        private void PrimConfigForm_Load(object sender, EventArgs e)
        {

            if (Prim != null)
            {
                Text = Prim.ToString();

                if (Prim.PrimConfigUI != null)
                {
                    Prim.PrimConfigUI.Dock = DockStyle.Fill;
                    tabPage1.Text = Prim.PrimConfigUI.Text;
                    tabPage1.Controls.Add(Prim.PrimConfigUI);
                }
              
                if (Prim.PrimDebugUI != null)
                {
                    Prim.PrimDebugUI.Dock = DockStyle.Fill;
                    tabPage2.Text = Prim.PrimDebugUI.Text;
                    tabPage2.Controls.Add(Prim.PrimDebugUI);
                }


            }
        }

        public IPrim Prim;
    }
}
