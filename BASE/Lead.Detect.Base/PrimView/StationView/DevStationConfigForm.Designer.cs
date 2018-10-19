namespace Lead.Detect.View.StationView
{
    partial class DevStationConfigForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtGridViewStation = new System.Windows.Forms.DataGridView();
            this.ColStationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColStationType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStationCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contxtMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeStationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewStation)).BeginInit();
            this.contxtMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtGridViewStation);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(1251, 633);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtGridViewStation
            // 
            this.dtGridViewStation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridViewStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewStation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColStationName,
            this.ColEnable,
            this.ColStationType,
            this.ColGuid,
            this.ColStationCol});
            this.dtGridViewStation.ContextMenuStrip = this.contxtMenuStrip;
            this.dtGridViewStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridViewStation.Location = new System.Drawing.Point(0, 25);
            this.dtGridViewStation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtGridViewStation.Name = "dtGridViewStation";
            this.dtGridViewStation.RowTemplate.Height = 27;
            this.dtGridViewStation.Size = new System.Drawing.Size(1251, 105);
            this.dtGridViewStation.TabIndex = 1;
            // 
            // ColStationName
            // 
            this.ColStationName.HeaderText = "工站名称";
            this.ColStationName.Name = "ColStationName";
            // 
            // ColEnable
            // 
            this.ColEnable.HeaderText = "使能";
            this.ColEnable.Name = "ColEnable";
            // 
            // ColStationType
            // 
            this.ColStationType.HeaderText = "工站类型";
            this.ColStationType.Items.AddRange(new object[] {
            "BO_S1",
            "BO_S2",
            "BO_S3",
            "BO_S4",
            "BO_S5",
            "BO_S6",
            "BG_S1",
            "BG_S2",
            "BG_S3",
            "BG_S4",
            "BG_S5",
            "BG_S6",
            "Other"});
            this.ColStationType.Name = "ColStationType";
            // 
            // ColGuid
            // 
            this.ColGuid.HeaderText = "工站ID";
            this.ColGuid.Name = "ColGuid";
            // 
            // ColStationCol
            // 
            this.ColStationCol.HeaderText = "工站备注";
            this.ColStationCol.Name = "ColStationCol";
            // 
            // contxtMenuStrip
            // 
            this.contxtMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contxtMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeStationToolStripMenuItem,
            this.applyToolStripMenuItem,
            this.applyToolStripMenuItem1});
            this.contxtMenuStrip.Name = "contxtMenuStrip";
            this.contxtMenuStrip.Size = new System.Drawing.Size(233, 108);
            this.contxtMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contxtMenuStrip_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.addToolStripMenuItem.Text = "Add Station";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeStationToolStripMenuItem
            // 
            this.removeStationToolStripMenuItem.Name = "removeStationToolStripMenuItem";
            this.removeStationToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.removeStationToolStripMenuItem.Text = "Remove Station";
            this.removeStationToolStripMenuItem.Click += new System.EventHandler(this.removeStationToolStripMenuItem_Click);
            // 
            // applyToolStripMenuItem
            // 
            this.applyToolStripMenuItem.Name = "applyToolStripMenuItem";
            this.applyToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.applyToolStripMenuItem.Text = "Show Station Config";
            this.applyToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // applyToolStripMenuItem1
            // 
            this.applyToolStripMenuItem1.Name = "applyToolStripMenuItem1";
            this.applyToolStripMenuItem1.Size = new System.Drawing.Size(232, 26);
            this.applyToolStripMenuItem1.Text = "Create Station";
            this.applyToolStripMenuItem1.Click += new System.EventHandler(this.applyToolStripMenuItem1_Click_1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1251, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "保存";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            this.toolStripLabel1.MouseLeave += new System.EventHandler(this.toolStripLabel1_MouseLeave);
            this.toolStripLabel1.MouseHover += new System.EventHandler(this.toolStripLabel1_MouseHover);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel2.Text = "加载";
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel3.Text = "测试";
            this.toolStripLabel3.Click += new System.EventHandler(this.toolStripLabel3_Click);
            // 
            // DevStationConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 633);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DevConfigForm";
            this.Text = "DevStationConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.DevConfigForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewStation)).EndInit();
            this.contxtMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contxtMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeStationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyToolStripMenuItem;
        private System.Windows.Forms.DataGridView dtGridViewStation;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem applyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStationName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColEnable;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColStationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStationCol;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    }
}