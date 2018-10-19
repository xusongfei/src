namespace Lead.Detect.Element
{
    partial class EleCylinderControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.tBoxInfo = new System.Windows.Forms.TextBox();
            this.tBoxName = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbType);
            this.splitContainer1.Panel2.Controls.Add(this.tBoxInfo);
            this.splitContainer1.Panel2.Controls.Add(this.tBoxName);
            this.splitContainer1.Size = new System.Drawing.Size(200, 70);
            this.splitContainer1.SplitterDistance = 60;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "气缸：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.labelDetails_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "注释：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.labelName_Click);
            // 
            // lbType
            // 
            this.lbType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbType.Location = new System.Drawing.Point(0, 42);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(135, 22);
            this.lbType.TabIndex = 14;
            this.lbType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbType.Click += new System.EventHandler(this.lbType_Click);
            // 
            // tBoxInfo
            // 
            this.tBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tBoxInfo.Location = new System.Drawing.Point(0, 20);
            this.tBoxInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tBoxInfo.Multiline = true;
            this.tBoxInfo.Name = "tBoxInfo";
            this.tBoxInfo.Size = new System.Drawing.Size(135, 22);
            this.tBoxInfo.TabIndex = 13;
            this.tBoxInfo.TextChanged += new System.EventHandler(this.tBoxInfo_TextChanged);
            // 
            // tBoxName
            // 
            this.tBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tBoxName.FormattingEnabled = true;
            this.tBoxName.Location = new System.Drawing.Point(0, 0);
            this.tBoxName.Name = "tBoxName";
            this.tBoxName.Size = new System.Drawing.Size(135, 20);
            this.tBoxName.TabIndex = 12;
            this.tBoxName.SelectedIndexChanged += new System.EventHandler(this.tBoxName_SelectedIndexChanged);
            this.tBoxName.TextChanged += new System.EventHandler(this.tBoxName_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // EleCylinderControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Controls.Add(this.splitContainer1);
            this.Name = "EleCylinderControl";
            this.Size = new System.Drawing.Size(200, 70);
            this.Load += new System.EventHandler(this.EleCylinder_Load);
            this.Click += new System.EventHandler(this.EleCylinder_Click);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.Label lbType;
        public System.Windows.Forms.TextBox tBoxInfo;
        private System.Windows.Forms.ComboBox tBoxName;
    }
}
