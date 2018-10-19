using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.ViewTaskDefault
{
    public partial class ViewTaskDefaultConfigControl : UserControl
    {
        private DefaultConfig defaultConfig;

        public ViewTaskDefaultConfigControl()
        {
            InitializeComponent();
        }

        public ViewTaskDefaultConfigControl(DefaultConfig config)
        {
            InitializeComponent();
            defaultConfig = config;
        }

        private void ViewTaskDefaultConfigControl_Load(object sender, EventArgs e)
        {
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
        }
    }
}