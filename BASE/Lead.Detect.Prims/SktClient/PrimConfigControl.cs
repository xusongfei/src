using System;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.PrimSktClient
{
    public delegate void UpdatePrimConnState(PrimConnState state);

    public delegate void UpdatePrimRunState(PrimRunState state);

    public delegate void UpdateRevMag(string msg);

    public partial class PrimConfigControl : UserControl
    {
        private ClientConfig _config;
        private readonly PrimSocketClient _primSocketClient;

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public PrimConfigControl(PrimSocketClient primSocket)
        {
            InitializeComponent();
            _primSocketClient = primSocket;
            _config = _primSocketClient._config;
        }


        public string AglName
        {
            set { tTaskBoxName.Text = value; }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cBoxNotifyMode.SelectedIndex == 0)
                _primSocketClient.NotifyMode = SktNotifyMode.DataEvent;
            else if (cBoxNotifyMode.SelectedIndex == 1) _primSocketClient.NotifyMode = SktNotifyMode.DataQueue;

            if (!string.IsNullOrEmpty(tBoxReConnCnt.Text)) _primSocketClient.ReconnectCnt = int.Parse(tBoxReConnCnt.Text);

            if (!string.IsNullOrEmpty(tBoxHeartTime.Text)) _primSocketClient.HeartInfoCycleTime = int.Parse(tBoxHeartTime.Text);

            if (!string.IsNullOrEmpty(tBoxHeartInfo.Text)) _primSocketClient.HeartInfo = tBoxHeartInfo.Text;

            _primSocketClient.HeartFlag = cBoxHeart.Checked;

            if (!string.IsNullOrEmpty(tBoxPort.Text)) _primSocketClient.Port = tBoxPort.Text;

            if (!string.IsNullOrEmpty(tBoxIP.Text)) _primSocketClient.DstIp = tBoxIP.Text;

            if (!string.IsNullOrEmpty(tBoxRevQueueCnt.Text)) _primSocketClient.RevQueueCnt = int.Parse(tBoxRevQueueCnt.Text);

            if (!string.IsNullOrEmpty(tBoxSendQueueCnt.Text)) _primSocketClient.SendQueueCnt = int.Parse(tBoxSendQueueCnt.Text);

            _primSocketClient.NetName = tBoxNetName.Text;
            _primSocketClient.Name = tTaskBoxName.Text;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            _primSocketClient.IPrimInit();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var result = "";
            _primSocketClient.IPrimConnect(ref result);
            _primSocketClient.IPrimStart();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxMsg.Text)) _primSocketClient.SendInfo("", tBoxMsg.Text);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _primSocketClient.IPrimStop();
        }

        private void cBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            _primSocketClient.PrimDebug = cBoxDebug.Checked;
        }

        private void cBoxHeart_CheckedChanged(object sender, EventArgs e)
        {
            _primSocketClient.HeartFlag = cBoxHeart.Checked;
        }

        private void cBoxNotifyMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxNotifyMode.SelectedIndex == 0)
                _primSocketClient.NotifyMode = SktNotifyMode.DataEvent;
            else if (cBoxNotifyMode.SelectedIndex == 1) _primSocketClient.NotifyMode = SktNotifyMode.DataQueue;
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
            if (_primSocketClient.NotifyMode == SktNotifyMode.DataEvent)
                cBoxNotifyMode.SelectedIndex = 0;
            else if (_primSocketClient.NotifyMode == SktNotifyMode.DataQueue) cBoxNotifyMode.SelectedIndex = 1;

            tBoxReConnCnt.Text = _primSocketClient.ReconnectCnt.ToString();

            tBoxHeartTime.Text = _primSocketClient.HeartInfoCycleTime.ToString();

            if (!string.IsNullOrEmpty(_primSocketClient.HeartInfo)) tBoxHeartInfo.Text = _primSocketClient.HeartInfo;

            cBoxHeart.Checked = _primSocketClient.HeartFlag;

            if (!string.IsNullOrEmpty(_primSocketClient.Port)) tBoxPort.Text = _primSocketClient.Port;

            if (!string.IsNullOrEmpty(_primSocketClient.DstIp)) tBoxIP.Text = _primSocketClient.DstIp;

            tBoxSendQueueCnt.Text = _primSocketClient.SendQueueCnt.ToString();

            tBoxRevQueueCnt.Text = _primSocketClient.RevQueueCnt.ToString();

            if (!string.IsNullOrEmpty(_primSocketClient.NetName)) tBoxNetName.Text = _primSocketClient.NetName;
            tTaskBoxName.Text = _primSocketClient.Name;
        }

        public void SetPrimConnState(PrimConnState state)
        {
            if (tBoxConnState.InvokeRequired)
            {
                var update = new Action<PrimConnState>(SetPrimConnState);
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

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void tBoxHeartInfo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxHeartInfo.Text)) _primSocketClient.HeartInfo = tBoxHeartInfo.Text;
        }

        private void tBoxHeartTime_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxHeartTime.Text)) _primSocketClient.HeartInfoCycleTime = int.Parse(tBoxHeartTime.Text);
        }

        private void tBoxInfo_TextChanged(object sender, EventArgs e)
        {
        }

        private void tBoxIP_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxIP.Text)) _primSocketClient.DstIp = tBoxIP.Text;
        }

        private void tBoxNetName_TextChanged(object sender, EventArgs e)
        {
            _primSocketClient.NetName = tBoxNetName.Text;
        }

        private void tBoxPort_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxPort.Text)) _primSocketClient.Port = tBoxPort.Text;
        }

        private void tBoxReConnCnt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxReConnCnt.Text)) _primSocketClient.ReconnectCnt = int.Parse(tBoxReConnCnt.Text);
        }

        private void tBoxRevQueueCnt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxRevQueueCnt.Text)) _primSocketClient.RevQueueCnt = int.Parse(tBoxRevQueueCnt.Text);
        }

        private void tBoxSendQueueCnt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxSendQueueCnt.Text)) _primSocketClient.SendQueueCnt = int.Parse(tBoxSendQueueCnt.Text);
        }

        private void tTaskBoxName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {
        }
    }
}