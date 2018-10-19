using System;
using System.Windows.Forms;
using Lead.Detect.PrimCameraDalsa.Dalsa;

namespace Lead.Detect.PrimCameraDalsa
{
    public partial class PrimConfigControl : UserControl
    {
        private DalsaConfig _dalsaConfig;
        private DalsaCore _dalsaCore;

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public string ShowTitle
        {
            set { lbTitle.Text = value; }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var num = 0;
            int.TryParse(txtPixel.Text, out num);
            _dalsaConfig.BitsPerPixel = num;

            var width = 0;
            int.TryParse(textBoxWidth.Text, out width);
            _dalsaConfig.Width = width;

            var delay = 0;
            int.TryParse(doBoxDeyT.Text, out delay);
            _dalsaConfig.DelayTime = delay;

            var height = 0;
            int.TryParse(textBoxHeight.Text, out height);
            _dalsaConfig.Height = height;
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSavepath.Text = folderBrowserDialog1.SelectedPath;
                _dalsaConfig.SavePath = txtSavepath.Text;
            }
        }

        private void btnSavePath_Click_1(object sender, EventArgs e)
        {
            btnSavePath_Click(null, null);
        }

        private void button_Freeze_Click(object sender, EventArgs e)
        {
            if (_dalsaCore.Xfer != null)
                if (_dalsaCore.Xfer.Freeze())
                    _dalsaCore.Xfer.Abort();
        }

        private void button_Freeze_Click_1(object sender, EventArgs e)
        {
            button_Freeze_Click(null, null);
        }

        private void button_Grab_Click(object sender, EventArgs e)
        {
            if (_dalsaCore.Xfer != null)
                if (_dalsaCore.Xfer.Grab())
                {
                }
        }

        private void button_Grab_Click_1(object sender, EventArgs e)
        {
            button_Grab_Click(null, null);
        }

        private void button_LoadConfig_Click(object sender, EventArgs e)
        {
            _dalsaCore.DestroyObjects();
            var acConfigDlg = new AcqConfigDlg(null, "", AcqConfigDlg.ServerCategory.ServerAcq);
            if (acConfigDlg.ShowDialog() == DialogResult.OK)
            {
                var onlineSave = _dalsaCore.Online;
                _dalsaCore.Online = true;
                var serverLocationSave = _dalsaCore.Location;
                _dalsaCore.DestroyObjects();

                _dalsaConfig.FileName = acConfigDlg.m_ConfigFile;
                _dalsaConfig.ResourceIndex = acConfigDlg.m_ResourceIndex;
                _dalsaConfig.ServerName = acConfigDlg.m_ServerName;

                if (!_dalsaCore.CreateNewObjects(acConfigDlg.ServerLocation, _dalsaConfig.FileName, false))
                {
                    MessageBox.Show("New objects creation has failed. Restoring original object ");
                    _dalsaCore.Location = serverLocationSave;
                    _dalsaCore.Online = onlineSave;
                    if (!_dalsaCore.CreateNewObjects(null, "", true))
                    {
                        MessageBox.Show("Original object creation has failed. Closing application ");
                        Application.Exit();
                    }
                }

                _dalsaConfig.Width = _dalsaCore.m_View.Buffer.Width;
                _dalsaConfig.Height = _dalsaCore.m_View.Buffer.Height;
                _dalsaCore.ObjSize = _dalsaConfig.Width * _dalsaConfig.Height;
                textBoxWidth.Text = _dalsaConfig.Width.ToString();
                textBoxHeight.Text = _dalsaConfig.Height.ToString();
            }
            else
            {
                MessageBox.Show("No Modification in Acquisition");
            }
        }

        private void button_LoadConfig_Click_1(object sender, EventArgs e)
        {
            button_LoadConfig_Click(null, null);
        }

        private void button_Snap_Click(object sender, EventArgs e)
        {
            if (_dalsaCore.CoreSnap())
            {
            }
        }

        private void button_Snap_Click_1(object sender, EventArgs e)
        {
            button_Snap_Click(null, null);
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dalsaConfig.ImgFormat = cbFormat.Text;
        }

        private void cbFormat_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbFormat_SelectedIndexChanged(null, null);
        }

        private void cbSaveFile_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSaveFile.Checked)
            {
                cbFormat.Enabled = true;
                btnSavePath.Enabled = true;
                _dalsaConfig.IsSave = true;
            }
            else
            {
                cbFormat.Enabled = false;
                btnSavePath.Enabled = false;
                _dalsaConfig.IsSave = false;
            }
        }

        private void cbSaveFile_CheckedChanged_1(object sender, EventArgs e)
        {
            cbSaveFile_CheckedChanged(null, null);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dalsaConfig.ImgFrom = comboBox2.Text;
            if (comboBox2.Text == "File")
            {
                button_Snap.Enabled = false;
                button_Grab.Enabled = false;
                button_Freeze.Enabled = false;
                button_LoadConfig.Enabled = false;
            }
            else if (comboBox2.Text == "Device")
            {
                button_Snap.Enabled = true;
                button_Grab.Enabled = true;
                button_Freeze.Enabled = true;
                button_LoadConfig.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox2_SelectedIndexChanged(null, null);
        }

        private void comBoxTrigMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dalsaConfig.TrigMode = String2TirgMode(comBoxTrigMode.Text);
        }

        private void comBoxTrigMode_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comBoxTrigMode_SelectedIndexChanged(null, null);
        }

        public DalsaConfig Export()
        {
            var num = 0;
            int.TryParse(txtPixel.Text, out num);
            _dalsaConfig.BitsPerPixel = num;

            var width = 0;
            int.TryParse(textBoxWidth.Text, out width);
            _dalsaConfig.Width = width;

            var delay = 0;
            int.TryParse(doBoxDeyT.Text, out delay);
            _dalsaConfig.DelayTime = delay;

            var height = 0;
            int.TryParse(textBoxHeight.Text, out height);
            _dalsaConfig.Height = height;
            return _dalsaConfig;
        }

        private void LoadConfig()
        {
            if (_dalsaConfig != null)
            {
                comboBox2.Text = _dalsaConfig.ImgFrom;

                if (comboBox2.Text == "File")
                {
                    button_Snap.Enabled = false;
                    button_Grab.Enabled = false;
                    button_Freeze.Enabled = false;
                    button_LoadConfig.Enabled = false;
                }
                else if (comboBox2.Text == "Device")
                {
                    button_Snap.Enabled = true;
                    button_Grab.Enabled = true;
                    button_Freeze.Enabled = true;
                    button_LoadConfig.Enabled = true;
                }

                textBoxWidth.Text = _dalsaConfig.Width.ToString();
                textBoxHeight.Text = _dalsaConfig.Height.ToString();
                comBoxTrigMode.Text = TirgMode2String(_dalsaConfig.TrigMode);
                doBoxDeyT.Text = _dalsaConfig.DelayTime.ToString();

                cbFormat.Text = _dalsaConfig.ImgFormat;
                //textBox1.Text = _dalsaConfig.SavePath;
                txtSavepath.Text = _dalsaConfig.SavePath ?? "";
                cbSaveFile.Checked = _dalsaConfig.IsSave;
                txtPixel.Text = _dalsaConfig.BitsPerPixel.ToString();
            }
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
        }

        public void SetDalsaConfig(DalsaConfig dalsaConfig)
        {
            _dalsaConfig = dalsaConfig;
            LoadConfig();
        }

        public void SetDalsaCore(DalsaCore dalsaCore)
        {
            _dalsaCore = dalsaCore;
        }

        private TirgMode String2TirgMode(string str)
        {
            var mode = TirgMode.Other;

            switch (str)
            {
                case "ExtHard":
                    mode = TirgMode.ExtHardWare;
                    break;
                case "SoftCmd":
                    mode = TirgMode.SoftWareCmd;
                    break;
                default:
                    mode = TirgMode.Other;
                    break;
            }

            return mode;
        }

        private string TirgMode2String(TirgMode mode)
        {
            var str = "";

            switch (mode)
            {
                case TirgMode.ExtHardWare:
                    str = "ExtHard";
                    break;
                case TirgMode.SoftWareCmd:
                    str = "SoftCmd";
                    break;
                case TirgMode.Other:
                    str = "";
                    break;
                default:
                    str = "";
                    break;
            }

            return str;
        }

        private void ucDalsaConfig_Load(object sender, EventArgs e)
        {
        }

        public void UpdateControls(bool bAcqNoGrab, bool bAcqGrab, bool bNoGrab, bool online)
        {
            button_Grab.Enabled = bAcqNoGrab && online;
            button_Snap.Enabled = bAcqNoGrab && online;
            button_Freeze.Enabled = bAcqGrab && online;
            button_LoadConfig.Enabled = bAcqNoGrab || bNoGrab;
        }
    }
}