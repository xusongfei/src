using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.loadUtils;
using Lead.Detect.MachineIOTest.UserDefine;

namespace Lead.Detect.MachineIOTest
{
    public partial class MachineForm : Form
    {
        public MachineForm()
        {
            InitializeComponent();
        }

        private void MachineForm_Load(object sender, EventArgs e)
        {
            Text = EnvironmentManager.Ins.LastDevProject;


            // di do cy vio
            diControl1.DiExs = Machine.Ins.DiExs.Values.ToList();
            diControl1.LoadFramework();
            diControl1.FrameworkActivate();
            doControl1.DoExs = Machine.Ins.DoExs.Values.ToList();
            doControl1.LoadFramework();
            doControl1.FrameworkActivate();
            cylinderControl1.CyExs = Machine.Ins.CylinderExs.Values.ToList();
            cylinderControl1.LoadFramework();
            cylinderControl1.FrameworkActivate();
            vioControl1.VioExs = Machine.Ins.VioExs.Values.ToList();
            vioControl1.LoadFramework();
            vioControl1.FrameworkActivate();

            // platforms
            foreach (var p in Machine.Ins.Platforms.Values)
            {
                tabControl1.TabPages.Add(p.Name, p.Description + $"({p.Name})");
                var platformControl = new PlatformControl()
                {
                    Dock = DockStyle.Fill,
                };
                platformControl.LoadPlatform(p);

                tabControl1.TabPages[p.Name].Controls.Add(platformControl);
            }


            //config
            richTextBoxMachine.Text = Machine.Ins.SerializeToString();


        }


        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @".\Config");
        }

        private void 打开machinecfgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                System.Diagnostics.Process.Start("notepad++.exe", @".\Config\machinesim.cfg");
            }
            else
            {
                System.Diagnostics.Process.Start("notepad++.exe", @".\Config\machine.cfg");
            }
        }

        private void prims文件devToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnvironmentManager.Ins.ShowPrimsEditor();
        }

        private void 设备选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = $"Device File (*.dev)|*.dev",
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                EnvironmentManager.Ins.NewBrowseDevProject = ofd.FileName;

                this.Close();
            }
        }
    }
}