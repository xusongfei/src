namespace Lead.Detect.PrimSktClient
{
    partial class PrimConfigControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tBoxConnState = new System.Windows.Forms.TextBox();
            this.tBoxRunState = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tTaskBoxName = new System.Windows.Forms.TextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cBoxNotifyMode = new System.Windows.Forms.ComboBox();
            this.cBoxHeart = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBoxPort = new System.Windows.Forms.TextBox();
            this.tBoxHeartTime = new System.Windows.Forms.TextBox();
            this.tBoxRevQueueCnt = new System.Windows.Forms.TextBox();
            this.tBoxSendQueueCnt = new System.Windows.Forms.TextBox();
            this.tBoxNetName = new System.Windows.Forms.TextBox();
            this.tBoxReConnCnt = new System.Windows.Forms.TextBox();
            this.tBoxHeartInfo = new System.Windows.Forms.TextBox();
            this.tBoxIP = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cBoxDebug = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tBoxMsg = new System.Windows.Forms.TextBox();
            this.tBoxInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tBoxConnState);
            this.panel1.Controls.Add(this.tBoxRunState);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tTaskBoxName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 44);
            this.panel1.TabIndex = 24;
            // 
            // tBoxConnState
            // 
            this.tBoxConnState.BackColor = System.Drawing.Color.Khaki;
            this.tBoxConnState.Dock = System.Windows.Forms.DockStyle.Right;
            this.tBoxConnState.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxConnState.Location = new System.Drawing.Point(419, 0);
            this.tBoxConnState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxConnState.Multiline = true;
            this.tBoxConnState.Name = "tBoxConnState";
            this.tBoxConnState.Size = new System.Drawing.Size(211, 44);
            this.tBoxConnState.TabIndex = 15;
            this.tBoxConnState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tBoxRunState
            // 
            this.tBoxRunState.BackColor = System.Drawing.Color.Khaki;
            this.tBoxRunState.Dock = System.Windows.Forms.DockStyle.Right;
            this.tBoxRunState.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxRunState.Location = new System.Drawing.Point(630, 0);
            this.tBoxRunState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxRunState.Multiline = true;
            this.tBoxRunState.Name = "tBoxRunState";
            this.tBoxRunState.Size = new System.Drawing.Size(211, 44);
            this.tBoxRunState.TabIndex = 14;
            this.tBoxRunState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SkyBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 44);
            this.label1.TabIndex = 12;
            this.label1.Text = "Client Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tTaskBoxName
            // 
            this.tTaskBoxName.BackColor = System.Drawing.Color.OldLace;
            this.tTaskBoxName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tTaskBoxName.Location = new System.Drawing.Point(202, 3);
            this.tTaskBoxName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tTaskBoxName.Multiline = true;
            this.tTaskBoxName.Name = "tTaskBoxName";
            this.tTaskBoxName.Size = new System.Drawing.Size(211, 39);
            this.tTaskBoxName.TabIndex = 13;
            this.tTaskBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tTaskBoxName.TextChanged += new System.EventHandler(this.tTaskBoxName_TextChanged);
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(841, 43);
            this.lbTitle.TabIndex = 23;
            this.lbTitle.Text = "Socket Client Config";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(841, 375);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.cBoxNotifyMode);
            this.tabPage1.Controls.Add(this.cBoxHeart);
            this.tabPage1.Controls.Add(this.btnApply);
            this.tabPage1.Controls.Add(this.groupBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tBoxPort);
            this.tabPage1.Controls.Add(this.tBoxHeartTime);
            this.tabPage1.Controls.Add(this.tBoxRevQueueCnt);
            this.tabPage1.Controls.Add(this.tBoxSendQueueCnt);
            this.tabPage1.Controls.Add(this.tBoxNetName);
            this.tabPage1.Controls.Add(this.tBoxReConnCnt);
            this.tabPage1.Controls.Add(this.tBoxHeartInfo);
            this.tabPage1.Controls.Add(this.tBoxIP);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(833, 342);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // cBoxNotifyMode
            // 
            this.cBoxNotifyMode.FormattingEnabled = true;
            this.cBoxNotifyMode.Items.AddRange(new object[] {
            "Event",
            "Queue"});
            this.cBoxNotifyMode.Location = new System.Drawing.Point(243, 191);
            this.cBoxNotifyMode.Name = "cBoxNotifyMode";
            this.cBoxNotifyMode.Size = new System.Drawing.Size(121, 28);
            this.cBoxNotifyMode.TabIndex = 129;
            this.cBoxNotifyMode.SelectedIndexChanged += new System.EventHandler(this.cBoxNotifyMode_SelectedIndexChanged);
            // 
            // cBoxHeart
            // 
            this.cBoxHeart.AutoSize = true;
            this.cBoxHeart.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cBoxHeart.Location = new System.Drawing.Point(364, 113);
            this.cBoxHeart.Name = "cBoxHeart";
            this.cBoxHeart.Size = new System.Drawing.Size(72, 29);
            this.cBoxHeart.TabIndex = 128;
            this.cBoxHeart.Text = "心跳";
            this.cBoxHeart.UseVisualStyleBackColor = true;
            this.cBoxHeart.CheckedChanged += new System.EventHandler(this.cBoxHeart_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.LightGreen;
            this.btnApply.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnApply.Location = new System.Drawing.Point(632, 266);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(78, 34);
            this.btnApply.TabIndex = 127;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnStop);
            this.groupBox.Controls.Add(this.btnRun);
            this.groupBox.Controls.Add(this.btnInit);
            this.groupBox.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox.Location = new System.Drawing.Point(406, 235);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(220, 84);
            this.groupBox.TabIndex = 126;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "EXE";
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.SandyBrown;
            this.btnStop.Location = new System.Drawing.Point(140, 28);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(74, 41);
            this.btnStop.TabIndex = 105;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.LightGreen;
            this.btnRun.Location = new System.Drawing.Point(72, 28);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(60, 41);
            this.btnRun.TabIndex = 105;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnInit
            // 
            this.btnInit.BackColor = System.Drawing.Color.SkyBlue;
            this.btnInit.Location = new System.Drawing.Point(6, 28);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(60, 41);
            this.btnInit.TabIndex = 105;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = false;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(238, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 25);
            this.label3.TabIndex = 122;
            this.label3.Text = "Port:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(309, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 25);
            this.label8.TabIndex = 125;
            this.label8.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(238, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 25);
            this.label7.TabIndex = 125;
            this.label7.Text = "心跳周期:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(238, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 25);
            this.label10.TabIndex = 125;
            this.label10.Text = "数据通知方式:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(572, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(126, 25);
            this.label12.TabIndex = 125;
            this.label12.Text = "Rev队列大小:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(572, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 25);
            this.label11.TabIndex = 125;
            this.label11.Text = "Send队列大小:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(563, 159);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 25);
            this.label13.TabIndex = 125;
            this.label13.Text = "网络名称:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(18, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(169, 25);
            this.label9.TabIndex = 125;
            this.label9.Text = "通讯断开重连次数:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(18, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 25);
            this.label6.TabIndex = 125;
            this.label6.Text = "心跳信息:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(18, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 25);
            this.label2.TabIndex = 125;
            this.label2.Text = "IP:";
            // 
            // tBoxPort
            // 
            this.tBoxPort.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxPort.Location = new System.Drawing.Point(243, 41);
            this.tBoxPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxPort.Name = "tBoxPort";
            this.tBoxPort.Size = new System.Drawing.Size(100, 31);
            this.tBoxPort.TabIndex = 120;
            this.tBoxPort.TextChanged += new System.EventHandler(this.tBoxPort_TextChanged);
            // 
            // tBoxHeartTime
            // 
            this.tBoxHeartTime.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxHeartTime.Location = new System.Drawing.Point(243, 114);
            this.tBoxHeartTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxHeartTime.Name = "tBoxHeartTime";
            this.tBoxHeartTime.Size = new System.Drawing.Size(64, 31);
            this.tBoxHeartTime.TabIndex = 121;
            this.tBoxHeartTime.TextChanged += new System.EventHandler(this.tBoxHeartTime_TextChanged);
            // 
            // tBoxRevQueueCnt
            // 
            this.tBoxRevQueueCnt.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxRevQueueCnt.Location = new System.Drawing.Point(568, 111);
            this.tBoxRevQueueCnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxRevQueueCnt.Name = "tBoxRevQueueCnt";
            this.tBoxRevQueueCnt.Size = new System.Drawing.Size(163, 31);
            this.tBoxRevQueueCnt.TabIndex = 121;
            this.tBoxRevQueueCnt.TextChanged += new System.EventHandler(this.tBoxRevQueueCnt_TextChanged);
            // 
            // tBoxSendQueueCnt
            // 
            this.tBoxSendQueueCnt.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxSendQueueCnt.Location = new System.Drawing.Point(568, 41);
            this.tBoxSendQueueCnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxSendQueueCnt.Name = "tBoxSendQueueCnt";
            this.tBoxSendQueueCnt.Size = new System.Drawing.Size(163, 31);
            this.tBoxSendQueueCnt.TabIndex = 121;
            this.tBoxSendQueueCnt.TextChanged += new System.EventHandler(this.tBoxSendQueueCnt_TextChanged);
            // 
            // tBoxNetName
            // 
            this.tBoxNetName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxNetName.Location = new System.Drawing.Point(568, 188);
            this.tBoxNetName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxNetName.Name = "tBoxNetName";
            this.tBoxNetName.Size = new System.Drawing.Size(163, 31);
            this.tBoxNetName.TabIndex = 121;
            this.tBoxNetName.TextChanged += new System.EventHandler(this.tBoxNetName_TextChanged);
            // 
            // tBoxReConnCnt
            // 
            this.tBoxReConnCnt.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxReConnCnt.Location = new System.Drawing.Point(23, 191);
            this.tBoxReConnCnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxReConnCnt.Name = "tBoxReConnCnt";
            this.tBoxReConnCnt.Size = new System.Drawing.Size(163, 31);
            this.tBoxReConnCnt.TabIndex = 121;
            this.tBoxReConnCnt.TextChanged += new System.EventHandler(this.tBoxReConnCnt_TextChanged);
            // 
            // tBoxHeartInfo
            // 
            this.tBoxHeartInfo.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxHeartInfo.Location = new System.Drawing.Point(23, 114);
            this.tBoxHeartInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxHeartInfo.Name = "tBoxHeartInfo";
            this.tBoxHeartInfo.Size = new System.Drawing.Size(163, 31);
            this.tBoxHeartInfo.TabIndex = 121;
            this.tBoxHeartInfo.TextChanged += new System.EventHandler(this.tBoxHeartInfo_TextChanged);
            // 
            // tBoxIP
            // 
            this.tBoxIP.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxIP.Location = new System.Drawing.Point(23, 41);
            this.tBoxIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxIP.Name = "tBoxIP";
            this.tBoxIP.Size = new System.Drawing.Size(163, 31);
            this.tBoxIP.TabIndex = 121;
            this.tBoxIP.TextChanged += new System.EventHandler(this.tBoxIP_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.cBoxDebug);
            this.tabPage2.Controls.Add(this.btnSend);
            this.tabPage2.Controls.Add(this.tBoxMsg);
            this.tabPage2.Controls.Add(this.tBoxInfo);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(833, 342);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // cBoxDebug
            // 
            this.cBoxDebug.AutoSize = true;
            this.cBoxDebug.Location = new System.Drawing.Point(408, 251);
            this.cBoxDebug.Name = "cBoxDebug";
            this.cBoxDebug.Size = new System.Drawing.Size(61, 24);
            this.cBoxDebug.TabIndex = 136;
            this.cBoxDebug.Text = "调试";
            this.cBoxDebug.UseVisualStyleBackColor = true;
            this.cBoxDebug.CheckedChanged += new System.EventHandler(this.cBoxDebug_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(682, 168);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(119, 37);
            this.btnSend.TabIndex = 135;
            this.btnSend.Text = "发送消息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tBoxMsg
            // 
            this.tBoxMsg.Location = new System.Drawing.Point(392, 60);
            this.tBoxMsg.Multiline = true;
            this.tBoxMsg.Name = "tBoxMsg";
            this.tBoxMsg.Size = new System.Drawing.Size(409, 93);
            this.tBoxMsg.TabIndex = 134;
            // 
            // tBoxInfo
            // 
            this.tBoxInfo.Location = new System.Drawing.Point(11, 60);
            this.tBoxInfo.Multiline = true;
            this.tBoxInfo.Name = "tBoxInfo";
            this.tBoxInfo.Size = new System.Drawing.Size(376, 227);
            this.tBoxInfo.TabIndex = 133;
            this.tBoxInfo.TextChanged += new System.EventHandler(this.tBoxInfo_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 25);
            this.label5.TabIndex = 131;
            this.label5.Text = "数据接收:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(387, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 25);
            this.label4.TabIndex = 132;
            this.label4.Text = "数据发送:";
            // 
            // PrimConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbTitle);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrimConfigControl";
            this.Size = new System.Drawing.Size(841, 462);
            this.Load += new System.EventHandler(this.PrimConfigControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tBoxConnState;
        private System.Windows.Forms.TextBox tBoxRunState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tTaskBoxName;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBoxPort;
        private System.Windows.Forms.TextBox tBoxIP;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tBoxMsg;
        private System.Windows.Forms.TextBox tBoxInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBoxHeartTime;
        private System.Windows.Forms.TextBox tBoxHeartInfo;
        private System.Windows.Forms.CheckBox cBoxHeart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tBoxReConnCnt;
        private System.Windows.Forms.ComboBox cBoxNotifyMode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tBoxRevQueueCnt;
        private System.Windows.Forms.TextBox tBoxSendQueueCnt;
        private System.Windows.Forms.CheckBox cBoxDebug;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tBoxNetName;
    }
}
