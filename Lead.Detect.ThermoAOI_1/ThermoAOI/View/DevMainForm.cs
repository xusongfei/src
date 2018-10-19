using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Lead.Detect.DatabaseHelper;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.Helper;
using Lead.Detect.ThermoAOI.Common;
using Lead.Detect.ThermoAOI.Machine.newTasks;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI.View
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


            Machine.Machine.Ins.AlarmEvent += ShowAlarm;

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
                leftTrans.TestStartEvent += UpdateLStart;
                leftTrans.TestingEvent += UpdateLTesting;
                leftTrans.TestFinishEvent += UpdateLTestResult;
            }

            var rightTrans = Machine.Machine.Ins.Find<StationTask>("RightTrans") as newTransTask;
            if (rightTrans != null)
            {
                rightTrans.TestStartEvent += UpdateRStart;
                rightTrans.TestingEvent += UpdateRTesting;
                rightTrans.TestFinishEvent += UpdateRTestResult;
            }

            timer1.Start();

            ShowTestPoints();
        }


        private void DevMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DockState = DockState.Document;
            e.Cancel = true;
        }

        private void DevMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Machine.Machine.Ins.AlarmEvent -= ShowAlarm;
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

        #region query panel


        private void buttonQueryAll_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = SqlLiteHelper.DB.From<ProductTestDataEntity>().ToDataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清除所有本地数据？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            SqlLiteHelper.DB.DeleteAll<ProductTestDataEntity>();

            MessageBox.Show("清除完成！");
        }
        private void buttonQueryProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxProductBarcode.Text))
            {
                MessageBox.Show("条码为空！");
                return;
            }

            dataGridView1.DataSource = SqlLiteHelper.DB.From<ProductTestDataEntity>().Where(p => p.Barcode == textBoxProductBarcode.Text).ToDataTable();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        #endregion

        #region display result

        public void UpdateLStart()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateLStart));
            }
            else
            {
                productLeft.UpdateStart();
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
                productLeft.UpdateTesting(data);
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
                productLeft.UpdateResult(data);
            }
        }
        public void UpdateRStart()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateRStart));
            }
            else
            {
                productRight.UpdateStart();
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
                productRight.UpdateTesting(data);
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
                productRight.UpdateResult(data);
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

        public void ShowAlarm(string alarm, LogLevel level)
        {
            if (InvokeRequired)
                BeginInvoke(new Action<string, LogLevel>(ShowAlarm), alarm, level);
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
            TabText = $"设备状态:{Machine.Machine.Ins.State}:{Machine.Machine.Ins.AutoState.GetState()}";

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


        private void ShowTestPoints()
        {
            List<PosXYZ> pos = new List<PosXYZ>();

            pos.Add(new PosXYZ(26.4, -41.25, -10.1634656250032) { Status = false });
            pos.Add(new PosXYZ(26.4, -216.25, -10.1671531249992) { Status = false });
            pos.Add(new PosXYZ(135.2, -216.25, -10.3912197916618) { Status = false });
            pos.Add(new PosXYZ(135.2, -41.25, -10.1985322916657) { Status = false });
            pos.Add(new PosXYZ(57.67, -94.58, -10.2268653333341) { Status = false });
            pos.Add(new PosXYZ(57.67, -162.81, -10.2560192083326) { Status = false });
            pos.Add(new PosXYZ(103.94, -162.81, -10.2539562916642) { Status = false });
            pos.Add(new PosXYZ(103.94, -94.58, -10.2078024166657) { Status = false });
            pos.Add(new PosXYZ(64.49, -66.55, 0.243366209398313) { Status = true });
            pos.Add(new PosXYZ(64.49, -96.55, 0.280177180953067) { Status = true });
            pos.Add(new PosXYZ(94.49, -96.55, 0.299250468875222) { Status = true });
            pos.Add(new PosXYZ(94.49, -66.55, 0.277439497320468) { Status = true });
            pos.Add(new PosXYZ(79.49, -81.55, 0.274308339136768) { Status = true });
            pos.Add(new PosXYZ(47.49, -49.55, 0.299865129028064) { Status = true });
            pos.Add(new PosXYZ(47.49, -113.55, 0.291395201678207) { Status = true });
            pos.Add(new PosXYZ(111.49, -113.55, 0.263751549245471) { Status = true });
            pos.Add(new PosXYZ(111.49, -49.55, 0.274221476595328) { Status = true });


            displayControl1.UpdatePos(pos);
        }

        private void labelLeftFile_Click(object sender, EventArgs e)
        {

        }
    }
}