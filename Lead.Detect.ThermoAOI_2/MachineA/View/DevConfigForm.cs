using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.loadUtils;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.FrameworkExtension.userControls;
using Lead.Detect.MeasureComponents.Calibration;
using Lead.Detect.ThermoAOI2.MachineA.UserDefine;
using Lead.Detect.ThermoAOI2.MachineA.UserDefine.Tasks;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Project;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo2;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI2.MachineA.View
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
            propertyGridSettings.SelectedObject = Machine.Ins.Settings;
            richTextBoxMachine.Text = Machine.Ins.SerializeToString();


            //product
            tabControl1.SelectedTab = tabPageProduct;
            measureProjectSelctionControl1.Station = Machine.Ins.Find<Station>("MainStation");
            measureProjectSelctionControl1.ProjecType = typeof(MeasureProjectA);
            measureProjectSelctionControl1.ProjectFile = Machine.Ins.Settings.MeasureProjectFile;
            measureProjectSelctionControl1.SelectMeasureProjectUpdateEvent += f =>
            {

                Machine.Ins.Settings.MeasureProjectFile = f;
                Machine.Ins.Settings.Save();
            };
            measureProjectSelctionControl1.LoadMeasureProject();


            //calib
            stationControl.Machine = Machine.Ins;
            stationControl.Station = Machine.Ins.Find<Station>("MainStation");


            //test page

            cameraExDebugControl1.Camera = (Machine.Ins.Find<StationTask>("MeasureTask") as MeasureTask)?.Camera;
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


     

        #region calib

        private void buttonCameraCalib_Click(object sender, EventArgs e)
        {
            if (Machine.Ins.RunningState != RunningState.WaitRun)
            {
                return;
            }

            //select calib type


            //show calib form
            var calib = new MachineACalib()
            {
                CalibInfo = "CameraACalib",
                Description = "Calib",
                Station = Machine.Ins.Find<Station>("MainStation"),
                Camera1 = (Machine.Ins.Find<StationTask>("MeasureTask") as MeasureTask)?.Camera,
                Platform = (Machine.Ins.Find<StationTask>("MeasureTask") as MeasureTask)?.Platform,
                Project = (Machine.Ins.Find<StationTask>("MeasureTask") as MeasureTask)?.Project,
            };

            calib.LogEvent += UpdateCalib;


            var calibForm = new AutoCalibForm()
            {
                Calib = calib,
            };


            if (calibForm.ShowDialog() == DialogResult.OK)
            {
                //save calib


            }


        }

        private void UpdateCalib(string log, LogLevel level)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, LogLevel>(UpdateCalib), log, level);
            }
            else
            {
                richTextBoxCalibData.AppendText(log);
            }
        }

        #endregion
    }
}