using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    public partial class MeasureProjectEditor : Form
    {
        public List<PlatformEx> Platforms = new List<PlatformEx>();

        public Type MeasureProjectType = typeof(MeasureProject);

        public MeasureProject Project;

        public MeasureProjectEditor()
        {
            InitializeComponent();
        }

        private void MeasureProjectEditor_Load(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project = (MeasureProject) Activator.CreateInstance(MeasureProjectType);
            LoadProject();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @".\Config\Project";
            ofd.Filter = @"(Measure Project)|*.mprj|(All Files)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Project = MeasureProject.Load(ofd.FileName, MeasureProjectType);
                LoadProject();
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = @".\Config\Project";
            sfd.Filter = @"(Measure Project)|*.mprj|(All Files)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Project = propertyGrid1.SelectedObject as MeasureProject;
                Project?.Save(sfd.FileName);
            }
        }

        private void LoadProject()
        {
            if (Project != null)
            {
                textBox1.Text = Project.ProjectName;
                propertyGrid1.SelectedObject = Project;
                propertyGrid1.ExpandAllGridItems();

                richTextBox1.Text = Project.ToString();


                comboBoxPos.Items.Clear();
                var props = Project.GetType().GetProperties();
                foreach (var p in props)
                {
                    if (p.PropertyType == typeof(List<PosXYZU>))
                    {
                        comboBoxPos.Items.Add(p.Name);
                    }

                    if (p.PropertyType == typeof(List<PosXYZ>))
                    {
                        comboBoxPos.Items.Add(p.Name);
                    }
                }
            }
        }

        private void buttonEditPositions_Click(object sender, EventArgs e)
        {
            var posPropName = comboBoxPos.Text;

            //find pos prop
            PropertyInfo posProp = null;
            var props = Project.GetType().GetProperties();
            foreach (var p in props)
            {
                if (p.PropertyType == typeof(List<PosXYZ>)
                    || p.PropertyType == typeof(List<PosXYZU>)
                    || p.PropertyType == typeof(List<PosXYZUVW>))
                {
                    if (p.Name == posPropName)
                    {
                        posProp = p;
                        break;
                    }
                }
            }


            List<IPlatformPos> testpos = null;
            if (posProp != null)
            {
                if (posProp.PropertyType == typeof(List<PosXYZ>))
                {
                    testpos = (posProp.GetValue(Project) as List<PosXYZ>)?.Cast<IPlatformPos>().ToList();
                }
                else if (posProp.PropertyType == typeof(List<PosXYZU>))
                {
                    testpos = (posProp.GetValue(Project) as List<PosXYZU>)?.Cast<IPlatformPos>().ToList();
                }
                else if (posProp.PropertyType == typeof(List<PosXYZUVW>))
                {
                    testpos = (posProp.GetValue(Project) as List<PosXYZUVW>)?.Cast<IPlatformPos>().ToList();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }


            var posEditForm = new MeasureProjectPosEditor()
            {
                StartPosition = FormStartPosition.CenterParent,
                TestPos = testpos,
                Platforms = Platforms,
                MeasureProjectType = MeasureProjectType,
            };

            //start edit pos
            posEditForm.ShowDialog();

            if (posProp.PropertyType == typeof(List<PosXYZ>))
            {
                posProp.SetValue(Project, posEditForm.TestPos.Cast<PosXYZ>().ToList());
            }
            else if (posProp.PropertyType == typeof(List<PosXYZU>))
            {
                posProp.SetValue(Project, posEditForm.TestPos.Cast<PosXYZU>().ToList());
            }
            else if (posProp.PropertyType == typeof(List<PosXYZUVW>))
            {
                posProp.SetValue(Project, posEditForm.TestPos.Cast<PosXYZUVW>().ToList());
            }
            else
            {
                return;
            }

            //update project
            LoadProject();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            textBox1.Text = Project.ProjectName;
            richTextBox1.Text = Project.ToString();
        }

        private void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            textBox1.Text = Project.ProjectName;
            richTextBox1.Text = Project.ToString();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
    }
}