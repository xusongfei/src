using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
    public partial class MeasureProjectEditor : Form
    {
        public List<PlatformEx> Platforms = new List<PlatformEx>();

        public Type DefaultProducType = typeof(MeasureProject);

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
            Project = (MeasureProject)Activator.CreateInstance(DefaultProducType);
            LoadProject();

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @".\Config\Project";
            ofd.Filter = @"(Measure Project)|*.mprj|(All Files)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Project = MeasureProject.Load(ofd.FileName, DefaultProducType);
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
                if (p.PropertyType == typeof(List<PosXYZ>))
                {
                    if (p.Name == posPropName)
                    {
                        posProp = p;
                    }
                }
            }

            if (posProp != null)
            {
                var posEditForm = new MeasureProjectPosEditor()
                {
                    TestPos = posProp.GetValue(Project) as List<PosXYZ>,
                    Platforms = Platforms,
                };

                //start edit pos
                posEditForm.ShowDialog();

                //update project
                LoadProject();
            }
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
    }
}
