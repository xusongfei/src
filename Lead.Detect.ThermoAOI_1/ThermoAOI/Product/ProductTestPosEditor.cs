using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOI.Calibration;
using Lead.Detect.ThermoAOI.Machine.Common;

namespace Lead.Detect.ThermoAOI.Product
{
    public partial class ProductTestPosEditor : Form
    {

        public PlatformEx Platform;

        public List<PosXYZ> TestPos;

        public ProductTestPosEditor()
        {
            InitializeComponent();
        }

        private void ProductTestPosEditor_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            comboBoxPlatform.Items.Add(PlatformType.LUp);
            comboBoxPlatform.Items.Add(PlatformType.LDown);
            comboBoxPlatform.Items.Add(PlatformType.RUp);
            comboBoxPlatform.Items.Add(PlatformType.RDown);

            comboBoxJump.Items.AddRange(new object[] { 0, -10, -20, -50, -80 });
            comboBoxJump.SelectedIndex = 0;


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
            }
        }

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

                var loadpos = new List<PosXYZ>();
                foreach (var d in data)
                {
                    if (d.StartsWith("//") || d.StartsWith("\\"))
                    {
                        continue;
                    }

                    var vals = d.Split(new[] { ',', ' ' });
                    if (vals.Length >= 2)
                    {
                        loadpos.Add(new PosXYZ(double.Parse(vals[0]), double.Parse(vals[1]), 0));
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
                        var buffer = Encoding.ASCII.GetBytes($"{pos.X:F2} {pos.Y:F2}\r\n");
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }

        }

        private void buttonImportTestPos_Click(object sender, EventArgs e)
        {
            var data = richTextBoxTestPos.Lines;

            var loadpos = new List<PosXYZ>();

            try
            {
                foreach (var d in data)
                {
                    if (d.StartsWith("//") || d.StartsWith(@"\\"))
                    {
                        continue;
                    }

                    var vals = d.Split(new[] { ',', ' ' });
                    if (vals.Length == 3)
                    {
                        loadpos.Add(new PosXYZ(double.Parse(vals[0]), double.Parse(vals[1]), double.Parse(vals[2])));
                    }
                    else if (vals.Length == 5)
                    {
                        loadpos.Add(new PosXYZ(double.Parse(vals[1]), double.Parse(vals[2]), double.Parse(vals[3]))
                        {
                            Name = vals[0],
                            Description = vals[4],
                        });
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show($"点位数据格式异常 (x y z) or (name x y z desc)");
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
            var loadpos = new List<PosXYZ>();
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
                        loadpos.Add(new PosXYZ(startX + j * stepX, startY + i * stepY, 0));
                    }
                }

                loadpos = loadpos.OrderBy(p => p.X).ToList();
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

        private void buttonMoveToTestPos_Click(object sender, EventArgs e)
        {
            try
            {
                buttonMoveToTestPos.Enabled = false;
                Cursor = Cursors.WaitCursor;
                switch (comboBoxPlatform.Text)
                {
                    case "LUp":
                        Platform = Machine.Machine.Ins.Find<PlatformEx>("LeftUp");
                        break;
                    case "LDown":
                        Platform = Machine.Machine.Ins.Find<PlatformEx>("LeftDown");
                        break;
                    case "RUp":
                        Platform = Machine.Machine.Ins.Find<PlatformEx>("RightUp");
                        break;
                    case "RDown":
                        Platform = Machine.Machine.Ins.Find<PlatformEx>("RightDown");
                        break;
                    default:
                        return;
                }

                var data = comboBoxTestPos.Text.Split(',');
                if (data.Length >= 5)
                {
                    var pos = new PosXYZ(double.Parse(data[2]), double.Parse(data[3]), double.Parse(data[4]));

                    pos = CalibrationConfig.TransformToPlatformPos(Machine.Machine.Ins.Settings.Calibration, (PlatformType)Enum.Parse(typeof(PlatformType), comboBoxPlatform.Text), pos, Platform.CurPos[2], comboBoxDownGT.Text == "GT1" ? 1 : 2);

                    if (MessageBox.Show($"{Platform.Name}:{string.Join(",", Platform.Axis.Select(a => a.Name).ToArray())} Jump To Pos {pos}?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var ret = Platform.ExitAuto().Jump(pos, double.Parse(comboBoxJump.Text), timeout: 5000, checkLimit: false);
                        var error = !ret ? $"error {Platform.ShowStatus()}" : "normal";
                        MessageBox.Show($"{Platform.Name} Jump {pos} {error} Finish");
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

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清除所有点位？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                TestPos.Clear();
                LoadTestPos();
            }
        }
    }
}
