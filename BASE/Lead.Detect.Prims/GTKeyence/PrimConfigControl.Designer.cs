namespace Lead.Detect.PrimPlcOmron
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Active",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "ConnectionType",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "LocalPort",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "PeerAddress",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "UseRoutePath",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "RoutePath",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "IsConnected",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "TypeName",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "DoNotFragment",
            ""}, -1);
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lbTitle = new System.Windows.Forms.Label();
            this.contxtMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.增加行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tTaskBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tBoxRunState = new System.Windows.Forms.TextBox();
            this.tBoxConnState = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tPageCompletConfig = new System.Windows.Forms.TabPage();
            this.tabNJCompolet = new System.Windows.Forms.TabControl();
            this.tabPageCommunication = new System.Windows.Forms.TabPage();
            this.btnGetCommunication = new System.Windows.Forms.Button();
            this.listViewOfEachValue = new System.Windows.Forms.ListView();
            this.variableName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.variableValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.labelConnectionType = new System.Windows.Forms.Label();
            this.txtRoutePath = new System.Windows.Forms.TextBox();
            this.labelRoutePath = new System.Windows.Forms.Label();
            this.radioRoutePath = new System.Windows.Forms.RadioButton();
            this.groupBoxRoutePath = new System.Windows.Forms.GroupBox();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.labelPortNo = new System.Windows.Forms.Label();
            this.radioPeerAddress = new System.Windows.Forms.RadioButton();
            this.groupBoxPeerAddress = new System.Windows.Forms.GroupBox();
            this.numPortNo = new System.Windows.Forms.NumericUpDown();
            this.tabPageController = new System.Windows.Forms.TabPage();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.btnSetStatus = new System.Windows.Forms.Button();
            this.btnGetStatus = new System.Windows.Forms.Button();
            this.cmbCPUStatus = new System.Windows.Forms.ComboBox();
            this.labelCPUStatus = new System.Windows.Forms.Label();
            this.cmbRunStatus = new System.Windows.Forms.ComboBox();
            this.labelRunStatus = new System.Windows.Forms.Label();
            this.cmbRunMode = new System.Windows.Forms.ComboBox();
            this.labelRunMode = new System.Windows.Forms.Label();
            this.groupBoxClock = new System.Windows.Forms.GroupBox();
            this.btnSetClock = new System.Windows.Forms.Button();
            this.btnGetClock = new System.Windows.Forms.Button();
            this.txtClock = new System.Windows.Forms.TextBox();
            this.groupBoxUnitName = new System.Windows.Forms.GroupBox();
            this.btnSetUnitName = new System.Windows.Forms.Button();
            this.btnGetUnitName = new System.Windows.Forms.Button();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.tabPageOption = new System.Windows.Forms.TabPage();
            this.groupBoxPlcEncoding = new System.Windows.Forms.GroupBox();
            this.btnSetPlcEncoding = new System.Windows.Forms.Button();
            this.btnGetPlcEncoding = new System.Windows.Forms.Button();
            this.cmbPlcEncodingName = new System.Windows.Forms.ComboBox();
            this.groupBoxHeartBeat = new System.Windows.Forms.GroupBox();
            this.txtCurrentTime = new System.Windows.Forms.TextBox();
            this.labelCurrentTime = new System.Windows.Forms.Label();
            this.btnSetHeartBeat = new System.Windows.Forms.Button();
            this.btnGetHeartBeatTimer = new System.Windows.Forms.Button();
            this.labelSEC = new System.Windows.Forms.Label();
            this.txtTimerInterval = new System.Windows.Forms.TextBox();
            this.labelHeartBeatTimer = new System.Windows.Forms.Label();
            this.groupBoxMessageTimeLimit = new System.Windows.Forms.GroupBox();
            this.btnSetMessageTimeLimit = new System.Windows.Forms.Button();
            this.btnGetMessageTimeLimit = new System.Windows.Forms.Button();
            this.labelMS = new System.Windows.Forms.Label();
            this.txtReceiveTimeLimit = new System.Windows.Forms.TextBox();
            this.groupBoxTagTableVariables = new System.Windows.Forms.GroupBox();
            this.groupBoxBinaryVariableEdit = new System.Windows.Forms.GroupBox();
            this.btnWriteRaw = new System.Windows.Forms.Button();
            this.btnReadRaw = new System.Windows.Forms.Button();
            this.txtBinaryValue = new System.Windows.Forms.TextBox();
            this.labelValueOfBinary = new System.Windows.Forms.Label();
            this.btnReadRawDataMultiple = new System.Windows.Forms.Button();
            this.txtBinaryVariableName = new System.Windows.Forms.TextBox();
            this.labelNameForUseBinary = new System.Windows.Forms.Label();
            this.groupBoxVariableEdit = new System.Windows.Forms.GroupBox();
            this.btnWriteVariable = new System.Windows.Forms.Button();
            this.btnReadVariable = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.btnReadVariableMultiple = new System.Windows.Forms.Button();
            this.txtVariableName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.btnGetVaiable = new System.Windows.Forms.Button();
            this.chkSystemVariable = new System.Windows.Forms.CheckBox();
            this.listViewOfVariableNames = new System.Windows.Forms.ListView();
            this.variableNameOfTagTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.variableTypeOfTagTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.variableValueOfTagTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkDoNotFragment = new System.Windows.Forms.CheckBox();
            this.tPageFinsDataConfig = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvDataConfig = new System.Windows.Forms.DataGridView();
            this.dataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataAssigned = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataCurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Notify = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBoxMonitor = new System.Windows.Forms.CheckBox();
            this.lbCycTime = new System.Windows.Forms.Label();
            this.doUpDown = new System.Windows.Forms.DomainUpDown();
            this.btnConfigApply = new System.Windows.Forms.Button();
            this.tBoxTableDataPath = new System.Windows.Forms.TextBox();
            this.btnTableSaveAs = new System.Windows.Forms.Button();
            this.btnTableLoad = new System.Windows.Forms.Button();
            this.btnTableBrowse = new System.Windows.Forms.Button();
            this.btnDelRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnParamSave = new System.Windows.Forms.Button();
            this.tPageBasicConfig = new System.Windows.Forms.TabPage();
            this.btnApply = new System.Windows.Forms.Button();
            this.comBoxCommMode = new System.Windows.Forms.ComboBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.tBoxLocalIp = new System.Windows.Forms.TextBox();
            this.tBoxDstPort = new System.Windows.Forms.TextBox();
            this.tBoxDstIp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.njCompolet1 = new OMRON.Compolet.CIPCompolet64.NJCompolet(this.components);
            this.contxtMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tPageCompletConfig.SuspendLayout();
            this.tabNJCompolet.SuspendLayout();
            this.tabPageCommunication.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            this.groupBoxPeerAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPortNo)).BeginInit();
            this.tabPageController.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            this.groupBoxClock.SuspendLayout();
            this.groupBoxUnitName.SuspendLayout();
            this.tabPageOption.SuspendLayout();
            this.groupBoxPlcEncoding.SuspendLayout();
            this.groupBoxHeartBeat.SuspendLayout();
            this.groupBoxMessageTimeLimit.SuspendLayout();
            this.groupBoxTagTableVariables.SuspendLayout();
            this.groupBoxBinaryVariableEdit.SuspendLayout();
            this.groupBoxVariableEdit.SuspendLayout();
            this.tPageFinsDataConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataConfig)).BeginInit();
            this.tPageBasicConfig.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 300;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(1057, 43);
            this.lbTitle.TabIndex = 16;
            this.lbTitle.Text = "OmronPLC Config";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTitle.Click += new System.EventHandler(this.lbTitle_Click);
            // 
            // contxtMenuStrip
            // 
            this.contxtMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contxtMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.增加行ToolStripMenuItem,
            this.删除行ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contxtMenuStrip.Name = "contextMenuStrip1";
            this.contxtMenuStrip.Size = new System.Drawing.Size(243, 76);
            // 
            // 增加行ToolStripMenuItem
            // 
            this.增加行ToolStripMenuItem.Name = "增加行ToolStripMenuItem";
            this.增加行ToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.增加行ToolStripMenuItem.Text = "Add Row";
            this.增加行ToolStripMenuItem.Click += new System.EventHandler(this.增加行ToolStripMenuItem_Click);
            // 
            // 删除行ToolStripMenuItem
            // 
            this.删除行ToolStripMenuItem.Name = "删除行ToolStripMenuItem";
            this.删除行ToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.删除行ToolStripMenuItem.Text = "Remove Selected Row";
            this.删除行ToolStripMenuItem.Click += new System.EventHandler(this.删除行ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.清空ToolStripMenuItem.Text = "Clear";
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
            this.label1.Text = "OmronPlcName";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxRunState
            // 
            this.tBoxRunState.BackColor = System.Drawing.Color.Khaki;
            this.tBoxRunState.Dock = System.Windows.Forms.DockStyle.Right;
            this.tBoxRunState.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxRunState.Location = new System.Drawing.Point(846, 0);
            this.tBoxRunState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxRunState.Multiline = true;
            this.tBoxRunState.Name = "tBoxRunState";
            this.tBoxRunState.Size = new System.Drawing.Size(211, 44);
            this.tBoxRunState.TabIndex = 14;
            this.tBoxRunState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tBoxConnState
            // 
            this.tBoxConnState.BackColor = System.Drawing.Color.Khaki;
            this.tBoxConnState.Dock = System.Windows.Forms.DockStyle.Right;
            this.tBoxConnState.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxConnState.Location = new System.Drawing.Point(635, 0);
            this.tBoxConnState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxConnState.Multiline = true;
            this.tBoxConnState.Name = "tBoxConnState";
            this.tBoxConnState.Size = new System.Drawing.Size(211, 44);
            this.tBoxConnState.TabIndex = 15;
            this.tBoxConnState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.panel1.Size = new System.Drawing.Size(1057, 44);
            this.panel1.TabIndex = 17;
            // 
            // tPageCompletConfig
            // 
            this.tPageCompletConfig.Controls.Add(this.tabNJCompolet);
            this.tPageCompletConfig.Controls.Add(this.groupBoxTagTableVariables);
            this.tPageCompletConfig.Location = new System.Drawing.Point(4, 36);
            this.tPageCompletConfig.Name = "tPageCompletConfig";
            this.tPageCompletConfig.Size = new System.Drawing.Size(1049, 563);
            this.tPageCompletConfig.TabIndex = 2;
            this.tPageCompletConfig.Text = "Complet Config";
            this.tPageCompletConfig.UseVisualStyleBackColor = true;
            // 
            // tabNJCompolet
            // 
            this.tabNJCompolet.Controls.Add(this.tabPageCommunication);
            this.tabNJCompolet.Controls.Add(this.tabPageController);
            this.tabNJCompolet.Controls.Add(this.tabPageOption);
            this.tabNJCompolet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabNJCompolet.ItemSize = new System.Drawing.Size(90, 17);
            this.tabNJCompolet.Location = new System.Drawing.Point(3, 3);
            this.tabNJCompolet.Name = "tabNJCompolet";
            this.tabNJCompolet.SelectedIndex = 0;
            this.tabNJCompolet.Size = new System.Drawing.Size(525, 647);
            this.tabNJCompolet.TabIndex = 2;
            // 
            // tabPageCommunication
            // 
            this.tabPageCommunication.Controls.Add(this.btnGetCommunication);
            this.tabPageCommunication.Controls.Add(this.listViewOfEachValue);
            this.tabPageCommunication.Controls.Add(this.chkActive);
            this.tabPageCommunication.Controls.Add(this.groupBoxConnection);
            this.tabPageCommunication.Location = new System.Drawing.Point(4, 21);
            this.tabPageCommunication.Name = "tabPageCommunication";
            this.tabPageCommunication.Size = new System.Drawing.Size(517, 622);
            this.tabPageCommunication.TabIndex = 0;
            this.tabPageCommunication.Text = "Communication";
            // 
            // btnGetCommunication
            // 
            this.btnGetCommunication.Location = new System.Drawing.Point(366, 502);
            this.btnGetCommunication.Name = "btnGetCommunication";
            this.btnGetCommunication.Size = new System.Drawing.Size(120, 34);
            this.btnGetCommunication.TabIndex = 4;
            this.btnGetCommunication.Text = "Get";
            this.btnGetCommunication.Click += new System.EventHandler(this.btnGetCommunication_Click);
            // 
            // listViewOfEachValue
            // 
            this.listViewOfEachValue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.variableName,
            this.variableValue});
            this.listViewOfEachValue.FullRowSelect = true;
            this.listViewOfEachValue.GridLines = true;
            this.listViewOfEachValue.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18});
            this.listViewOfEachValue.Location = new System.Drawing.Point(26, 289);
            this.listViewOfEachValue.Name = "listViewOfEachValue";
            this.listViewOfEachValue.Size = new System.Drawing.Size(460, 210);
            this.listViewOfEachValue.TabIndex = 3;
            this.listViewOfEachValue.UseCompatibleStateImageBehavior = false;
            this.listViewOfEachValue.View = System.Windows.Forms.View.Details;
            // 
            // variableName
            // 
            this.variableName.Text = "Name";
            this.variableName.Width = 95;
            // 
            // variableValue
            // 
            this.variableValue.Text = "Value";
            this.variableValue.Width = 100;
            // 
            // chkActive
            // 
            this.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkActive.Location = new System.Drawing.Point(26, 258);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(113, 24);
            this.chkActive.TabIndex = 1;
            this.chkActive.Text = "Active";
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.cmbConnectionType);
            this.groupBoxConnection.Controls.Add(this.labelConnectionType);
            this.groupBoxConnection.Controls.Add(this.txtRoutePath);
            this.groupBoxConnection.Controls.Add(this.labelRoutePath);
            this.groupBoxConnection.Controls.Add(this.radioRoutePath);
            this.groupBoxConnection.Controls.Add(this.groupBoxRoutePath);
            this.groupBoxConnection.Controls.Add(this.txtIPAddress);
            this.groupBoxConnection.Controls.Add(this.labelIPAddress);
            this.groupBoxConnection.Controls.Add(this.labelPortNo);
            this.groupBoxConnection.Controls.Add(this.radioPeerAddress);
            this.groupBoxConnection.Controls.Add(this.groupBoxPeerAddress);
            this.groupBoxConnection.Location = new System.Drawing.Point(13, 12);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(473, 239);
            this.groupBoxConnection.TabIndex = 0;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.Items.AddRange(new object[] {
            "UCMM",
            "Class3"});
            this.cmbConnectionType.Location = new System.Drawing.Point(205, 24);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(192, 28);
            this.cmbConnectionType.TabIndex = 12;
            this.cmbConnectionType.Text = "UCMM";
            this.cmbConnectionType.SelectedIndexChanged += new System.EventHandler(this.cmbConnectionType_SelectedIndexChanged);
            // 
            // labelConnectionType
            // 
            this.labelConnectionType.Location = new System.Drawing.Point(38, 24);
            this.labelConnectionType.Name = "labelConnectionType";
            this.labelConnectionType.Size = new System.Drawing.Size(154, 24);
            this.labelConnectionType.TabIndex = 11;
            this.labelConnectionType.Text = "ConnectionType";
            // 
            // txtRoutePath
            // 
            this.txtRoutePath.Location = new System.Drawing.Point(205, 192);
            this.txtRoutePath.Name = "txtRoutePath";
            this.txtRoutePath.Size = new System.Drawing.Size(192, 27);
            this.txtRoutePath.TabIndex = 10;
            this.txtRoutePath.Text = "2%192.168.250.1\\1%0";
            this.txtRoutePath.TextChanged += new System.EventHandler(this.txtRoutePath_TextChanged);
            // 
            // labelRoutePath
            // 
            this.labelRoutePath.Location = new System.Drawing.Point(77, 192);
            this.labelRoutePath.Name = "labelRoutePath";
            this.labelRoutePath.Size = new System.Drawing.Size(102, 24);
            this.labelRoutePath.TabIndex = 9;
            this.labelRoutePath.Text = "RoutePath:";
            // 
            // radioRoutePath
            // 
            this.radioRoutePath.Location = new System.Drawing.Point(51, 168);
            this.radioRoutePath.Name = "radioRoutePath";
            this.radioRoutePath.Size = new System.Drawing.Size(128, 24);
            this.radioRoutePath.TabIndex = 7;
            this.radioRoutePath.Text = "RoutePath";
            // 
            // groupBoxRoutePath
            // 
            this.groupBoxRoutePath.Location = new System.Drawing.Point(38, 168);
            this.groupBoxRoutePath.Name = "groupBoxRoutePath";
            this.groupBoxRoutePath.Size = new System.Drawing.Size(384, 60);
            this.groupBoxRoutePath.TabIndex = 8;
            this.groupBoxRoutePath.TabStop = false;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(205, 120);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(189, 27);
            this.txtIPAddress.TabIndex = 6;
            this.txtIPAddress.Text = "192.168.250.1";
            this.txtIPAddress.TextChanged += new System.EventHandler(this.txtIPAddress_TextChanged);
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.Location = new System.Drawing.Point(77, 120);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(102, 24);
            this.labelIPAddress.TabIndex = 5;
            this.labelIPAddress.Text = "IP Address:";
            // 
            // labelPortNo
            // 
            this.labelPortNo.Location = new System.Drawing.Point(77, 96);
            this.labelPortNo.Name = "labelPortNo";
            this.labelPortNo.Size = new System.Drawing.Size(77, 24);
            this.labelPortNo.TabIndex = 3;
            this.labelPortNo.Text = "Port No";
            // 
            // radioPeerAddress
            // 
            this.radioPeerAddress.Location = new System.Drawing.Point(51, 60);
            this.radioPeerAddress.Name = "radioPeerAddress";
            this.radioPeerAddress.Size = new System.Drawing.Size(154, 24);
            this.radioPeerAddress.TabIndex = 1;
            this.radioPeerAddress.TabStop = true;
            this.radioPeerAddress.Text = "PeerAddress";
            this.radioPeerAddress.CheckedChanged += new System.EventHandler(this.radioPeerAddress_CheckedChanged);
            // 
            // groupBoxPeerAddress
            // 
            this.groupBoxPeerAddress.Controls.Add(this.numPortNo);
            this.groupBoxPeerAddress.Location = new System.Drawing.Point(38, 60);
            this.groupBoxPeerAddress.Name = "groupBoxPeerAddress";
            this.groupBoxPeerAddress.Size = new System.Drawing.Size(384, 96);
            this.groupBoxPeerAddress.TabIndex = 2;
            this.groupBoxPeerAddress.TabStop = false;
            // 
            // numPortNo
            // 
            this.numPortNo.Location = new System.Drawing.Point(166, 24);
            this.numPortNo.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numPortNo.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPortNo.Name = "numPortNo";
            this.numPortNo.Size = new System.Drawing.Size(192, 27);
            this.numPortNo.TabIndex = 0;
            this.numPortNo.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPortNo.ValueChanged += new System.EventHandler(this.numPortNo_ValueChanged);
            // 
            // tabPageController
            // 
            this.tabPageController.Controls.Add(this.groupBoxStatus);
            this.tabPageController.Controls.Add(this.groupBoxClock);
            this.tabPageController.Controls.Add(this.groupBoxUnitName);
            this.tabPageController.Location = new System.Drawing.Point(4, 21);
            this.tabPageController.Name = "tabPageController";
            this.tabPageController.Size = new System.Drawing.Size(517, 622);
            this.tabPageController.TabIndex = 1;
            this.tabPageController.Text = "Controller";
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.btnSetStatus);
            this.groupBoxStatus.Controls.Add(this.btnGetStatus);
            this.groupBoxStatus.Controls.Add(this.cmbCPUStatus);
            this.groupBoxStatus.Controls.Add(this.labelCPUStatus);
            this.groupBoxStatus.Controls.Add(this.cmbRunStatus);
            this.groupBoxStatus.Controls.Add(this.labelRunStatus);
            this.groupBoxStatus.Controls.Add(this.cmbRunMode);
            this.groupBoxStatus.Controls.Add(this.labelRunMode);
            this.groupBoxStatus.Location = new System.Drawing.Point(13, 156);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(499, 144);
            this.groupBoxStatus.TabIndex = 2;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // btnSetStatus
            // 
            this.btnSetStatus.Location = new System.Drawing.Point(384, 72);
            this.btnSetStatus.Name = "btnSetStatus";
            this.btnSetStatus.Size = new System.Drawing.Size(102, 36);
            this.btnSetStatus.TabIndex = 5;
            this.btnSetStatus.Text = "Set";
            this.btnSetStatus.Click += new System.EventHandler(this.btnSetStatus_Click);
            // 
            // btnGetStatus
            // 
            this.btnGetStatus.Location = new System.Drawing.Point(384, 36);
            this.btnGetStatus.Name = "btnGetStatus";
            this.btnGetStatus.Size = new System.Drawing.Size(102, 36);
            this.btnGetStatus.TabIndex = 2;
            this.btnGetStatus.Text = "Get";
            this.btnGetStatus.Click += new System.EventHandler(this.btnGetStatus_Click);
            // 
            // cmbCPUStatus
            // 
            this.cmbCPUStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbCPUStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbCPUStatus.Items.AddRange(new object[] {
            "Stop",
            "Run"});
            this.cmbCPUStatus.Location = new System.Drawing.Point(154, 108);
            this.cmbCPUStatus.Name = "cmbCPUStatus";
            this.cmbCPUStatus.Size = new System.Drawing.Size(192, 30);
            this.cmbCPUStatus.TabIndex = 7;
            // 
            // labelCPUStatus
            // 
            this.labelCPUStatus.Location = new System.Drawing.Point(26, 108);
            this.labelCPUStatus.Name = "labelCPUStatus";
            this.labelCPUStatus.Size = new System.Drawing.Size(102, 24);
            this.labelCPUStatus.TabIndex = 6;
            this.labelCPUStatus.Text = "CPUStatus";
            // 
            // cmbRunStatus
            // 
            this.cmbRunStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbRunStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbRunStatus.Items.AddRange(new object[] {
            "Stop",
            "Run"});
            this.cmbRunStatus.Location = new System.Drawing.Point(154, 72);
            this.cmbRunStatus.Name = "cmbRunStatus";
            this.cmbRunStatus.Size = new System.Drawing.Size(192, 30);
            this.cmbRunStatus.TabIndex = 4;
            // 
            // labelRunStatus
            // 
            this.labelRunStatus.Location = new System.Drawing.Point(26, 72);
            this.labelRunStatus.Name = "labelRunStatus";
            this.labelRunStatus.Size = new System.Drawing.Size(100, 24);
            this.labelRunStatus.TabIndex = 3;
            this.labelRunStatus.Text = "RunStatus";
            // 
            // cmbRunMode
            // 
            this.cmbRunMode.Items.AddRange(new object[] {
            "Program",
            "Run"});
            this.cmbRunMode.Location = new System.Drawing.Point(154, 36);
            this.cmbRunMode.Name = "cmbRunMode";
            this.cmbRunMode.Size = new System.Drawing.Size(192, 28);
            this.cmbRunMode.TabIndex = 1;
            // 
            // labelRunMode
            // 
            this.labelRunMode.Location = new System.Drawing.Point(26, 36);
            this.labelRunMode.Name = "labelRunMode";
            this.labelRunMode.Size = new System.Drawing.Size(88, 24);
            this.labelRunMode.TabIndex = 0;
            this.labelRunMode.Text = "RunMode";
            // 
            // groupBoxClock
            // 
            this.groupBoxClock.Controls.Add(this.btnSetClock);
            this.groupBoxClock.Controls.Add(this.btnGetClock);
            this.groupBoxClock.Controls.Add(this.txtClock);
            this.groupBoxClock.Location = new System.Drawing.Point(13, 84);
            this.groupBoxClock.Name = "groupBoxClock";
            this.groupBoxClock.Size = new System.Drawing.Size(499, 72);
            this.groupBoxClock.TabIndex = 1;
            this.groupBoxClock.TabStop = false;
            this.groupBoxClock.Text = "Clock";
            this.groupBoxClock.Enter += new System.EventHandler(this.groupBoxClock_Enter);
            // 
            // btnSetClock
            // 
            this.btnSetClock.Location = new System.Drawing.Point(384, 24);
            this.btnSetClock.Name = "btnSetClock";
            this.btnSetClock.Size = new System.Drawing.Size(102, 36);
            this.btnSetClock.TabIndex = 2;
            this.btnSetClock.Text = "Set";
            this.btnSetClock.Click += new System.EventHandler(this.btnSetClock_Click);
            // 
            // btnGetClock
            // 
            this.btnGetClock.Location = new System.Drawing.Point(282, 24);
            this.btnGetClock.Name = "btnGetClock";
            this.btnGetClock.Size = new System.Drawing.Size(102, 36);
            this.btnGetClock.TabIndex = 1;
            this.btnGetClock.Text = "Get";
            this.btnGetClock.Click += new System.EventHandler(this.btnGetClock_Click);
            // 
            // txtClock
            // 
            this.txtClock.Location = new System.Drawing.Point(13, 24);
            this.txtClock.Name = "txtClock";
            this.txtClock.Size = new System.Drawing.Size(243, 27);
            this.txtClock.TabIndex = 0;
            // 
            // groupBoxUnitName
            // 
            this.groupBoxUnitName.Controls.Add(this.btnSetUnitName);
            this.groupBoxUnitName.Controls.Add(this.btnGetUnitName);
            this.groupBoxUnitName.Controls.Add(this.txtUnitName);
            this.groupBoxUnitName.Location = new System.Drawing.Point(13, 12);
            this.groupBoxUnitName.Name = "groupBoxUnitName";
            this.groupBoxUnitName.Size = new System.Drawing.Size(499, 72);
            this.groupBoxUnitName.TabIndex = 0;
            this.groupBoxUnitName.TabStop = false;
            this.groupBoxUnitName.Text = "Unit Name";
            // 
            // btnSetUnitName
            // 
            this.btnSetUnitName.Location = new System.Drawing.Point(384, 24);
            this.btnSetUnitName.Name = "btnSetUnitName";
            this.btnSetUnitName.Size = new System.Drawing.Size(102, 36);
            this.btnSetUnitName.TabIndex = 2;
            this.btnSetUnitName.Text = "Set";
            this.btnSetUnitName.Click += new System.EventHandler(this.btnSetUnitName_Click);
            // 
            // btnGetUnitName
            // 
            this.btnGetUnitName.Location = new System.Drawing.Point(282, 24);
            this.btnGetUnitName.Name = "btnGetUnitName";
            this.btnGetUnitName.Size = new System.Drawing.Size(102, 36);
            this.btnGetUnitName.TabIndex = 1;
            this.btnGetUnitName.Text = "Get";
            this.btnGetUnitName.Click += new System.EventHandler(this.btnGetUnitName_Click);
            // 
            // txtUnitName
            // 
            this.txtUnitName.Location = new System.Drawing.Point(13, 24);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(243, 27);
            this.txtUnitName.TabIndex = 0;
            // 
            // tabPageOption
            // 
            this.tabPageOption.Controls.Add(this.groupBoxPlcEncoding);
            this.tabPageOption.Controls.Add(this.groupBoxHeartBeat);
            this.tabPageOption.Controls.Add(this.groupBoxMessageTimeLimit);
            this.tabPageOption.Location = new System.Drawing.Point(4, 21);
            this.tabPageOption.Name = "tabPageOption";
            this.tabPageOption.Size = new System.Drawing.Size(517, 622);
            this.tabPageOption.TabIndex = 2;
            this.tabPageOption.Text = "Options";
            // 
            // groupBoxPlcEncoding
            // 
            this.groupBoxPlcEncoding.Controls.Add(this.btnSetPlcEncoding);
            this.groupBoxPlcEncoding.Controls.Add(this.btnGetPlcEncoding);
            this.groupBoxPlcEncoding.Controls.Add(this.cmbPlcEncodingName);
            this.groupBoxPlcEncoding.Location = new System.Drawing.Point(13, 204);
            this.groupBoxPlcEncoding.Name = "groupBoxPlcEncoding";
            this.groupBoxPlcEncoding.Size = new System.Drawing.Size(499, 84);
            this.groupBoxPlcEncoding.TabIndex = 2;
            this.groupBoxPlcEncoding.TabStop = false;
            this.groupBoxPlcEncoding.Text = "PlcEncoding";
            // 
            // btnSetPlcEncoding
            // 
            this.btnSetPlcEncoding.Location = new System.Drawing.Point(384, 36);
            this.btnSetPlcEncoding.Name = "btnSetPlcEncoding";
            this.btnSetPlcEncoding.Size = new System.Drawing.Size(102, 36);
            this.btnSetPlcEncoding.TabIndex = 2;
            this.btnSetPlcEncoding.Text = "Set";
            this.btnSetPlcEncoding.Click += new System.EventHandler(this.btnSetPlcEncoding_Click);
            // 
            // btnGetPlcEncoding
            // 
            this.btnGetPlcEncoding.Location = new System.Drawing.Point(282, 36);
            this.btnGetPlcEncoding.Name = "btnGetPlcEncoding";
            this.btnGetPlcEncoding.Size = new System.Drawing.Size(102, 36);
            this.btnGetPlcEncoding.TabIndex = 1;
            this.btnGetPlcEncoding.Text = "Get";
            this.btnGetPlcEncoding.Click += new System.EventHandler(this.btnGetPlcEncoding_Click);
            // 
            // cmbPlcEncodingName
            // 
            this.cmbPlcEncodingName.Items.AddRange(new object[] {
            "utf-8",
            "shift_jis"});
            this.cmbPlcEncodingName.Location = new System.Drawing.Point(13, 36);
            this.cmbPlcEncodingName.Name = "cmbPlcEncodingName";
            this.cmbPlcEncodingName.Size = new System.Drawing.Size(256, 28);
            this.cmbPlcEncodingName.TabIndex = 0;
            this.cmbPlcEncodingName.Text = "utf-8";
            // 
            // groupBoxHeartBeat
            // 
            this.groupBoxHeartBeat.Controls.Add(this.txtCurrentTime);
            this.groupBoxHeartBeat.Controls.Add(this.labelCurrentTime);
            this.groupBoxHeartBeat.Controls.Add(this.btnSetHeartBeat);
            this.groupBoxHeartBeat.Controls.Add(this.btnGetHeartBeatTimer);
            this.groupBoxHeartBeat.Controls.Add(this.labelSEC);
            this.groupBoxHeartBeat.Controls.Add(this.txtTimerInterval);
            this.groupBoxHeartBeat.Controls.Add(this.labelHeartBeatTimer);
            this.groupBoxHeartBeat.Enabled = false;
            this.groupBoxHeartBeat.Location = new System.Drawing.Point(13, 96);
            this.groupBoxHeartBeat.Name = "groupBoxHeartBeat";
            this.groupBoxHeartBeat.Size = new System.Drawing.Size(499, 108);
            this.groupBoxHeartBeat.TabIndex = 1;
            this.groupBoxHeartBeat.TabStop = false;
            this.groupBoxHeartBeat.Text = "HeartBeat";
            // 
            // txtCurrentTime
            // 
            this.txtCurrentTime.Location = new System.Drawing.Point(166, 72);
            this.txtCurrentTime.Name = "txtCurrentTime";
            this.txtCurrentTime.ReadOnly = true;
            this.txtCurrentTime.Size = new System.Drawing.Size(320, 27);
            this.txtCurrentTime.TabIndex = 6;
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.Location = new System.Drawing.Point(13, 72);
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(141, 24);
            this.labelCurrentTime.TabIndex = 5;
            this.labelCurrentTime.Text = "_CurrentTime";
            // 
            // btnSetHeartBeat
            // 
            this.btnSetHeartBeat.Location = new System.Drawing.Point(384, 24);
            this.btnSetHeartBeat.Name = "btnSetHeartBeat";
            this.btnSetHeartBeat.Size = new System.Drawing.Size(102, 36);
            this.btnSetHeartBeat.TabIndex = 4;
            this.btnSetHeartBeat.Text = "Set";
            // 
            // btnGetHeartBeatTimer
            // 
            this.btnGetHeartBeatTimer.Location = new System.Drawing.Point(282, 24);
            this.btnGetHeartBeatTimer.Name = "btnGetHeartBeatTimer";
            this.btnGetHeartBeatTimer.Size = new System.Drawing.Size(102, 36);
            this.btnGetHeartBeatTimer.TabIndex = 3;
            this.btnGetHeartBeatTimer.Text = "Get";
            // 
            // labelSEC
            // 
            this.labelSEC.Location = new System.Drawing.Point(218, 36);
            this.labelSEC.Name = "labelSEC";
            this.labelSEC.Size = new System.Drawing.Size(51, 24);
            this.labelSEC.TabIndex = 2;
            this.labelSEC.Text = "(sec)";
            // 
            // txtTimerInterval
            // 
            this.txtTimerInterval.Location = new System.Drawing.Point(166, 24);
            this.txtTimerInterval.Name = "txtTimerInterval";
            this.txtTimerInterval.Size = new System.Drawing.Size(52, 27);
            this.txtTimerInterval.TabIndex = 1;
            this.txtTimerInterval.Text = "1";
            // 
            // labelHeartBeatTimer
            // 
            this.labelHeartBeatTimer.Location = new System.Drawing.Point(13, 36);
            this.labelHeartBeatTimer.Name = "labelHeartBeatTimer";
            this.labelHeartBeatTimer.Size = new System.Drawing.Size(141, 24);
            this.labelHeartBeatTimer.TabIndex = 0;
            this.labelHeartBeatTimer.Text = "HeartBeatTimer";
            // 
            // groupBoxMessageTimeLimit
            // 
            this.groupBoxMessageTimeLimit.Controls.Add(this.btnSetMessageTimeLimit);
            this.groupBoxMessageTimeLimit.Controls.Add(this.btnGetMessageTimeLimit);
            this.groupBoxMessageTimeLimit.Controls.Add(this.labelMS);
            this.groupBoxMessageTimeLimit.Controls.Add(this.txtReceiveTimeLimit);
            this.groupBoxMessageTimeLimit.Location = new System.Drawing.Point(13, 12);
            this.groupBoxMessageTimeLimit.Name = "groupBoxMessageTimeLimit";
            this.groupBoxMessageTimeLimit.Size = new System.Drawing.Size(499, 84);
            this.groupBoxMessageTimeLimit.TabIndex = 0;
            this.groupBoxMessageTimeLimit.TabStop = false;
            this.groupBoxMessageTimeLimit.Text = "Message Time Limit";
            // 
            // btnSetMessageTimeLimit
            // 
            this.btnSetMessageTimeLimit.Location = new System.Drawing.Point(384, 36);
            this.btnSetMessageTimeLimit.Name = "btnSetMessageTimeLimit";
            this.btnSetMessageTimeLimit.Size = new System.Drawing.Size(102, 36);
            this.btnSetMessageTimeLimit.TabIndex = 3;
            this.btnSetMessageTimeLimit.Text = "Set";
            this.btnSetMessageTimeLimit.Click += new System.EventHandler(this.btnSetMessageTimeLimit_Click);
            // 
            // btnGetMessageTimeLimit
            // 
            this.btnGetMessageTimeLimit.Location = new System.Drawing.Point(282, 36);
            this.btnGetMessageTimeLimit.Name = "btnGetMessageTimeLimit";
            this.btnGetMessageTimeLimit.Size = new System.Drawing.Size(102, 36);
            this.btnGetMessageTimeLimit.TabIndex = 2;
            this.btnGetMessageTimeLimit.Text = "Get";
            this.btnGetMessageTimeLimit.Click += new System.EventHandler(this.btnGetMessageTimeLimit_Click);
            // 
            // labelMS
            // 
            this.labelMS.Location = new System.Drawing.Point(141, 48);
            this.labelMS.Name = "labelMS";
            this.labelMS.Size = new System.Drawing.Size(51, 24);
            this.labelMS.TabIndex = 1;
            this.labelMS.Text = "(ms)";
            // 
            // txtReceiveTimeLimit
            // 
            this.txtReceiveTimeLimit.Location = new System.Drawing.Point(13, 36);
            this.txtReceiveTimeLimit.Name = "txtReceiveTimeLimit";
            this.txtReceiveTimeLimit.Size = new System.Drawing.Size(115, 27);
            this.txtReceiveTimeLimit.TabIndex = 0;
            this.txtReceiveTimeLimit.Text = "0";
            this.txtReceiveTimeLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxTagTableVariables
            // 
            this.groupBoxTagTableVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTagTableVariables.Controls.Add(this.groupBoxBinaryVariableEdit);
            this.groupBoxTagTableVariables.Controls.Add(this.groupBoxVariableEdit);
            this.groupBoxTagTableVariables.Controls.Add(this.btnGetVaiable);
            this.groupBoxTagTableVariables.Controls.Add(this.chkSystemVariable);
            this.groupBoxTagTableVariables.Controls.Add(this.listViewOfVariableNames);
            this.groupBoxTagTableVariables.Controls.Add(this.chkDoNotFragment);
            this.groupBoxTagTableVariables.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxTagTableVariables.Location = new System.Drawing.Point(540, 3);
            this.groupBoxTagTableVariables.Name = "groupBoxTagTableVariables";
            this.groupBoxTagTableVariables.Size = new System.Drawing.Size(501, 557);
            this.groupBoxTagTableVariables.TabIndex = 3;
            this.groupBoxTagTableVariables.TabStop = false;
            this.groupBoxTagTableVariables.Text = "TagTable Variables";
            // 
            // groupBoxBinaryVariableEdit
            // 
            this.groupBoxBinaryVariableEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBinaryVariableEdit.Controls.Add(this.btnWriteRaw);
            this.groupBoxBinaryVariableEdit.Controls.Add(this.btnReadRaw);
            this.groupBoxBinaryVariableEdit.Controls.Add(this.txtBinaryValue);
            this.groupBoxBinaryVariableEdit.Controls.Add(this.labelValueOfBinary);
            this.groupBoxBinaryVariableEdit.Controls.Add(this.btnReadRawDataMultiple);
            this.groupBoxBinaryVariableEdit.Controls.Add(this.txtBinaryVariableName);
            this.groupBoxBinaryVariableEdit.Controls.Add(this.labelNameForUseBinary);
            this.groupBoxBinaryVariableEdit.Location = new System.Drawing.Point(6, 443);
            this.groupBoxBinaryVariableEdit.Name = "groupBoxBinaryVariableEdit";
            this.groupBoxBinaryVariableEdit.Size = new System.Drawing.Size(475, 108);
            this.groupBoxBinaryVariableEdit.TabIndex = 4;
            this.groupBoxBinaryVariableEdit.TabStop = false;
            this.groupBoxBinaryVariableEdit.Text = "Binary Variable Edit";
            // 
            // btnWriteRaw
            // 
            this.btnWriteRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteRaw.Location = new System.Drawing.Point(360, 60);
            this.btnWriteRaw.Name = "btnWriteRaw";
            this.btnWriteRaw.Size = new System.Drawing.Size(102, 36);
            this.btnWriteRaw.TabIndex = 6;
            this.btnWriteRaw.Text = "WriteRaw";
            this.btnWriteRaw.Click += new System.EventHandler(this.btnWriteRaw_Click);
            // 
            // btnReadRaw
            // 
            this.btnReadRaw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadRaw.Location = new System.Drawing.Point(258, 60);
            this.btnReadRaw.Name = "btnReadRaw";
            this.btnReadRaw.Size = new System.Drawing.Size(102, 36);
            this.btnReadRaw.TabIndex = 5;
            this.btnReadRaw.Text = "ReadRaw";
            this.btnReadRaw.Click += new System.EventHandler(this.btnReadRaw_Click);
            // 
            // txtBinaryValue
            // 
            this.txtBinaryValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBinaryValue.Location = new System.Drawing.Point(102, 60);
            this.txtBinaryValue.Name = "txtBinaryValue";
            this.txtBinaryValue.Size = new System.Drawing.Size(130, 27);
            this.txtBinaryValue.TabIndex = 4;
            this.txtBinaryValue.Text = "00";
            // 
            // labelValueOfBinary
            // 
            this.labelValueOfBinary.Location = new System.Drawing.Point(13, 60);
            this.labelValueOfBinary.Name = "labelValueOfBinary";
            this.labelValueOfBinary.Size = new System.Drawing.Size(64, 24);
            this.labelValueOfBinary.TabIndex = 3;
            this.labelValueOfBinary.Text = "Value";
            // 
            // btnReadRawDataMultiple
            // 
            this.btnReadRawDataMultiple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadRawDataMultiple.Location = new System.Drawing.Point(258, 24);
            this.btnReadRawDataMultiple.Name = "btnReadRawDataMultiple";
            this.btnReadRawDataMultiple.Size = new System.Drawing.Size(204, 36);
            this.btnReadRawDataMultiple.TabIndex = 2;
            this.btnReadRawDataMultiple.Text = "ReadRawMultiple";
            this.btnReadRawDataMultiple.Click += new System.EventHandler(this.btnReadRawDataMultiple_Click);
            // 
            // txtBinaryVariableName
            // 
            this.txtBinaryVariableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBinaryVariableName.Location = new System.Drawing.Point(102, 24);
            this.txtBinaryVariableName.Name = "txtBinaryVariableName";
            this.txtBinaryVariableName.Size = new System.Drawing.Size(130, 27);
            this.txtBinaryVariableName.TabIndex = 1;
            // 
            // labelNameForUseBinary
            // 
            this.labelNameForUseBinary.Location = new System.Drawing.Point(13, 24);
            this.labelNameForUseBinary.Name = "labelNameForUseBinary";
            this.labelNameForUseBinary.Size = new System.Drawing.Size(64, 24);
            this.labelNameForUseBinary.TabIndex = 0;
            this.labelNameForUseBinary.Text = "Name";
            // 
            // groupBoxVariableEdit
            // 
            this.groupBoxVariableEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVariableEdit.Controls.Add(this.btnWriteVariable);
            this.groupBoxVariableEdit.Controls.Add(this.btnReadVariable);
            this.groupBoxVariableEdit.Controls.Add(this.txtValue);
            this.groupBoxVariableEdit.Controls.Add(this.labelValue);
            this.groupBoxVariableEdit.Controls.Add(this.btnReadVariableMultiple);
            this.groupBoxVariableEdit.Controls.Add(this.txtVariableName);
            this.groupBoxVariableEdit.Controls.Add(this.labelName);
            this.groupBoxVariableEdit.Location = new System.Drawing.Point(6, 329);
            this.groupBoxVariableEdit.Name = "groupBoxVariableEdit";
            this.groupBoxVariableEdit.Size = new System.Drawing.Size(475, 108);
            this.groupBoxVariableEdit.TabIndex = 3;
            this.groupBoxVariableEdit.TabStop = false;
            this.groupBoxVariableEdit.Text = "Variable Edit";
            // 
            // btnWriteVariable
            // 
            this.btnWriteVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteVariable.Location = new System.Drawing.Point(360, 60);
            this.btnWriteVariable.Name = "btnWriteVariable";
            this.btnWriteVariable.Size = new System.Drawing.Size(102, 36);
            this.btnWriteVariable.TabIndex = 6;
            this.btnWriteVariable.Text = "Write";
            this.btnWriteVariable.Click += new System.EventHandler(this.btnWriteVariable_Click);
            // 
            // btnReadVariable
            // 
            this.btnReadVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadVariable.Location = new System.Drawing.Point(258, 60);
            this.btnReadVariable.Name = "btnReadVariable";
            this.btnReadVariable.Size = new System.Drawing.Size(102, 36);
            this.btnReadVariable.TabIndex = 5;
            this.btnReadVariable.Text = "Read";
            this.btnReadVariable.Click += new System.EventHandler(this.btnReadVariable_Click);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(102, 60);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(130, 27);
            this.txtValue.TabIndex = 4;
            this.txtValue.Text = "0";
            // 
            // labelValue
            // 
            this.labelValue.Location = new System.Drawing.Point(13, 60);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(64, 24);
            this.labelValue.TabIndex = 3;
            this.labelValue.Text = "Value";
            // 
            // btnReadVariableMultiple
            // 
            this.btnReadVariableMultiple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadVariableMultiple.Location = new System.Drawing.Point(258, 24);
            this.btnReadVariableMultiple.Name = "btnReadVariableMultiple";
            this.btnReadVariableMultiple.Size = new System.Drawing.Size(204, 36);
            this.btnReadVariableMultiple.TabIndex = 2;
            this.btnReadVariableMultiple.Text = "ReadVariableMultiple";
            this.btnReadVariableMultiple.Click += new System.EventHandler(this.btnReadVariableMultiple_Click);
            // 
            // txtVariableName
            // 
            this.txtVariableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVariableName.Location = new System.Drawing.Point(102, 24);
            this.txtVariableName.Name = "txtVariableName";
            this.txtVariableName.Size = new System.Drawing.Size(130, 27);
            this.txtVariableName.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(13, 24);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(64, 24);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name";
            // 
            // btnGetVaiable
            // 
            this.btnGetVaiable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetVaiable.Location = new System.Drawing.Point(290, 272);
            this.btnGetVaiable.Name = "btnGetVaiable";
            this.btnGetVaiable.Size = new System.Drawing.Size(205, 36);
            this.btnGetVaiable.TabIndex = 2;
            this.btnGetVaiable.Text = "Variable Names";
            this.btnGetVaiable.Click += new System.EventHandler(this.btnGetVaiable_Click);
            // 
            // chkSystemVariable
            // 
            this.chkSystemVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSystemVariable.Location = new System.Drawing.Point(5, 267);
            this.chkSystemVariable.Name = "chkSystemVariable";
            this.chkSystemVariable.Size = new System.Drawing.Size(192, 30);
            this.chkSystemVariable.TabIndex = 1;
            this.chkSystemVariable.Text = "System Variable";
            // 
            // listViewOfVariableNames
            // 
            this.listViewOfVariableNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewOfVariableNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.variableNameOfTagTable,
            this.variableTypeOfTagTable,
            this.variableValueOfTagTable});
            this.listViewOfVariableNames.FullRowSelect = true;
            this.listViewOfVariableNames.GridLines = true;
            this.listViewOfVariableNames.HideSelection = false;
            this.listViewOfVariableNames.Location = new System.Drawing.Point(4, 33);
            this.listViewOfVariableNames.Name = "listViewOfVariableNames";
            this.listViewOfVariableNames.Size = new System.Drawing.Size(491, 233);
            this.listViewOfVariableNames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewOfVariableNames.TabIndex = 0;
            this.listViewOfVariableNames.UseCompatibleStateImageBehavior = false;
            this.listViewOfVariableNames.View = System.Windows.Forms.View.Details;
            this.listViewOfVariableNames.SelectedIndexChanged += new System.EventHandler(this.listViewOfVariableNames_SelectedIndexChanged);
            // 
            // variableNameOfTagTable
            // 
            this.variableNameOfTagTable.Text = "Name";
            this.variableNameOfTagTable.Width = 81;
            // 
            // variableTypeOfTagTable
            // 
            this.variableTypeOfTagTable.Text = "Type";
            this.variableTypeOfTagTable.Width = 104;
            // 
            // variableValueOfTagTable
            // 
            this.variableValueOfTagTable.Text = "Value";
            this.variableValueOfTagTable.Width = 99;
            // 
            // chkDoNotFragment
            // 
            this.chkDoNotFragment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDoNotFragment.Location = new System.Drawing.Point(5, 292);
            this.chkDoNotFragment.Name = "chkDoNotFragment";
            this.chkDoNotFragment.Size = new System.Drawing.Size(179, 36);
            this.chkDoNotFragment.TabIndex = 2;
            this.chkDoNotFragment.Text = "DoNotFragment";
            this.chkDoNotFragment.CheckedChanged += new System.EventHandler(this.chkDoNotFragment_CheckedChanged);
            // 
            // tPageFinsDataConfig
            // 
            this.tPageFinsDataConfig.BackColor = System.Drawing.Color.LightGray;
            this.tPageFinsDataConfig.Controls.Add(this.splitContainer1);
            this.tPageFinsDataConfig.Location = new System.Drawing.Point(4, 36);
            this.tPageFinsDataConfig.Name = "tPageFinsDataConfig";
            this.tPageFinsDataConfig.Size = new System.Drawing.Size(1049, 563);
            this.tPageFinsDataConfig.TabIndex = 1;
            this.tPageFinsDataConfig.Text = "FinsData Config";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvDataConfig);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cBoxMonitor);
            this.splitContainer1.Panel2.Controls.Add(this.lbCycTime);
            this.splitContainer1.Panel2.Controls.Add(this.doUpDown);
            this.splitContainer1.Panel2.Controls.Add(this.btnConfigApply);
            this.splitContainer1.Panel2.Controls.Add(this.tBoxTableDataPath);
            this.splitContainer1.Panel2.Controls.Add(this.btnTableSaveAs);
            this.splitContainer1.Panel2.Controls.Add(this.btnTableLoad);
            this.splitContainer1.Panel2.Controls.Add(this.btnTableBrowse);
            this.splitContainer1.Panel2.Controls.Add(this.btnDelRow);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddRow);
            this.splitContainer1.Panel2.Controls.Add(this.btnParamSave);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1049, 563);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvDataConfig
            // 
            this.dgvDataConfig.AllowUserToAddRows = false;
            this.dgvDataConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDataConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataName,
            this.dataType,
            this.dataAssigned,
            this.dataAddress,
            this.dataChange,
            this.dataCurrentValue,
            this.ReadMode,
            this.Notify});
            this.dgvDataConfig.ContextMenuStrip = this.contxtMenuStrip;
            this.dgvDataConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataConfig.Location = new System.Drawing.Point(0, 0);
            this.dgvDataConfig.Name = "dgvDataConfig";
            this.dgvDataConfig.RowTemplate.Height = 27;
            this.dgvDataConfig.Size = new System.Drawing.Size(1049, 362);
            this.dgvDataConfig.TabIndex = 0;
            this.dgvDataConfig.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvDataConfig_CellBeginEdit);
            this.dgvDataConfig.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataConfig_CellClick);
            this.dgvDataConfig.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataConfig_CellDoubleClick);
            this.dgvDataConfig.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataConfig_CellEndEdit);
            this.dgvDataConfig.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataConfig_CellValidated);
            this.dgvDataConfig.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvDataConfig_KeyUp);
            // 
            // dataName
            // 
            this.dataName.DataPropertyName = "DataName";
            this.dataName.HeaderText = "名称";
            this.dataName.Name = "dataName";
            // 
            // dataType
            // 
            this.dataType.DataPropertyName = "DataType";
            this.dataType.HeaderText = "数据类型";
            this.dataType.Items.AddRange(new object[] {
            "bool",
            "byte",
            "short",
            "int16",
            "int32",
            "int64",
            "float",
            "double",
            "long",
            "string",
            "array",
            "struct",
            "class"});
            this.dataType.Name = "dataType";
            this.dataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataAssigned
            // 
            this.dataAssigned.DataPropertyName = "DataSection";
            this.dataAssigned.HeaderText = "分配区域";
            this.dataAssigned.Items.AddRange(new object[] {
            "CIO",
            "D",
            "WR",
            "HR",
            "AR",
            "C",
            "T",
            "Other"});
            this.dataAssigned.Name = "dataAssigned";
            this.dataAssigned.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataAssigned.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataAddress
            // 
            this.dataAddress.DataPropertyName = "DataAddress";
            this.dataAddress.HeaderText = "地址";
            this.dataAddress.Name = "dataAddress";
            // 
            // dataChange
            // 
            this.dataChange.DataPropertyName = "DataModifyValue";
            this.dataChange.HeaderText = "修改";
            this.dataChange.Name = "dataChange";
            // 
            // dataCurrentValue
            // 
            this.dataCurrentValue.DataPropertyName = "DataCurrentValue";
            this.dataCurrentValue.HeaderText = "在线值";
            this.dataCurrentValue.Name = "dataCurrentValue";
            // 
            // ReadMode
            // 
            this.ReadMode.HeaderText = "读取模式";
            this.ReadMode.Items.AddRange(new object[] {
            "Cycle",
            "NonCycle",
            "Other"});
            this.ReadMode.Name = "ReadMode";
            // 
            // Notify
            // 
            this.Notify.HeaderText = "事件通知";
            this.Notify.Name = "Notify";
            // 
            // cBoxMonitor
            // 
            this.cBoxMonitor.AutoSize = true;
            this.cBoxMonitor.Location = new System.Drawing.Point(917, 18);
            this.cBoxMonitor.Name = "cBoxMonitor";
            this.cBoxMonitor.Size = new System.Drawing.Size(74, 31);
            this.cBoxMonitor.TabIndex = 119;
            this.cBoxMonitor.Text = "监控";
            this.cBoxMonitor.UseVisualStyleBackColor = true;
            this.cBoxMonitor.CheckedChanged += new System.EventHandler(this.cBoxMonitor_CheckedChanged);
            // 
            // lbCycTime
            // 
            this.lbCycTime.AutoSize = true;
            this.lbCycTime.Location = new System.Drawing.Point(413, 18);
            this.lbCycTime.Name = "lbCycTime";
            this.lbCycTime.Size = new System.Drawing.Size(97, 27);
            this.lbCycTime.TabIndex = 118;
            this.lbCycTime.Text = "CycTime:";
            this.lbCycTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // doUpDown
            // 
            this.doUpDown.Location = new System.Drawing.Point(513, 14);
            this.doUpDown.Name = "doUpDown";
            this.doUpDown.Size = new System.Drawing.Size(108, 34);
            this.doUpDown.TabIndex = 117;
            this.doUpDown.Text = "0";
            this.doUpDown.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
            // 
            // btnConfigApply
            // 
            this.btnConfigApply.Location = new System.Drawing.Point(204, 11);
            this.btnConfigApply.Name = "btnConfigApply";
            this.btnConfigApply.Size = new System.Drawing.Size(205, 41);
            this.btnConfigApply.TabIndex = 116;
            this.btnConfigApply.Text = "ConfigApply";
            this.btnConfigApply.UseVisualStyleBackColor = true;
            this.btnConfigApply.Click += new System.EventHandler(this.btnConfigApply_Click);
            // 
            // tBoxTableDataPath
            // 
            this.tBoxTableDataPath.Location = new System.Drawing.Point(8, 101);
            this.tBoxTableDataPath.Name = "tBoxTableDataPath";
            this.tBoxTableDataPath.Size = new System.Drawing.Size(401, 34);
            this.tBoxTableDataPath.TabIndex = 115;
            // 
            // btnTableSaveAs
            // 
            this.btnTableSaveAs.Location = new System.Drawing.Point(641, 98);
            this.btnTableSaveAs.Name = "btnTableSaveAs";
            this.btnTableSaveAs.Size = new System.Drawing.Size(108, 41);
            this.btnTableSaveAs.TabIndex = 114;
            this.btnTableSaveAs.Text = "Save As";
            this.btnTableSaveAs.UseVisualStyleBackColor = true;
            this.btnTableSaveAs.Click += new System.EventHandler(this.btnTableSaveAs_Click);
            // 
            // btnTableLoad
            // 
            this.btnTableLoad.Location = new System.Drawing.Point(513, 98);
            this.btnTableLoad.Name = "btnTableLoad";
            this.btnTableLoad.Size = new System.Drawing.Size(108, 41);
            this.btnTableLoad.TabIndex = 114;
            this.btnTableLoad.Text = "Load";
            this.btnTableLoad.UseVisualStyleBackColor = true;
            this.btnTableLoad.Click += new System.EventHandler(this.btnTableLoad_Click);
            // 
            // btnTableBrowse
            // 
            this.btnTableBrowse.Location = new System.Drawing.Point(418, 97);
            this.btnTableBrowse.Name = "btnTableBrowse";
            this.btnTableBrowse.Size = new System.Drawing.Size(80, 41);
            this.btnTableBrowse.TabIndex = 114;
            this.btnTableBrowse.Text = "...";
            this.btnTableBrowse.UseVisualStyleBackColor = true;
            this.btnTableBrowse.Click += new System.EventHandler(this.btnTableBrowse_Click);
            // 
            // btnDelRow
            // 
            this.btnDelRow.Location = new System.Drawing.Point(106, 11);
            this.btnDelRow.Name = "btnDelRow";
            this.btnDelRow.Size = new System.Drawing.Size(80, 41);
            this.btnDelRow.TabIndex = 113;
            this.btnDelRow.Text = "-";
            this.btnDelRow.UseVisualStyleBackColor = true;
            this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(8, 11);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(80, 41);
            this.btnAddRow.TabIndex = 113;
            this.btnAddRow.Text = "+";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnParamSave
            // 
            this.btnParamSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnParamSave.Location = new System.Drawing.Point(876, 98);
            this.btnParamSave.Name = "btnParamSave";
            this.btnParamSave.Size = new System.Drawing.Size(164, 41);
            this.btnParamSave.TabIndex = 112;
            this.btnParamSave.Text = "保存配置";
            this.btnParamSave.UseVisualStyleBackColor = false;
            this.btnParamSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tPageBasicConfig
            // 
            this.tPageBasicConfig.BackColor = System.Drawing.Color.LightGray;
            this.tPageBasicConfig.Controls.Add(this.btnApply);
            this.tPageBasicConfig.Controls.Add(this.comBoxCommMode);
            this.tPageBasicConfig.Controls.Add(this.groupBox);
            this.tPageBasicConfig.Controls.Add(this.tBoxLocalIp);
            this.tPageBasicConfig.Controls.Add(this.tBoxDstPort);
            this.tPageBasicConfig.Controls.Add(this.tBoxDstIp);
            this.tPageBasicConfig.Controls.Add(this.label8);
            this.tPageBasicConfig.Controls.Add(this.label7);
            this.tPageBasicConfig.Controls.Add(this.label3);
            this.tPageBasicConfig.Controls.Add(this.label5);
            this.tPageBasicConfig.Controls.Add(this.label6);
            this.tPageBasicConfig.Controls.Add(this.label4);
            this.tPageBasicConfig.Controls.Add(this.label10);
            this.tPageBasicConfig.Controls.Add(this.label9);
            this.tPageBasicConfig.Controls.Add(this.label2);
            this.tPageBasicConfig.Location = new System.Drawing.Point(4, 36);
            this.tPageBasicConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tPageBasicConfig.Name = "tPageBasicConfig";
            this.tPageBasicConfig.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tPageBasicConfig.Size = new System.Drawing.Size(1049, 563);
            this.tPageBasicConfig.TabIndex = 0;
            this.tPageBasicConfig.Text = "Basic Config";
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.LightGreen;
            this.btnApply.Location = new System.Drawing.Point(842, 433);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(78, 41);
            this.btnApply.TabIndex = 114;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // comBoxCommMode
            // 
            this.comBoxCommMode.FormattingEnabled = true;
            this.comBoxCommMode.Items.AddRange(new object[] {
            "Fins",
            "Complet"});
            this.comBoxCommMode.Location = new System.Drawing.Point(212, 124);
            this.comBoxCommMode.Name = "comBoxCommMode";
            this.comBoxCommMode.Size = new System.Drawing.Size(166, 35);
            this.comBoxCommMode.TabIndex = 113;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnStop);
            this.groupBox.Controls.Add(this.btnRun);
            this.groupBox.Controls.Add(this.btnInit);
            this.groupBox.Location = new System.Drawing.Point(608, 405);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(220, 77);
            this.groupBox.TabIndex = 111;
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
            // tBoxLocalIp
            // 
            this.tBoxLocalIp.Location = new System.Drawing.Point(516, 79);
            this.tBoxLocalIp.Name = "tBoxLocalIp";
            this.tBoxLocalIp.Size = new System.Drawing.Size(166, 34);
            this.tBoxLocalIp.TabIndex = 1;
            // 
            // tBoxDstPort
            // 
            this.tBoxDstPort.Location = new System.Drawing.Point(396, 79);
            this.tBoxDstPort.Name = "tBoxDstPort";
            this.tBoxDstPort.Size = new System.Drawing.Size(101, 34);
            this.tBoxDstPort.TabIndex = 1;
            // 
            // tBoxDstIp
            // 
            this.tBoxDstIp.Location = new System.Drawing.Point(212, 79);
            this.tBoxDstIp.Name = "tBoxDstIp";
            this.tBoxDstIp.Size = new System.Drawing.Size(166, 34);
            this.tBoxDstIp.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(513, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 27);
            this.label8.TabIndex = 0;
            this.label8.Text = "192.168.250.10";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(391, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 27);
            this.label7.TabIndex = 0;
            this.label7.Text = "9600";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(213, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "192.168.250.1 ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(404, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 27);
            this.label5.TabIndex = 0;
            this.label5.Text = "Dst Port";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(534, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "Local IP";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dst IP";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 27);
            this.label10.TabIndex = 0;
            this.label10.Text = "CommMode:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 27);
            this.label9.TabIndex = 0;
            this.label9.Text = "CommInfo:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "CommInfo Sample:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPageBasicConfig);
            this.tabControl1.Controls.Add(this.tPageFinsDataConfig);
            this.tabControl1.Controls.Add(this.tPageCompletConfig);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tabControl1.Location = new System.Drawing.Point(0, 87);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1057, 603);
            this.tabControl1.TabIndex = 18;
            // 
            // njCompolet1
            // 
            this.njCompolet1.Active = false;
            this.njCompolet1.ConnectionType = OMRON.Compolet.CIPCompolet64.ConnectionType.UCMM;
            this.njCompolet1.DontFragment = false;
            this.njCompolet1.LocalPort = 2;
            this.njCompolet1.PeerAddress = "192.168.250.1";
            this.njCompolet1.ReceiveTimeLimit = ((long)(750));
            this.njCompolet1.RoutePath = "2%192.168.250.1\\1%0";
            this.njCompolet1.UseRoutePath = false;
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
            this.Size = new System.Drawing.Size(1057, 690);
            this.Load += new System.EventHandler(this.PrimConfigControl_Load);
            this.contxtMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tPageCompletConfig.ResumeLayout(false);
            this.tabNJCompolet.ResumeLayout(false);
            this.tabPageCommunication.ResumeLayout(false);
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.groupBoxPeerAddress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPortNo)).EndInit();
            this.tabPageController.ResumeLayout(false);
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxClock.ResumeLayout(false);
            this.groupBoxClock.PerformLayout();
            this.groupBoxUnitName.ResumeLayout(false);
            this.groupBoxUnitName.PerformLayout();
            this.tabPageOption.ResumeLayout(false);
            this.groupBoxPlcEncoding.ResumeLayout(false);
            this.groupBoxHeartBeat.ResumeLayout(false);
            this.groupBoxHeartBeat.PerformLayout();
            this.groupBoxMessageTimeLimit.ResumeLayout(false);
            this.groupBoxMessageTimeLimit.PerformLayout();
            this.groupBoxTagTableVariables.ResumeLayout(false);
            this.groupBoxBinaryVariableEdit.ResumeLayout(false);
            this.groupBoxBinaryVariableEdit.PerformLayout();
            this.groupBoxVariableEdit.ResumeLayout(false);
            this.groupBoxVariableEdit.PerformLayout();
            this.tPageFinsDataConfig.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataConfig)).EndInit();
            this.tPageBasicConfig.ResumeLayout(false);
            this.tPageBasicConfig.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.ContextMenuStrip contxtMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 增加行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.TextBox tTaskBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBoxRunState;
        private System.Windows.Forms.TextBox tBoxConnState;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tPageCompletConfig;
        private System.Windows.Forms.TabControl tabNJCompolet;
        private System.Windows.Forms.TabPage tabPageCommunication;
        private System.Windows.Forms.Button btnGetCommunication;
        private System.Windows.Forms.ListView listViewOfEachValue;
        private System.Windows.Forms.ColumnHeader variableName;
        private System.Windows.Forms.ColumnHeader variableValue;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.ComboBox cmbConnectionType;
        private System.Windows.Forms.Label labelConnectionType;
        private System.Windows.Forms.TextBox txtRoutePath;
        private System.Windows.Forms.Label labelRoutePath;
        private System.Windows.Forms.RadioButton radioRoutePath;
        private System.Windows.Forms.GroupBox groupBoxRoutePath;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label labelIPAddress;
        private System.Windows.Forms.Label labelPortNo;
        private System.Windows.Forms.RadioButton radioPeerAddress;
        private System.Windows.Forms.GroupBox groupBoxPeerAddress;
        private System.Windows.Forms.NumericUpDown numPortNo;
        private System.Windows.Forms.TabPage tabPageController;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Button btnSetStatus;
        private System.Windows.Forms.Button btnGetStatus;
        private System.Windows.Forms.ComboBox cmbCPUStatus;
        private System.Windows.Forms.Label labelCPUStatus;
        private System.Windows.Forms.ComboBox cmbRunStatus;
        private System.Windows.Forms.Label labelRunStatus;
        private System.Windows.Forms.ComboBox cmbRunMode;
        private System.Windows.Forms.Label labelRunMode;
        private System.Windows.Forms.GroupBox groupBoxClock;
        private System.Windows.Forms.Button btnSetClock;
        private System.Windows.Forms.Button btnGetClock;
        private System.Windows.Forms.TextBox txtClock;
        private System.Windows.Forms.GroupBox groupBoxUnitName;
        private System.Windows.Forms.Button btnSetUnitName;
        private System.Windows.Forms.Button btnGetUnitName;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.TabPage tabPageOption;
        private System.Windows.Forms.GroupBox groupBoxPlcEncoding;
        private System.Windows.Forms.Button btnSetPlcEncoding;
        private System.Windows.Forms.Button btnGetPlcEncoding;
        private System.Windows.Forms.ComboBox cmbPlcEncodingName;
        private System.Windows.Forms.GroupBox groupBoxHeartBeat;
        private System.Windows.Forms.TextBox txtCurrentTime;
        private System.Windows.Forms.Label labelCurrentTime;
        private System.Windows.Forms.Button btnSetHeartBeat;
        private System.Windows.Forms.Button btnGetHeartBeatTimer;
        private System.Windows.Forms.Label labelSEC;
        private System.Windows.Forms.TextBox txtTimerInterval;
        private System.Windows.Forms.Label labelHeartBeatTimer;
        private System.Windows.Forms.GroupBox groupBoxMessageTimeLimit;
        private System.Windows.Forms.Button btnSetMessageTimeLimit;
        private System.Windows.Forms.Button btnGetMessageTimeLimit;
        private System.Windows.Forms.Label labelMS;
        private System.Windows.Forms.TextBox txtReceiveTimeLimit;
        private System.Windows.Forms.GroupBox groupBoxTagTableVariables;
        private System.Windows.Forms.GroupBox groupBoxBinaryVariableEdit;
        private System.Windows.Forms.Button btnWriteRaw;
        private System.Windows.Forms.Button btnReadRaw;
        private System.Windows.Forms.TextBox txtBinaryValue;
        private System.Windows.Forms.Label labelValueOfBinary;
        private System.Windows.Forms.Button btnReadRawDataMultiple;
        private System.Windows.Forms.TextBox txtBinaryVariableName;
        private System.Windows.Forms.Label labelNameForUseBinary;
        private System.Windows.Forms.GroupBox groupBoxVariableEdit;
        private System.Windows.Forms.Button btnWriteVariable;
        private System.Windows.Forms.Button btnReadVariable;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Button btnReadVariableMultiple;
        private System.Windows.Forms.TextBox txtVariableName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button btnGetVaiable;
        private System.Windows.Forms.CheckBox chkSystemVariable;
        public System.Windows.Forms.ListView listViewOfVariableNames;
        private System.Windows.Forms.ColumnHeader variableNameOfTagTable;
        private System.Windows.Forms.ColumnHeader variableTypeOfTagTable;
        protected System.Windows.Forms.ColumnHeader variableValueOfTagTable;
        private System.Windows.Forms.CheckBox chkDoNotFragment;
        private System.Windows.Forms.TabPage tPageFinsDataConfig;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDataConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataName;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataType;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataAssigned;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataCurrentValue;
        private System.Windows.Forms.DataGridViewComboBoxColumn ReadMode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Notify;
        private System.Windows.Forms.Label lbCycTime;
        private System.Windows.Forms.DomainUpDown doUpDown;
        private System.Windows.Forms.Button btnConfigApply;
        private System.Windows.Forms.TextBox tBoxTableDataPath;
        private System.Windows.Forms.Button btnTableSaveAs;
        private System.Windows.Forms.Button btnTableLoad;
        private System.Windows.Forms.Button btnTableBrowse;
        private System.Windows.Forms.Button btnDelRow;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnParamSave;
        private System.Windows.Forms.TabPage tPageBasicConfig;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox comBoxCommMode;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.TextBox tBoxLocalIp;
        private System.Windows.Forms.TextBox tBoxDstPort;
        private System.Windows.Forms.TextBox tBoxDstIp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        public OMRON.Compolet.CIPCompolet64.NJCompolet njCompolet1;
        private System.Windows.Forms.CheckBox cBoxMonitor;
    }
}
