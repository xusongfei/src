using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.PrimMotionADLink.ADLink
{
    public partial class DemoForm : Form
    {
        private readonly Adlink204C _card0;
        private int _bufTotalPoint, _bufFreeSpace, _bufUsageSpace, _bufRunningCnt;
        private readonly int[] _interruptCount = new int[6];
        private DataTable _pointTable = new DataTable();

        //连续运动(ptline)标志位和计数器
        private bool _ptEnabled;
        private int _selectAxis;


        //中断线程标志位和计数器
        private readonly bool[] _waitIntFlag = new bool[6];
        private List<Point> ptList = new List<Point>();


        public DemoForm()
        {
            InitializeComponent();
            _card0 = new Adlink204C();
        }

        private void btnAbsoluteMove_Click(object sender, EventArgs e)
        {
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praDec = Convert.ToDouble(txtPraDec.Text);
            var praVm = Convert.ToInt32(txtPraVm.Text);
            var pulse = Convert.ToInt32(txtAbsolutePulse.Text);

            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_DEC, praDec); // Set deceleration rate

            if (_card0.IsLoadXmlFile)
            {
                var task = new Task(() =>
                {
                    APS168.APS_absolute_move(_selectAxis, pulse, praVm);

                    //等待Motion Down完成
                    var motionStatusMdn = 5;
                    while ((APS168.APS_motion_status(_selectAxis) & (1 << motionStatusMdn)) == 0) Thread.Sleep(100);
                    MessageBox.Show("运动完成！");
                });
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件！");
            }
        }

        private void btnDisableInterrupt_Click(object sender, EventArgs e)
        {
            _waitIntFlag[0] = false;
            Thread.Sleep(1000);

            APS168.APS_int_enable(0, 0);
        }

        private void btnGetLantch_Click(object sender, EventArgs e)
        {
            var size = 30;
            var LatchData = new LATCH_POINT[size];
            APS168.APS_get_ltc_fifo_point(0, 0, ref size, ref LatchData[0]);


            for (var i = 0; i < size; i++)
                if (LatchData[i].position > 0)
                {
                    richTextBox1.Text += string.Format("Latch {0}: {1}\r\n", i, LatchData[i].position);
                    richTextBox1.ScrollToCaret();
                }

            var count = 0;
            APS168.APS_get_trigger_count(0, 0, ref count);
            richTextBox1.Text += string.Format("Trigger Count={0} \r\n", count);
        }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var homeMode = cmbHomeMode.SelectedIndex;
            var homeDir = cmbHomeDir.SelectedIndex;
            var praCurve = Convert.ToDouble(txtPraCurve.Text);
            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praVm = Convert.ToDouble(txtPraVm.Text);


            if (_card0.IsLoadXmlFile && homeMode >= 0 && homeDir >= 0)
            {
                var task = new Task(() => StartHoming(_selectAxis, homeMode, homeDir, praCurve, praAcc, praVm));
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件并选择回零模式！");
            }
        }

        private void BtnInitialClick(object sender, EventArgs e)
        {
            if (!_card0.IsInitialed)
            {
                _card0.Initial(Convert.ToInt32(txtCardId.Text), 0);
                btnInitial.BackColor = _card0.IsInitialed ? Color.LawnGreen : Color.DimGray;
            }
        }


        private void btnLatch_Click(object sender, EventArgs e)
        {
            var ret = 0;
            //注意FPGA必须是FC开头或更新的
            ret = APS168.APS_enable_ltc_fifo(0, 0, 0); // Disable position latch
            ret = APS168.APS_reset_ltc_fifo(0, 0); // Reset position latch queue
            ret = APS168.APS_set_ltc_fifo_param(0, 0, 0x10, Convert.ToInt32("000000001", 2)); // Set input source bit0-7=di8-di15(扩展IO的1-8), bit 9-11=trigger 1-4
            ret = APS168.APS_set_ltc_fifo_param(0, 0, 0x11, 0); // Set EncoderNo
            ret = APS168.APS_set_ltc_fifo_param(0, 0, 0x12, 0); // Set Logic  1=Risingedge
            ret = APS168.APS_enable_ltc_fifo(0, 0, 1); // Start position latch

            BeginInvoke(new Action(() => { richTextBox1.Text += "Latch Enabled\r\n"; }));
        }

        private void btnLinearTigger_Click(object sender, EventArgs e)
        {
            //重置触发计数
            APS168.APS_reset_trigger_count(_card0.CardId, 0);
            APS168.APS_reset_trigger_count(_card0.CardId, 1);

            //关闭触发
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, 0);

            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));

            //Source Encoder0
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_LCMP0_SRC, 0);

            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_SRC, Convert.ToInt32("10000", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_PWD, 250000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_LOGIC, 1);

            APS168.APS_set_trigger_linear(_card0.CardId, 0, 1, 7, 1000);
        }

        private void BtnLoadConfigClick(object sender, EventArgs e)
        {
            if (!_card0.IsInitialed)
            {
                MessageBox.Show("请先初始化卡片！");
                return;
            }

            //判断配置文件是否存在
            if (File.Exists(txtXmlFilename.Text))
            {
                if (_card0.LoadParamFromFile(txtXmlFilename.Text))
                {
                    cmbSelectAxis.Items.Clear();
                    for (var i = _card0.StartAxisId; i < _card0.StartAxisId + _card0.TotalAxis; i++) cmbSelectAxis.Items.Add(i);
                    cmbSelectAxis.SelectedIndex = 0;
                    btnLoadConfig.BackColor = _card0.IsLoadXmlFile ? Color.LawnGreen : Color.DimGray;
                }
            }
            else
            {
                MessageBox.Show(txtXmlFilename.Text + "不存在，请选择XML配置文件!");
                var dialog = new OpenFileDialog();
                dialog.Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog.InitialDirectory = "C:";
                dialog.Title = "Select a XML File";
                if (dialog.ShowDialog() == DialogResult.OK) txtXmlFilename.Text = dialog.FileName;
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            var csvFile = new CsvReader("pointTable.csv");

            _pointTable = csvFile.ReadIntoDataTable(new[] {typeof(int), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double)});
            dataGridView1.DataSource = _pointTable;

            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
        }

        private void btnManualTrigger_Click(object sender, EventArgs e)
        {
            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));

            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_SRC, Convert.ToInt32("0001", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_PWD, 250000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_LOGIC, 0);

            APS168.APS_set_trigger_manual(_card0.CardId, 0);
        }

        private void btnMotionTrigTest_Click(object sender, EventArgs e)
        {
            //当前位置清零，或者说是对应轴反馈清零
            APS168.APS_set_position(0, 0);
            APS168.APS_set_position(1, 0);
            APS168.APS_set_position(2, 0);
            APS168.APS_set_position(3, 0);

            //重置触发计数
            APS168.APS_reset_trigger_count(_card0.CardId, 0);
            APS168.APS_reset_trigger_count(_card0.CardId, 1);

            //关闭触发
            //APS168.APS_set_trigger_param(_card0.CardId, (short)APS_Define.TGR_TRG_EN, 0);

            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0011", 2));

            //Source Encoder3
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_LCMP1_SRC, 3);

            //Trigger output 1 (TRG1) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG1_SRC, Convert.ToInt32("100001", 2));

            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG1_PWD, 1000); //  pulse width=  value * 0.02 us

            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG1_LOGIC, 1);

            APS168.APS_set_trigger_linear(_card0.CardId, 1, 1, 7000, 19);

            //Axis0 相对运动


            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praDec = Convert.ToDouble(txtPraDec.Text);
            var praVm = Convert.ToInt32(txtPraVm.Text);
            var pulse = Convert.ToInt32(txtRelativePulse.Text);

            APS168.APS_set_axis_param_f(0, (int) APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(0, (int) APS_Define.PRA_DEC, praDec); // Set deceleration rate

            APS168.APS_relative_move(0, pulse, praVm);

            //光源输出点取反
            var index = 10;
            var doValue = 0;
            APS168.APS_read_d_output(_card0.CardId, 0, ref doValue);
            doValue = doValue ^ (1 << index); //Index 这一位异或1，  也就是取反
            APS168.APS_write_d_output(_card0.CardId, 0, doValue);

            Thread.Sleep(700);

            doValue = 0;
            APS168.APS_read_d_output(_card0.CardId, 0, ref doValue);
            doValue = doValue ^ (1 << index); //Index 这一位异或1，  也就是取反
            APS168.APS_write_d_output(_card0.CardId, 0, doValue);
        }

        private void btnRandomTigger_Click(object sender, EventArgs e)
        {
            //重置触发计数
            APS168.APS_reset_trigger_count(_card0.CardId, 0);
            APS168.APS_reset_trigger_count(_card0.CardId, 1);

            //关闭触发
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, 0);

            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0011", 2));

            //Source Encoder0
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TCMP0_SRC, 0);
            //Bi-direction(No direction)
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TCMP0_DIR, 2);

            //Source Encoder1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TCMP1_SRC, 1);
            //Bi-direction(No direction)
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TCMP1_DIR, 2);

            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_SRC, Convert.ToInt32("0100", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_PWD, 250000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_LOGIC, 1);

            //Trigger output 1 (TRG1) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG1_SRC, Convert.ToInt32("1000", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG1_PWD, 250000); // pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG1_LOGIC, 0);

            // 两个轴编码器位置比较触发点
            var CMP_x = new int[4] {2000, 5000, 8000, 10000};
            var CMP_y = new int[4] {2000, 5000, 8000, 10000};
            APS168.APS_set_trigger_table(_card0.CardId, 0, CMP_x, 4);
            APS168.APS_set_trigger_table(_card0.CardId, 1, CMP_y, 4);
        }

        private void btnRelativeMove_Click(object sender, EventArgs e)
        {
            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var praAcc = Convert.ToDouble(txtPraAcc.Text);
            var praDec = Convert.ToDouble(txtPraDec.Text);
            var praVm = Convert.ToInt32(txtPraVm.Text);
            var pulse = Convert.ToInt32(txtRelativePulse.Text);

            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_DEC, praDec); // Set deceleration rate


            if (_card0.IsLoadXmlFile)
            {
                var task = new Task(() =>
                {
                    APS168.APS_relative_move(_selectAxis, pulse, praVm);

                    //等待Motion Down完成
                    var motionStatusMdn = 5;
                    while ((APS168.APS_motion_status(_selectAxis) & (1 << motionStatusMdn)) == 0) Thread.Sleep(100);
                    MessageBox.Show("运动完成！");
                });
                task.Start();
            }
            else
            {
                MessageBox.Show("请加载配置文件！");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //重置触发计数
            //APS168.APS_reset_trigger_count(_card0.CardId, 0);

            //关闭触发
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, 0);

            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));

            //Source Encoder0
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TCMP0_SRC, 0);
            //0=Negative direction , 1= Positive direction, 2=Bi-direction(No direction)
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TCMP0_DIR, 2);

            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_SRC, Convert.ToInt32("0100", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_PWD, 50000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_LOGIC, 0); //设置1可能会多一次触发

            //重置触发计数
            //APS168.APS_reset_trigger_count(_card0.CardId, 0);


            //线程1，设置触发点，开始运动
            Task.Factory.StartNew(() =>
            {
                //位置比较触发点
                var CMP_x = new[]
                {
                    2000, 4000, 7000, 9000, 12000, 15000, 19000, 22000, 23000, 25000,
                    24000, 21000, 18000, 15000, 11000, 9000
                };

                APS168.APS_set_trigger_table(_card0.CardId, 0, CMP_x, CMP_x.Length);

                //运动到30000
                APS168.APS_absolute_move(_selectAxis, 30000, 30000);
                while ((APS168.APS_motion_status(_selectAxis) & (1 << 5)) == 0) //motionStatusMdn = 5
                    Thread.Sleep(100);

                //运动到0
                APS168.APS_absolute_move(_selectAxis, 0, 30000);
                while ((APS168.APS_motion_status(_selectAxis) & (1 << 5)) == 0) //motionStatusMdn = 5
                    Thread.Sleep(100);
            });
        }

        private void btnSetInt_Click(object sender, EventArgs e)
        {
            // 开启1个DI上升沿中断
            var board_id = 0;
            var item = 9; // DI interrupt rising


            // Step 1: set interrupt factor 
            var interruptId = APS168.APS_set_int_factor(board_id, item, 0, 1);

            // Step 2: set interrupt main switch 
            var ret = APS168.APS_int_enable(board_id, 1); // Enable the interrupt main switch


            BeginInvoke(new Action(() => { richTextBox2.Text += "Interrupt Enabled\r\n"; }));

            //开启线程
            Task.Factory.StartNew(() =>
            {
                var count = 0;
                while (true)
                    if (APS168.APS_wait_single_int(interruptId, 0x0000ff) == 0) //Wait interrupt
                    {
                        double pos = 0;
                        APS168.APS_get_position_f(0, ref pos);
                        APS168.APS_reset_int(interruptId);

                        BeginInvoke(new Action(() =>
                        {
                            richTextBox2.Text += string.Format("Interrupt Postion[{0}] = {1}\r\n", count++, pos);
                            richTextBox2.SelectionStart = richTextBox2.TextLength;
                            richTextBox2.ScrollToCaret();
                        }));
                    }
            });
        }


        private void btnSetInterrupt_Click(object sender, EventArgs e)
        {
            // 开启2个DI上升沿中断
            var board_id = 0;
            var item = 9; // DI interrupt rising
            int interruptId;

            // Step 1: set interrupt factor 
            interruptId = APS168.APS_set_int_factor(board_id, item, 0, 1); //di8


            // Step 2: set interrupt main switch 
            var ret = APS168.APS_int_enable(board_id, 1); // Enable the interrupt main switch


            if (_waitIntFlag[0])
            {
                MessageBox.Show("中断线程" + 0 + "正在运行，请先停止！");
            }
            else
            {
                //_waitIntFlag[i] = true;
                //Task.Factory.StartNew(() => this.WaitInterrutp(i, interruptId[i]));
                var task = new Task(() => WaitInterrutp(0, interruptId));
                _waitIntFlag[0] = true;
                task.Start();
            }

            Thread.Sleep(2000);
        }

        private void btnSetPosZero_Click(object sender, EventArgs e)
        {
            APS168.APS_set_position(_selectAxis, 0);
        }

        private void btnStartPt_Click(object sender, EventArgs e)
        {
            _bufTotalPoint = _pointTable.Rows.Count;
            if (_bufTotalPoint > 1)
            {
                _ptEnabled = true;
                var t = new Task(() => PointTableMove());
                t.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            APS168.APS_write_d_channel_output(0, 0, 10, 0);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var ret = APS168.APS_pt_stop(_card0.CardId, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var ret = APS168.APS_pt_start(_card0.CardId, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));

            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_SRC, Convert.ToInt32("0001", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_PWD, 250000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_LOGIC, 0);

            APS168.APS_set_trigger_manual(_card0.CardId, 0);
        }


        private void button7_Click(object sender, EventArgs e)
        {
            //重置触发计数
            APS168.APS_reset_trigger_count(_card0.CardId, 0);
            APS168.APS_reset_trigger_count(_card0.CardId, 1);

            //关闭触发
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, 0);

            //TRG 0 ~ 3 enable by bit 
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG_EN, Convert.ToInt32("0001", 2));

            //Source Encoder0
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_LCMP0_SRC, 0);

            //Trigger output 0 (TRG0) source Bit( 1:On, 0:Off)    Bit 0: Manual  Bit 1:Reserved Bit  2:TCMP0  Bit 3:TCMP1  Bit 4:LCMP0  Bit 5:LCMP1
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_SRC, Convert.ToInt32("10000", 2));
            //TRG1 pulse width
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_PWD, 250000); //  pulse width=  value * 0.02 us
            //TRG 1 logic  0: Not inverse  1:Inverse
            APS168.APS_set_trigger_param(_card0.CardId, (short) APS_Define.TGR_TRG0_LOGIC, 1);

            APS168.APS_set_trigger_linear(_card0.CardId, 0, 0, 1000, 50);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            APS168.APS_write_d_channel_output(0, 0, 10, 1);
        }

        private void cmbSelectAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     解除从动轴绑定
        /// </summary>
        /// <param name="slaveAxis"></param>
        /// <returns></returns>
        public int DisableEGear(int slaveAxis)
        {
            var status = 0;
            var ret = 0;
            APS168.APS_get_gear_status(slaveAxis, ref status);
            if (status != 0) ret = APS168.APS_start_gear(slaveAxis, 0);
            return ret;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     获取Gear Status，0 Disable 1 Standard Mode 2 Gantry Mode
        /// </summary>
        /// <param name="slaveAxis"></param>
        /// <returns></returns>
        public int GetGearStatus(int slaveAxis)
        {
            var status = 0;
            APS168.APS_get_gear_status(slaveAxis, ref status);
            return status;
        }

        private void label43_Click(object sender, EventArgs e)
        {
        }

        private void lblDo_Click(object sender, EventArgs e)
        {
            var label = (Label) sender;
            var index = Convert.ToInt32(label.Tag);
            var doValue = 0;
            APS168.APS_read_d_output(_card0.CardId, 0, ref doValue);
            doValue = doValue ^ (1 << index); //Index 这一位异或1，  也就是取反
            APS168.APS_write_d_output(_card0.CardId, 0, doValue);
        }


        private void lblJog_MouseDown(object sender, MouseEventArgs e)
        {
            APS168.APS_set_axis_param(_selectAxis, (int) APS_Define.PRA_JG_MODE, 1);
            APS168.APS_set_axis_param(_selectAxis, (int) APS_Define.PRA_JG_DIR, Convert.ToInt32(((Label) sender).Tag));
            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_JG_ACC, Convert.ToDouble(txtPraAcc.Text));
            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_JG_DEC, Convert.ToDouble(txtPraAcc.Text));
            APS168.APS_set_axis_param_f(_selectAxis, (int) APS_Define.PRA_JG_VM, Convert.ToDouble(txtPraVm.Text));
            APS168.APS_jog_start(_selectAxis, 1);
        }

        private void lblJog_MouseUp(object sender, MouseEventArgs e)
        {
            APS168.APS_jog_start(_selectAxis, 0);
        }

        private void lblJogNegative_Click(object sender, EventArgs e)
        {
        }

        private void lblJogPostive_Click(object sender, EventArgs e)
        {
        }

        private void lblServoOn_Click(object sender, EventArgs e)
        {
            if (!_card0.IsLoadXmlFile)
            {
                MessageBox.Show("请先加载配置文件！");
                return;
            }

            _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            var motionIoStatus = APS168.APS_motion_io_status(_selectAxis);
            var status = (motionIoStatus & (1 << 7)) != 0;
            APS168.APS_set_servo_on(_selectAxis, Convert.ToInt32(!status));
        }

        private void PointTableMove()
        {
            var ptStatus = new PTSTS();
            var Line = new PTLINE();
            var dwell = new PTDWL();
            dwell.DwTime = 200;

            int ptbId = 0, dimension = 2;
            var axisIdArray = new[] {0, 1};

            //double vStart = 5000;
            //double vMax = 40000;
            //double vEnd = 5000;


            var ret = APS168.APS_pt_disable(_card0.CardId, ptbId);
            ret = APS168.APS_pt_enable(_card0.CardId, ptbId, dimension, axisIdArray);

            ret = APS168.APS_pt_set_absolute(_card0.CardId, ptbId);
            ret = APS168.APS_pt_set_trans_buffered(_card0.CardId, ptbId);
            ret = APS168.APS_pt_set_acc(_card0.CardId, ptbId, 500000);
            ret = APS168.APS_pt_set_dec(_card0.CardId, ptbId, 500000);


            foreach (DataRow row in _pointTable.Rows)
                while (true)
                {
                    ret = APS168.APS_get_pt_status(_card0.CardId, ptbId, ref ptStatus);
                    _bufFreeSpace = ptStatus.PntBufFreeSpace;
                    _bufUsageSpace = ptStatus.PntBufUsageSpace;
                    _bufRunningCnt = (int) ptStatus.RunningCnt;
                    if (ptStatus.PntBufFreeSpace > 10)
                    {
                        //ret = APS168.APS_pt_set_vs(_card0.CardId, ptbId, vStart);
                        ret = APS168.APS_pt_set_vm(_card0.CardId, ptbId, Convert.ToDouble(row[4]));
                        ret = APS168.APS_pt_set_ve(_card0.CardId, ptbId, Convert.ToDouble(row[5]));

                        Line.Dim = dimension;
                        Line.Pos = new[] {Convert.ToDouble(row[1]), Convert.ToDouble(row[2]), 0, 0, 0, 0};
                        ret = APS168.APS_pt_line(_card0.CardId, ptbId, ref Line, ref ptStatus);
                        ret = APS168.APS_pt_ext_set_do_ch(_card0.CardId, ptbId, 8, 1);
                        ret = APS168.APS_pt_dwell(_card0.CardId, ptbId, ref dwell, ref ptStatus);
                        ret = APS168.APS_pt_ext_set_do_ch(_card0.CardId, ptbId, 8, 0);
                        ret = APS168.APS_pt_dwell(_card0.CardId, ptbId, ref dwell, ref ptStatus);
                        break;
                    }

                    Thread.Sleep(1);
                }

            //等待buffer跑完, motionStatus的ptbFlag=false则连续运动结束。
            var ptbFlag = true;
            while (ptbFlag)
            {
                var motionStatus = APS168.APS_motion_status(axisIdArray[0]);
                ptbFlag = (motionStatus & (1 << 11)) != 0;

                ret = APS168.APS_get_pt_status(_card0.CardId, ptbId, ref ptStatus);
                _bufFreeSpace = ptStatus.PntBufFreeSpace;
                _bufUsageSpace = ptStatus.PntBufUsageSpace;
                _bufRunningCnt = (int) ptStatus.RunningCnt;
                Thread.Sleep(100);
            }
        }

        /// <summary>
        ///     设置龙门模式的电子齿轮。启动此功能后只需移动主动轴
        /// </summary>
        /// <param name="slaveAxis">从动轴</param>
        /// <param name="masterAxis"></param>
        /// <param name="gearEngageRate"></param>
        /// <param name="gearRatio"></param>
        /// <returns></returns>
        public int SetEGearGantry(int slaveAxis, int masterAxis, bool isSetGearRatio = true, double protect1Offset = 1000.0, double protect2Offset = 1000.0)
        {
            var status = 0;
            var ret = 0;
            APS168.APS_get_gear_status(slaveAxis, ref status);
            //if (status != 0)
            //{
            //    ret = APS168.APS_start_gear(slaveAxis, 0);
            //    Thread.Sleep(50);
            //}
            if (status == 0)
            {
                //Select master Axis command to be slaveAxis's master
                ret = APS168.APS_set_axis_param(slaveAxis, (int) APS_Define.PRA_GEAR_MASTER, masterAxis);
                if (isSetGearRatio) ret = APS168.APS_set_axis_param_f(slaveAxis, (int) APS_Define.PRA_GEAR_RATIO, 1.0);
                //Set Gear Protect1
                ret = APS168.APS_set_axis_param_f(slaveAxis, (int) APS_Define.PRA_GANTRY_PROTECT_1, protect1Offset);

                //set Gear Protect2
                ret = APS168.APS_set_axis_param_f(slaveAxis, (int) APS_Define.PRA_GANTRY_PROTECT_2, protect2Offset);

                ret = APS168.APS_start_gear(slaveAxis, 2);
            }

            return ret;
        }


        /// <summary>
        ///     设置标准模式的电子齿轮。启动此功能后只需移动主动轴
        /// </summary>
        /// <param name="slaveAxis">从动轴</param>
        /// <param name="masterAxis"></param>
        /// <param name="gearEngageRate"></param>
        /// <param name="gearRatio"></param>
        /// <returns></returns>
        public int SetEGearStandard(int slaveAxis, int masterAxis, double gearEngageRate = 1000.0, double gearRatio = 1.0)
        {
            var status = 0;
            var ret = 0;
            APS168.APS_get_gear_status(slaveAxis, ref status);
            //if (status != 0)
            //{
            //    ret = APS168.APS_start_gear(slaveAxis, 0);
            //    Thread.Sleep(50);
            //}
            if (status == 0)
            {
                //Select master Axis command to be slaveAxis's master
                ret = APS168.APS_set_axis_param(slaveAxis, (int) APS_Define.PRA_GEAR_MASTER, masterAxis);

                //Set Gear engage rate
                ret = APS168.APS_set_axis_param_f(slaveAxis, (int) APS_Define.PRA_GEAR_ENGAGE_RATE, gearEngageRate);

                //set Gear ratio
                ret = APS168.APS_set_axis_param_f(slaveAxis, (int) APS_Define.PRA_GEAR_RATIO, gearRatio);

                ret = APS168.APS_start_gear(slaveAxis, 1);
            }

            return ret;
        }

        private void StartHoming(int axisId, int homeMode, int homeDir, double praCurve, double praAcc, double praVm)
        {
            // 1. Select home mode and config home parameters 
            APS168.APS_set_axis_param(axisId, (int) APS_Define.PRA_HOME_MODE, homeMode); // Set home mode
            APS168.APS_set_axis_param(axisId, (int) APS_Define.PRA_HOME_DIR, homeDir); // Set home direction
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_CURVE, praCurve); // Set acceleration paten (T-curve)
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_ACC, praAcc); // Set homing acceleration rate
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_VM, praVm); // Set homing maximum velocity.
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_VO, praVm / 5); // Set homing VO speed
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_EZA, 0); // Set EZ signal alignment (yes or no)
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_SHIFT, 0); // Set home position shfit distance. 
            APS168.APS_set_axis_param_f(axisId, (int) APS_Define.PRA_HOME_POS, 0); // Set final home position.

            //servo on
            APS168.APS_set_servo_on(axisId, 1);
            Thread.Sleep(500); // Wait stable.


            // 2. Start home move
            APS168.APS_home_move(axisId);

            int motionStatusCstp = 0, motionStatusAstp = 16;
            while ((APS168.APS_motion_status(axisId) & (1 << motionStatusCstp)) == 0) Thread.Sleep(100);
            Thread.Sleep(500);

            if ((APS168.APS_motion_status(axisId) & (1 << motionStatusAstp)) == 0)
                MessageBox.Show("轴" + axisId + "回零成功！");
            else
                MessageBox.Show("轴" + axisId + "回零失败！");
        }

        private void tabMotion_Click(object sender, EventArgs e)
        {
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void tmrUpdateUi_Tick(object sender, EventArgs e)
        {
            if (_card0.IsLoadXmlFile)
            {
                _selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
                double d = 0;
                APS168.APS_get_position_f(_selectAxis, ref d);
                txtFeedbackPos.Text = d.ToString("f0");
                txtFeedbackPosition.Text = d.ToString("f0");

                switch (tabControl1.SelectedTab.Name)
                {
                    case "tabMotion":
                        int index;
                        bool status;
                        int motionIoStatus, motionStatus;
                        //刷新MotionIoStatus
                        motionIoStatus = APS168.APS_motion_io_status(_selectAxis);
                        foreach (var label in grpMotionIo.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (motionIoStatus & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }

                        //刷新MotionStatus
                        motionStatus = APS168.APS_motion_status(_selectAxis);
                        foreach (var label in grpMotion.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (motionStatus & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }

                        //刷新Feedback信息
                        double value = 0;
                        APS168.APS_get_command_f(_selectAxis, ref value);
                        txtCommandPosition.Text = value.ToString("f0");

                        //APS168.APS_get_position_f(_selectAxis, ref value);
                        //txtFeedbackPos.Text = value.ToString("f0");
                        //txtFeedbackPosition.Text = value.ToString("f0");

                        APS168.APS_get_target_position_f(_selectAxis, ref value);
                        txtTargetPosition.Text = value.ToString("f0");

                        APS168.APS_get_error_position_f(_selectAxis, ref value);
                        txtErrorPosition.Text = value.ToString("f0");

                        APS168.APS_get_command_velocity_f(_selectAxis, ref value);
                        txtCommandVelocity.Text = value.ToString("f0");

                        APS168.APS_get_feedback_velocity_f(_selectAxis, ref value);
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

                    case "tabIo":
                        var diValue = 0;
                        APS168.APS_read_d_input(_card0.CardId, 0, ref diValue);
                        foreach (var label in grpDi.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (diValue & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }


                        var doValue = 0;
                        APS168.APS_read_d_output(_card0.CardId, 0, ref doValue);
                        foreach (var label in grpDo.Controls.OfType<Label>())
                            if (label.BorderStyle == BorderStyle.FixedSingle)
                            {
                                index = Convert.ToInt32(label.Tag);
                                status = (doValue & (1 << index)) != 0;
                                label.BackColor = status ? Color.LawnGreen : Color.DimGray;
                            }

                        break;
                    case "tabTrigger":
                    {
                        var count = 0;
                        APS168.APS_get_trigger_count(0, 0, ref count);
                        txtTriggerCount0.Text = count.ToString();

                        APS168.APS_get_trigger_count(0, 1, ref count);
                        txtTriggerCount1.Text = count.ToString();
                    }
                        break;
                    case "tabPtline":
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

        private void txtFeedbackPos_TextChanged(object sender, EventArgs e)
        {
        }


        private void WaitInterrutp(int taskId, int interruptId)
        {
            // 显示对话框表示等待触发线程执行了
            MessageBox.Show("TaskId=" + taskId + "   interruptId= " + interruptId);

            while (_waitIntFlag[taskId])
                if (APS168.APS_wait_single_int(interruptId, 0x0000ff) == 0) //Wait interrupt
                {
                    _interruptCount[taskId]++;
                    APS168.APS_reset_int(interruptId);

                    BeginInvoke((MethodInvoker) delegate
                    {
                        txtIntCount0.Text = _interruptCount[0].ToString();
                        txtIntCount1.Text = _interruptCount[1].ToString();
                        txtIntCount2.Text = _interruptCount[2].ToString();
                        txtIntCount3.Text = _interruptCount[3].ToString();
                        txtIntCount4.Text = _interruptCount[4].ToString();
                        txtIntCount5.Text = _interruptCount[5].ToString();
                    });
                }
        }
    }
}