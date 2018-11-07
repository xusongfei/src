using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    public partial class MeasureProjectSelctionControl : UserControl
    {
        public MeasureProjectSelctionControl()
        {
            InitializeComponent();
        }

        public Station Station;

        public MeasureProject Project;

        public Type ProjecType;

        public string ProjectFile;


        public event Action<string> SelectMeasureProjectUpdateEvent;


        private void MeasureProjectSelctionControl_Load(object sender, EventArgs e)
        {
            if (Station != null)
            {
                groupBoxStation.Text = $"{Station.Name} {Station.Description}";
            }

        }
        public void LoadMeasureProject()
        {
            if (Station != null)
            {
                groupBoxStation.Text = $"{Station.Name} {Station.Description}";
            }

            if (!string.IsNullOrEmpty(ProjectFile))
            {
                if (ProjecType != null)
                {
                    try
                    {
                        Project = MeasureProject.Load(ProjectFile, ProjecType);
                    }
                    catch (Exception ex)
                    {
                        Project = null;
                        MessageBox.Show($"加载{ProjecType}测试文件异常：{ex.Message}");
                        return;
                    }
                }

                Display();
            }
        }


        private void Display()
        {
            if (Station != null)
            {
                groupBoxStation.Text = $"{Station.Name} {Station.Description}";
            }

            if (!string.IsNullOrEmpty(ProjectFile))
            {
                richTextBoxFile.Text = ProjectFile;
            }

            if (Project != null)
            {
                richTextBoxMprj.Text = Project.ToString();
            }
        }

        private void buttonBrowseMprjFile_Click(object sender, EventArgs e)
        {
            if (Station == null || Station.RunningState != RunningState.WaitReset)
            {
                MessageBox.Show($"工站{Station.Name} {Station.Description}未停止，请停止运行后更换测试文件！");
                return;
            }


            var fd = new OpenFileDialog
            {
                InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Config"),
                Filter = @"(Measure Project)|*.mprj",
                Multiselect = false
            };


            if (fd.ShowDialog() == DialogResult.OK)
            {
                ProjectFile = fd.FileName.Replace(Directory.GetCurrentDirectory(), ".");

                LoadMeasureProject();

                OnSelectMeasureProjectUpdateEvent(ProjectFile);
            }
        }

        private void buttonEditCurFile_Click(object sender, EventArgs e)
        {
            if (Project != null)
            {
                try
                {
                    var editor = new MeasureProjectEditor();
                    editor.StartPosition = FormStartPosition.CenterParent;
                    editor.MeasureProjectType = Project.GetType();
                    editor.Platforms = Station.Machine.Platforms.Values.ToList();
                    editor.Project = Project;
                    editor.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"编辑当前文件异常：{ex.Message}");
                }
            }

            LoadMeasureProject();
        }

        private void buttonNewMprjFile_Click(object sender, EventArgs e)
        {
            if (Project != null)
            {
                try
                {
                    var editor = new MeasureProjectEditor();
                    editor.StartPosition = FormStartPosition.CenterParent;
                    editor.MeasureProjectType = Project.GetType();
                    editor.Platforms = Station.Machine.Platforms.Values.ToList();
                    editor.Project = null;
                    editor.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"新建测试文件异常：{ex.Message}");
                }
            }

        }

        protected virtual void OnSelectMeasureProjectUpdateEvent(string file)
        {
            SelectMeasureProjectUpdateEvent?.Invoke(file);
        }

    }
}