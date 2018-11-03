namespace Lead.Detect.Element.EleControl
{
    partial class EleDiConfigControl
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
            this.btnRun = new System.Windows.Forms.Button();
            this.lbInputName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxnable1 = new System.Windows.Forms.CheckBox();
            this.tBoxDI1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tmrUpdateCylinder = new System.Windows.Forms.Timer(this.components);
            this.comboBoxPrimDev = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(301, 10);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(72, 30);
            this.btnRun.TabIndex = 31;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lbInputName
            // 
            this.lbInputName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbInputName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbInputName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbInputName.Location = new System.Drawing.Point(114, 10);
            this.lbInputName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInputName.Name = "lbInputName";
            this.lbInputName.Size = new System.Drawing.Size(173, 30);
            this.lbInputName.TabIndex = 27;
            this.lbInputName.Text = "InputName:";
            this.lbInputName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Location = new System.Drawing.Point(7, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 30);
            this.label6.TabIndex = 28;
            this.label6.Text = "InputName:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(7, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 29;
            this.label1.Text = "DevName:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cBoxnable1
            // 
            this.cBoxnable1.AutoSize = true;
            this.cBoxnable1.Location = new System.Drawing.Point(241, 110);
            this.cBoxnable1.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxnable1.Name = "cBoxnable1";
            this.cBoxnable1.Size = new System.Drawing.Size(60, 16);
            this.cBoxnable1.TabIndex = 32;
            this.cBoxnable1.Text = "Enable";
            this.cBoxnable1.UseVisualStyleBackColor = true;
            this.cBoxnable1.CheckedChanged += new System.EventHandler(this.cBoxnable1_CheckedChanged);
            // 
            // tBoxDI1
            // 
            this.tBoxDI1.Location = new System.Drawing.Point(94, 106);
            this.tBoxDI1.Margin = new System.Windows.Forms.Padding(2);
            this.tBoxDI1.Name = "tBoxDI1";
            this.tBoxDI1.Size = new System.Drawing.Size(122, 21);
            this.tBoxDI1.TabIndex = 34;
            this.tBoxDI1.TextChanged += new System.EventHandler(this.tBoxDI1_TextChanged);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Location = new System.Drawing.Point(7, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 30);
            this.label3.TabIndex = 33;
            this.label3.Text = " DI :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrUpdateCylinder
            // 
            this.tmrUpdateCylinder.Interval = 50;
            this.tmrUpdateCylinder.Tick += new System.EventHandler(this.tmrUpdateCylinder_Tick);
            // 
            // comboBoxPrimDev
            // 
            this.comboBoxPrimDev.FormattingEnabled = true;
            this.comboBoxPrimDev.Location = new System.Drawing.Point(95, 65);
            this.comboBoxPrimDev.Name = "comboBoxPrimDev";
            this.comboBoxPrimDev.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPrimDev.TabIndex = 39;
            this.comboBoxPrimDev.SelectedIndexChanged += new System.EventHandler(this.comboBoxPrimDev_SelectedIndexChanged);
            this.comboBoxPrimDev.TextChanged += new System.EventHandler(this.comboBoxPrimDev_TextChanged);
            // 
            // EleDiConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(423, 156);
            this.Controls.Add(this.comboBoxPrimDev);
            this.Controls.Add(this.tBoxDI1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cBoxnable1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lbInputName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EleDiConfigControl";
            this.Load += new System.EventHandler(this.EleDiConfigControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lbInputName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cBoxnable1;
        private System.Windows.Forms.TextBox tBoxDI1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer tmrUpdateCylinder;
        private System.Windows.Forms.ComboBox comboBoxPrimDev;
    }
}
