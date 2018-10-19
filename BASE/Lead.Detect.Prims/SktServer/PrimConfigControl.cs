using System;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimSktServer
{
    public partial class PrimConfigControl : UserControl
    {
        private ServerConfig _config;
        private readonly PrimSocketServer _primSocketServer;

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public PrimConfigControl(PrimSocketServer primSocket)
        {
            InitializeComponent();
            _primSocketServer = primSocket;
            _config = _primSocketServer._config;
        }


        public string AglName
        {
            set { tTaskBoxName.Text = value; }
        }

        public void AddPoint(string point, int mode)
        {
            if (cboIpPort.InvokeRequired)
            {
                var addPoint = new Action<string, int>(AddPoint);
                cboIpPort.Invoke(addPoint, point, mode);
            }
            else
            {
                if (mode == -1)
                    cboIpPort.Items.Clear();
                else
                    cboIpPort.Items.Add(point);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cBoxNotifyMode.SelectedIndex == 0)
                _primSocketServer.NotifyMode = SktNotifyMode.DataEvent;
            else if (cBoxNotifyMode.SelectedIndex == 1) _primSocketServer.NotifyMode = SktNotifyMode.DataQueue;

            if (!string.IsNullOrEmpty(tBoxHeartInfo.Text)) _primSocketServer.HeartInfo = tBoxHeartInfo.Text;


            if (!string.IsNullOrEmpty(tBoxPort.Text)) _primSocketServer.Port = tBoxPort.Text;

            if (!string.IsNullOrEmpty(tBoxIP.Text)) _primSocketServer.BindIp = tBoxIP.Text;

            if (!string.IsNullOrEmpty(tBoxRevQueueCnt.Text)) _primSocketServer.RevQueueCnt = int.Parse(tBoxRevQueueCnt.Text);

            if (!string.IsNullOrEmpty(tBoxSendQueueCnt.Text)) _primSocketServer.SendQueueCnt = int.Parse(tBoxSendQueueCnt.Text);

            _primSocketServer.NetName = tBoxNetName.Text;
            _primSocketServer.Name = tTaskBoxName.Text;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _primSocketServer.IPrimInit();
            var result = "";
            _primSocketServer.IPrimConnect(ref result);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _primSocketServer.IPrimStart();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxMsg.Text) && !string.IsNullOrEmpty(cboIpPort.Text))
            {
                var port = cboIpPort.Text;
                _primSocketServer.SendInfoByPoint(port, tBoxMsg.Text);
            }
        }

        private void cBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            _primSocketServer.PrimDebug = cBoxDebug.Checked;
        }

        private void cBoxHeart_CheckedChanged(object sender, EventArgs e)
        {
            _primSocketServer.HeartFlag = cBoxHeart.Checked;
        }

        private void cBoxNotifyMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxNotifyMode.SelectedIndex == 0)
                _primSocketServer.NotifyMode = SktNotifyMode.DataEvent;
            else if (cBoxNotifyMode.SelectedIndex == 1) _primSocketServer.NotifyMode = SktNotifyMode.DataQueue;
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
            if (_primSocketServer.NotifyMode == SktNotifyMode.DataEvent)
                cBoxNotifyMode.SelectedIndex = 0;
            else if (_primSocketServer.NotifyMode == SktNotifyMode.DataQueue) cBoxNotifyMode.SelectedIndex = 1;


            if (!string.IsNullOrEmpty(_primSocketServer.HeartInfo)) tBoxHeartInfo.Text = _primSocketServer.HeartInfo;

            if (!string.IsNullOrEmpty(_primSocketServer.Port)) tBoxPort.Text = _primSocketServer.Port;

            if (!string.IsNullOrEmpty(_primSocketServer.BindIp)) tBoxIP.Text = _primSocketServer.BindIp;
            tBoxSendQueueCnt.Text = _primSocketServer.SendQueueCnt.ToString();
            tBoxRevQueueCnt.Text = _primSocketServer.RevQueueCnt.ToString();
            if (!string.IsNullOrEmpty(_primSocketServer.NetName)) tBoxNetName.Text = _primSocketServer.NetName;
            tTaskBoxName.Text = _primSocketServer.Name;
        }


        public void SetPrimConnState(PrimConnState state)
        {
            if (tBoxConnState.InvokeRequired)
            {
                var update = new UpdatePrimConnState(SetPrimConnState);
                Invoke(update, state);
            }
            else
            {
                switch (state)
                {
                    case PrimConnState.UnConnected:
                        tBoxConnState.BackColor = Color.Khaki;
                        tBoxConnState.Text = "UnConnected";
                        break;
                    case PrimConnState.Connected:
                        tBoxConnState.BackColor = Color.LightGreen;
                        tBoxConnState.Text = "Connected";
                        break;
                    case PrimConnState.Other:
                        tBoxConnState.BackColor = Color.Khaki;
                        tBoxConnState.Text = "UnConnected";
                        break;
                }
            }
        }

        public void SetPrimRunState(PrimRunState state)
        {
            if (tBoxRunState.InvokeRequired)
            {
                var update = new UpdatePrimRunState(SetPrimRunState);
                Invoke(update, state);
            }
            else
            {
                switch (state)
                {
                    case PrimRunState.Idle:
                        tBoxRunState.BackColor = Color.Khaki;
                        tBoxRunState.Text = "Idle";
                        break;
                    case PrimRunState.Running:
                        tBoxRunState.BackColor = Color.LightGreen;
                        tBoxRunState.Text = "Running";
                        break;
                    case PrimRunState.Err:
                        tBoxRunState.BackColor = Color.SandyBrown;
                        tBoxRunState.Text = "Err";
                        break;
                    case PrimRunState.Other:
                        tBoxRunState.BackColor = Color.Khaki;
                        tBoxRunState.Text = "Idle";
                        break;
                }
            }
        }

        public void ShowRevMsg(string msg)
        {
            if (tBoxInfo.InvokeRequired)
            {
                var updateMsg = new UpdateRevMag(ShowRevMsg);
                tBoxInfo.Invoke(updateMsg, msg);
            }
            else
            {
                tBoxInfo.AppendText(msg + "\r\n");
            }
        }

        private void tBoxHeartInfo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxHeartInfo.Text)) _primSocketServer.HeartInfo = tBoxHeartInfo.Text;
        }

        private void tBoxIP_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxIP.Text)) _primSocketServer.BindIp = tBoxIP.Text;
        }

        private void tBoxNetName_TextChanged(object sender, EventArgs e)
        {
            _primSocketServer.NetName = tBoxNetName.Text;
        }

        private void tBoxPort_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxPort.Text)) _primSocketServer.Port = tBoxPort.Text;
        }

        private void tBoxRevQueueCnt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxRevQueueCnt.Text)) _primSocketServer.RevQueueCnt = int.Parse(tBoxRevQueueCnt.Text);
        }

        private void tBoxSendQueueCnt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxSendQueueCnt.Text)) _primSocketServer.SendQueueCnt = int.Parse(tBoxSendQueueCnt.Text);
        }
    }
}