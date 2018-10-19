using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.PrimIOAdlink.ADLink;

namespace Lead.Detect.PrimIOAdlink
{
    public delegate void UpdatePrimConnState(PrimConnState state);

    public delegate void UpdatePrimRunState(PrimRunState state);

    public partial class PrimConfigControl : UserControl
    {
        private readonly ADLinkConfig _config;
        private readonly PrimIO7432 _primADLink;

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public PrimConfigControl(PrimIO7432 prim)
        {
            InitializeComponent();
            _primADLink = prim;
            _config = _primADLink._config;
        }

        public string ShowTitle
        {
            set { lbTitle.Text = value; }
        }

        public string ADLinkName
        {
            set { tBoxADLinkName.Text = value; }
        }


        private void btnInitial_Click(object sender, EventArgs e)
        {
            if (_primADLink == null) return;
            var ret = _primADLink.IPrimInit();
            btnInitial.BackColor = _primADLink._isInitialed ? Color.LawnGreen : Color.DimGray;
        }

        private void DoPress_Click(object sender, EventArgs e)
        {
            var doValue = 0;
            _primADLink.ReadMultiDOutputFromAPI(_config.DevIndex, 0, ref doValue);

            var label = (Label) sender;
            var index = Convert.ToInt32(label.Tag);
            var status = (doValue >> index) & 1;

            if (status == 0)
                status = 1;
            else
                status = 0;
            DASK.DO_WriteLine((ushort) _config.DevIndex, 0, (ushort) index, (ushort) status);
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
            ADLinkName = _primADLink.Name;
            txtCardId.Text = _config.DevIndex.ToString();
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
                switch (_primADLink.PrimConnStat)
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
                switch (_primADLink.PrimRunStat)
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

        private void tBoxADLinkName_TextChanged(object sender, EventArgs e)
        {
            _config.Name = tBoxADLinkName.Text;
        }

        private void tmrUpdateUI_Tick(object sender, EventArgs e)
        {
            if (_primADLink._isInitialed)
            {
                int index;
                bool status;

                var diValue = 0;
                _primADLink.ReadMultiDInputFromAPI(_config.DevIndex, 0, ref diValue);
                foreach (var label in grpDi.Controls.OfType<Label>())
                    if (label.BorderStyle == BorderStyle.FixedSingle)
                    {
                        index = Convert.ToInt32(label.Tag);
                        status = (diValue & (1 << index)) != 0;
                        label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                    }

                var doValue = 0;
                _primADLink.ReadMultiDOutputFromAPI(_config.DevIndex, 0, ref doValue);
                foreach (var label in grpDo.Controls.OfType<Label>())
                    if (label.BorderStyle == BorderStyle.FixedSingle)
                    {
                        index = Convert.ToInt32(label.Tag);
                        status = (doValue & (1 << index)) != 0;
                        label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                    }
            }
        }

        private void txtCardId_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCardId.Text)) return;
            _config.DevIndex = Convert.ToInt32(txtCardId.Text);
        }
    }
}