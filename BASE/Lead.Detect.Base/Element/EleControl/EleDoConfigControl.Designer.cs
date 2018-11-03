namespace Lead.Detect.Element.EleControl
{
    partial class EleDoConfigControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tBoxDO1 = new System.Windows.Forms.TextBox();
            this.cBoxDOEnable1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnDOSet1 = new System.Windows.Forms.Button();
            this.tmrUpdateCylinder = new System.Windows.Forms.Timer(this.components);
            this.comboBoxPrimDev = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(28, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "DevName:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Location = new System.Drawing.Point(28, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 37);
            this.label5.TabIndex = 4;
            this.label5.Text = "DO :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxDO1
            // 
            this.tBoxDO1.Location = new System.Drawing.Point(144, 152);
            this.tBoxDO1.Name = "tBoxDO1";
            this.tBoxDO1.Size = new System.Drawing.Size(162, 23);
            this.tBoxDO1.TabIndex = 5;
            this.tBoxDO1.TextChanged += new System.EventHandler(this.tBoxDO1_TextChanged);
            // 
            // cBoxDOEnable1
            // 
            this.cBoxDOEnable1.AutoSize = true;
            this.cBoxDOEnable1.Location = new System.Drawing.Point(318, 157);
            this.cBoxDOEnable1.Name = "cBoxDOEnable1";
            this.cBoxDOEnable1.Size = new System.Drawing.Size(89, 21);
            this.cBoxDOEnable1.TabIndex = 6;
            this.cBoxDOEnable1.Text = "DO Enable";
            this.cBoxDOEnable1.UseVisualStyleBackColor = true;
            this.cBoxDOEnable1.CheckedChanged += new System.EventHandler(this.cBoxDOEnable1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Location = new System.Drawing.Point(28, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 37);
            this.label6.TabIndex = 2;
            this.label6.Text = "CylinderName:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelName.Location = new System.Drawing.Point(171, 11);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(230, 37);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "CylinderName:";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(529, 11);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 37);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(420, 11);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(96, 37);
            this.btnRun.TabIndex = 26;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnDOSet1
            // 
            this.btnDOSet1.Location = new System.Drawing.Point(445, 150);
            this.btnDOSet1.Name = "btnDOSet1";
            this.btnDOSet1.Size = new System.Drawing.Size(75, 36);
            this.btnDOSet1.TabIndex = 27;
            this.btnDOSet1.Text = "TRIGGER";
            this.btnDOSet1.UseVisualStyleBackColor = true;
            this.btnDOSet1.Click += new System.EventHandler(this.btnDOSet1_Click);
            // 
            // tmrUpdateCylinder
            // 
            this.tmrUpdateCylinder.Interval = 50;
            this.tmrUpdateCylinder.Tick += new System.EventHandler(this.tmrCylinderUpdate_Tick);
            // 
            // comboBoxPrimDev
            // 
            this.comboBoxPrimDev.FormattingEnabled = true;
            this.comboBoxPrimDev.Location = new System.Drawing.Point(144, 79);
            this.comboBoxPrimDev.Name = "comboBoxPrimDev";
            this.comboBoxPrimDev.Size = new System.Drawing.Size(162, 25);
            this.comboBoxPrimDev.TabIndex = 38;
            this.comboBoxPrimDev.SelectedIndexChanged += new System.EventHandler(this.comboBoxPrimDev_SelectedIndexChanged);
            this.comboBoxPrimDev.TextChanged += new System.EventHandler(this.comboBoxPrimDev_TextChanged);
            // 
            // EleDoConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(633, 220);
            this.Controls.Add(this.comboBoxPrimDev);
            this.Controls.Add(this.btnDOSet1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.cBoxDOEnable1);
            this.Controls.Add(this.tBoxDO1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EleDoConfigControl";
            this.Load += new System.EventHandler(this.EleCylinderConfigControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tBoxDO1;
        private System.Windows.Forms.CheckBox cBoxDOEnable1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnDOSet1;
        private System.Windows.Forms.Timer tmrUpdateCylinder;
        private System.Windows.Forms.ComboBox comboBoxPrimDev;
    }
}
