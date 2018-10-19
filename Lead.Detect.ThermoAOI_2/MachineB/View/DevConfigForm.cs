using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.loadUtils;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.FrameworkExtension.userControls;
using Lead.Detect.MeasureComponents.LaserControl;
using Lead.Detect.MeasureComponents.LMILaser;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI2.MachineB.View
{
    public partial class DevConfigForm : DockContent, IFrameworkControl
    {
        public DevConfigForm()
        {
            InitializeComponent();
        }

        private void DevConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void DevConfigForm_Load(object sender, EventArgs e)
        {

            //di do cy
            diControl1.DiExs = Machine.Ins.DiExs.Values.ToList();
            diControl1.LoadFramework();
            diControl1.FrameworkActivate();

            doControl1.DoExs = Machine.Ins.DoExs.Values.ToList();
            doControl1.LoadFramework();
            doControl1.FrameworkActivate();

            vioControl1.VioExs = Machine.Ins.VioExs.Values.ToList();
            vioControl1.LoadFramework();
            vioControl1.FrameworkActivate();

            cyliderControl1.CyExs = Machine.Ins.CylinderExs.Values.ToList();
            cyliderControl1.LoadFramework();
            cyliderControl1.FrameworkActivate();


            //load platforms
            var platforms = Machine.Ins.Platforms.Values;
            tabControlPlatform.TabPages.Clear();
            foreach (var p in platforms)
            {
                tabControlPlatform.TabPages.Add(p.Name, p.Description + $"({p.Name})");
                var platformControl = new PlatformControl()
                {
                    Dock = DockStyle.Fill,
                };
                platformControl.LoadPlatform(p);

                tabControlPlatform.TabPages[p.Name].Controls.Add(platformControl);
            }


            //config
            propertyGridCommonConfig.SelectedObject = Machine.Ins.Settings;
            propertyGridMachineConfig.SelectedObject = Machine.Ins.Settings;
            richTextBoxMachine.Text = Machine.Ins.SerializeToString();



            //product
            tabControl1.SelectedTab = tabPageProduct;
            textBoxMeasureProj.Text = Machine.Ins.Settings.MeasureProjectFile;

            //calib
            stationControl.Machine = Machine.Ins;
            stationControl.Station = Machine.Ins.Find<Station>("MainStation");


            //test page

            lineLaserExDebugControl1.LineLaserEx = new LmiLaser() { Name = "top", IpStr = "192.168.2.10", JobName = "test" };
            lineLaserExDebugControl2.LineLaserEx = new LmiLaser() { Name = "down", IpStr = "192.168.1.10", JobName = "test" };

            cameraExDebugControl1.Camera = (Machine.Ins.Find<StationTask>("CameraTask") as MeasureTask)?.Camera;
        }


        public void LoadFramework()
        {
            diControl1.LoadFramework();
            doControl1.LoadFramework();
            vioControl1.LoadFramework();
            cyliderControl1.LoadFramework();
        }

        public void FrameworkActivate()
        {
            diControl1.FrameworkActivate();
            doControl1.FrameworkActivate();
            vioControl1.FrameworkActivate();
            cyliderControl1.FrameworkActivate();
        }

        public void FrameworkDeactivate()
        {
            diControl1.FrameworkDeactivate();
            doControl1.FrameworkDeactivate();
            vioControl1.FrameworkDeactivate();
            cyliderControl1.FrameworkDeactivate();
        }

        public void FrameworkUpdate()
        {
        }

        private void buttonMeasureProjectEditor_Click(object sender, EventArgs e)
        {
            try
            {
                var mprjForm = new MeasureProjectEditor()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    DefaultProducType = typeof(MeasureProjectB),

                    Platforms = Machine.Ins.Platforms.Values.ToList(),
                };

                mprjForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开文件异常：{ex.Message}");
            }
        }

        private void buttonBrowseMeasureProj_Click(object sender, EventArgs e)
        {
            var station = Machine.Ins.Find<Station>("MainStation");
            if (station == null || station.AutoState != StationAutoState.WaitReset)
            {
                MessageBox.Show($"工站未停止，请停止运行后更换测试文件！");
                return;
            }


            var fd = new OpenFileDialog
            {
                InitialDirectory = @".\Config",
                Filter = @"(Measure Project)|*.mprj",
                Multiselect = false
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Machine.Ins.Settings.MeasureProjectFile = fd.FileName.Replace(Directory.GetCurrentDirectory(), ".");
                textBoxMeasureProj.Text = Machine.Ins.Settings.MeasureProjectFile;
            }
        }

        private void buttonOpenMeasureProj_Click(object sender, EventArgs e)
        {
            try
            {
                var mprjForm = new MeasureProjectEditor()
                {
                    StartPosition = FormStartPosition.CenterParent,
                    DefaultProducType = typeof(MeasureProjectB),

                    Platforms = Machine.Ins.Platforms.Values.ToList(),
                    Project = MeasureProject.Load(textBoxMeasureProj.Text, typeof(MeasureProjectB))
                };

                mprjForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开文件异常：{ex.Message}");
            }
        }
    }
}