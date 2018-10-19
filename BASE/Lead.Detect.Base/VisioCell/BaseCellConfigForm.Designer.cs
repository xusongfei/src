namespace Lead.Detect.DefaultCell
{
    partial class BaseCellConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseCellConfigForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgvPrim = new System.Windows.Forms.DataGridView();
            this.PrimName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrimGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutputDis = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ConfigDis = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DebugDis = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TabType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TabIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMenuStripPrimConfig = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPrimConfig = new System.Windows.Forms.ToolStrip();
            this.tStripLbUpdate = new System.Windows.Forms.ToolStripButton();
            this.tStripLbApplicate = new System.Windows.Forms.ToolStripButton();
            this.dtgvTask = new System.Windows.Forms.DataGridView();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Use = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RunMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStripCellConfig = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPrim)).BeginInit();
            this.cMenuStripPrimConfig.SuspendLayout();
            this.toolStripPrimConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTask)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.dtgvPrim);
            this.splitContainer1.Panel1.Controls.Add(this.toolStripPrimConfig);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgvTask);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripCellConfig);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 484);
            this.splitContainer1.SplitterDistance = 337;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtgvPrim
            // 
            this.dtgvPrim.AllowDrop = true;
            this.dtgvPrim.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvPrim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvPrim.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrimName,
            this.PrimGuid,
            this.OutputDis,
            this.ConfigDis,
            this.DebugDis,
            this.TabType,
            this.TabIndex,
            this.ControlIndex});
            this.dtgvPrim.ContextMenuStrip = this.cMenuStripPrimConfig;
            this.dtgvPrim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvPrim.Location = new System.Drawing.Point(0, 27);
            this.dtgvPrim.Name = "dtgvPrim";
            this.dtgvPrim.RowTemplate.Height = 27;
            this.dtgvPrim.Size = new System.Drawing.Size(1016, 310);
            this.dtgvPrim.TabIndex = 1;
            this.dtgvPrim.DragDrop += new System.Windows.Forms.DragEventHandler(this.dtgvPrim_DragDrop);
            this.dtgvPrim.DragEnter += new System.Windows.Forms.DragEventHandler(this.dtgvPrim_DragEnter);
            // 
            // PrimName
            // 
            this.PrimName.HeaderText = "PrimName";
            this.PrimName.Name = "PrimName";
            // 
            // PrimGuid
            // 
            this.PrimGuid.HeaderText = "PrimGuid";
            this.PrimGuid.Name = "PrimGuid";
            // 
            // OutputDis
            // 
            this.OutputDis.HeaderText = "OutputDis";
            this.OutputDis.Name = "OutputDis";
            // 
            // ConfigDis
            // 
            this.ConfigDis.HeaderText = "ConfigDis";
            this.ConfigDis.Name = "ConfigDis";
            // 
            // DebugDis
            // 
            this.DebugDis.HeaderText = "DebugDis";
            this.DebugDis.Name = "DebugDis";
            // 
            // TabType
            // 
            this.TabType.HeaderText = "TabType";
            this.TabType.Items.AddRange(new object[] {
            "SingleTab",
            "MultiTab"});
            this.TabType.Name = "TabType";
            // 
            // TabIndex
            // 
            this.TabIndex.HeaderText = "TabIndex";
            this.TabIndex.Name = "TabIndex";
            // 
            // ControlIndex
            // 
            this.ControlIndex.HeaderText = "ControlIndex";
            this.ControlIndex.Name = "ControlIndex";
            // 
            // cMenuStripPrimConfig
            // 
            this.cMenuStripPrimConfig.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMenuStripPrimConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.addToolStripMenuItem,
            this.deleteAllToolStripMenuItem,
            this.updateToolStripMenuItem});
            this.cMenuStripPrimConfig.Name = "cMenuStripPrimConfig";
            this.cMenuStripPrimConfig.Size = new System.Drawing.Size(153, 114);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.updateToolStripMenuItem.Text = "Update";
            // 
            // toolStripPrimConfig
            // 
            this.toolStripPrimConfig.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPrimConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tStripLbUpdate,
            this.tStripLbApplicate});
            this.toolStripPrimConfig.Location = new System.Drawing.Point(0, 0);
            this.toolStripPrimConfig.Name = "toolStripPrimConfig";
            this.toolStripPrimConfig.Size = new System.Drawing.Size(1016, 27);
            this.toolStripPrimConfig.TabIndex = 0;
            this.toolStripPrimConfig.Text = "tStripPrimConfig";
            // 
            // tStripLbUpdate
            // 
            this.tStripLbUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tStripLbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tStripLbUpdate.Image")));
            this.tStripLbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripLbUpdate.Name = "tStripLbUpdate";
            this.tStripLbUpdate.Size = new System.Drawing.Size(24, 24);
            this.tStripLbUpdate.Text = "刷新";
            this.tStripLbUpdate.Click += new System.EventHandler(this.tStripLbUpdate_Click);
            // 
            // tStripLbApplicate
            // 
            this.tStripLbApplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tStripLbApplicate.Image = ((System.Drawing.Image)(resources.GetObject("tStripLbApplicate.Image")));
            this.tStripLbApplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tStripLbApplicate.Name = "tStripLbApplicate";
            this.tStripLbApplicate.Size = new System.Drawing.Size(24, 24);
            this.tStripLbApplicate.Text = "应用";
            this.tStripLbApplicate.Click += new System.EventHandler(this.tStripLbApplicate_Click);
            // 
            // dtgvTask
            // 
            this.dtgvTask.AllowDrop = true;
            this.dtgvTask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskName,
            this.Use,
            this.RunMode});
            this.dtgvTask.ContextMenuStrip = this.cMenuStripPrimConfig;
            this.dtgvTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvTask.Location = new System.Drawing.Point(0, 25);
            this.dtgvTask.Name = "dtgvTask";
            this.dtgvTask.RowTemplate.Height = 27;
            this.dtgvTask.Size = new System.Drawing.Size(1016, 118);
            this.dtgvTask.TabIndex = 2;
            // 
            // TaskName
            // 
            this.TaskName.HeaderText = "TaskName";
            this.TaskName.Name = "TaskName";
            // 
            // Use
            // 
            this.Use.HeaderText = "Use";
            this.Use.Name = "Use";
            // 
            // RunMode
            // 
            this.RunMode.HeaderText = "RunMode";
            this.RunMode.Name = "RunMode";
            // 
            // toolStripCellConfig
            // 
            this.toolStripCellConfig.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripCellConfig.Location = new System.Drawing.Point(0, 0);
            this.toolStripCellConfig.Name = "toolStripCellConfig";
            this.toolStripCellConfig.Size = new System.Drawing.Size(1016, 25);
            this.toolStripCellConfig.TabIndex = 0;
            this.toolStripCellConfig.Text = "toolStripCellConfig";
            // 
            // DefaultCellConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 484);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DefaultCellConfigForm";
            this.TabText = "Cell Config";
            this.Text = "Cell Config";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPrim)).EndInit();
            this.cMenuStripPrimConfig.ResumeLayout(false);
            this.toolStripPrimConfig.ResumeLayout(false);
            this.toolStripPrimConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvTask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dtgvPrim;
        private System.Windows.Forms.ToolStrip toolStripPrimConfig;
        private System.Windows.Forms.DataGridView dtgvTask;
        private System.Windows.Forms.ToolStrip toolStripCellConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Use;
        private System.Windows.Forms.DataGridViewComboBoxColumn RunMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrimName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrimGuid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OutputDis;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ConfigDis;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DebugDis;
        private System.Windows.Forms.DataGridViewComboBoxColumn TabType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TabIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlIndex;
        private System.Windows.Forms.ContextMenuStrip cMenuStripPrimConfig;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tStripLbApplicate;
        private System.Windows.Forms.ToolStripButton tStripLbUpdate;
    }
}