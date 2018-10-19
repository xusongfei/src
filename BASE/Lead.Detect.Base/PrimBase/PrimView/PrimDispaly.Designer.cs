namespace Lead.Detect.Base.PrimView
{
    partial class PrimDisplay
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
            this.tBoxName = new System.Windows.Forms.TextBox();
            this.pBoxIcon = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dispalyConfigUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayOutputUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.displayDebugUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxIcon)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tBoxName);
            this.panel1.Controls.Add(this.pBoxIcon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(60, 90);
            this.panel1.TabIndex = 0;
            // 
            // tBoxName
            // 
            this.tBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tBoxName.Location = new System.Drawing.Point(0, 60);
            this.tBoxName.Name = "tBoxName";
            this.tBoxName.Size = new System.Drawing.Size(60, 23);
            this.tBoxName.TabIndex = 1;
            this.tBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBoxName.Enter += new System.EventHandler(this.tBoxName_Enter);
            this.tBoxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBoxName_KeyDown);
            // 
            // pBoxIcon
            // 
            this.pBoxIcon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pBoxIcon.Location = new System.Drawing.Point(0, 0);
            this.pBoxIcon.Name = "pBoxIcon";
            this.pBoxIcon.Size = new System.Drawing.Size(60, 60);
            this.pBoxIcon.TabIndex = 0;
            this.pBoxIcon.TabStop = false;
            this.pBoxIcon.DoubleClick += new System.EventHandler(this.pBoxIcon_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dispalyConfigUIToolStripMenuItem,
            this.displayOutputUIToolStripMenuItem,
            this.displayDebugUIToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripSeparator1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 98);
            // 
            // dispalyConfigUIToolStripMenuItem
            // 
            this.dispalyConfigUIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.dispalyConfigUIToolStripMenuItem.Name = "dispalyConfigUIToolStripMenuItem";
            this.dispalyConfigUIToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.dispalyConfigUIToolStripMenuItem.Text = "ConfigUI";
            this.dispalyConfigUIToolStripMenuItem.Click += new System.EventHandler(this.dispalyConfigUIToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // displayOutputUIToolStripMenuItem
            // 
            this.displayOutputUIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem1,
            this.hideToolStripMenuItem1});
            this.displayOutputUIToolStripMenuItem.Name = "displayOutputUIToolStripMenuItem";
            this.displayOutputUIToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.displayOutputUIToolStripMenuItem.Text = "OutputUI";
            this.displayOutputUIToolStripMenuItem.Click += new System.EventHandler(this.displayOutputUIToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem1
            // 
            this.showToolStripMenuItem1.Name = "showToolStripMenuItem1";
            this.showToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.showToolStripMenuItem1.Text = "Show";
            this.showToolStripMenuItem1.Click += new System.EventHandler(this.showToolStripMenuItem1_Click);
            // 
            // hideToolStripMenuItem1
            // 
            this.hideToolStripMenuItem1.Name = "hideToolStripMenuItem1";
            this.hideToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.hideToolStripMenuItem1.Text = "Hide";
            this.hideToolStripMenuItem1.Click += new System.EventHandler(this.hideToolStripMenuItem1_Click);
            // 
            // displayDebugUIToolStripMenuItem
            // 
            this.displayDebugUIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem2,
            this.hideToolStripMenuItem2});
            this.displayDebugUIToolStripMenuItem.Name = "displayDebugUIToolStripMenuItem";
            this.displayDebugUIToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.displayDebugUIToolStripMenuItem.Text = "DebugUI";
            this.displayDebugUIToolStripMenuItem.Click += new System.EventHandler(this.displayDebugUIToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem2
            // 
            this.showToolStripMenuItem2.Name = "showToolStripMenuItem2";
            this.showToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.showToolStripMenuItem2.Text = "Show";
            this.showToolStripMenuItem2.Click += new System.EventHandler(this.showToolStripMenuItem2_Click);
            // 
            // hideToolStripMenuItem2
            // 
            this.hideToolStripMenuItem2.Name = "hideToolStripMenuItem2";
            this.hideToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.hideToolStripMenuItem2.Text = "Hide";
            this.hideToolStripMenuItem2.Click += new System.EventHandler(this.hideToolStripMenuItem2_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // PrimDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrimDisplay";
            this.Size = new System.Drawing.Size(60, 90);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxIcon)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox tBoxName;
        public System.Windows.Forms.PictureBox pBoxIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dispalyConfigUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayDebugUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem displayOutputUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem2;
    }
}
