using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.MeasureComponents.LaserControl
{
    public partial class LineLaserExDebugControl : UserControl
    {

        public ILineLaserEx LineLaserEx;

        public LineLaserExDebugControl()
        {
            InitializeComponent();


        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            LineLaserEx?.Connect();
        }
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            LineLaserEx?.Disconnect();
        }

        private void buttonTrigger_Click(object sender, EventArgs e)
        {
            var ret = LineLaserEx.Trigger(string.Empty);
            richTextBox1.Text = string.Join("\r\n", ret.Select(r => $"{string.Join(",", r.Select(val => val.ToString("F3")))}"));
        }

        private void LineLaserExDebugControl_Load(object sender, EventArgs e)
        {

            groupBox1.Text = LineLaserEx?.Name;
        }

        private void buttonGetResult_Click(object sender, EventArgs e)
        {
            var ret = LineLaserEx.GetResult();
            richTextBox1.Text = string.Join("\r\n", ret.Select(r => $"{string.Join(",", r.Select(val => val.ToString("F3")))}"));
        }
    }
}
