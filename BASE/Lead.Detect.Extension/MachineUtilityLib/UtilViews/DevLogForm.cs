using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using WeifenLuo.WinFormsUI.Docking;

namespace MachineUtilityLib.UtilViews
{
    public partial class DevLogForm : DockContent
    {
        public DevLogForm()
        {
            InitializeComponent();
        }

        private void DevOperateForm_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
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
                    });
                    tabControl1.TabPages[tab].Controls.Add(richTextBoxs[tab]);
                }
                else
                {
                    var tbox = richTextBoxs[tab];
                    if (tbox.TextLength > 20000)
                    {
                        tbox.Clear();
                    }

                    var log = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}[{level}]:{msg}\r\n";

                    var clr = Color.Green;
                    switch (level)
                    {

                        case LogLevel.Fatal:
                            clr = Color.Red;
                            break;
                        case LogLevel.Error:
                            clr = Color.LightCoral;
                            break;
                        case LogLevel.Warning:
                            clr = Color.Yellow;
                            break;
                        case LogLevel.Info:
                            clr = Color.Black;
                            break;
                        default:
                            clr = Color.Green;
                            break;
                    }

                    tbox.SelectionColor = clr;
                    tbox.AppendText(log);
                    tbox.ScrollToCaret();
                }
            }
        }


    }
}