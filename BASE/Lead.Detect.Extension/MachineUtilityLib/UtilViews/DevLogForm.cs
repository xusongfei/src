using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    public partial class DevLogForm : DockContent
    {
        public DevLogForm()
        {
            InitializeComponent();
        }

        private void DevOperateForm_Load(object sender, EventArgs e)
        {
            HideOnClose = true;
        }

        private void DevLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }


        private Dictionary<string, RichTextBox> richTextBoxs = new Dictionary<string, RichTextBox>();


        public void UpdateLog(string tab, string msg, LogLevel level)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, string, LogLevel>(UpdateLog), msg, level);
            }
            else
            {
                if (tabControl1.TabPages[tab] == null)
                {
                    tabControl1.TabPages.Add(tab, tab);
                    richTextBoxs.Add(tab, new RichTextBox()
                    {
                        Dock = DockStyle.Fill,
                        ReadOnly = true,
                        WordWrap = false,
                    });
                    tabControl1.TabPages[tab].Controls.Add(richTextBoxs[tab]);
                }
                else
                {
                    try
                    {
                        var tbox = richTextBoxs[tab];
                        if (tbox.TextLength > 20000)
                        {
                            tbox.Clear();
                        }

                        var log = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")} [{level}]: {msg}\r\n";

                        var clr = Color.Green;
                        switch (level)
                        {
                            case LogLevel.Fatal:
                                clr = Color.Red;
                                break;
                            case LogLevel.Error:
                                clr = Color.Red;
                                break;
                            case LogLevel.Warning:
                                clr = Color.LightCoral;
                                break;
                            case LogLevel.Info:
                                clr = Color.Green;
                                break;
                            case LogLevel.Debug:
                                clr = Color.Blue;
                                break;
                            default:
                                clr = Color.Blue;
                                break;
                        }

                        tbox.SelectionColor = clr;
                        tbox.AppendText(log);
                        tbox.ScrollToCaret();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void 打开日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = tabControl1.SelectedTab;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Log", tab.Text);
                var logFile = DateTime.Now.ToString("yyyyMMdd") + ".log";

                System.Diagnostics.Process.Start("notepad++.exe", Path.Combine(dir, logFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开日志失败 {ex.Message}！");
            }
        }

        private void 打开日志文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var tab = tabControl1.SelectedTab;
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Log", tab.Text);

                System.Diagnostics.Process.Start("explorer.exe", dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开日志失败 {ex.Message}！");
            }
        }
    }
}