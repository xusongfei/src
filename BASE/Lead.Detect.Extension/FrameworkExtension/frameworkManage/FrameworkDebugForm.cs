using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    public partial class FrameworkDebugForm : Form
    {
        public FrameworkDebugForm()
        {
            InitializeComponent();
        }

        private void FrameworkDebugForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
        }


        public void UpdateLog(string log)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(UpdateLog), log);
            }
            else
            {
                richTextBox1.AppendText($"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}: {log}\r\n");
            }
        }
    }
}