﻿using System;
using System.Drawing;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.Helper;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.ThermoAOI.View
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
                switch (level)
                {
                    case LogLevel.Warning:
                        if (MainPanel != null) Show(MainPanel, DockState.Document);
                        richTextBoxAlarm.ForeColor = Color.Gold;
                        break;
                    case LogLevel.Error:
                    case LogLevel.Fatal:
                        if (MainPanel != null) Show(MainPanel, DockState.Document);
                        richTextBoxAlarm.ForeColor = Color.Red;
                        break;
                    default:
                        return;
                }

                richTextBoxAlarm.AppendText($"{DateTime.Now.ToString("yyyyMMdd-HHmmss.ff")}-{level}-{msg}\r\n");
                richTextBoxAlarm.ScrollToCaret();
            }
        }

        private void DevAlarmForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DockState = DockState.Hidden;
            e.Cancel = true;
        }
    }
}