using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.MachineUtilityLib.UtilViews;
using Lead.Detect.ThermoAOI2.MachineA.UserDefine;
using Lead.Detect.ThermoAOI2.MachineA.View;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI2.MachineA
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

            var sc = Screen.GetWorkingArea(this);
            Size = new Size(sc.Width / 2, sc.Height);
            Location = new Point(sc.Width / 2, 0);
            //WindowState = FormWindowState.Maximized;
            BringToFront();

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
                try
                {
                    Machine.Ins.Stop();
                    _devConfigForm.FrameworkDeactivate();
                    Application.DoEvents();
                    Thread.Sleep(800);
                    Application.DoEvents();
                }
                catch
                {

                }
                finally
                {
                    e.Cancel = false;
                }
            }
        }


        #region  menu item


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

        private void 数据文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @".\Data");
        }

        private void 更改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cf = new ChangePasswordForm();
            cf.ShowDialog();
        }
    }
}