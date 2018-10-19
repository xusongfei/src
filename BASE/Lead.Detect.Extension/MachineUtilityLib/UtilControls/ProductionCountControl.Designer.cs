namespace MachineUtilityLib.UtilControls
{
    partial class ProductionCountControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxAll = new System.Windows.Forms.GroupBox();
            this.textBoxALL = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBoxOK = new System.Windows.Forms.GroupBox();
            this.textBoxOK = new System.Windows.Forms.TextBox();
            this.groupBoxNG = new System.Windows.Forms.GroupBox();
            this.textBoxNG = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBoxOK.SuspendLayout();
            this.groupBoxNG.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生产统计";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxAll);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(294, 44);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBoxAll
            // 
            this.groupBoxAll.Controls.Add(this.textBoxALL);
            this.groupBoxAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAll.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAll.Name = "groupBoxAll";
            this.groupBoxAll.Size = new System.Drawing.Size(97, 44);
            this.groupBoxAll.TabIndex = 0;
            this.groupBoxAll.TabStop = false;
            this.groupBoxAll.Text = "总计";
            // 
            // textBoxALL
            // 
            this.textBoxALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxALL.Location = new System.Drawing.Point(3, 17);
            this.textBoxALL.Name = "textBoxALL";
            this.textBoxALL.ReadOnly = true;
            this.textBoxALL.Size = new System.Drawing.Size(91, 21);
            this.textBoxALL.TabIndex = 0;
            this.textBoxALL.Text = "0";
            this.textBoxALL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxALL.WordWrap = false;
            this.textBoxALL.DoubleClick += new System.EventHandler(this.textBox_DoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBoxOK);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBoxNG);
            this.splitContainer2.Size = new System.Drawing.Size(193, 44);
            this.splitContainer2.SplitterDistance = 96;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBoxOK
            // 
            this.groupBoxOK.Controls.Add(this.textBoxOK);
            this.groupBoxOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOK.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOK.Name = "groupBoxOK";
            this.groupBoxOK.Size = new System.Drawing.Size(96, 44);
            this.groupBoxOK.TabIndex = 1;
            this.groupBoxOK.TabStop = false;
            this.groupBoxOK.Text = "OK";
            // 
            // textBoxOK
            // 
            this.textBoxOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOK.Location = new System.Drawing.Point(3, 17);
            this.textBoxOK.Name = "textBoxOK";
            this.textBoxOK.ReadOnly = true;
            this.textBoxOK.Size = new System.Drawing.Size(90, 21);
            this.textBoxOK.TabIndex = 1;
            this.textBoxOK.Text = "0";
            this.textBoxOK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxOK.WordWrap = false;
            this.textBoxOK.DoubleClick += new System.EventHandler(this.textBox_DoubleClick);
            // 
            // groupBoxNG
            // 
            this.groupBoxNG.Controls.Add(this.textBoxNG);
            this.groupBoxNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxNG.Location = new System.Drawing.Point(0, 0);
            this.groupBoxNG.Name = "groupBoxNG";
            this.groupBoxNG.Size = new System.Drawing.Size(93, 44);
            this.groupBoxNG.TabIndex = 1;
            this.groupBoxNG.TabStop = false;
            this.groupBoxNG.Text = "NG";
            // 
            // textBoxNG
            // 
            this.textBoxNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNG.Location = new System.Drawing.Point(3, 17);
            this.textBoxNG.Name = "textBoxNG";
            this.textBoxNG.ReadOnly = true;
            this.textBoxNG.Size = new System.Drawing.Size(87, 21);
            this.textBoxNG.TabIndex = 1;
            this.textBoxNG.Text = "0";
            this.textBoxNG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxNG.WordWrap = false;
            this.textBoxNG.DoubleClick += new System.EventHandler(this.textBox_DoubleClick);
            // 
            // ProductionCountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(300, 64);
            this.Name = "ProductionCountControl";
            this.Size = new System.Drawing.Size(300, 64);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxAll.ResumeLayout(false);
            this.groupBoxAll.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBoxOK.ResumeLayout(false);
            this.groupBoxOK.PerformLayout();
            this.groupBoxNG.ResumeLayout(false);
            this.groupBoxNG.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBoxAll;
        private System.Windows.Forms.GroupBox groupBoxOK;
        private System.Windows.Forms.GroupBox groupBoxNG;
        private System.Windows.Forms.TextBox textBoxALL;
        private System.Windows.Forms.TextBox textBoxOK;
        private System.Windows.Forms.TextBox textBoxNG;
    }
}
