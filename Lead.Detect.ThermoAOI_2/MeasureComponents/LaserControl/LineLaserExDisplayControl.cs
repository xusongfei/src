using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;

namespace Lead.Detect.MeasureComponents.LaserControl
{
    public partial class LineLaserExDisplayControl : UserControl
    {
        public LineLaserExDisplayControl()
        {
            InitializeComponent();

            tabPageConfig.Visible = false;
        }


        public void UpdateOperationLevel(OperationLevel level)
        {
            tabPageConfig.Visible = true;
        }


        public void BindComponent(ILineLaserEx lineLaser)
        {
            tabPageDisplay.Text = lineLaser.Name;
            lineLaser.DisplayResultEvent += UpdateAfterTrigger;
        }

        public void UpdateAfterTrigger(Image image)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Image>(UpdateAfterTrigger), image);
            }
            else
            {
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
