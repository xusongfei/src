using System;
using System.IO;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.UtilViews;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI2.MachineB.View
{
    public partial class DevMainForm : DockContent
    {
        public DockPanel MainPanel;
        public DevLogForm DevLogForm;
        public DevAlarmForm DevAlarmForm;
        public DevConfigForm DevConfigForm;

        public OperationLevel OperationLevel = OperationLevel.Operator;


        public DevMainForm()
        {
            InitializeComponent();
        }

        private void DevMainForm_Load(object sender, EventArgs e)
        {
            Text = $"{Machine.Ins.Settings.Name} {Machine.Ins.Settings.Version}";
            label1.Text = Machine.Ins.Settings.Name;

            if (!Directory.Exists(@".\Log"))
            {
                Directory.CreateDirectory(@".\Log");
            }


            var measureTask = Machine.Ins.Find<StationTask>("MeasureTask") as MeasureTask;
            if (measureTask != null)
            {
                lineLaserExDisplayControl1.BindComponent(measureTask.Laser1);
                lineLaserExDisplayControl2.BindComponent(measureTask.Laser2);
            }


            Machine.Ins.ShowAlarmEvent += ShowShowAlarm;


            //bind log event
            foreach (var t in Machine.Ins.Tasks)
            {
                t.Value.LogEvent += (log, level) => UpdateTaskLog(t.Value.Name, log, level);
                t.Value.LogEvent += (log, level) =>
                {
                    LoggerHelper.Log($@".\Log\{t.Value.Name}", log, level);
                };
            }


            //bind product update event
            var transTask = Machine.Ins.Find<StationTask>("TransTask") as TransTask;
            if (transTask != null)
            {
                transTask.TestProcessControl.TestStartEvent += p => _thermoProductDisplayControl1.UpdateStart();
                transTask.TestProcessControl.TestingEvent += p => _thermoProductDisplayControl1.UpdateTesting(p);
                transTask.TestProcessControl.TestFinishEvent += p =>
                {
                    _thermoProductDisplayControl1.UpdateResult(p);
                    productionCountControl1.UpdateProduction(Machine.Ins.Settings.Production);
                };
            }


            timer1.Start();
        }


        private void DevMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DockState = DockState.Document;
            e.Cancel = true;
        }

        private void DevMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Machine.Ins.ShowAlarmEvent -= ShowShowAlarm;
        }


        #region main panel

        private void btnStart_Click(object sender, EventArgs e)
        {
            Machine.Ins.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Machine.Ins.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Machine.Ins.Reset();
        }

        private void buttonLamp_Click(object sender, EventArgs e)
        {
            Machine.Ins.Find<IDoEx>("DOLamp")?.Toggle();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                OperationLevel = OperationLevel.Administrator;
                lbOpLvl.Text = OperationLevel.ToString();

            }
            else
            {
                var f = new FrmPassword();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    if (f.pwdFlag)
                    {
                        OperationLevel = OperationLevel.Administrator;
                        lbOpLvl.Text = OperationLevel.ToString();
                    }
                    else
                    {
                        OperationLevel = OperationLevel.Operator;
                        lbOpLvl.Text = OperationLevel.ToString();
                    }
                }

            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (OperationLevel != OperationLevel.Administrator && FrameworkExtenion.IsSimulate == false)
            {
                MessageBox.Show($"操作权限异常：{OperationLevel}");
                return;
            }
            else

            {
                DevConfigForm.Show(MainPanel, DockState.Document);
            }
        }

        #endregion

        public void UpdateTaskLog(string tab, string log, LogLevel level)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string, string, LogLevel>(UpdateTaskLog), tab, log, level);
            else
                DevLogForm?.UpdateLog(tab, log, level);
        }

        public void ShowShowAlarm(string alarm, LogLevel level)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string, LogLevel>(ShowShowAlarm), alarm, level);
            else
            {
                if (level == LogLevel.None)
                {
                    this.Show(MainPanel, DockState.Document);
                }
                else
                {
                    DevAlarmForm.ShowAlarm(alarm, level);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TabText = $"设备状态:{Machine.Ins.RunState}:{Machine.Ins.RunningState.GetState()}";

            labelFile.Text = Path.GetFileName(Machine.Ins.Settings.MeasureProjectFile);
        }



    }
}