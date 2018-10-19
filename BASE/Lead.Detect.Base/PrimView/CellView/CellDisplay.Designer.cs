namespace Lead.Detect.View.CellView
{
    partial class CellDisplay
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbGuid = new System.Windows.Forms.Label();
            this.tBoxName = new System.Windows.Forms.TextBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dispalyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diaplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.ContextMenuStrip = this.contextMenuStrip;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 60);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbGuid);
            this.splitContainer1.Panel1.Controls.Add(this.tBoxName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnDel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Size = new System.Drawing.Size(261, 60);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbGuid
            // 
            this.lbGuid.BackColor = System.Drawing.Color.Ivory;
            this.lbGuid.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbGuid.Location = new System.Drawing.Point(0, 30);
            this.lbGuid.Name = "lbGuid";
            this.lbGuid.Size = new System.Drawing.Size(205, 30);
            this.lbGuid.TabIndex = 2;
            this.lbGuid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxName
            // 
            this.tBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tBoxName.Location = new System.Drawing.Point(0, 0);
            this.tBoxName.Multiline = true;
            this.tBoxName.Name = "tBoxName";
            this.tBoxName.Size = new System.Drawing.Size(205, 30);
            this.tBoxName.TabIndex = 1;
            this.tBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDel
            // 
            this.btnDel.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDel.Location = new System.Drawing.Point(0, 30);
            this.btnDel.Name = "btnDel";
            this.btnDel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDel.Size = new System.Drawing.Size(48, 30);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "-";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(48, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configUIToolStripMenuItem,
            this.outputUIToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(146, 52);
            // 
            // configUIToolStripMenuItem
            // 
            this.configUIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dispalyToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.configUIToolStripMenuItem.Name = "configUIToolStripMenuItem";
            this.configUIToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.configUIToolStripMenuItem.Text = "ConfigUI";
            // 
            // dispalyToolStripMenuItem
            // 
            this.dispalyToolStripMenuItem.Name = "dispalyToolStripMenuItem";
            this.dispalyToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.dispalyToolStripMenuItem.Text = "Dispaly";
            this.dispalyToolStripMenuItem.Click += new System.EventHandler(this.dispalyToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // outputUIToolStripMenuItem
            // 
            this.outputUIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diaplayToolStripMenuItem,
            this.hideToolStripMenuItem1});
            this.outputUIToolStripMenuItem.Name = "outputUIToolStripMenuItem";
            this.outputUIToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.outputUIToolStripMenuItem.Text = "OutputUI";
            // 
            // diaplayToolStripMenuItem
            // 
            this.diaplayToolStripMenuItem.Name = "diaplayToolStripMenuItem";
            this.diaplayToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.diaplayToolStripMenuItem.Text = "Diaplay";
            this.diaplayToolStripMenuItem.Click += new System.EventHandler(this.diaplayToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem1
            // 
            this.hideToolStripMenuItem1.Name = "hideToolStripMenuItem1";
            this.hideToolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
            this.hideToolStripMenuItem1.Text = "Hide";
            this.hideToolStripMenuItem1.Click += new System.EventHandler(this.hideToolStripMenuItem1_Click);
            // 
            // CellDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CellDisplay";
            this.Size = new System.Drawing.Size(261, 60);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbGuid;
        private System.Windows.Forms.TextBox tBoxName;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem configUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dispalyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diaplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem1;
    }
}
