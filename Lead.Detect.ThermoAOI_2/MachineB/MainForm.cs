using System;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine;
using Lead.Detect.ThermoAOI2.MachineB.View;
using MachineUtilityLib.UtilViews;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI2.MachineB
{
    public partial class MainForm : Form
    {
        private DevMainForm _devMainForm;
        private DevAlarmForm _devAlarmForm;
        private DevLogForm _devLogForm;
        private DevConfigForm _devConfigForm;

        private VersionForm _verForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            if (!FrameworkExtenion.IsSimulate)
            {
                WindowState = FormWindowState.Maximized;
                BringToFront();
            }

            //Text = MotionWrapperExtension.IsSimulate ? "仿真模式" : Machine.Machine.Ins.Settings.Name;
            Text = Machine.Ins.Settings.Name;
            Text = $"{Machine.Ins.Settings.Name} {Machine.Ins.Settings.Version}";

            _devAlarmForm = new DevAlarmForm()
            {
                MainPanel = mainPanel,
            };
            _devLogForm = new DevLogForm();
            _devConfigForm = new DevConfigForm();
            _devMainForm = new DevMainForm
            {
                MainPanel = mainPanel,
                DevConfigForm = _devConfigForm,
                DevAlarmForm = _devAlarmForm,
                DevLogForm = _devLogForm,
            };


            _devMainForm.Show(mainPanel, DockState.Document);

            _devLogForm.Show(mainPanel, DockState.Document);
            _devLogForm.Hide();
            _devAlarmForm.Show(mainPanel, DockState.Document);
            _devAlarmForm.Hide();
            _devConfigForm.Show(mainPanel, DockState.Document);
            _devConfigForm.Hide();


            _verForm = new VersionForm();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason != CloseReason.UserClosing)
            //    return;

            if (MessageBox.Show("是否退出当前程序", "重要消息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                Machine.Ins.Stop();
                _devConfigForm.FrameworkDeactivate();
                Application.DoEvents();
                Thread.Sleep(300);
                Application.DoEvents();
                e.Cancel = false;
            }
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _devConfigForm.FrameworkDeactivate();
            Application.DoEvents();
            Thread.Sleep(100);
            Application.DoEvents();
        }
        #region  menu item

        private void deviceEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnvironmentManager.Ins.ShowPrimsEditor();
        }


        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Machine.Ins.Save();
                MessageBox.Show("保存成功.", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void versionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _verForm?.ShowDialog();
        }

        #endregion


        #region  project view menu


        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _devMainForm?.Show(mainPanel, DockState.Document);
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _devLogForm.Show(mainPanel, DockState.Document);
        }

        private void 报警ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _devAlarmForm.Show(mainPanel, DockState.Document);
        }

        private void 显示全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _devLogForm.Show(mainPanel, DockState.Document);
            _devAlarmForm.Show(mainPanel, DockState.Document);
        }

        #endregion

    }
}