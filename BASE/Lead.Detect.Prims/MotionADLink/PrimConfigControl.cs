using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.PrimMotionADLink.ADLink;

namespace Lead.Detect.PrimMotionADLink
{
    public delegate void UpdatePrimConnState(PrimConnState state);

    public delegate void UpdatePrimRunState(PrimRunState state);

    public partial class PrimConfigControl : UserControl
    {
        private int _bufTotalPoint = 0;
        private int _bufFreeSpace = 0;
        private int _bufUsageSpace = 0;
        private int _bufRunningCnt = 0;
        private readonly ADLinkConfig _config;
        private readonly PrimADLink _primADLink;
        private bool _ptEnabled = false;

        private int _selectAxis;

        //private uint runningCntLast = 0;

        public PrimConfigControl()
        {
            InitializeComponent();
        }

        public PrimConfigControl(PrimADLink prim)
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

        private void btnAbsoluteMove_Click(object sender, EventArgs e)
        {
        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtXmlFilename.Text = openFileDialog.FileName;

                if (string.IsNullOrEmpty(txtXmlFilename.Text)) return;

                if (!File.Exists(txtXmlFilename.Text))
                {
                    MessageBox.Show("选择文件不存在");
                    return;
                }

                _config.ConfigFilePath = txtXmlFilename.Text;
            }
        }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            var boardId = Convert.ToInt32(txtCardId.Text);
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var homeMode = cmbHomeMode.SelectedIndex;
            var homeDir = cmbHomeDir.SelectedIndex;
            var praCurve = Convert.ToDouble(txtPraCurve.Text);
            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praVm = Convert.ToDouble(txtPraVm.Text);


            if (_primADLink._isInitialed && homeMode >= 0 && homeDir >= 0)
            {
                var task = new Task(() => StartHoming(boardId, _selectAxis, homeMode, homeDir, praCurve, praAcc, praVm));
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件并选择回零模式！");
            }
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            if (_primADLink == null) return;

            var ret = _primADLink.IPrimInit();

            btnInitial.BackColor = _primADLink._isInitialed ? Color.LawnGreen : Color.DimGray;
        }

        private void btnLineTrigTest_Click(object sender, EventArgs e)
        {
            int motionStatusCstp = 0, motionStatusAstp = 16;
            var boardId = Convert.ToInt32(txtCardId.Text);

            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praDec = Convert.ToDouble(txtPraDec.Text);
            APS168.APS_set_axis_param_f(0, (int)APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(0, (int)APS_Define.PRA_DEC, praDec); // Set deceleration rate

            //回起始点
            APS168.APS_absolute_move(0, -220000, 100000);
            APS168.APS_absolute_move(1, -15000, 100000);
            APS168.APS_absolute_move(2, 0, 10000);

            while ((APS168.APS_motion_status(0) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);

            if ((APS168.APS_motion_status(0) & (1 << motionStatusAstp)) == 0)
            {
            }

            APS168.APS_absolute_move(0, -62000, 50000);

            while ((APS168.APS_motion_status(0) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);

            if ((APS168.APS_motion_status(0) & (1 << motionStatusAstp)) == 0)
            {
            }

            //关闭触发
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG_EN, 0);
            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));
            //Source Encoder0
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TCMP0_SRC, 0);
            //Bi-direction(No direction)
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TCMP0_DIR, 1);
            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG0_SRC, Convert.ToInt32("11000", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG0_PWD, 2000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG0_LOGIC, 0);

            //重置触发计数
            APS168.APS_reset_trigger_count(boardId, 0);
            APS168.APS_reset_trigger_count(boardId, 1);

            APS168.APS_set_trigger_linear(boardId, 0, -61000, 900, 11);
            APS168.APS_relative_move(0, 20000, 5000);

            //Int32 ptbId = 0;          //Point table id 0
            //Int32 dimension = 3;      //2D point table
            //Int32 i = 0;
            //Int32 ret = 0;

            //int[] Axis_ID_Array = new int[] { 0, 1, 2 };

            //PTSTS Status = new PTSTS();
            //PTLINE Line = new PTLINE();

            ////Enable point table
            //ret = APS168.APS_pt_disable(boardId, ptbId);
            ///////////////////////////ret = APS168.APS_pt_enable(boardId, ptbId, dimension, Axis_ID_Array);
            //ret = APS168.APS_pt_enable(boardId, ptbId, dimension, Axis_ID_Array);

            ////Configuration
            //ret = APS168.APS_pt_set_absolute(boardId, ptbId); //Set to absolute mode
            //ret = APS168.APS_pt_set_trans_buffered(boardId, ptbId); //Set to buffer mode
            //ret = APS168.APS_pt_set_acc(boardId, ptbId, 5000000); //Set acc
            //ret = APS168.APS_pt_set_dec(boardId, ptbId, 5000000); //Set dec

            //ret = APS168.APS_get_pt_status(boardId, ptbId, ref Status);
            //if ((Status.BitSts & 0x02) == 0) //Buffer is not Full
            //{

            //    Line.Dim = 3;
            //    double Vslow = 300000;

            //    {
            //        PTDWL pwdell = new PTDWL();
            //        pwdell.DwTime = 2000;

            //        //1st line 


            //        ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow);
            //        ret = APS168.APS_pt_set_ve(boardId, ptbId, Vslow);
            //        Line.Pos = new double[] { -218000, -22000, 50, 0, 0, 0 };
            //        ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);//1段起始点

            //        APS168.APS_set_trigger_linear(boardId, 0, -72000, 900, 11);

            //        double praAcc = Convert.ToDouble(txtPraAcc.Text);
            //        double praDec = Convert.ToDouble(txtPraDec.Text);

            //        APS168.APS_set_axis_param_f(1, (Int32)APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            //        APS168.APS_set_axis_param_f(1, (Int32)APS_Define.PRA_DEC, praDec); // Set deceleration rate

            //        APS168.APS_relative_move(0, 20000, 100000);
            //    }
            //}
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            if (!_primADLink._isInitialed)
            {
                MessageBox.Show("请先初始化卡片！");
                return;
            }

            //判断配置文件是否存在
            if (File.Exists(_config.ConfigFilePath))
            {
                var msg = "";
                var iRet = _primADLink.IPrimConnect(ref msg);

                if (iRet != 0) return;

                cmbSelectAxis.Items.Clear();
                for (var i = _primADLink._startAxisId; i < _primADLink._startAxisId + _primADLink._totalAxis; i++) cmbSelectAxis.Items.Add(i);
                cmbSelectAxis.SelectedIndex = 0;

                if (_primADLink.PrimConnStat == PrimConnState.Connected)
                    btnLoadConfig.BackColor = Color.LawnGreen;
                else
                    btnLoadConfig.BackColor = Color.DimGray;
            }
            else
            {
                MessageBox.Show(txtXmlFilename.Text + "不存在，请选择XML配置文件!");
                var dialog = new OpenFileDialog();
                dialog.Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog.InitialDirectory = "C:";
                dialog.Title = "Select a XML File";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _config.ConfigFilePath = dialog.FileName;
                    txtXmlFilename.Text = dialog.FileName;
                }
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
        }

        private void btnRelativeMove_Click(object sender, EventArgs e)
        {
            var boardId = Convert.ToInt32(txtCardId.Text);
            var selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praDec = Convert.ToDouble(txtPraDec.Text);
            var praVm = Convert.ToInt32(txtPraVm.Text);
            var pulse = Convert.ToInt32(txtRelativePulse.Text);

            _primADLink.AxisSetAcc(boardId, selectAxis, praAcc);
            _primADLink.AxisSetDec(boardId, selectAxis, praDec);

            if (_primADLink.PrimConnStat == PrimConnState.Connected)
            {
                var task = new Task(() =>
                {
                    APS168.APS_relative_move(selectAxis, pulse, praVm);

                    //等待Motion Down完成
                    var motionStatusMdn = 5;
                    while ((APS168.APS_motion_status(selectAxis) & (1 << motionStatusMdn)) == 0) Thread.Sleep(100);
                    MessageBox.Show("运动完成！");
                });
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件！");
            }
        }

        private void btnStartDemo_Click(object sender, EventArgs e)
        {
            var form = new DemoForm();
            form.Show();
        }

        private void btnStartPt_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var boardId = Convert.ToInt32(txtCardId.Text);
            int motionStatusCstp = 0, motionStatusAstp = 16;
            APS168.APS_absolute_move(0, -220000, 50000);
            while ((APS168.APS_motion_status(0) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);

            if ((APS168.APS_motion_status(0) & (1 << motionStatusAstp)) == 0)
                MessageBox.Show("轴" + "到位成功！");
            else
                MessageBox.Show("轴" + "到位失败！");

            Thread.Sleep(500);

            APS168.APS_absolute_move(1, 30000, 10000);
            while ((APS168.APS_motion_status(1) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);

            if ((APS168.APS_motion_status(1) & (1 << motionStatusAstp)) == 0)
                MessageBox.Show("轴" + "到位成功！");
            else
                MessageBox.Show("轴" + "到位失败！");

            APS168.APS_absolute_move(2, 0, 10000);
            while ((APS168.APS_motion_status(2) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);

            if ((APS168.APS_motion_status(2) & (1 << motionStatusAstp)) == 0)
                MessageBox.Show("轴" + "到位成功！");
            else
                MessageBox.Show("轴" + "到位失败！");

            APS168.APS_absolute_move(1, -15000, 10000);

            //关闭触发
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG_EN, 0);
            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));
            //Source Encoder0
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TCMP0_SRC, 0);
            //Bi-direction(No direction)
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TCMP0_DIR, 1);
            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG0_SRC, Convert.ToInt32("0100", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG0_PWD, 1500); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(boardId, (short)APS_Define.TGR_TRG0_LOGIC, 0);

            //add points to 1th
            //int[] xPoint = new int[] { 100000, 90000, 80000 };


            var xPoint = new int[330];
            var csvFile = new CsvReader("x1.csv");

            var xTable = csvFile.ReadIntoDataTable(new[] { typeof(int) });
            for (var i = 0; i < 330; i++) xPoint[i] = Convert.ToInt32(xTable.Rows[i][0]); //表格行的数据

            APS168.APS_set_trigger_table(boardId, 0, xPoint, 330);
        }

        private void buttonArc3_Click(object sender, EventArgs e)
        {
            var boardId = Convert.ToInt32(txtCardId.Text);
            //重置触发计数
            APS168.APS_reset_trigger_count(boardId, 0);
            APS168.APS_reset_trigger_count(boardId, 1);

            var ptbId = 0; //Point table id 0
            var dimension = 3; //2D point table
            var ret = 0;

            var Axis_ID_Array = new[] { 0, 1, 2 };

            var Status = new PTSTS();
            var Line = new PTLINE();

            //Check servo on or not
            //for (i = 0; i < dimension; i++)
            //{
            //    ret = APS168.APS_set_servo_on(Axis_ID_Array, 1);
            //}
            //Thread.Sleep(500); // Wait stable.

            //Enable point table
            ret = APS168.APS_pt_disable(boardId, ptbId);
            /////////////////////////ret = APS168.APS_pt_enable(boardId, ptbId, dimension, Axis_ID_Array);
            ret = APS168.APS_pt_enable(boardId, ptbId, dimension, Axis_ID_Array);

            //Configuration
            ret = APS168.APS_pt_set_absolute(boardId, ptbId); //Set to absolute mode
            ret = APS168.APS_pt_set_trans_buffered(boardId, ptbId); //Set to buffer mode
            ret = APS168.APS_pt_set_acc(boardId, ptbId, 5000000); //Set acc
            ret = APS168.APS_pt_set_dec(boardId, ptbId, 5000000); //Set dec

            ret = APS168.APS_get_pt_status(boardId, ptbId, ref Status);
            if ((Status.BitSts & 0x02) == 0) //Buffer is not Full
            {
                Line.Dim = 3;


                double vepoint = 100000;

                double Vquick = 1500000;
                double Vslow = 300000;


                {
                    var pwdell = new PTDWL();
                    pwdell.DwTime = 2000;

                    //Line.Pos = new double[] { -206000, -20000, 0, 0, 0, 0 };
                    //ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    //1st line 


                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, Vquick);
                    Line.Pos = new double[] { -218000, -22000, 50, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status); //1段起始点

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, Vslow * 0.6);
                    Line.Pos = new double[] { -56000, -22000, 50, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status); //high


                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { 114000, -22000, 50, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status); //low

                    if (checkBoxOnly1.Checked)
                    {
                        ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);
                        ret = APS168.APS_pt_start(boardId, ptbId);
                        return;
                    }

                    //圆弧线段1
                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -25000, 17000, 6250, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { 35000, 17000, 6250, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ////2nd line
                    ////bypass


                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick); //Delete by Tony
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -126000, 14000, 12450, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status); //2段起始点


                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick); //Set vm to 10000
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint); //Set ve to 5000
                    Line.Pos = new double[] { 34000, 14000, 12450, 0, 0, 0 }; //触发2 
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    //圆弧线段2
                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -125000, 18000, 18750, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -65000, 18000, 18750, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);


                    //3 line slow
                    //bypass

                    //slow

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -205000, -22000, 25000, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status); //第3段起始点


                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -95000, -22000, 25000, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);


                    //quick

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick); //ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick); //Set vm to 10000
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint); //ret = APS168.APS_pt_set_ve(boardId, ptbId, vepointlow); //Set ve to 5000


                    //ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow);//ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick); //Set vm to 10000
                    //ret = APS168.APS_pt_set_ve(boardId, ptbId, Vslow * 2); //ret = APS168.APS_pt_set_ve(boardId, ptbId, vepointlow); //Set ve to 5000
                    Line.Pos = new double[] { 120000, -22000, 25000, 0, 0, 0 }; //触发3
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);


                    //圆弧线段3
                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow); //ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -20000, 20500, 31250, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow); //ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { 35000, 20500, 31250, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ////4 line


                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick); //Set vm to 10000
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint); //Set ve to 5000
                    Line.Pos = new double[] { -130000, 20000, 37600, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vslow); //Set vm to 10000
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint); //Set ve to 5000
                    Line.Pos = new double[] { 35000, 20000, 37600, 0, 0, 0 }; //触发4
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    //圆弧线段4
                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -125000, 23000, 43750, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);

                    ret = APS168.APS_pt_set_vm(boardId, ptbId, Vquick);
                    ret = APS168.APS_pt_set_ve(boardId, ptbId, vepoint);
                    Line.Pos = new double[] { -90000, 23000, 43750, 0, 0, 0 };
                    ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);
                }

                //Push 1st point to buffer
                ret = APS168.APS_pt_line(boardId, ptbId, ref Line, ref Status);
            }

            ret = APS168.APS_pt_start(boardId, ptbId);
        }

        private void cmbSelectAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lblJogNegative_Click(object sender, EventArgs e)
        {
        }

        private void lblJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            var boardId = Convert.ToInt32(txtCardId.Text);
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);

            _primADLink.AxisSetJogMode(boardId, _selectAxis, 1);
            _primADLink.AxisSetJogDir(boardId, _selectAxis, 1);
            _primADLink.AxisSetJogAcc(boardId, _selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _primADLink.AxisSetJogDec(boardId, _selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _primADLink.AxisSetJogMaxVel(boardId, _selectAxis, Convert.ToDouble(txtPraVm.Text));
            _primADLink.AxisJogStart(boardId, _selectAxis, 1);

            //APS168.APS_set_axis_param(_selectAxis, (int)APS_Define.PRA_JG_MODE, 1);
            //APS168.APS_set_axis_param(_selectAxis, (int)APS_Define.PRA_JG_DIR, Convert.ToInt32(((Label)sender).Tag));//1
            //APS168.APS_set_axis_param_f(_selectAxis, (int)APS_Define.PRA_JG_ACC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxis, (int)APS_Define.PRA_JG_DEC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxis, (int)APS_Define.PRA_JG_VM, Convert.ToDouble(txtPraVm.Text));
            //APS168.APS_jog_start(_selectAxis, 1);
        }

        private void lblJogNegative_MouseUp(object sender, MouseEventArgs e)
        {
            //APS168.APS_jog_start(_selectAxis, 0);
            var boardId = Convert.ToInt32(txtCardId.Text);
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            _primADLink.AxisJogStart(boardId, _selectAxis, 0);
        }

        private void lblJogPostive_Click(object sender, EventArgs e)
        {
        }

        private void lblJogPostive_MouseDown(object sender, MouseEventArgs e)
        {
            var boardId = Convert.ToInt32(txtCardId.Text);
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);

            _primADLink.AxisSetJogMode(boardId, _selectAxis, 1);
            _primADLink.AxisSetJogDir(boardId, _selectAxis, 0);
            _primADLink.AxisSetJogAcc(boardId, _selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _primADLink.AxisSetJogDec(boardId, _selectAxis, Convert.ToDouble(txtPraAcc.Text));
            _primADLink.AxisSetJogMaxVel(boardId, _selectAxis, Convert.ToDouble(txtPraVm.Text));
            _primADLink.AxisJogStart(boardId, _selectAxis, 1);

            //APS168.APS_set_axis_param(_selectAxis, (int)APS_Define.PRA_JG_MODE, 1);
            //APS168.APS_set_axis_param(_selectAxis, (int)APS_Define.PRA_JG_DIR, Convert.ToInt32(((Label)sender).Tag));//0
            //APS168.APS_set_axis_param_f(_selectAxis, (int)APS_Define.PRA_JG_ACC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxis, (int)APS_Define.PRA_JG_DEC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxis, (int)APS_Define.PRA_JG_VM, Convert.ToDouble(txtPraVm.Text));
            //APS168.APS_jog_start(_selectAxis, 1);
        }

        private void lblJogPostive_MouseUp(object sender, MouseEventArgs e)
        {
            //APS168.APS_jog_start(_selectAxis, 0);
            var boardId = Convert.ToInt32(txtCardId.Text);
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            _primADLink.AxisJogStart(boardId, _selectAxis, 0);
        }

        private void lblServoOn_Click(object sender, EventArgs e)
        {
            if (_primADLink.PrimConnStat != PrimConnState.Connected)
            {
                MessageBox.Show("请先加载配置文件！");
                return;
            }

            var boardId = Convert.ToInt32(txtCardId.Text);
            var selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var motionIoStatus = _primADLink.AxisMotionIOStatus(boardId, selectAxis);
            var status = (motionIoStatus & (1 << 7)) != 0;
            _primADLink.AxisSetEnable(boardId, selectAxis, !status);
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void PrimConfigControl_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_config.ConfigFilePath)) txtXmlFilename.Text = _config.ConfigFilePath;

            ADLinkName = _primADLink.Name;
            txtCardId.Text = _config.DevIndex.ToString();
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

        private void StartHoming(int boardId, int axisId, int homeMode, int homeDir, double praCurve, double praAcc, double praVm)
        {
            //
            // 1. Select home mode and config home parameters 

            _primADLink.AxisSetHomeMode(boardId, axisId, homeMode); // Set home mode
            _primADLink.AxisSetHomeDir(boardId, axisId, homeDir); // Set home direction
            _primADLink.AxisSetHomeCurve(boardId, axisId, praCurve); // Set acceleration paten (T-curve)
            _primADLink.AxisSetHomeAcc(boardId, axisId, praAcc); // Set homing acceleration rate
            _primADLink.AxisSetHomeMaxVel(boardId, axisId, praVm); // Set homing maximum velocity.
            _primADLink.AxisSetHomeVO(boardId, axisId, praVm / 5); // Set homing VO speed
            _primADLink.AxisSetHomeEZA(boardId, axisId, 0); // Set EZ signal alignment (yes or no)
            _primADLink.AxisSetHomeShift(boardId, axisId, 0); // Set home position shfit distance. 
            _primADLink.AxisSetHomePos(boardId, axisId, 0); // Set final home position. 
            _primADLink.AxisHomeMove(boardId, axisId); // Start Axis Home Move

            //servo on
            _primADLink.AxisSetEnable(boardId, axisId, true);
            Thread.Sleep(500); // Wait stable.


            // 2. Start home move
            //APS168.APS_home_move(axisId);
            _primADLink.AxisHomeMove(boardId, axisId); // Start Axis Home Move

            int motionStatusCstp = 0, motionStatusAstp = 16;
            while ((APS168.APS_motion_status(axisId) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);
            Thread.Sleep(500);

            //if ((APS168.APS_motion_status(axisId) & 1 << motionStatusAstp) == 0)
            if ((_primADLink.GetAxisMotionStatus(axisId) & (1 << motionStatusAstp)) == 0)
                MessageBox.Show("轴" + axisId + "回零成功！");
            else
                MessageBox.Show("轴" + axisId + "回零失败！");
        }

        private void tBoxADLinkName_TextChanged(object sender, EventArgs e)
        {
            _config.Name = tBoxADLinkName.Text;
        }

        private void tmrPT_Tick(object sender, EventArgs e)
        {
            //int boardId = Convert.ToInt32(txtCardId.Text);

            //PTSTS ptsts = new PTSTS();
            //APS168.APS_get_pt_status(0, 0, ref ptsts);
            //uint runningCnt = 0;
            //runningCnt = ptsts.RunningCnt;
            //textBox1.Text = Convert.ToString(runningCnt);

            //if (runningCnt != runningCntLast)
            //{
            //    if (runningCnt == 1)
            //    {

            //    }


            //    else if (runningCnt == 3)
            //    {
            //        //圆弧触发1
            //        int[] xPoint = new int[] { -10000, 20000 };
            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 2);


            //    }

            //    else if (runningCnt == 4)
            //    {
            //        ////add points to 2th
            //        //int[] xPoint = new int[] { 70000, 60000, 50000 };
            //        //APS168.APS_set_trigger_table(_card0.CardId, 0, xPoint, 3);


            //        int[] xPoint = new int[52];
            //        var csvFile = new CsvReader("x2.csv");

            //        var xTable = csvFile.ReadIntoDataTable(new[] { typeof(int) });
            //        for (int i = 0; i < 52; i++)
            //        {
            //            xPoint[i] = Convert.ToInt32(xTable.Rows[i][0]);

            //        }
            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 52);

            //    }

            //    else if (runningCnt == 7)
            //    {
            //        //圆弧触发2
            //        int[] xPoint = new int[] { -110000, -90000 };
            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 2);

            //    }


            //    else if (runningCnt == 8)
            //    {
            //        //add points to 3th
            //        int[] xPoint = new int[266];
            //        var csvFile = new CsvReader("x3.csv");

            //        var xTable = csvFile.ReadIntoDataTable(new[] { typeof(int) });
            //        for (int i = 0; i < 266; i++)
            //        {
            //            xPoint[i] = Convert.ToInt32(xTable.Rows[i][0]); //表格行的数据

            //        }

            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 266);
            //    }

            //    else if (runningCnt == 12)
            //    {
            //        //圆弧触发3
            //        int[] xPoint = new int[] { 10000, 20000 };
            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 2);

            //    }

            //    else if (runningCnt == 13)
            //    {
            //        //add points to 4th
            //        int[] xPoint = new int[264];
            //        var csvFile = new CsvReader("x4.csv");

            //        var xTable = csvFile.ReadIntoDataTable(new[] { typeof(int) });
            //        for (int i = 0; i < 264; i++)
            //        {
            //            xPoint[i] = Convert.ToInt32(xTable.Rows[i][0]); //表格行的数据

            //        }

            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 264);
            //    }

            //    else if (runningCnt == 15)
            //    {
            //        //圆弧触发1
            //        int[] xPoint = new int[] { -120000, -100000 };
            //        APS168.APS_set_trigger_table(boardId, 0, xPoint, 2);

            //    }

            //    runningCntLast = runningCnt;
            //}
        }

        private void tmrUpdateUI_Tick(object sender, EventArgs e)
        {
            if (_primADLink._isInitialed)
            {
                _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
                double d = 0;
                _primADLink.GetAxisPositionF(_selectAxis, ref d);
                //APS168.APS_get_position_f(_selectAxis, ref d);
                txtFeedbackPos.Text = d.ToString("f0");
                txtFeedbackPosition.Text = d.ToString("f0");

                switch (tabControl1.SelectedTab.Name)
                {
                    case "tPageAxisMove":
                        int index;
                        bool status;
                        int motionIoStatus, motionStatus;
                        //刷新MotionIoStatus
                        //motionIoStatus = APS168.APS_motion_io_status(_selectAxis);
                        motionIoStatus = _primADLink.GetAxisMotionIOStatus(_selectAxis);
                        foreach (var label in grpMotionIo.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (motionIoStatus & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }

                        //刷新MotionStatus
                        //motionStatus = APS168.APS_motion_status(_selectAxis);
                        motionStatus = _primADLink.GetAxisMotionStatus(_selectAxis);
                        foreach (var label in grpMotion.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (motionStatus & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }

                        //刷新Feedback信息
                        double value = 0;
                        _primADLink.GetAxisCommandF(_selectAxis, ref value);
                        //APS168.APS_get_command_f(_selectAxis, ref value);
                        txtCommandPosition.Text = value.ToString("f0");

                        //APS168.APS_get_position_f(_selectAxis, ref value);
                        //txtFeedbackPos.Text = value.ToString("f0");
                        //txtFeedbackPosition.Text = value.ToString("f0");

                        _primADLink.GetAxisPositionF(_selectAxis, ref value);
                        //APS168.APS_get_target_position_f(_selectAxis, ref value);
                        txtTargetPosition.Text = value.ToString("f0");

                        _primADLink.GetAxisErrPositionF(_selectAxis, ref value);
                        //APS168.APS_get_error_position_f(_selectAxis, ref value);
                        txtErrorPosition.Text = value.ToString("f0");

                        _primADLink.GetAxisCommandVelocityF(_selectAxis, ref value);
                        //APS168.APS_get_command_velocity_f(_selectAxis, ref value);
                        txtCommandVelocity.Text = value.ToString("f0");

                        _primADLink.GetAxisFeedbackVelocityF(_selectAxis, ref value);
                        //APS168.APS_get_feedback_velocity_f(_selectAxis, ref value);
                        txtFeedbackVelocity.Text = value.ToString("f0");


                        //刷新ServoOn, motionIoStatus第7位
                        status = (motionIoStatus & (1 << 7)) != 0;
                        lblServoOn.BackColor = status ? Color.LawnGreen : Color.DimGray;

                        //刷新JOG正 和 JOG负
                        var jogPostive = (motionStatus & (1 << 15)) != 0 && (motionStatus & (1 << 4)) != 0;
                        var jogNegative = (motionStatus & (1 << 15)) != 0 && (motionStatus & (1 << 4)) == 0;
                        lblJogPostive.BackColor = jogPostive ? Color.LawnGreen : Color.DimGray;
                        lblJogNegative.BackColor = jogNegative ? Color.LawnGreen : Color.DimGray;
                        break;

                    case "tPageIO":
                        var diValue = 0;

                        //APS168.APS_read_d_input(_card0.CardId, 0, ref diValue);
                        _primADLink.ReadMultiDInputFromAPI(_config.DevIndex, 0, ref diValue);
                        foreach (var label in grpDi.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (diValue & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }


                        var doValue = 0;
                        //APS168.APS_read_d_output(_card0.CardId, 0, ref doValue);
                        _primADLink.ReadMultiDOutputFromAPI(_config.DevIndex, 0, ref doValue);
                        foreach (var label in grpDo.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (doValue & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }

                        break;
                    case "tPageCmpTrig":
                        {
                            var count = 0;
                            APS168.APS_get_trigger_count(Convert.ToInt32(txtCardId.Text), 0, ref count);
                            lbTrigCntCH0.Text = count.ToString();

                            APS168.APS_get_trigger_count(Convert.ToInt32(txtCardId.Text), 1, ref count);
                            lbTrigCntCH1.Text = count.ToString();
                        }
                        break;
                    case "tPageContinueMove":
                        {
                            if (_ptEnabled)
                            {
                                txtTotalPoint.Text = _bufTotalPoint.ToString();
                                txtBufFreeSpace.Text = _bufFreeSpace.ToString();
                                txtbufUsageSpace.Text = _bufUsageSpace.ToString();
                                txtRunningCnt.Text = _bufRunningCnt.ToString();
                            }
                        }
                        break;
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