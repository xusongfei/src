using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.userControls;

namespace Lead.Detect.FrameworkExtension.platforms.motionPlatforms
{
    public partial class PlatformControl : UserControl, IFrameworkControl
    {
        public PlatformControl()
        {
            InitializeComponent();


            buttonMove_Plus = new[] { buttonX_Plus, buttonY_Plus, buttonZ_Plus, buttonU_Plus, buttonV_Plus, buttonW_Plus, };
            buttonMove_Minus = new[] { buttonX_Minus, buttonY_Minus, buttonZ_Minus, buttonU_Minus, buttonV_Minus, buttonW_Minus, };

            textBox_Pos = new[] { textBoxXPos, textBoxYPos, textBoxZPos, textBoxUPos, textBoxVPos, textBoxWPos, };
            comboBox_Step = new[] { comboBoxStepX, comboBoxStepY, comboBoxStepZ, comboBoxStepU, comboBoxStepV, comboBoxStepW };
            textBox_Vel = new[] { textBoxXVel, textBoxYVel, textBoxZVel, textBoxUVel, textBoxVVel, textBoxWVel, };
            textBox_Acc = new[] { textBoxXAcc, textBoxYAcc, textBoxZAcc, textBoxUAcc, textBoxVAcc, textBoxWAcc, };

            textBox_Mel = new[] { textBoxX_Mel, textBoxY_Mel, textBoxZ_Mel, textBoxU_Mel, textBoxV_Mel, textBoxW_Mel, };
            textBox_Pel = new[] { textBoxX_Pel, textBoxY_Pel, textBoxZ_Pel, textBoxU_Pel, textBoxV_Pel, textBoxW_Pel, };
            textBox_Org = new[] { textBoxX_Org, textBoxY_Org, textBoxZ_Org, textBoxU_Org, textBoxV_Org, textBoxW_Org, };

            button_Servo = new[] { buttonServoX, buttonServoY, buttonServoZ, buttonServoU, buttonServoV, buttonServoW };
            button_Home = new Button[] { buttonHomeX, buttonHomeY, buttonHomeZ, buttonHomeU, buttonHomeV, buttonHomeW };
            comboBox_HomeOrder = new[] { comboBoxHomeX, comboBoxHomeY, comboBoxHomeZ, comboBoxHomeU, comboBoxHomeV, comboBoxHomeW };


            buttonMovePlusName = new[] { "X+", "Y+", "Z+", "U+", "V+", "W+" };
            buttonMoveMinusName = new[] { "X-", "Y-", "Z-", "U-", "V-", "W-" };


            radioButton_StepDistance = new[] { radioButtonLong, radioButtonMedium, radioButtonShort };
        }

        private Button[] buttonMove_Plus;
        private Button[] buttonMove_Minus;
        private string[] buttonMovePlusName;
        private string[] buttonMoveMinusName;

        private TextBox[] textBox_Pos;
        private ComboBox[] comboBox_Step;
        private TextBox[] textBox_Vel;
        private TextBox[] textBox_Acc;
        private TextBox[] textBox_Mel;
        private TextBox[] textBox_Pel;
        private TextBox[] textBox_Org;

        private Button[] button_Servo;
        private Button[] button_Home;
        private ComboBox[] comboBox_HomeOrder;

        private RadioButton[] radioButton_StepDistance;

        private PlatformEx _platform;

        public void LoadPlatform(PlatformEx platform)
        {
            if (platform == null)
            {
                Visible = false;
                return;
            }

            if (_platform == platform)
            {
                return;
            }
            _platform = platform;
            Visible = true;

            LoadManualPanel();
            LoadTeachPanel();
            LoadPositionPanel();
            LoadSetttingsPanel();

            timer1.Interval = 100;
            timer1.Start();
        }

        private void LoadManualPanel()
        {
            if (_platform == null)
            {
                return;
            }

            //load steps
            for (var i = 0; i < comboBox_Step.Length; i++)
            {
                var cb = comboBox_Step[i];
                if (i < _platform.Axis.Length && _platform.Axis[i] != null)
                {
                    cb.Items.Clear();
                    cb.Items.AddRange(new object[] { 0.02, 0.1, 0.5, 1, 5, 10, 20, 30, 40, 50, 100, 150 });
                    cb.SelectedItem = 10;

                    buttonMove_Plus[i].BackColor = Color.White;
                    buttonMove_Minus[i].BackColor = Color.White;
                }
                else
                {
                    cb.Enabled = false;

                    buttonMove_Plus[i].Enabled = false;
                    buttonMove_Minus[i].Enabled = false;

                    textBox_Pos[i].Text = string.Empty;
                    textBox_Pos[i].Enabled = false;
                    textBox_Vel[i].Text = string.Empty;
                    textBox_Vel[i].Enabled = false;
                    textBox_Acc[i].Text = string.Empty;
                    textBox_Acc[i].Enabled = false;
                    textBox_Mel[i].Enabled = false;
                    textBox_Pel[i].Enabled = false;
                    textBox_Org[i].Enabled = false;
                }
            }

            radioButtonLong.Checked = true;

            //load home order
            for (var i = 0; i < comboBox_HomeOrder.Length; i++)
            {
                var cb = comboBox_HomeOrder[i];
                if (i < _platform.Axis.Length && _platform.Axis[i] != null)
                {
                    cb.Items.Clear();
                    cb.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5 });
                    cb.SelectedItem = _platform.Axis[i].HomeOrder;

                    button_Home[i].BackColor = Color.White;
                    button_Servo[i].BackColor = Color.White;
                }
                else
                {
                    cb.Enabled = false;
                    button_Home[i].Enabled = false;
                    button_Servo[i].Enabled = false;
                }
            }
        }


        private void LoadTeachPanel(string lastpos = null)
        {
            if (_platform == null)
            {
                return;
            }

            var pos = _platform.Positions.Select(p => $"{p.Index:D3}-{p.Name}-{p.Description}").ToArray();

            //teach pos
            comboBoxTeachPos.Items.Clear();
            comboBoxTeachPos.Items.AddRange(pos);
            comboBoxTeachPos.SelectedItem = lastpos;

            //test pos
            comboBoxTestPos.Items.Clear();
            comboBoxTestPos.Items.AddRange(pos);
            comboBoxTestPos.SelectedItem = lastpos;

            //test jump height
            comboBoxJumpHeight.Items.Clear();
            comboBoxJumpHeight.Items.AddRange(new object[] { 0, -5, -10, -20, -30, -50 });
            comboBoxJumpHeight.SelectedItem = -10;
        }

        private void LoadPositionPanel()
        {
            dataGridViewPositions.AllowUserToOrderColumns = false;
            dataGridViewPositions.Rows.Clear();
            dataGridViewPositions.Columns.Clear();

            if (_platform == null)
            {
                return;
            }

            var prop = _platform.PosType.GetProperties();
            foreach (var p in prop)
            {
                var col = dataGridViewPositions.Columns.Add(p.Name, p.Name);
                dataGridViewPositions.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            foreach (var p in _platform.Positions)
            {
                var r = dataGridViewPositions.Rows.Add(1);

                for (var index = 0; index < prop.Length; index++)
                {
                    dataGridViewPositions.Rows[r].Cells[index].Value = prop[index].GetValue(p);
                }
            }
        }

        private void LoadSetttingsPanel()
        {
            tabControlSettings.TabPages.Clear();

            if (_platform == null)
            {
                return;
            }

            foreach (var axis in _platform.Axis)
            {
                if (axis == null)
                {
                    continue;
                }

                tabControlSettings.TabPages.Add(new TabPage()
                {
                    Name = axis.Name,
                    Dock = DockStyle.Fill,
                    Text = axis.Name,
                });

                tabControlSettings.TabPages[axis.Name].Controls.Add(new PropertyGrid()
                {
                    Dock = DockStyle.Fill,
                    SelectedObject = axis,
                });
            }


            for (int i = 0; i < _platform.SafeChecks.Count; i++)
            {
                if (_platform.SafeChecks[i] == null)
                {
                    continue;
                }

                tabControlSettings.TabPages.Add(new TabPage()
                {
                    Name = $"SafeCheck{i}",
                    Dock = DockStyle.Fill,
                    Text = _platform.SafeChecks[i].ToString(),
                });

                tabControlSettings.TabPages[$"SafeCheck{i}"].Controls.Add(new PropertyGrid()
                {
                    Dock = DockStyle.Fill,
                    SelectedObject = _platform.SafeChecks[i],
                });
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _platform.Axis.Length; i++)
            {
                var axis = _platform.Axis[i];
                if (axis != null)
                {
                    textBox_Pos[i].Text = axis.GetPos().ToString("F3");
                    textBox_Pos[i].BackColor = axis.GetMdn() ? Color.White : Color.Lime;
                    textBox_Vel[i].Text = axis.AxisSpeed.ToString("F2");
                    textBox_Acc[i].Text = axis.AxisAcc.ToString("F2");

                    if (axis.GetAlarm())
                    {
                        button_Servo[i].BackColor = Color.Red;
                        buttonMove_Plus[i].BackColor = Color.Red;
                        buttonMove_Minus[i].BackColor = Color.Red;
                    }
                    else
                    {
                        button_Servo[i].BackColor = axis.GetServo() ? Color.Lime : Color.White;
                        buttonMove_Plus[i].BackColor = axis.GetServo() ? Color.White : Color.LightGray;
                        buttonMove_Minus[i].BackColor = axis.GetServo() ? Color.White : Color.LightGray;
                    }

                    textBox_Mel[i].BackColor = axis.GetMel() ? Color.Red : Color.White;
                    textBox_Pel[i].BackColor = axis.GetPel() ? Color.Red : Color.White;
                    textBox_Org[i].BackColor = axis.GetOrg() ? Color.Lime : Color.White;
                }
            }
        }


        #region  manual panel

        private void comboBoxPlatformDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonManual_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            try
            {
                if (btn != null)
                {
                    btn.Enabled = false;
                    Cursor = Cursors.WaitCursor;
                    if (btn.Text.Contains("+"))
                    {
                        var i = Array.IndexOf(buttonMovePlusName, btn.Text);
                        if (i >= 0 && _platform.Axis[i] != null)
                        {
                            var axis = _platform.Axis[i];
                            var step = double.Parse(comboBox_Step[i].Text);
                            bool isCheckLimit = true;

                            if (step > 10 || axis.GetMel() || axis.GetPel())
                            {
                                if (MessageBox.Show($"{axis.Name} 开始移动 {step} mm  正限位 {axis.GetPel()} 负限位 {axis.GetMel()}?",
                                        "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    return;
                                }

                                isCheckLimit = false;
                            }

                            var ret = _platform.ExitAuto().MoveRel(i, step, 10000, isCheckLimit);
                            if (!ret)
                            {
                                MessageBox.Show($"{axis.Name} 移动异常: {_platform.ShowStatus()} {axis.Error}！");
                            }
                        }
                    }
                    else if (btn.Text.Contains("-"))
                    {
                        var i = Array.IndexOf(buttonMoveMinusName, btn.Text);
                        if (i >= 0 && _platform.Axis[i] != null)
                        {
                            var axis = _platform.Axis[i];
                            var step = double.Parse(comboBox_Step[i].Text);
                            var isCheckLimit = true;

                            if (step > 10 || axis.GetMel() || axis.GetPel())
                            {
                                if (MessageBox.Show($"{axis.Name} 开始移动 -{step} mm  正限位 {axis.GetPel()} 负限位 {axis.GetMel()}?",
                                        "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    return;
                                }
                                isCheckLimit = false;
                            }

                            var ret = _platform.ExitAuto().MoveRel(i, -step, 10000, isCheckLimit);
                            if (!ret)
                            {
                                MessageBox.Show($"{axis.Name} 移动异常: {_platform.ShowStatus()} {axis.Error}！");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Move Error:{ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
                if (btn != null) btn.Enabled = true;
            }
        }


        private void buttonAxisStopAll_Click(object sender, EventArgs e)
        {
            _platform.Stop();
        }

        private void buttonServo_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var i = Array.IndexOf(button_Servo, btn);
                if (i >= 0 && _platform.Axis[i] != null)
                {
                    var axis = _platform.Axis[i];
                    var ret = true;
                    _platform.ExitAuto().Servo(i, !axis.GetServo());

                    MessageBox.Show($"{axis.Name} 使能 {axis.GetServo()} {(ret ? "成功" : "异常")}");
                }
            }
        }

        private void buttonAxisServo_Click(object sender, EventArgs e)
        {
            _platform.ExitAuto().Servo(-1, true);
        }

        private void buttonAxisServoOff_Click(object sender, EventArgs e)
        {
            _platform.ExitAuto().Servo(-1, false);
        }

        private void comboBoxHomeOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb != null)
            {
                var i = Array.IndexOf(comboBox_HomeOrder, cb);
                if (i >= 0 && _platform.Axis[i] != null)
                {
                    var axis = _platform.Axis[i];
                    axis.HomeOrder = int.Parse(cb.Text);
                }
            }
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var i = Array.IndexOf(button_Home, btn);
                if (i >= 0 && _platform.Axis[i] != null)
                {
                    var axis = _platform.Axis[i];
                    if (MessageBox.Show($"{axis.Name} 开始回原点?", "回原点", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    var ret = _platform.ExitAuto().Home(i, timeout: 30000);
                    MessageBox.Show($"{axis.Name} {i} 回原点 {(ret ? "成功" : $"异常 {_platform.ShowStatus()}")}");
                }
            }
        }

        private void buttonAxisHomeAll_Click(object sender, EventArgs e)
        {
            var msg = string.Join(",", _platform.Axis.ToList().FindAll(a => a != null).OrderBy(a => a.HomeOrder).Select(a => a.Name));

            if (MessageBox.Show($"{msg} 开始回原点?", "回原点", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            try
            {
                var ret = _platform.ExitAuto().Home(timeout: 10000);

                MessageBox.Show($"{msg} 回原点 {(ret ? "成功" : $"异常 {_platform.ShowStatus()}")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        private void radioButtonDistance_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb != null)
            {
                var i = Array.IndexOf(radioButton_StepDistance, rb);
                if (i >= 0)
                {
                    for (int j = 0; j < comboBox_Step.Length; j++)
                    {
                        if (i == 0)
                        {
                            comboBox_Step[j].SelectedItem = 10;
                        }
                        else if (i == 1)
                        {
                            comboBox_Step[j].SelectedItem = 1;
                        }
                        else if (i == 2)
                        {
                            comboBox_Step[j].SelectedItem = 0.1;
                        }
                    }
                }
            }
        }

        private void comboBoxStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb != null)
            {
                cb.Focus();
            }
        }

        #endregion

        #region teach panel

        private void buttonTeachPos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTeachPos.Text) || !comboBoxTeachPos.Text.Contains("-"))
            {
                MessageBox.Show("示教 点位名称异常");
                return;
            }

            try
            {
                var posData = comboBoxTeachPos.Text.Split('-');
                var index = 0;
                var name = string.Empty;
                var desc = string.Empty;

                if (posData.Length == 1)
                {
                    name = posData[0];
                }
                else if (posData.Length == 2)
                {
                    index = int.Parse(posData[0]);
                    name = posData[1];
                }
                else if (posData.Length >= 3)
                {
                    index = int.Parse(posData[0]);
                    name = posData[1];
                    desc = posData[2];
                }



                if (_platform.Positions.Exists(p => p.Name == name))
                {
                    var lastpos = _platform.Positions.First(p => p.Name == name);

                    if (MessageBox.Show($"更新点 {index} {name} {desc} : {string.Join(",", lastpos.Data().Select(v => v.ToString("F3")))} 为 {string.Join(",", _platform.CurPos.Select(v => v.ToString("F3")))}?",
                            "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show($"示教点 {index} {name} {desc} : {string.Join(",", _platform.CurPos.Select(v => v.ToString("F3")))}?",
                            "警告", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                _platform.TeachPos(name);
                LoadTeachPanel(name);
                LoadPositionPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"示教点异常：{ex.Message}");
                return;
            }
        }


        private void buttonDeletePos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTeachPos.Text) || !comboBoxTeachPos.Text.Contains("-"))
            {
                MessageBox.Show("示教 点位名称异常");
                return;
            }


            try
            {

                var index = int.Parse(comboBoxTeachPos.Text.Split('-')[0]);
                var pos = comboBoxTeachPos.Text.Split('-')[1];

                if (_platform.Positions.Exists(p => p.Name == pos))
                {
                    _platform.Positions.RemoveAll(p => p.Name == pos);
                }

                LoadPositionPanel();

                LoadTeachPanel(pos);

                comboBoxTeachPos.Text = string.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"示教点异常：{ex.Message}");
                return;
            }
        }

        private void buttonMovePos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTestPos.Text) || !comboBoxTestPos.Text.Contains("-"))
            {
                MessageBox.Show("绝对运动 点位名称异常");
                return;
            }


            var pos = comboBoxTestPos.Text.Split('-')[1];
            if (_platform.Positions.Exists(p => p.Name == pos))
            {
                var moveForm = new PlatformMoveForm()
                {
                    Platform = _platform,
                    ObjPos = _platform.Positions.First(p => p.Name == pos),
                    MoveMode = "MoveP",
                    JumpHeight = 0,

                    StartPosition = FormStartPosition.CenterParent,
                };
                moveForm.ShowDialog();

                //
                return;
                //try
                //{
                //    var lastpos = _platform.Positions.First(p => p.Name == pos);
                //    if (MessageBox.Show($"移动到 {pos} {lastpos}?",
                //            "绝对运动", MessageBoxButtons.YesNo) == DialogResult.No)
                //    {
                //        return;
                //    }

                //    var ret = _platform.ExitAuto().MoveAbs(pos);
                //    if (!ret)
                //    {
                //        MessageBox.Show($"绝对运动异常");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"绝对运动异常:{ex.Message}");
                //}
            }
        }

        private void buttonJumpPos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxTestPos.Text) || !comboBoxTestPos.Text.Contains("-"))
            {
                MessageBox.Show("JUMP移动异常:点位名称异常");
                return;
            }

            var pos = comboBoxTestPos.Text.Split('-')[1];
            if (_platform.Positions.Exists(p => p.Name == pos))
            {
                try
                {
                    var lastpos = _platform.Positions.First(p => p.Name == pos);

                    var jh = double.Parse(comboBoxJumpHeight.Text);

                    var moveForm = new PlatformMoveForm()
                    {
                        Platform = _platform,
                        ObjPos = _platform.Positions.First(p => p.Name == pos),
                        MoveMode = "Jump",
                        JumpHeight = (int)jh,

                        StartPosition = FormStartPosition.CenterParent,
                    };
                    moveForm.ShowDialog();

                    return;
                    //
                    //if (MessageBox.Show($"JUMP移动到 {pos} {lastpos} 缩回高度 {jh:F2}?",
                    //        "JUMP移动", MessageBoxButtons.YesNo) == DialogResult.No)
                    //{
                    //    return;
                    //}

                    //var ret = _platform.ExitAuto().Jump(pos, double.Parse(comboBoxJumpHeight.Text));
                    //if (!ret)
                    //{
                    //    MessageBox.Show($"JUMP移动异常");
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"JUMP移动异常:{ex.Message}");
                }
            }
        }

        #endregion

        #region  position panel

        private void buttonReloadPosition_Click(object sender, EventArgs e)
        {
            _platform.Load();
            LoadPositionPanel();
        }

        private void buttonSavePosition_Click(object sender, EventArgs e)
        {
            SavePositionPanel();

            LoadTeachPanel();
        }

        private void SavePositionPanel()
        {
            try
            {
                var prop = _platform.PosType.GetProperties();
                for (var i = 0; i < _platform.Positions.Count; i++)
                {
                    var pos = _platform.Positions[i];
                    var row = dataGridViewPositions.Rows[i];
                    for (var index = 0; index < prop.Length; index++)
                    {
                        prop[index].SetValue(pos, row.Cells[index].Value);
                    }
                }

                _platform.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存点位异常:{ex.Message}");
            }
        }

        private void dataGridViewPositions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridViewPositions.RowCount
                && e.RowIndex >= 0
                && e.ColumnIndex < dataGridViewPositions.ColumnCount
                && e.ColumnIndex >= 0)
            {
                var prop = _platform.PosType.GetProperties();

                var cell = dataGridViewPositions.Rows[e.RowIndex].Cells[e.ColumnIndex];

                try
                {
                    if (prop[e.ColumnIndex].PropertyType.Name == "Double")
                    {
                        cell.Value = double.Parse(cell.Value.ToString());
                    }
                    else if (prop[e.ColumnIndex].PropertyType.Name == "Int32")
                    {
                        cell.Value = int.Parse(cell.Value.ToString());
                    }
                    else if (prop[e.ColumnIndex].PropertyType.Name == "Boolean")
                    {
                        cell.Value = bool.Parse(cell.Value.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"编辑异常 行 {e.RowIndex} 列 {e.ColumnIndex}:{ex.Message}");
                }
            }
        }

        #endregion

        public void LoadFramework()
        {

        }

        public void FrameworkActivate()
        {

        }

        public void FrameworkDeactivate()
        {

        }

        public void FrameworkUpdate()
        {

        }

        private void buttonRefreshPosition_Click(object sender, EventArgs e)
        {
            LoadPositionPanel();
        }


    }
}