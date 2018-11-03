using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element.EleControl
{
    public partial class EleVioConfigControl : Form
    {
        public static object EleCollections;


        private EleVio _ele;
        private IMotionCard _motion;

        public EleVioConfigControl()
        {
            InitializeComponent();
        }

        private void EleCylinderConfigControl_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            btnRun.PerformClick();
        }

        public int LoadDo(EleVio newDo)
        {
            if (newDo == null)
            {
                return -1;
            }

            _ele = newDo;

            labelName.Text = newDo.Name;

            if (DevPrimsManager.Instance.Prims.Count > 0)
            {
                comboBoxPrimDev.Items.AddRange(DevPrimsManager.Instance.Prims.FindAll(p => p is IMotionCard).Select(p => p.Name).ToArray());
            }

            if (string.IsNullOrEmpty(_ele.Driver))
                comboBoxPrimDev.Text = "";
            else
                comboBoxPrimDev.Text = _ele.Driver;


            tBoxDO1.Text = _ele.Port.ToString();
            cBoxDOEnable1.Checked = _ele.Enable;


            btnRun.PerformClick();

            return 0;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var prim = DevPrimsManager.Instance.GetPrimByName(_ele.Driver);
            if (prim == null)
            {
                MessageBox.Show("没有找到名称为： " + _ele.Driver + " 的设备！");
                return;
            }

            if (_motion != prim)
            {
                _motion = (IMotionCard) prim;
            }

            btnRun.BackColor = Color.LightGreen;
            btnStop.BackColor = Color.LightGray;
            tmrUpdateCylinder.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnRun.BackColor = Color.LightGray;
            btnStop.BackColor = Color.LightGray;
            tmrUpdateCylinder.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void tmrCylinderUpdate_Tick(object sender, EventArgs e)
        {
            if (((IPrim) _motion).PrimConnStat == PrimConnState.Connected)
            {
                labelName.BackColor = Color.LawnGreen;
            }
            else
            {
                labelName.BackColor = SystemColors.GradientActiveCaption;
                return;
            }

            var val = -1;

            if (_ele.Enable)
            {
                _motion.ReadSingleDOutput(_motion.DevIndex, 0, _ele.Port, out val);
                if (val != -1) label1.BackColor = val == 0 ? SystemColors.GradientActiveCaption : Color.LawnGreen;
            }
        }

        public void UpdateUI()
        {
            comboBoxPrimDev.Enabled = _ele.Enable;

            tBoxDO1.Enabled = _ele.Enable & _ele.Enable;

            btnDOSet1.Enabled = tBoxDO1.Enabled;
        }

        private void comboBoxPrimDev_TextChanged(object sender, EventArgs e)
        {
            _ele.Driver = comboBoxPrimDev.Text;
        }

        private void comboBoxPrimDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ele.Driver = comboBoxPrimDev.Text;
        }

        private void tBoxDO1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDO1.Text))
            {
                var value = -1;
                int.TryParse(tBoxDO1.Text, out value);
                if (value != -1) _ele.Port = value;
            }
        }


        private void btnDOSet1_Click(object sender, EventArgs e)
        {
            if (_motion != null)
            {
                int sts;
                _motion.ReadSingleDOutput(_motion.DevIndex, 0, _ele.Port, out sts);
                _motion.WriteSingleDOutput(_motion.DevIndex, 0, _ele.Port, sts == 1 ? 0 : 1);
            }
        }


        private void cBoxDOEnable1_CheckedChanged(object sender, EventArgs e)
        {
            _ele.Enable = cBoxDOEnable1.Checked;
            UpdateUI();
        }
    }
}