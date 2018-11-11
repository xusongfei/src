using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    public partial class PlatformMoveForm : Form
    {
        public PlatformMoveForm()
        {
            InitializeComponent();

            checkBoxEnable = new[] { checkBoxEnableX, checkBoxEnableY, checkBoxEnableZ, checkBoxEnableU, checkBoxEnableV, checkBoxEnableW };
            textBoxCurPos = new[] { textBoxCurPosX, textBoxCurPosY, textBoxCurPosZ, textBoxCurPosU, textBoxCurPosV, textBoxCurPosW };
            textBoxMovePos = new[] { textBoxMovePosX, textBoxMovePosY, textBoxMovePosZ, textBoxMovePosU, textBoxMovePosV, textBoxMovePosW };

            labelAxis = new[] { labelX, labelY, labelZ, labelU, labelV, labelW };
        }


        public IPlatformPos ObjPos;
        public string MoveMode;
        public PlatformEx Platform;
        public int JumpHeight;


        private CheckBox[] checkBoxEnable;
        private Label[] labelAxis;
        private TextBox[] textBoxCurPos;
        private TextBox[] textBoxMovePos;

        private void PlatformMoveForm_Load(object sender, EventArgs e)
        {
            checkBoxCheckLimit.Checked = true;
            comboBoxMoveMode.Items.AddRange(new[] { "MoveP", "Jump" });
            comboBoxJumpHeight.Items.AddRange(new object[] { 0, -10, -20, -30, -50 });

            if (Platform != null)
            {
                Text = $"{Platform.Name} {Platform.Description}";

                for (var i = 0; i < checkBoxEnable.Length; i++)
                {
                    checkBoxEnable[i].Checked = i < Platform.Axis.Length && Platform[i] != null;
                    checkBoxEnable[i].Enabled = i < Platform.Axis.Length && Platform[i] != null;
                    textBoxCurPos[i].Enabled = i < Platform.Axis.Length && Platform[i] != null;
                    textBoxMovePos[i].Enabled = i < Platform.Axis.Length && Platform[i] != null;
                }

                comboBoxPos.Items.AddRange(Platform.Positions.Select(p => $"{p.Index:D3}-{p.Name}-{p.Description}").ToArray());
                comboBoxPos.SelectedItem = $"{ObjPos.Index:D3}-{ObjPos.Name}-{ObjPos.Description}";

                comboBoxMoveMode.SelectedItem = MoveMode;
                comboBoxJumpHeight.SelectedItem = JumpHeight;

                timer1.Interval = 100;
                timer1.Enabled = true;
            }
        }

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb != null)
            {
                var i = Array.IndexOf(checkBoxEnable, cb);
                if (i >= 0)
                {
                    textBoxMovePos[i].Enabled = cb.Checked;
                    textBoxMovePos[i].Enabled = cb.Checked;
                }
            }
        }

        private void comboBoxPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var pos = Platform.Positions.FirstOrDefault(p => p.Name == comboBoxPos.Text.Split('-')[1]);
                if (pos != null)
                {
                    for (int i = 0; i < pos.Data().Length && i < textBoxMovePos.Length; i++)
                    {
                        textBoxMovePos[i].Text = pos.Data()[i].ToString("F3");
                    }
                }
            }
            catch (Exception)
            {
                for (int i = 0; i < textBoxMovePos.Length; i++)
                {
                    textBoxMovePos[i].Text = "0";
                }
            }
        }

        private void buttonMoveStart_Click(object sender, EventArgs e)
        {
            if (Platform == null)
            {
                return;
            }

            buttonStartMove.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                var pos = Platform.Positions.FirstOrDefault(p => p.Name == comboBoxPos.Text.Split('-')[1]);
                if (pos != null)
                {
                    //get obj pos
                    var curpos = Platform.CurPos;
                    var objpos = textBoxMovePos.Select(t => double.Parse(t.Text)).Take(curpos.Length).ToArray();
                    var moveEnable = checkBoxEnable.Select(c => c.Checked).ToArray();

                    for (int i = 0; i < curpos.Length; i++)
                    {
                        if (!checkBoxEnable[i].Checked)
                        {
                            objpos[i] = curpos[i];
                        }
                    }

                    if (comboBoxMoveMode.Text == "MoveP")
                    {
                        if (MessageBox.Show($"开始 MoveP 运动 {string.Join(", ", curpos.Select(p => p.ToString("F3")))} 到 {string.Join(", ", objpos.Select(p => p.ToString("F3")))} ?",
                                Platform.Name, MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            return;
                        }

                        var ret = Platform.ExitAuto().MoveAbs(moveEnable, objpos, timeout: 10000, checkLimit: checkBoxCheckLimit.Checked);
                        if (!ret)
                        {
                            MessageBox.Show($"MoveP Error:{Platform.ShowStatus()}!", Platform.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (comboBoxMoveMode.Text == "Jump")
                    {
                        var jh = double.Parse(comboBoxJumpHeight.Text);
                        if (MessageBox.Show($"开始 Jump 缩回高度 {jh:F3} 运动 {string.Join(", ", curpos.Select(p => p.ToString("F3")))} 到 {string.Join(", ", objpos.Select(p => p.ToString("F3")))} ?",
                                Platform.Name, MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            return;
                        }

                        var ret = Platform.ExitAuto().Jump(objpos, jh, timeout: 10000, checkLimit: checkBoxCheckLimit.Checked);
                        if (!ret)
                        {
                            MessageBox.Show($"Jump Error:{Platform.ShowStatus()}!", Platform.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"运动模式 Error:{comboBoxMoveMode.Text}!", Platform.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Platform.ShowStatus()}:{ex.Message}");
            }
            finally
            {
                buttonStartMove.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void buttonMoveStop_Click(object sender, EventArgs e)
        {
            if (Platform == null)
            {
                return;
            }

            Platform.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Platform != null)
            {
                var curpos = Platform.CurPos;
                for (int i = 0; i < curpos.Length && i < textBoxCurPos.Length; i++)
                {
                    labelAxis[i].BackColor = Platform[i] != null && Platform[i].GetServo() ? Color.Lime : Color.White;

                    textBoxCurPos[i].Text = curpos[i].ToString("F3");
                    textBoxCurPos[i].BackColor = Platform[i] != null && (Platform[i].GetMel() || Platform[i].GetPel()) ? Color.Red : Color.White;
                }
            }
        }
    }
}