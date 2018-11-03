using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Project
{
    public partial class MeasureProjectPosEditor : Form
    {
        public List<PlatformEx> Platforms;

        public List<IPlatformPos> TestPos;


        public Type MeasureProjectType;


        public MeasureProjectPosEditor()
        {
            InitializeComponent();
        }

        private void MeasureProjectPosEditor_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;


            comboBoxJump.Items.AddRange(new object[] { 0, -10, -20, -50, -80 });
            comboBoxJump.SelectedIndex = 0;

            if (Platforms != null)
            {
                comboBoxMovePlatform.Items.Clear();
                comboBoxMovePlatform.Items.AddRange(Platforms.Select(p => (object)p.Name).ToArray());

                comboBoxCurPlatform.Items.Clear();
                comboBoxCurPlatform.Items.AddRange(Platforms.Select(p => (object)p.Name).ToArray());
            }


            platformControl1.LoadPlatform(null);


            LoadTestPos();
        }

        private void LoadTestPos()
        {
            if (TestPos != null)
            {
                comboBoxTestPos.Items.Clear();
                comboBoxTestPos.Items.AddRange(TestPos.Select(p => (object)p.ToString()).ToArray());

                listBoxTestPos.Items.Clear();
                listBoxTestPos.Items.AddRange(TestPos.Select(p => (object)p.ToString()).ToArray());

                richTextBoxPosData.Clear();
                foreach (var p in TestPos)
                {
                    richTextBoxPosData.AppendText($"{p.ToString()}\r\n");
                }
            }
        }

        #region pos file operation

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog()
            {
                InitialDirectory = @".\Config",
                Filter = "(Test Points)|*.pts",
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllLines(fd.FileName);

                var loadpos = new List<IPlatformPos>();
                foreach (var d in data)
                {
                    if (d.StartsWith("//") || d.StartsWith("\\"))
                    {
                        continue;
                    }

                    var vals = d.Split(new[] { ',', ' ' });
                    if (vals.Length == 3)
                    {
                        loadpos.Add(new PosXYZ(double.Parse(vals[0]), double.Parse(vals[1]), double.Parse(vals[2])));
                    }
                    else if (vals.Length == 4)
                    {
                        loadpos.Add(new PosXYZU(double.Parse(vals[0]), double.Parse(vals[1]), double.Parse(vals[2]), double.Parse(vals[3])));
                    }
                }

                TestPos = loadpos;

                LoadTestPos();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new SaveFileDialog()
            {
                InitialDirectory = @".\Config",
                Filter = "(Test Points)|*.pts",
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                using (var fs = new FileStream(Path.Combine(fd.FileName), FileMode.Create))
                {
                    foreach (var pos in TestPos)
                    {
                        var buffer = Encoding.ASCII.GetBytes($"{string.Join(",", pos.Data().Select(d => d.ToString("F3")))}\r\n");
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        #endregion


        #region import pos

        private void comboBoxCurPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCurPosType.Items.Clear();
            var platform = Platforms.FirstOrDefault(p => p.Name == comboBoxCurPlatform.Text);
            if (platform == null)
            {
                return;
            }

            comboBoxCurPosType.Items.Add(string.Empty);
            foreach (var posConvertFunc in platform.PosConvertFuncs)
            {
                comboBoxCurPosType.Items.Add(posConvertFunc.Key);
            }

            comboBoxCurPosType.SelectedIndex = comboBoxCurPosType.Items.Count > 0 ? 0 : -1;
        }

        private void buttonCurPlatformMove_Click(object sender, EventArgs e)
        {
            var platform = Platforms.FirstOrDefault(p => p.Name == comboBoxCurPlatform.Text);
            if (platform == null)
            {
                platformControl1.LoadPlatform(null);
                tabPageMove.Text = $"移动";
                return;
            }

            platformControl1.LoadPlatform(platform);
            tabControlMain.SelectedTab = tabPageMove;
            tabPageMove.Text = $"移动 {platform.Name} {platform.Description}";
        }

        private void buttonImportAddCurPos_Click(object sender, EventArgs e)
        {
            var platform = Platforms.FirstOrDefault(p => p.Name == comboBoxCurPlatform.Text);
            if (platform == null)
            {
                return;
            }

            var data = platform.GetPos(comboBoxCurPosType.Text);
            if (data == null)
            {
                MessageBox.Show($"Get Cur Pos Convert Error: Platform GetPos Fail {comboBoxCurPosType.Text}");
                return;
            }

            if (platform is PlatformXyz)
            {
                var pos = new PosXYZ(data);
                richTextBoxTestPos.Text += $"{pos.X:F2} {pos.Y:F2} {pos.Z:F2}\r\n";
            }
            else if (platform is PlatformXyzu)
            {
                var pos = new PosXYZU(data);
                richTextBoxTestPos.Text += $"{pos.X:F2} {pos.Y:F2} {pos.Z:F2} {pos.U:F2}\r\n";
            }
            else
            {
                MessageBox.Show($"Get Cur Pos Convert Error : {platform.GetType()} Error");
                return;
            }
        }

        private void buttonImportTestPos_Click(object sender, EventArgs e)
        {
            var data = richTextBoxTestPos.Lines;

            var loadpos = new List<IPlatformPos>();

            try
            {
                foreach (var d in data)
                {
                    if (d.StartsWith("//") || d.StartsWith(@"\\") || string.IsNullOrEmpty(d))
                    {
                        continue;
                    }

                    var vals = d.Split(new[] { ',', ' ' });
                    if (vals.Length == 3)
                    {
                        loadpos.Add(new PosXYZ(double.Parse(vals[0]), double.Parse(vals[1]), double.Parse(vals[2])));
                    }
                    else if (vals.Length == 4)
                    {
                        loadpos.Add(new PosXYZU(double.Parse(vals[0]), double.Parse(vals[1]), double.Parse(vals[2]), double.Parse(vals[3])));
                    }
                    else if (vals.Length == 5)
                    {
                        loadpos.Add(new PosXYZ(double.Parse(vals[1]), double.Parse(vals[2]), double.Parse(vals[3]))
                        {
                            Name = vals[0],
                            Description = vals[4],
                        });
                    }
                    else if (vals.Length == 6)
                    {
                        loadpos.Add(new PosXYZU(double.Parse(vals[1]), double.Parse(vals[2]), double.Parse(vals[3]), double.Parse(vals[4]))
                        {
                            Name = vals[0],
                            Description = vals[5],
                        });
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"点位数据格式异常 (x y z) (x y z u) or (name x y z desc) (name x y z u desc)");
                return;
            }

            if (MessageBox.Show($"加载点位:\r\n{string.Join("\r\n", loadpos.Select(p => p.ToString()))}", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TestPos.AddRange(loadpos);
                LoadTestPos();
            }
        }


        private void buttonCreateArrayPos_Click(object sender, EventArgs e)
        {

            var loadpos = new List<IPlatformPos>();

            try
            {
                var startX = double.Parse(textBoxArrayStartX.Text);
                var startY = double.Parse(textBoxArrayStartY.Text);
                var stepX = double.Parse(textBoxArrayXStep.Text);
                var stepY = double.Parse(textBoxArrayYStep.Text);
                var countX = int.Parse(textBoxArrayXCount.Text);
                var countY = int.Parse(textBoxArrayYCount.Text);

                //row
                for (int i = 0; i < countX; i++)
                {
                    //col
                    for (int j = 0; j < countY; j++)
                    {
                        if (TestPos.Count == 0)
                        {
                            loadpos.Add(new PosXYZ(startX + j * stepX, startY + i * stepY, 0));
                        }
                        else if (TestPos.First().GetType() == typeof(PosXYZ))
                        {
                            loadpos.Add(new PosXYZ(startX + j * stepX, startY + i * stepY, 0));
                        }
                        else if (TestPos.First().GetType() == typeof(PosXYZU))
                        {
                            loadpos.Add(new PosXYZU(startX + j * stepX, startY + i * stepY, 0));
                        }
                        else if (TestPos.First().GetType() == typeof(PosXYZUVW))
                        {
                            loadpos.Add(new PosXYZUVW(startX + j * stepX, startY + i * stepY, 0));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            if (MessageBox.Show($"加载点位:\r\n{string.Join("\r\n", loadpos.Select(p => p.ToString()))}", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TestPos.AddRange(loadpos);
                LoadTestPos();
            }
        }

        #endregion


        #region test pos

        private void comboBoxMovePlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMovePosType.Items.Clear();
            var platform = Platforms.FirstOrDefault(p => p.Name == comboBoxMovePlatform.Text);
            if (platform == null)
            {
                return;
            }

            comboBoxMovePosType.Items.Add(string.Empty);
            foreach (var posConvertFunc in platform.PosConvertFuncs)
            {
                comboBoxMovePosType.Items.Add(posConvertFunc.Key);
            }

            comboBoxMovePosType.SelectedIndex = comboBoxMovePosType.Items.Count > 0 ? 0 : -1;
        }

        private void buttonMoveToTestPos_Click(object sender, EventArgs e)
        {
            try
            {
                buttonMoveToTestPos.Enabled = false;
                Cursor = Cursors.WaitCursor;


                var platform = Platforms.FirstOrDefault(p => p.Name == comboBoxMovePlatform.Text);
                if (platform == null)
                {
                    return;
                }


                var data = comboBoxTestPos.Text.Split(',');
                if (data.Length >= 5)
                {
                    double[] pos = null;

                    if (platform is PlatformXyz)
                    {
                        pos = new double[] { double.Parse(data[2]), double.Parse(data[3]), double.Parse(data[4]) };
                    }
                    else if (platform is PlatformXyzu)
                    {
                        pos = new double[] { double.Parse(data[2]), double.Parse(data[3]), double.Parse(data[4]), double.Parse(data[5]) };
                    }
                    else
                    {
                        throw new Exception($"Move Pos Convert Error : {comboBoxMovePosType.Text}");
                    }

                    pos = platform.GetPos(comboBoxMovePosType.Text, pos);
                    if (pos == null)
                    {
                        throw new Exception($"Move Pos Convert Error : {comboBoxMovePosType.Text}");
                    }

                    if (MessageBox.Show($"{platform.Name}:{string.Join(",", platform.Axis.Select(a => a == null ? "NULL" : a.Name).ToArray())} Jump To {string.Join(",", pos.Select(p => p.ToString("F2")))}?",
                            "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var ret = platform.ExitAuto().Jump(pos, double.Parse(comboBoxJump.Text), timeout: 10000, checkLimit: false);
                        if (!ret)
                        {
                            var error = !ret ? $"error {platform.ShowStatus()}" : "normal";
                            MessageBox.Show($"{platform.Name} Jump {string.Join(",", pos.Select(p => p.ToString("F2")))} {error} Finish");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Platform Jump Test Error: {ex.Message}");
            }
            finally
            {
                buttonMoveToTestPos.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        #endregion


        #region pos view

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清除所有点位？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                TestPos.Clear();
                LoadTestPos();
            }
        }

        private void buttonUpdatePosData_Click(object sender, EventArgs e)
        {
            var newPos = new List<IPlatformPos>();
            var data = richTextBoxPosData.Lines;
            var line = string.Empty;

            if (TestPos.Count > 0 && TestPos.First().GetType() == typeof(PosXYZ))
            {
                try
                {
                    foreach (var l in data)
                    {
                        if (string.IsNullOrEmpty(l))
                        {
                            continue;
                        }
                        line = l;
                        newPos.Add(PosXYZ.Create(l));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PosXYZ点位数据异常： {line} {ex.Message}");
                    return;
                }
            }
            else if (TestPos.Count > 0 && TestPos.First().GetType() == typeof(PosXYZU))
            {
                try
                {
                    foreach (var l in data)
                    {
                        if (string.IsNullOrEmpty(l))
                        {
                            continue;
                        }
                        line = l;
                        newPos.Add(PosXYZU.Create(l));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PosXYZU点位数据异常： {line} {ex.Message}");
                    return;
                }
            }
            else if (TestPos.Count > 0 && TestPos.First().GetType() == typeof(PosXYZUVW))
            {
                try
                {
                    foreach (var l in data)
                    {
                        if (string.IsNullOrEmpty(l))
                        {
                            continue;
                        }
                        line = l;
                        newPos.Add(PosXYZUVW.Create(l));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PosXYZUVW点位数据异常： {line} {ex.Message}");
                    return;
                }
            }
            else
            {
                return;
            }


            if (MessageBox.Show($"是否更新点位为：\r\n{string.Join("\r\n", newPos.Select(p => p.ToString()))} ", "更新点位数据", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            try
            {
                TestPos.Clear();
                TestPos.AddRange(newPos);

                LoadTestPos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新点位数据异常:{ex.Message}");
            }
        }

        #endregion

        private void 排序优化所有点位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MeasureProjectType != null)
            {
                if (MeasureProjectType == typeof(MeasureProject1))
                {
                    TestPos = new MeasureProject1OrderOptimizeMethod().Optimize(TestPos.Cast<PosXYZ>().ToList()).Cast<IPlatformPos>().ToList();

                    MessageBox.Show("排序优化完成！");
                }
            }

            LoadTestPos();
        }

        private void buttonViewConvertPos_Click(object sender, EventArgs e)
        {

            var platform = Platforms.FirstOrDefault(p => p.Name == comboBoxMovePlatform.Text);
            if (platform == null)
            {
                return;
            }


            var newPos = new List<IPlatformPos>();

            foreach (var pos in TestPos)
            {
                if (platform is PlatformXyz)
                {
                    newPos.Add(new PosXYZ(platform.GetPos(comboBoxMovePosType.Text, pos.Data())));
                }
                else if (platform is PlatformXyzu)
                {
                    newPos.Add(new PosXYZU(platform.GetPos(comboBoxMovePosType.Text, pos.Data())));
                }
                else if (platform is PlatformXyzuvw)
                {
                    newPos.Add(new PosXYZUVW(platform.GetPos(comboBoxMovePosType.Text, pos.Data())));
                }
            }


            var posViewer = new PlatformPosViewer();
            posViewer.Positions = newPos;
            posViewer.ShowDialog();
        }
    }
}
