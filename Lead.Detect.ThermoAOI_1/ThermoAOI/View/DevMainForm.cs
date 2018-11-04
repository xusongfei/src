using System;
using System.IO;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineUtilityLib.UtilViews;
using Lead.Detect.ThermoAOI.Machine1.Machine.newTasks;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI.Machine1.View
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
            label1.Text = Machine.Machine.Ins.Settings.Name;

            if (!Directory.Exists(@".\Log")) Directory.CreateDirectory(@".\Log");


            Machine.Machine.Ins.ShowAlarmEvent += ShowShowAlarm;

            labelLeft.Text = Machine.Machine.Ins.Find<Station>("LeftStation")?.Name;
            labelRight.Text = Machine.Machine.Ins.Find<Station>("RightStation")?.Name;

            labelLeftFile.Text = Machine.Machine.Ins.Settings.LeftProjectFilePath;
            labelRightFile.Text = Machine.Machine.Ins.Settings.RightProjectFilePath;

            //bind event
            foreach (var t in Machine.Machine.Ins.Tasks)
            {
                t.Value.LogEvent += (log, level) => UpdateTaskLog(t.Value.Name, log, level);
                t.Value.LogEvent += (log, level) =>
                {
                    LoggerHelper.Log($@".\Log\{t.Value.Name}", log, level);
                };
            }

            //bind trans event
            var leftTrans = Machine.Machine.Ins.Find<StationTask>("LeftTrans") as newTransTask;
            if (leftTrans != null)
            {
                leftTrans.TestProcessControl.TestStartEvent += UpdateLStart;
                leftTrans.TestProcessControl.TestingEvent += UpdateLTesting;
                leftTrans.TestProcessControl.TestFinishEvent += UpdateLTestResult;

                _thermoProductDisplayControl1.Station = leftTrans.Station;
            }

            var rightTrans = Machine.Machine.Ins.Find<StationTask>("RightTrans") as newTransTask;
            if (rightTrans != null)
            {
                rightTrans.TestProcessControl.TestStartEvent += UpdateRStart;
                rightTrans.TestProcessControl.TestingEvent += UpdateRTesting;
                rightTrans.TestProcessControl.TestFinishEvent += UpdateRTestResult;

                _thermoProductDisplayControl2.Station = rightTrans.Station;
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
            Machine.Machine.Ins.ShowAlarmEvent -= ShowShowAlarm;
        }


        #region main panel

        private void btnStart_Click(object sender, EventArgs e)
        {
            Machine.Machine.Ins.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Machine.Machine.Ins.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Machine.Machine.Ins.Reset();
        }

        private void buttonLamp_Click(object sender, EventArgs e)
        {
            Machine.Machine.Ins.Find<IDoEx>("DoLamp")?.Toggle();
        }

        private void btnLogin_Click(object sender, EventArgs e)
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

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (OperationLevel != OperationLevel.Administrator && FrameworkExtenion.IsSimulate == false)
            {
                MessageBox.Show($"操作权限异常：{OperationLevel}");
                return;
            }

            DevConfigForm.Show(MainPanel, DockState.Document);
        }

        #endregion



        #region display result

        public void UpdateLStart(Thermo1Product product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateLStart), product);
            }
            else
            {
                _thermoProductDisplayControl1.UpdateStart();
            }
        }


        public void UpdateLTesting(Thermo1Product data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateLTesting), data);
            }
            else
            {
                _thermoProductDisplayControl1.UpdateTesting(data);
            }
        }
        public void UpdateLTestResult(Thermo1Product data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateLTestResult), data);
            }
            else
            {
                _thermoProductDisplayControl1.UpdateResult(data);
            }
        }
        public void UpdateRStart(Thermo1Product product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateRStart), product);
            }
            else
            {
                _thermoProductDisplayControl2.UpdateStart();
            }
        }
        public void UpdateRTesting(Thermo1Product data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateRTesting), data);
            }
            else
            {
                _thermoProductDisplayControl2.UpdateTesting(data);
            }
        }
        public void UpdateRTestResult(Thermo1Product data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateRTestResult), data);
            }
            else
            {
                _thermoProductDisplayControl2.UpdateResult(data);
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
                    DevAlarmForm.ShowAlarm(alarm, level);
                }
                else
                {
                    DevAlarmForm.ShowAlarm(alarm, level);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TabText = $"设备状态:{Machine.Machine.Ins.RunState}:{Machine.Machine.Ins.RunningState.GetState()}";

            labelLeft.Text = $"左工站（{Machine.Machine.Ins.Settings.ProductionLeft.Display()}）";
            labelRight.Text = $"右工站（{Machine.Machine.Ins.Settings.ProductionRight.Display()}）";

            labelLeftFile.Text = Path.GetFileName(Machine.Machine.Ins.Settings.LeftProjectFilePath);
            labelRightFile.Text = Path.GetFileName(Machine.Machine.Ins.Settings.RightProjectFilePath);

        }

        private void labelLeft_DoubleClick(object sender, EventArgs e)
        {

            if (MessageBox.Show($"清除左工站统计:\r\n{Machine.Machine.Ins.Settings.ProductionLeft.ToString()}？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Machine.Machine.Ins.Settings.ProductionLeft.Clear();
            }


        }

        private void labelRight_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show($"清除右工站统计:\r\n{Machine.Machine.Ins.Settings.ProductionRight.ToString()}？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Machine.Machine.Ins.Settings.ProductionRight.Clear();
            }
        }

    }
}