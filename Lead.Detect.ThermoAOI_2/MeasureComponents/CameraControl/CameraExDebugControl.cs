using System;
using System.Windows.Forms;

namespace Lead.Detect.MeasureComponents.CameraControl
{
    public partial class CameraExDebugControl : UserControl
    {

        public ICameraEx Camera;

        public CameraExDebugControl()
        {
            InitializeComponent();

            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (Camera != null)
            {
                pictureBox1.Image = Camera.GrabOne();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }


        private void buttonGrabOne_Click(object sender, EventArgs e)
        {
            if (Camera != null)
            {
                pictureBox1.Image = Camera.GrabOne();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        private bool _isContinuous = false;
        private Timer _timer = new Timer();
        private void buttonContinuous_Click(object sender, EventArgs e)
        {
            if (Camera == null)
            {
                return;
            }

            if (!_isContinuous)
            {
                _isContinuous = true;

                _timer.Interval = 100;
                _timer.Start();

                buttonContinuous.Text = "连续采集停止";
            }
            else
            {
                _timer.Stop();
                _isContinuous = false;
                buttonContinuous.Text = "连续采集开始";
            }

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Camera?.Test();

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Camera?.Connect();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Camera?.Disconnect();
        }
    }
}
