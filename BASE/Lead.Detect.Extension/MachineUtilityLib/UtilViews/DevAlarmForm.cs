﻿using System;
using System.Drawing;
using Lead.Detect.FrameworkExtension;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    public partial class DevAlarmForm : DockContent
    {
        public DockPanel MainPanel;

        public DevAlarmForm()
        {
            InitializeComponent();
        }


        private void DevAlarmForm_Load(object sender, EventArgs e)
        {
            HideOnClose = true;
        }
        public void ShowAlarm(string msg, LogLevel level)
        {
            if (richTextBoxAlarm.InvokeRequired)
            {
                BeginInvoke((Action<string, LogLevel>)ShowAlarm, msg, level);
            }
            else
            {
                try
                {
                    switch (level)
                    {
                        case LogLevel.Warning:
                            if (MainPanel != null) Show(MainPanel, DockState.Document);
                            richTextBoxAlarm.SelectionColor = Color.LightCoral;
                            break;
                        case LogLevel.Error:
                        case LogLevel.Fatal:
                            if (MainPanel != null) Show(MainPanel, DockState.Document);
                            richTextBoxAlarm.SelectionColor = Color.Red;
                            break;
                        default:
                            richTextBoxAlarm.SelectionColor = Color.LightCoral;
                            return;
                    }

                    richTextBoxAlarm.AppendText($"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")} - {level} - {msg}\r\n");
                    richTextBoxAlarm.ScrollToCaret();
                }
                catch (Exception)
                {
                    
                }

            }
        }

        private void DevAlarmForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DockState = DockState.Hidden;
            e.Cancel = true;
        }
    }
}