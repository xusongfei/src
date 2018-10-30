namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
    partial class MeasureProjectPosEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxTestPos = new System.Windows.Forms.ComboBox();
            this.buttonMoveToTestPos = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxArrayXCount = new System.Windows.Forms.TextBox();
            this.textBoxArrayYCount = new System.Windows.Forms.TextBox();
            this.textBoxArrayXStep = new System.Windows.Forms.TextBox();
            this.textBoxArrayYStep = new System.Windows.Forms.TextBox();
            this.buttonCreateArrayPos = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxArrayStartX = new System.Windows.Forms.TextBox();
            this.textBoxArrayStartY = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxCurPosType = new System.Windows.Forms.ComboBox();
            this.comboBoxCurPlatform = new System.Windows.Forms.ComboBox();
            this.labelPosTip = new System.Windows.Forms.Label();
            this.labelPosTypeDesc = new System.Windows.Forms.Label();
            this.richTextBoxTestPos = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonCurPlatformMove = new System.Windows.Forms.Button();
            this.buttonImportAddCurPos = new System.Windows.Forms.Button();
            this.buttonImportAllPos = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxMovePosType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxJump = new System.Windows.Forms.ComboBox();
            this.comboBoxMovePlatform = new System.Windows.Forms.ComboBox();
            this.listBoxTestPos = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPagePosList = new System.Windows.Forms.TabPage();
            this.tabPagePosRawData = new System.Windows.Forms.TabPage();
            this.buttonUpdatePosData = new System.Windows.Forms.Button();
            this.richTextBoxPosData = new System.Windows.Forms.RichTextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagePos = new System.Windows.Forms.TabPage();
            this.tabPageMove = new System.Windows.Forms.TabPage();
            this.platformControl1 = new Lead.Detect.FrameworkExtension.platforms.motionPlatforms.PlatformControl();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPagePosList.SuspendLayout();
            this.tabPagePosRawData.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPagePos.SuspendLayout();
            this.tabPageMove.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxTestPos
            // 
            this.comboBoxTestPos.FormattingEnabled = true;
            this.comboBoxTestPos.Location = new System.Drawing.Point(41, 34);
            this.comboBoxTestPos.Name = "comboBoxTestPos";
            this.comboBoxTestPos.Size = new System.Drawing.Size(196, 20);
            this.comboBoxTestPos.TabIndex = 1;
            // 
            // buttonMoveToTestPos
            // 
            this.buttonMoveToTestPos.Location = new System.Drawing.Point(388, 55);
            this.buttonMoveToTestPos.Name = "buttonMoveToTestPos";
            this.buttonMoveToTestPos.Size = new System.Drawing.Size(92, 46);
            this.buttonMoveToTestPos.TabIndex = 2;
            this.buttonMoveToTestPos.Text = "Jump至测试点";
            this.buttonMoveToTestPos.UseVisualStyleBackColor = true;
            this.buttonMoveToTestPos.Click += new System.EventHandler(this.buttonMoveToTestPos_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(809, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "打开";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "另存为";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "清除所有点位";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // textBoxArrayXCount
            // 
            this.textBoxArrayXCount.Location = new System.Drawing.Point(156, 34);
            this.textBoxArrayXCount.Name = "textBoxArrayXCount";
            this.textBoxArrayXCount.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayXCount.TabIndex = 4;
            this.textBoxArrayXCount.Text = "0";
            // 
            // textBoxArrayYCount
            // 
            this.textBoxArrayYCount.Location = new System.Drawing.Point(156, 61);
            this.textBoxArrayYCount.Name = "textBoxArrayYCount";
            this.textBoxArrayYCount.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayYCount.TabIndex = 4;
            this.textBoxArrayYCount.Text = "0";
            // 
            // textBoxArrayXStep
            // 
            this.textBoxArrayXStep.Location = new System.Drawing.Point(269, 34);
            this.textBoxArrayXStep.Name = "textBoxArrayXStep";
            this.textBoxArrayXStep.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayXStep.TabIndex = 4;
            this.textBoxArrayXStep.Text = "10";
            // 
            // textBoxArrayYStep
            // 
            this.textBoxArrayYStep.Location = new System.Drawing.Point(269, 61);
            this.textBoxArrayYStep.Name = "textBoxArrayYStep";
            this.textBoxArrayYStep.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayYStep.TabIndex = 4;
            this.textBoxArrayYStep.Text = "10";
            // 
            // buttonCreateArrayPos
            // 
            this.buttonCreateArrayPos.Location = new System.Drawing.Point(388, 34);
            this.buttonCreateArrayPos.Name = "buttonCreateArrayPos";
            this.buttonCreateArrayPos.Size = new System.Drawing.Size(92, 48);
            this.buttonCreateArrayPos.TabIndex = 5;
            this.buttonCreateArrayPos.Text = "阵列点位";
            this.buttonCreateArrayPos.UseVisualStyleBackColor = true;
            this.buttonCreateArrayPos.Click += new System.EventHandler(this.buttonCreateArrayPos_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxArrayStartX);
            this.groupBox1.Controls.Add(this.textBoxArrayXCount);
            this.groupBox1.Controls.Add(this.buttonCreateArrayPos);
            this.groupBox1.Controls.Add(this.textBoxArrayXStep);
            this.groupBox1.Controls.Add(this.textBoxArrayStartY);
            this.groupBox1.Controls.Add(this.textBoxArrayYStep);
            this.groupBox1.Controls.Add(this.textBoxArrayYCount);
            this.groupBox1.Location = new System.Drawing.Point(309, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 96);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阵列点位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Y步长";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "列数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "起始点Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "X步长";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "行数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "起始点X";
            // 
            // textBoxArrayStartX
            // 
            this.textBoxArrayStartX.Location = new System.Drawing.Point(57, 34);
            this.textBoxArrayStartX.Name = "textBoxArrayStartX";
            this.textBoxArrayStartX.Size = new System.Drawing.Size(57, 21);
            this.textBoxArrayStartX.TabIndex = 4;
            this.textBoxArrayStartX.Text = "0";
            // 
            // textBoxArrayStartY
            // 
            this.textBoxArrayStartY.Location = new System.Drawing.Point(57, 61);
            this.textBoxArrayStartY.Name = "textBoxArrayStartY";
            this.textBoxArrayStartY.Size = new System.Drawing.Size(57, 21);
            this.textBoxArrayStartY.TabIndex = 4;
            this.textBoxArrayStartY.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.comboBoxCurPosType);
            this.groupBox2.Controls.Add(this.comboBoxCurPlatform);
            this.groupBox2.Controls.Add(this.labelPosTip);
            this.groupBox2.Controls.Add(this.labelPosTypeDesc);
            this.groupBox2.Controls.Add(this.richTextBoxTestPos);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.buttonCurPlatformMove);
            this.groupBox2.Controls.Add(this.buttonImportAddCurPos);
            this.groupBox2.Controls.Add(this.buttonImportAllPos);
            this.groupBox2.Location = new System.Drawing.Point(309, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 320);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入点位";
            // 
            // comboBoxCurPosType
            // 
            this.comboBoxCurPosType.FormattingEnabled = true;
            this.comboBoxCurPosType.Location = new System.Drawing.Point(388, 126);
            this.comboBoxCurPosType.Name = "comboBoxCurPosType";
            this.comboBoxCurPosType.Size = new System.Drawing.Size(92, 20);
            this.comboBoxCurPosType.TabIndex = 6;
            // 
            // comboBoxCurPlatform
            // 
            this.comboBoxCurPlatform.FormattingEnabled = true;
            this.comboBoxCurPlatform.Location = new System.Drawing.Point(388, 30);
            this.comboBoxCurPlatform.Name = "comboBoxCurPlatform";
            this.comboBoxCurPlatform.Size = new System.Drawing.Size(92, 20);
            this.comboBoxCurPlatform.TabIndex = 6;
            this.comboBoxCurPlatform.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurPlatform_SelectedIndexChanged);
            // 
            // labelPosTip
            // 
            this.labelPosTip.AutoSize = true;
            this.labelPosTip.Location = new System.Drawing.Point(6, 26);
            this.labelPosTip.Name = "labelPosTip";
            this.labelPosTip.Size = new System.Drawing.Size(257, 12);
            this.labelPosTip.TabIndex = 6;
            this.labelPosTip.Text = "点位数据格式: [x y z] or [name x y z desc]";
            // 
            // labelPosTypeDesc
            // 
            this.labelPosTypeDesc.AutoSize = true;
            this.labelPosTypeDesc.Location = new System.Drawing.Point(427, 111);
            this.labelPosTypeDesc.Name = "labelPosTypeDesc";
            this.labelPosTypeDesc.Size = new System.Drawing.Size(53, 12);
            this.labelPosTypeDesc.TabIndex = 6;
            this.labelPosTypeDesc.Text = "点位类型";
            // 
            // richTextBoxTestPos
            // 
            this.richTextBoxTestPos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxTestPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxTestPos.Location = new System.Drawing.Point(6, 41);
            this.richTextBoxTestPos.Name = "richTextBoxTestPos";
            this.richTextBoxTestPos.Size = new System.Drawing.Size(376, 273);
            this.richTextBoxTestPos.TabIndex = 0;
            this.richTextBoxTestPos.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(451, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "平台";
            // 
            // buttonCurPlatformMove
            // 
            this.buttonCurPlatformMove.Location = new System.Drawing.Point(388, 56);
            this.buttonCurPlatformMove.Name = "buttonCurPlatformMove";
            this.buttonCurPlatformMove.Size = new System.Drawing.Size(92, 48);
            this.buttonCurPlatformMove.TabIndex = 5;
            this.buttonCurPlatformMove.Text = "平台移动";
            this.buttonCurPlatformMove.UseVisualStyleBackColor = true;
            this.buttonCurPlatformMove.Click += new System.EventHandler(this.buttonCurPlatformMove_Click);
            // 
            // buttonImportAddCurPos
            // 
            this.buttonImportAddCurPos.Location = new System.Drawing.Point(388, 152);
            this.buttonImportAddCurPos.Name = "buttonImportAddCurPos";
            this.buttonImportAddCurPos.Size = new System.Drawing.Size(92, 48);
            this.buttonImportAddCurPos.TabIndex = 5;
            this.buttonImportAddCurPos.Text = "添加当前点位";
            this.buttonImportAddCurPos.UseVisualStyleBackColor = true;
            this.buttonImportAddCurPos.Click += new System.EventHandler(this.buttonImportAddCurPos_Click);
            // 
            // buttonImportAllPos
            // 
            this.buttonImportAllPos.Location = new System.Drawing.Point(388, 206);
            this.buttonImportAllPos.Name = "buttonImportAllPos";
            this.buttonImportAllPos.Size = new System.Drawing.Size(92, 48);
            this.buttonImportAllPos.TabIndex = 5;
            this.buttonImportAllPos.Text = "导入所有点位";
            this.buttonImportAllPos.UseVisualStyleBackColor = true;
            this.buttonImportAllPos.Click += new System.EventHandler(this.buttonImportTestPos_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxMovePosType);
            this.groupBox3.Controls.Add(this.buttonMoveToTestPos);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBoxJump);
            this.groupBox3.Controls.Add(this.comboBoxMovePlatform);
            this.groupBox3.Controls.Add(this.comboBoxTestPos);
            this.groupBox3.Location = new System.Drawing.Point(309, 435);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(486, 113);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "点位测试";
            // 
            // comboBoxMovePosType
            // 
            this.comboBoxMovePosType.FormattingEnabled = true;
            this.comboBoxMovePosType.Location = new System.Drawing.Point(302, 34);
            this.comboBoxMovePosType.Name = "comboBoxMovePosType";
            this.comboBoxMovePosType.Size = new System.Drawing.Size(80, 20);
            this.comboBoxMovePosType.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(261, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "Z缩回";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(243, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "点位类型";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "平台";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "点位";
            // 
            // comboBoxJump
            // 
            this.comboBoxJump.FormattingEnabled = true;
            this.comboBoxJump.Location = new System.Drawing.Point(302, 69);
            this.comboBoxJump.Name = "comboBoxJump";
            this.comboBoxJump.Size = new System.Drawing.Size(80, 20);
            this.comboBoxJump.TabIndex = 1;
            this.comboBoxJump.Text = "0";
            // 
            // comboBoxMovePlatform
            // 
            this.comboBoxMovePlatform.FormattingEnabled = true;
            this.comboBoxMovePlatform.Location = new System.Drawing.Point(41, 69);
            this.comboBoxMovePlatform.Name = "comboBoxMovePlatform";
            this.comboBoxMovePlatform.Size = new System.Drawing.Size(196, 20);
            this.comboBoxMovePlatform.TabIndex = 1;
            this.comboBoxMovePlatform.SelectedIndexChanged += new System.EventHandler(this.comboBoxMovePlatform_SelectedIndexChanged);
            // 
            // listBoxTestPos
            // 
            this.listBoxTestPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTestPos.FormattingEnabled = true;
            this.listBoxTestPos.HorizontalScrollbar = true;
            this.listBoxTestPos.ItemHeight = 12;
            this.listBoxTestPos.Location = new System.Drawing.Point(3, 3);
            this.listBoxTestPos.Name = "listBoxTestPos";
            this.listBoxTestPos.Size = new System.Drawing.Size(286, 513);
            this.listBoxTestPos.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPagePosList);
            this.tabControl1.Controls.Add(this.tabPagePosRawData);
            this.tabControl1.Location = new System.Drawing.Point(3, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(300, 545);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPagePosList
            // 
            this.tabPagePosList.Controls.Add(this.listBoxTestPos);
            this.tabPagePosList.Location = new System.Drawing.Point(4, 22);
            this.tabPagePosList.Name = "tabPagePosList";
            this.tabPagePosList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePosList.Size = new System.Drawing.Size(292, 519);
            this.tabPagePosList.TabIndex = 0;
            this.tabPagePosList.Text = "点位列表";
            this.tabPagePosList.UseVisualStyleBackColor = true;
            // 
            // tabPagePosRawData
            // 
            this.tabPagePosRawData.Controls.Add(this.buttonUpdatePosData);
            this.tabPagePosRawData.Controls.Add(this.richTextBoxPosData);
            this.tabPagePosRawData.Location = new System.Drawing.Point(4, 22);
            this.tabPagePosRawData.Name = "tabPagePosRawData";
            this.tabPagePosRawData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePosRawData.Size = new System.Drawing.Size(292, 519);
            this.tabPagePosRawData.TabIndex = 1;
            this.tabPagePosRawData.Text = "点位数据";
            this.tabPagePosRawData.UseVisualStyleBackColor = true;
            // 
            // buttonUpdatePosData
            // 
            this.buttonUpdatePosData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUpdatePosData.Location = new System.Drawing.Point(3, 465);
            this.buttonUpdatePosData.Name = "buttonUpdatePosData";
            this.buttonUpdatePosData.Size = new System.Drawing.Size(286, 51);
            this.buttonUpdatePosData.TabIndex = 1;
            this.buttonUpdatePosData.Text = "更新";
            this.buttonUpdatePosData.UseVisualStyleBackColor = true;
            this.buttonUpdatePosData.Click += new System.EventHandler(this.buttonUpdatePosData_Click);
            // 
            // richTextBoxPosData
            // 
            this.richTextBoxPosData.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxPosData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxPosData.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBoxPosData.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxPosData.Name = "richTextBoxPosData";
            this.richTextBoxPosData.Size = new System.Drawing.Size(286, 462);
            this.richTextBoxPosData.TabIndex = 0;
            this.richTextBoxPosData.Text = "";
            this.richTextBoxPosData.WordWrap = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPagePos);
            this.tabControlMain.Controls.Add(this.tabPageMove);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.ItemSize = new System.Drawing.Size(60, 25);
            this.tabControlMain.Location = new System.Drawing.Point(0, 25);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(809, 587);
            this.tabControlMain.TabIndex = 7;
            // 
            // tabPagePos
            // 
            this.tabPagePos.Controls.Add(this.tabControl1);
            this.tabPagePos.Controls.Add(this.groupBox3);
            this.tabPagePos.Controls.Add(this.groupBox2);
            this.tabPagePos.Controls.Add(this.groupBox1);
            this.tabPagePos.Location = new System.Drawing.Point(4, 29);
            this.tabPagePos.Name = "tabPagePos";
            this.tabPagePos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePos.Size = new System.Drawing.Size(801, 554);
            this.tabPagePos.TabIndex = 0;
            this.tabPagePos.Text = "点位";
            this.tabPagePos.UseVisualStyleBackColor = true;
            // 
            // tabPageMove
            // 
            this.tabPageMove.Controls.Add(this.platformControl1);
            this.tabPageMove.Location = new System.Drawing.Point(4, 29);
            this.tabPageMove.Name = "tabPageMove";
            this.tabPageMove.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMove.Size = new System.Drawing.Size(801, 554);
            this.tabPageMove.TabIndex = 1;
            this.tabPageMove.Text = "移动";
            this.tabPageMove.UseVisualStyleBackColor = true;
            // 
            // platformControl1
            // 
            this.platformControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.platformControl1.Location = new System.Drawing.Point(3, 3);
            this.platformControl1.Name = "platformControl1";
            this.platformControl1.Size = new System.Drawing.Size(795, 548);
            this.platformControl1.TabIndex = 0;
            // 
            // MeasureProjectPosEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 612);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MeasureProjectPosEditor";
            this.Text = "MeasureProjectPosEditor";
            this.Load += new System.EventHandler(this.MeasureProjectPosEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPagePosList.ResumeLayout(false);
            this.tabPagePosRawData.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPagePos.ResumeLayout(false);
            this.tabPageMove.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxTestPos;
        private System.Windows.Forms.Button buttonMoveToTestPos;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxArrayXCount;
        private System.Windows.Forms.TextBox textBoxArrayYCount;
        private System.Windows.Forms.TextBox textBoxArrayXStep;
        private System.Windows.Forms.TextBox textBoxArrayYStep;
        private System.Windows.Forms.Button buttonCreateArrayPos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxArrayStartX;
        private System.Windows.Forms.TextBox textBoxArrayStartY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxMovePlatform;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBoxTestPos;
        private System.Windows.Forms.Button buttonImportAllPos;
        private System.Windows.Forms.ListBox listBoxTestPos;
        private System.Windows.Forms.ComboBox comboBoxJump;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Button buttonImportAddCurPos;
        private System.Windows.Forms.ComboBox comboBoxCurPlatform;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonCurPlatformMove;
        private System.Windows.Forms.ComboBox comboBoxCurPosType;
        private System.Windows.Forms.Label labelPosTypeDesc;
        private System.Windows.Forms.ComboBox comboBoxMovePosType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelPosTip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPagePosList;
        private System.Windows.Forms.TabPage tabPagePosRawData;
        private System.Windows.Forms.RichTextBox richTextBoxPosData;
        private System.Windows.Forms.Button buttonUpdatePosData;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPagePos;
        private System.Windows.Forms.TabPage tabPageMove;
        private FrameworkExtension.platforms.motionPlatforms.PlatformControl platformControl1;
    }
}