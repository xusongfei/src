using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;
using Lead.Detect.ThermoAOITrajectoryLib;

namespace Lead.Detect.ThermoAOI.Product
{
    public partial class FlatnessProjectEditorForm : Form
    {

        public FlatnessProject FlatnessProject;

        public FlatnessProjectEditorForm()
        {
            InitializeComponent();
        }

        private void ProjectEditorForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            LoadProject();
        }

        private void LoadProject()
        {
            if (FlatnessProject != null)
            {
                label1.Text = FlatnessProject.ProjectName;
                propertyGrid1.SelectedObject = FlatnessProject;
                propertyGrid1.ExpandAllGridItems();

                richTextBox1.Text = FlatnessProject.ProductSettings.ToString();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlatnessProject = new FlatnessProject();
            LoadProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @".\Config";
            ofd.Filter = @"(All Files)|*.*|(Flatness ProjectFile)|*.fprj";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FlatnessProject = FlatnessProject.Load(ofd.FileName);
                LoadProject();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = @".\Config";
            sfd.Filter = @"(All Files)|*.*|(Flatness ProjectFile)|*.fprj";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FlatnessProject = propertyGrid1.SelectedObject as FlatnessProject;
                FlatnessProject?.Save(sfd.FileName);
            }
        }

        private void buttonProductTestPosUpEdit_Click(object sender, EventArgs e)
        {
            if (FlatnessProject == null)
            {
                MessageBox.Show("未加载点位文件！");
                return;
            }

            var posEditor = new ProductTestPosEditor()
            {
                Text = FlatnessProject.ProjectName,
                StartPosition = FormStartPosition.CenterScreen,
                TestPos = FlatnessProject.ProductSettings.UpTestPositions,
            };
            posEditor.ShowDialog();
        }

        private void buttonProductTestPosDownEdit_Click(object sender, EventArgs e)
        {
            if (FlatnessProject == null)
            {
                MessageBox.Show("未加载点位文件！");
                return;
            }

            var posEditor = new ProductTestPosEditor()
            {
                Text = FlatnessProject.ProjectName,
                StartPosition = FormStartPosition.CenterScreen,
                TestPos = FlatnessProject.ProductSettings.DownTestPositions,
            };
            posEditor.ShowDialog();
        }

        private void buttonViewUpPos_Click(object sender, EventArgs e)
        {
            if (FlatnessProject == null)
            {
                MessageBox.Show("未加载点位文件！");
                return;
            }

            var station = comboBoxUpStation.Text;

            var positions = FlatnessProject.ProductSettings.UpTestPositions;

            var newPos = new List<PosXYZ>();
            switch (station)
            {
                case "Left":
                    foreach (var p in positions)
                    {
                        var convert = Calibration.CalibrationConfig.TransformToPlatformPos(Machine.Machine.Ins.Settings.Calibration,
                        Machine.Common.PlatformType.LUp, p, p.Z);
                        convert.Name = p.Name;
                        convert.Description = p.Description;
                        convert.Index = p.Index;

                        newPos.Add(convert);
                    }
                    break;

                case "Right":
                    foreach (var p in positions)
                    {
                        var convert = Calibration.CalibrationConfig.TransformToPlatformPos(Machine.Machine.Ins.Settings.Calibration,
                        Machine.Common.PlatformType.RUp, p, p.Z);
                        convert.Name = p.Name;
                        convert.Description = p.Description;
                        convert.Index = p.Index;

                        newPos.Add(convert);
                    }
                    break;

                default:
                    return;
            }

            var view = new ProductTestPosViewer()
            {
                Positions = newPos,
            };

            view.ShowDialog();

        }

        private void buttonViewDownPos_Click(object sender, EventArgs e)
        {
            if (FlatnessProject == null)
            {
                MessageBox.Show("未加载点位文件！");
                return;
            }

            var station = comboBoxDownStation.Text;

            var positions = FlatnessProject.ProductSettings.DownTestPositions;

            var newPos = new List<PosXYZ>();
            switch (station)
            {
                case "Left":
                    foreach (var p in positions)
                    {
                        var convert = Calibration.CalibrationConfig.TransformToPlatformPos(Machine.Machine.Ins.Settings.Calibration,
                        Machine.Common.PlatformType.LDown, p, p.Z, p.Description == "GT1" ? 1 : 2);
                        convert.Name = p.Name;
                        convert.Description = p.Description;
                        convert.Index = p.Index;

                        newPos.Add(convert);
                    }
                    break;

                case "Right":
                    foreach (var p in positions)
                    {
                        var convert = Calibration.CalibrationConfig.TransformToPlatformPos(Machine.Machine.Ins.Settings.Calibration,
                       Machine.Common.PlatformType.RDown, p, p.Z, p.Description == "GT1" ? 1 : 2);
                        convert.Name = p.Name;
                        convert.Description = p.Description;
                        convert.Index = p.Index;

                        newPos.Add(convert);
                    }
                    break;

                default:
                    return;
            }

            var view = new ProductTestPosViewer()
            {
                Positions = newPos,
            };

            view.ShowDialog();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            richTextBox1.Text = FlatnessProject.ProductSettings.ToString();
        }

        private void buttonDownOptimize_Click(object sender, EventArgs e)
        {
            try
            {
                var pos = FlatnessProject.ProductSettings.DownTestPositions;
                if (pos.Count > 20)
                {
                    var ped1pos = pos.FindAll(p => p.Name == "ped1");
                    var ped2pos = pos.FindAll(p => p.Name == "ped2");

                    var orderpos = new List<PosXYZ>();
                    orderpos.AddRange(pos);
                    orderpos.RemoveAll(p => p.Name.StartsWith("ped"));
                    orderpos.Add(ped1pos.Last());
                    orderpos.Add(ped2pos.Last());

                    var trajDp = new DpTsp(new PosGraph(orderpos));
                    trajDp.RunTsp();

                    var order = trajDp.Order;

                    pos.Clear();
                    for (int i = 0; i < orderpos.Count; i++)
                    {
                        if (orderpos[order[i] - 1].Name == "ped1")
                        {
                            pos.AddRange(ped1pos);
                        }
                        else if (orderpos[order[i] - 1].Name == "ped2")
                        {
                            pos.AddRange(ped2pos);
                        }
                        else
                        {
                            pos.Add(orderpos[order[i] - 1]);
                        }
                    }
                }
                else
                {
                    var orderpos = new List<PosXYZ>();
                    orderpos.AddRange(pos);
                    var trajDp = new DpTsp(new PosGraph(orderpos));
                    trajDp.RunTsp();

                    var order = trajDp.Order;

                    pos.Clear();
                    for (int i = 0; i < orderpos.Count; i++)
                    {

                        pos.Add(orderpos[order[i] - 1]);
                    }
                }


                MessageBox.Show("排序完成！");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"排序异常: {ex.Message}！");
            }


        }

        private void 另存为mprjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = @".\Config";
            sfd.Filter = @"(All Files)|*.*|(Measure Project)|*.mprj";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FlatnessProject = propertyGrid1.SelectedObject as FlatnessProject;
                FlatnessProject?.Convert().Save(sfd.FileName);
            }
        }
    }
}