using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element.EleControl
{
    public partial class EleDiConfigControl : Form
    {
        private EleDi _di;
        private IMotionCard _motion;

        public EleDiConfigControl()
        {
            InitializeComponent();
        }

        private void EleDiConfigControl_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            btnRun.PerformClick();
        }

        public int LoadDi(EleDi config)
        {
            int iRet = 0;

            if (config == null)
            {
                return -1;
            }

            _di = config;

            lbInputName.Text = _di.Name;

            comboBoxPrimDev.Items.Clear();
            if (DevPrimsManager.Instance.Prims.Count > 0)
            {
                comboBoxPrimDev.Items.AddRange(DevPrimsManager.Instance.Prims.FindAll(p => p != null && p is IMotionCard).Select(p => p.Name).ToArray());
            }

            if (string.IsNullOrEmpty(_di.Driver))
                comboBoxPrimDev.Text = "";
            else
                comboBoxPrimDev.Text = _di.Driver;

            tBoxDI1.Text = _di.Port.ToString();
            cBoxnable1.Checked = _di.Enable;


            btnRun.PerformClick();

            return iRet;
        }

        private void comboBoxPrimDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            _di.Driver = comboBoxPrimDev.Text;
        }

        private void comboBoxPrimDev_TextChanged(object sender, EventArgs e)
        {
            _di.Driver = comboBoxPrimDev.Text;
        }

        private void tBoxDI1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDI1.Text))
            {
                int value = -1;
                int.TryParse(tBoxDI1.Text, out value);
                if (value != -1)
                {
                    _di.Port = value;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            IPrim prim = DevPrimsManager.Instance.GetPrimByName(_di.Driver);
            if (prim == null)
            {
                MessageBox.Show("没有找到名称为： " + _di.Driver + " 的设备！");
                return;
            }

            if (_motion != prim)
            {
                _motion = (IMotionCard) prim;
            }

            btnRun.BackColor = Color.Green;
            tmrUpdateCylinder.Start();
        }

        private void tmrUpdateCylinder_Tick(object sender, EventArgs e)
        {
            int val;
            if (_di.Enable)
            {
                val = -1;
                _motion.ReadSingleDInput(_motion.DevIndex, 0, _di.Port, out val);
                if (val != -1)
                {
                    label3.BackColor = (val == 0) ? System.Drawing.SystemColors.GradientActiveCaption : System.Drawing.Color.LawnGreen;
                }
            }
        }

        private void cBoxnable1_CheckedChanged(object sender, EventArgs e)
        {
            _di.Enable = cBoxnable1.Checked;
        }
    }
}