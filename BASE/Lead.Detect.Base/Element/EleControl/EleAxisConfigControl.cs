using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element
{
    public partial class EleAxisConfigControl : Form
    {
        private EleAxis _axis;
        private IMotionCard _motion;

        public EleAxisConfigControl()
        {
            InitializeComponent();
        }

        private void EleAxisConfigControl_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
        }


        public int LoadAxis(EleAxis axis)
        {
            var iRet = 0;

            tmrUpdateAxis.Stop();

            if (axis == null) return -1;
            _axis = axis;

            lbAxisName.Text = _axis.Name;

            if (DevPrimsManager.Instance.Prims.Count > 0)
            {
                comboBoxPrimDev.Items.AddRange(DevPrimsManager.Instance.Prims.FindAll(p => p is IMotionCard).Select(p => p.Name).ToArray());
            }

            if (string.IsNullOrEmpty(_axis.Driver))
                comboBoxPrimDev.Text = "";
            else
                comboBoxPrimDev.Text = _axis.Driver;


            tBoxAxisIdx.Text = _axis.AxisChannel.ToString();
            tBoxAxisLead.Text = _axis.AxisLead.ToString();
            cBoxAxisEnable.Checked = _axis.Enable;
            tBoxPosLimit.Text = _axis.PosCheckDI.ToString();
            cBoxPosLimitEnable.Checked = _axis.PosCheckDIEnable;
            tBoxNegLimit.Text = _axis.NegCheckDI.ToString();
            cBoxNegLimitEnable.Checked = _axis.NegCheckDIEnable;
            tBoxOrigin.Text = _axis.OriginCheckDI.ToString();
            cBoxOriginEnable.Checked = _axis.OriginCheckDIEnable;


            cmbHomeMode.SelectedIndex = _axis.HomeMode;
            cmbHomeDir.SelectedIndex = _axis.HomeDir;
            txtHomePraCurve.Text = _axis.HomeCurve.ToString();
            txtHomePraAcc.Text = _axis.HomeAcc.ToString();
            txtHomePraVm.Text = _axis.HomeVm.ToString();


            btnRun.BackColor = Color.DimGray;
            btnStop.BackColor = Color.DimGray;

            btnRun.PerformClick();

            return iRet;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var prim = DevPrimsManager.Instance.GetPrimByName(_axis.Driver);
            if (prim == null)
            {
                MessageBox.Show("没有找到名称为： " + _axis.Driver + " 的设备！");
                return;
            }

            if (_motion != prim) _motion = (IMotionCard) prim;

            tmrUpdateAxis.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrUpdateAxis.Stop();
        }

        private void tmrUpdateAxis_Tick(object sender, EventArgs e)
        {
            if (((IPrim) _motion).PrimConnStat == PrimConnState.Connected)
            {
                var selectAxis = _axis.AxisChannel;
                int index;
                bool status;
                int motionIoStatus, motionStatus;
                //刷新MotionIoStatus
                motionIoStatus = _motion.GetAxisMotionIOStatus(selectAxis);
                foreach (var label in grpMotionIo.Controls.OfType<Label>())
                    if (label.BorderStyle == BorderStyle.FixedSingle)
                    {
                        index = Convert.ToInt32(label.Tag);
                        status = (motionIoStatus & (1 << index)) != 0;
                        label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                    }

                //刷新MotionStatus
                motionStatus = _motion.GetAxisMotionStatus(selectAxis);
                foreach (var label in grpMotion.Controls.OfType<Label>())
                    if (label.BorderStyle == BorderStyle.FixedSingle)
                    {
                        index = Convert.ToInt32(label.Tag);
                        status = (motionStatus & (1 << index)) != 0;
                        label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                    }

                //刷新Feedback信息
                double value = 0;
                _motion.GetAxisCommandF(selectAxis, ref value);
                txtCommandPosition.Text = value.ToString("f0");

                _motion.GetAxisPositionF(selectAxis, ref value);
                txtFeedbackPosition.Text = value.ToString("f0");

                _motion.GetAxisPositionF(selectAxis, ref value);
                txtTargetPosition.Text = value.ToString("f0");

                _motion.GetAxisErrPositionF(selectAxis, ref value);
                txtErrorPosition.Text = value.ToString("f0");

                _motion.GetAxisCommandVelocityF(selectAxis, ref value);
                txtCommandVelocity.Text = value.ToString("f0");

                _motion.GetAxisFeedbackVelocityF(selectAxis, ref value);
                txtFeedbackVelocity.Text = value.ToString("f0");

                //刷新ServoOn, motionIoStatus第7位
                status = (motionIoStatus & (1 << 7)) != 0;
                lblServoOn.BackColor = status ? Color.LawnGreen : Color.DimGray;

                //刷新JOG正 和 JOG负
                var jogPostive = (motionStatus & (1 << 15)) != 0 && (motionStatus & (1 << 4)) != 0;
                var jogNegative = (motionStatus & (1 << 15)) != 0 && (motionStatus & (1 << 4)) == 0;
                lblJogPostive.BackColor = jogPostive ? Color.LawnGreen : Color.DimGray;
                lblJogNegative.BackColor = jogNegative ? Color.LawnGreen : Color.DimGray;

                lbAxisName.BackColor = Color.LawnGreen;
            }

            lbAxisName.BackColor = SystemColors.GradientActiveCaption;
        }

        #region  axis config

        private void tBoxAxisIdx_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxAxisIdx.Text))
            {
                var axisIdx = -1;
                int.TryParse(tBoxAxisIdx.Text, out axisIdx);

                if (axisIdx != -1) _axis.AxisChannel = axisIdx;
            }
        }

        private void cBoxAxisEnable_CheckedChanged(object sender, EventArgs e)
        {
            _axis.Enable = cBoxAxisEnable.Checked;
            tBoxAxisIdx.Enabled = cBoxAxisEnable.Checked;
        }

        private void tBoxAxisLead_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxAxisLead.Text))
            {
                var axislead = -1;
                int.TryParse(tBoxAxisLead.Text, out axislead);

                if (axislead != -1) _axis.AxisLead = axislead;
            }
        }

        private void cBoxOriginEnable_CheckedChanged(object sender, EventArgs e)
        {
            _axis.OriginCheckDIEnable = cBoxOriginEnable.Checked;
            tBoxOrigin.Enabled = cBoxOriginEnable.Checked;
        }

        private void tBoxOrigin_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxOrigin.Text))
            {
                var originCheckDI = -1;
                int.TryParse(tBoxOrigin.Text, out originCheckDI);

                if (originCheckDI != -1) _axis.OriginCheckDI = originCheckDI;
            }
        }

        private void tBoxNegLimit_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxNegLimit.Text))
            {
                var negCheckDI = -1;
                int.TryParse(tBoxNegLimit.Text, out negCheckDI);

                if (negCheckDI != -1) _axis.NegCheckDI = negCheckDI;
            }
        }

        private void cBoxNegLimitEnable_CheckedChanged(object sender, EventArgs e)
        {
            _axis.NegCheckDIEnable = cBoxNegLimitEnable.Checked;
            tBoxNegLimit.Enabled = cBoxNegLimitEnable.Checked;
        }

        private void tBoxPosLimit_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tBoxPosLimit.Text))
            {
                var posCheckDI = -1;
                int.TryParse(tBoxPosLimit.Text, out posCheckDI);

                if (posCheckDI != -1) _axis.PosCheckDI = posCheckDI;
            }
        }

        private void cBoxPosLimitEnable_CheckedChanged(object sender, EventArgs e)
        {
            _axis.PosCheckDIEnable = cBoxPosLimitEnable.Checked;
            tBoxPosLimit.Enabled = cBoxPosLimitEnable.Checked;
        }

        private void comboBoxPrimDev_TextChanged(object sender, EventArgs e)
        {
            _axis.Driver = comboBoxPrimDev.Text;
        }

        private void comboBoxPrimDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            _axis.Driver = comboBoxPrimDev.Text;
        }

        #endregion


        #region  axis motion

        private void lblServoOn_Click(object sender, EventArgs e)
        {
            if (((IPrim) _motion).PrimConnStat != PrimConnState.Connected)
            {
                MessageBox.Show("请先加载配置文件！");
                return;
            }

            var boardId = -1;
            var selectAxis = _axis.AxisChannel;
            var motionIoStatus = _motion.AxisMotionIOStatus(boardId, selectAxis);
            var status = (motionIoStatus & (1 << 7)) != 0;
            _motion.AxisSetEnable(boardId, selectAxis, !status);
        }

        private void btnAbsoluteMove_Click(object sender, EventArgs e)
        {
        }

        private void btnRelativeMove_Click(object sender, EventArgs e)
        {
            var boardId = -1;
            var selectAxis = _axis.AxisChannel;
            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praDec = Convert.ToDouble(txtPraDec.Text);
            var praVm = Convert.ToInt32(txtPraVm.Text);
            var pulse = Convert.ToInt32(txtRelativePulse.Text);

            _motion.AxisSetAcc(boardId, selectAxis, praAcc);
            _motion.AxisSetDec(boardId, selectAxis, praDec);

            if (((IPrim) _motion).PrimConnStat == PrimConnState.Connected)
            {
                var task = new Task(() =>
                {
                    _motion.AxisRelMove(boardId, selectAxis, pulse, praVm);

                    //等待Motion Down完成
                    var motionStatusMdn = 5;
                    while ((_motion.GetAxisMotionStatus(selectAxis) & (1 << motionStatusMdn)) == 0) Thread.Sleep(100);
                    MessageBox.Show("运动完成！");
                });
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件！");
            }
        }

        private void lblJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            var boardId = -1;
            var selectAxis = _axis.AxisChannel;

            _motion.AxisSetJogMode(boardId, selectAxis, 1);
            _motion.AxisSetJogDir(boardId, selectAxis, 1);
            _motion.AxisSetJogAcc(boardId, selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _motion.AxisSetJogDec(boardId, selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _motion.AxisSetJogMaxVel(boardId, selectAxis, Convert.ToDouble(txtPraVm.Text));
            _motion.AxisJogStart(boardId, selectAxis, 1);
        }

        private void lblJogNegative_MouseUp(object sender, MouseEventArgs e)
        {
            var boardId = -1;
            var selectAxis = _axis.AxisChannel;
            _motion.AxisJogStart(boardId, selectAxis, 0);
        }

        private void lblJogPostive_MouseDown(object sender, MouseEventArgs e)
        {
            var boardId = -1;
            var selectAxis = _axis.AxisChannel;

            _motion.AxisSetJogMode(boardId, selectAxis, 1);
            _motion.AxisSetJogDir(boardId, selectAxis, 0);
            _motion.AxisSetJogAcc(boardId, selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _motion.AxisSetJogDec(boardId, selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _motion.AxisSetJogMaxVel(boardId, selectAxis, Convert.ToDouble(txtPraVm.Text));
            _motion.AxisJogStart(boardId, selectAxis, 1);
        }

        private void lblJogPostive_MouseUp(object sender, MouseEventArgs e)
        {
            var boardId = -1;
            var selectAxis = _axis.AxisChannel;
            _motion.AxisJogStart(boardId, selectAxis, 0);
        }

        #endregion


        #region home

        private void cmbHomeDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            _axis.HomeDir = cmbHomeDir.SelectedIndex;
        }

        private void cmbHomeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _axis.HomeMode = cmbHomeMode.SelectedIndex;
        }

        private void txtHomePraAcc_TextChanged(object sender, EventArgs e)
        {
            var homeAcc = Convert.ToDouble(txtHomePraAcc.Text);
            _axis.HomeAcc = homeAcc;
        }

        private void txtHomePraCurve_TextChanged(object sender, EventArgs e)
        {
            var homeCurve = Convert.ToDouble(txtHomePraCurve.Text);
            _axis.HomeCurve = homeCurve;
        }

        private void txtHomePraVm_TextChanged(object sender, EventArgs e)
        {
            var homeVm = Convert.ToDouble(txtHomePraVm.Text);
            _axis.HomeVm = homeVm;
        }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            var boardId = -1;
            var selectAxis = _axis.AxisChannel;
            var homeMode = cmbHomeMode.SelectedIndex;
            var homeDir = cmbHomeDir.SelectedIndex;
            var praCurve = Convert.ToDouble(txtHomePraCurve.Text);
            var praAcc = Convert.ToDouble(txtHomePraAcc.Text);
            var praVm = Convert.ToDouble(txtHomePraVm.Text);


            if (((IPrim) _motion).PrimConnStat == PrimConnState.Connected && homeMode >= 0 && homeDir >= 0)
            {
                var task = new Task(() => StartHoming(boardId, selectAxis, homeMode, homeDir, praCurve, praAcc, praVm));
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件并选择回零模式！");
            }
        }

        private void StartHoming(int boardId, int axisId, int homeMode, int homeDir, double praCurve, double praAcc, double praVm)
        {
            //
            // 1. Select home mode and axis home parameters 

            _motion.AxisSetHomeMode(boardId, axisId, homeMode); // Set home mode
            _motion.AxisSetHomeDir(boardId, axisId, homeDir); // Set home direction
            _motion.AxisSetHomeCurve(boardId, axisId, praCurve); // Set acceleration paten (T-curve)
            _motion.AxisSetHomeAcc(boardId, axisId, praAcc); // Set homing acceleration rate
            _motion.AxisSetHomeMaxVel(boardId, axisId, praVm); // Set homing maximum velocity.
            _motion.AxisSetHomeVO(boardId, axisId, praVm / 5); // Set homing VO speed
            _motion.AxisSetHomeEZA(boardId, axisId, 0); // Set EZ signal alignment (yes or no)
            _motion.AxisSetHomeShift(boardId, axisId, 0); // Set home position shfit distance. 
            _motion.AxisSetHomePos(boardId, axisId, 0); // Set final home position. 
            _motion.AxisHomeMove(boardId, axisId); // Start Axis Home Move

            //servo on
            _motion.AxisSetEnable(boardId, axisId, true);
            Thread.Sleep(500); // Wait stable.


            // 2. Start home move
            _motion.AxisHomeMove(boardId, axisId); // Start Axis Home Move

            int motionStatusCstp = 0, motionStatusAstp = 16;
            while ((_motion.GetAxisMotionStatus(axisId) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);
            Thread.Sleep(500);

            if ((_motion.GetAxisMotionStatus(axisId) & (1 << motionStatusAstp)) == 0)
                MessageBox.Show("轴" + axisId + "回零成功！");
            else
                MessageBox.Show("轴" + axisId + "回零失败！");
        }

        #endregion
    }
}