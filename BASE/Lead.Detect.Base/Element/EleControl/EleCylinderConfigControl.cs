using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element
{
    public partial class EleCylinderConfigControl : Form
    {
        private EleCylinder _cy;
        private IMotionCard _motion;

        public EleCylinderConfigControl()
        {
            InitializeComponent();
        }

        private void EleCylinderConfigControl_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            btnRun.PerformClick();
        }

        public int LoadCylinder(EleCylinder config)
        {
            var iRet = 0;

            if (config == null) return -1;
            _cy = config;

            lbCylinderName.Text = config.Name;

            if (DevPrimsManager.Instance.Prims.Count > 0)
            {
                comboBoxPrimDev.Items.AddRange(DevPrimsManager.Instance.Prims.FindAll(p => p is IMotionCard).Select(p => p.Name).ToArray());
            }

            if (string.IsNullOrEmpty(_cy.Driver))
                comboBoxPrimDev.Text = "";
            else
                comboBoxPrimDev.Text = _cy.Driver;

            cBoxCylinderEnable.Checked = _cy.Enable;

            tBoxDI1.Text = _cy.DiOrg.ToString();
            tBoxDI2.Text = _cy.DiWork.ToString();
            tBoxDO1.Text = _cy.DoOrg.ToString();
            tBoxDO2.Text = _cy.DoWork.ToString();

            cBoxDIEnable1.Checked = _cy.DiOrgEnable;
            cBoxDIEnable2.Checked = _cy.DiWorkEnable;
            cBoxDOEnable1.Checked = _cy.DoOrgEnable;
            cBoxDOEnable2.Checked = _cy.DoWorkEnable;

            btnRun.PerformClick();

            return iRet;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var prim = DevPrimsManager.Instance.GetPrimByName(_cy.Driver);
            if (prim == null)
            {
                MessageBox.Show("没有找到名称为： " + _cy.Driver + " 的设备！");
                return;
            }

            if (_motion != prim) _motion = (IMotionCard) prim;


            tmrUpdateCylinder.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrUpdateCylinder.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void tmrCylinderUpdate_Tick(object sender, EventArgs e)
        {
            if (((IPrim) _motion).PrimConnStat == PrimConnState.Connected)
            {
                lbCylinderName.BackColor = Color.LawnGreen;
            }
            else
            {
                lbCylinderName.BackColor = SystemColors.GradientActiveCaption;
                return;
            }

            var val = -1;

            if (_cy.DiOrgEnable)
            {
                _motion.ReadSingleDInput(_motion.DevIndex, 0, _cy.DiOrg, out val);
                if (val != -1) label3.BackColor = val == 0 ? SystemColors.GradientActiveCaption : Color.LawnGreen;
            }

            if (_cy.DiWorkEnable)
            {
                val = -1;
                _motion.ReadSingleDInput(_motion.DevIndex, 0, _cy.DiWork, out val);
                if (val != -1) label4.BackColor = val == 0 ? SystemColors.GradientActiveCaption : Color.LawnGreen;
            }

            if (_cy.DoOrgEnable)
            {
                val = -1;
                _motion.ReadSingleDOutput(_motion.DevIndex, 0, _cy.DoOrg, out val);
                if (val != -1) label5.BackColor = val == 0 ? SystemColors.GradientActiveCaption : Color.LawnGreen;
            }

            if (_cy.DoWorkEnable)
            {
                val = -1;
                _motion.ReadSingleDOutput(_motion.DevIndex, 0, _cy.DoWork, out val);
                if (val != -1) label2.BackColor = val == 0 ? SystemColors.GradientActiveCaption : Color.LawnGreen;
            }
        }

        public void UpdateUI()
        {
            comboBoxPrimDev.Enabled = _cy.Enable;
            cBoxDOEnable1.Enabled = _cy.Enable;
            cBoxDOEnable2.Enabled = _cy.Enable;
            cBoxDIEnable1.Enabled = _cy.Enable;
            cBoxDIEnable2.Enabled = _cy.Enable;

            tBoxDO1.Enabled = _cy.Enable & _cy.DoOrgEnable;
            tBoxDO2.Enabled = _cy.Enable & _cy.DoWorkEnable;
            tBoxDI1.Enabled = _cy.Enable & _cy.DiOrgEnable;
            tBoxDI2.Enabled = _cy.Enable & _cy.DiWorkEnable;

            btnDOSet1.Enabled = tBoxDO1.Enabled;

            btnDOSet2.Enabled = tBoxDO2.Enabled;
        }

        #region  cy config

        private void comboBoxPrimDev_TextChanged(object sender, EventArgs e)
        {
            _cy.Driver = comboBoxPrimDev.Text;
        }

        private void comboBoxPrimDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cy.Driver = comboBoxPrimDev.Text;
        }

        private void tBoxDI1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDI1.Text))
            {
                var value = -1;
                int.TryParse(tBoxDI1.Text, out value);
                if (value != -1) _cy.DiOrg = value;
            }
        }

        private void tBoxDI2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDI2.Text))
            {
                var value = -1;
                int.TryParse(tBoxDI2.Text, out value);
                if (value != -1) _cy.DiWork = value;
            }
        }

        private void tBoxDO1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDO1.Text))
            {
                var value = -1;
                int.TryParse(tBoxDO1.Text, out value);
                if (value != -1) _cy.DoOrg = value;
            }
        }

        private void tBoxDO2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxDO2.Text))
            {
                var value = -1;
                int.TryParse(tBoxDO2.Text, out value);
                if (value != -1) _cy.DoWork = value;
            }
        }

        private void cBoxCylinderEnable_CheckedChanged(object sender, EventArgs e)
        {
            _cy.Enable = cBoxCylinderEnable.Checked;
            UpdateUI();
        }

        private void cBoxDIEnable1_CheckedChanged(object sender, EventArgs e)
        {
            _cy.DiOrgEnable = cBoxDIEnable1.Checked;
            UpdateUI();
        }

        private void cBoxDIEnable2_CheckedChanged(object sender, EventArgs e)
        {
            _cy.DiWorkEnable = cBoxDIEnable2.Checked;
            UpdateUI();
        }

        private void cBoxDOEnable1_CheckedChanged(object sender, EventArgs e)
        {
            _cy.DoOrgEnable = cBoxDOEnable1.Checked;
            UpdateUI();
        }

        private void cBoxDOEnable2_CheckedChanged(object sender, EventArgs e)
        {
            _cy.DoWorkEnable = cBoxDOEnable2.Checked;
            UpdateUI();
        }

        #endregion


        #region  cy 

        private void btnDOSet1_Click(object sender, EventArgs e)
        {
            if (_motion == null)
            {
                return;
            }

            int sts;
            _motion.ReadSingleDOutput(_motion.DevIndex, 0, _cy.DoOrg, out sts);
            _motion.WriteSingleDOutput(_motion.DevIndex, 0, _cy.DoOrg, sts == 1 ? 0 : 1);
        }

        private void btnDOSet2_Click(object sender, EventArgs e)
        {
            if (_motion == null)
            {
                return;
            }

            int sts;
            _motion.ReadSingleDOutput(_motion.DevIndex, 0, _cy.DoWork, out sts);
            _motion.WriteSingleDOutput(_motion.DevIndex, 0, _cy.DoWork, sts == 1 ? 0 : 1);
        }

        #endregion
    }
}