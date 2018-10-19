namespace Lead.Detect.Element
{
    partial class EleCylinderConfigControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.tBoxDO1 = new System.Windows.Forms.TextBox();
            this.tBoxDO2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tBoxDI1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBoxDI2 = new System.Windows.Forms.TextBox();
            this.cBoxCylinderEnable = new System.Windows.Forms.CheckBox();
            this.cBoxDOEnable1 = new System.Windows.Forms.CheckBox();
            this.cBoxDOEnable2 = new System.Windows.Forms.CheckBox();
            this.cBoxDIEnable2 = new System.Windows.Forms.CheckBox();
            this.cBoxDIEnable1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbCylinderName = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnDOSet1 = new System.Windows.Forms.Button();
            this.btnDOSet2 = new System.Windows.Forms.Button();
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
            this.label5.Text = "+ DO :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Location = new System.Drawing.Point(28, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 37);
            this.label2.TabIndex = 4;
            this.label2.Text = "- DO :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxDO1
            // 
            this.tBoxDO1.Location = new System.Drawing.Point(144, 152);
            this.tBoxDO1.Name = "tBoxDO1";
            this.tBoxDO1.Size = new System.Drawing.Size(162, 23);
            this.tBoxDO1.TabIndex = 5;
            this.tBoxDO1.TextChanged += new System.EventHandler(this.tBoxDO1_TextChanged);
            // 
            // tBoxDO2
            // 
            this.tBoxDO2.Location = new System.Drawing.Point(144, 199);
            this.tBoxDO2.Name = "tBoxDO2";
            this.tBoxDO2.Size = new System.Drawing.Size(162, 23);
            this.tBoxDO2.TabIndex = 5;
            this.tBoxDO2.TextChanged += new System.EventHandler(this.tBoxDO2_TextChanged);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Location = new System.Drawing.Point(28, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 37);
            this.label3.TabIndex = 4;
            this.label3.Text = "+ DI :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxDI1
            // 
            this.tBoxDI1.Location = new System.Drawing.Point(144, 246);
            this.tBoxDI1.Name = "tBoxDI1";
            this.tBoxDI1.Size = new System.Drawing.Size(162, 23);
            this.tBoxDI1.TabIndex = 5;
            this.tBoxDI1.TextChanged += new System.EventHandler(this.tBoxDI1_TextChanged);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label4.Location = new System.Drawing.Point(28, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 37);
            this.label4.TabIndex = 4;
            this.label4.Text = "- DI :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBoxDI2
            // 
            this.tBoxDI2.Location = new System.Drawing.Point(144, 293);
            this.tBoxDI2.Name = "tBoxDI2";
            this.tBoxDI2.Size = new System.Drawing.Size(162, 23);
            this.tBoxDI2.TabIndex = 5;
            this.tBoxDI2.TextChanged += new System.EventHandler(this.tBoxDI2_TextChanged);
            // 
            // cBoxCylinderEnable
            // 
            this.cBoxCylinderEnable.AutoSize = true;
            this.cBoxCylinderEnable.Location = new System.Drawing.Point(318, 111);
            this.cBoxCylinderEnable.Name = "cBoxCylinderEnable";
            this.cBoxCylinderEnable.Size = new System.Drawing.Size(117, 21);
            this.cBoxCylinderEnable.TabIndex = 6;
            this.cBoxCylinderEnable.Text = "Cylinder Enable";
            this.cBoxCylinderEnable.UseVisualStyleBackColor = true;
            this.cBoxCylinderEnable.CheckedChanged += new System.EventHandler(this.cBoxCylinderEnable_CheckedChanged);
            // 
            // cBoxDOEnable1
            // 
            this.cBoxDOEnable1.AutoSize = true;
            this.cBoxDOEnable1.Location = new System.Drawing.Point(318, 157);
            this.cBoxDOEnable1.Name = "cBoxDOEnable1";
            this.cBoxDOEnable1.Size = new System.Drawing.Size(102, 21);
            this.cBoxDOEnable1.TabIndex = 6;
            this.cBoxDOEnable1.Text = "+ DO Enable";
            this.cBoxDOEnable1.UseVisualStyleBackColor = true;
            this.cBoxDOEnable1.CheckedChanged += new System.EventHandler(this.cBoxDOEnable1_CheckedChanged);
            // 
            // cBoxDOEnable2
            // 
            this.cBoxDOEnable2.AutoSize = true;
            this.cBoxDOEnable2.Location = new System.Drawing.Point(318, 203);
            this.cBoxDOEnable2.Name = "cBoxDOEnable2";
            this.cBoxDOEnable2.Size = new System.Drawing.Size(98, 21);
            this.cBoxDOEnable2.TabIndex = 6;
            this.cBoxDOEnable2.Text = "- DO Enable";
            this.cBoxDOEnable2.UseVisualStyleBackColor = true;
            this.cBoxDOEnable2.CheckedChanged += new System.EventHandler(this.cBoxDOEnable2_CheckedChanged);
            // 
            // cBoxDIEnable2
            // 
            this.cBoxDIEnable2.AutoSize = true;
            this.cBoxDIEnable2.Location = new System.Drawing.Point(318, 295);
            this.cBoxDIEnable2.Name = "cBoxDIEnable2";
            this.cBoxDIEnable2.Size = new System.Drawing.Size(92, 21);
            this.cBoxDIEnable2.TabIndex = 6;
            this.cBoxDIEnable2.Text = "- DI Enable";
            this.cBoxDIEnable2.UseVisualStyleBackColor = true;
            this.cBoxDIEnable2.CheckedChanged += new System.EventHandler(this.cBoxDIEnable2_CheckedChanged);
            // 
            // cBoxDIEnable1
            // 
            this.cBoxDIEnable1.AutoSize = true;
            this.cBoxDIEnable1.Location = new System.Drawing.Point(318, 249);
            this.cBoxDIEnable1.Name = "cBoxDIEnable1";
            this.cBoxDIEnable1.Size = new System.Drawing.Size(96, 21);
            this.cBoxDIEnable1.TabIndex = 6;
            this.cBoxDIEnable1.Text = "+ DI Enable";
            this.cBoxDIEnable1.UseVisualStyleBackColor = true;
            this.cBoxDIEnable1.CheckedChanged += new System.EventHandler(this.cBoxDIEnable1_CheckedChanged);
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
            // lbCylinderName
            // 
            this.lbCylinderName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbCylinderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCylinderName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbCylinderName.Location = new System.Drawing.Point(171, 11);
            this.lbCylinderName.Name = "lbCylinderName";
            this.lbCylinderName.Size = new System.Drawing.Size(230, 37);
            this.lbCylinderName.TabIndex = 2;
            this.lbCylinderName.Text = "CylinderName:";
            this.lbCylinderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // btnDOSet2
            // 
            this.btnDOSet2.Location = new System.Drawing.Point(445, 196);
            this.btnDOSet2.Name = "btnDOSet2";
            this.btnDOSet2.Size = new System.Drawing.Size(75, 36);
            this.btnDOSet2.TabIndex = 27;
            this.btnDOSet2.Text = "TRIGGER";
            this.btnDOSet2.UseVisualStyleBackColor = true;
            this.btnDOSet2.Click += new System.EventHandler(this.btnDOSet2_Click);
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
            // 
            // EleCylinderConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(645, 348);
            this.Controls.Add(this.comboBoxPrimDev);
            this.Controls.Add(this.btnDOSet2);
            this.Controls.Add(this.btnDOSet1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.cBoxDIEnable1);
            this.Controls.Add(this.cBoxDIEnable2);
            this.Controls.Add(this.cBoxDOEnable2);
            this.Controls.Add(this.cBoxDOEnable1);
            this.Controls.Add(this.cBoxCylinderEnable);
            this.Controls.Add(this.tBoxDO2);
            this.Controls.Add(this.tBoxDI2);
            this.Controls.Add(this.tBoxDI1);
            this.Controls.Add(this.tBoxDO1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbCylinderName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EleCylinderConfigControl";
            this.Load += new System.EventHandler(this.EleCylinderConfigControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBoxDO1;
        private System.Windows.Forms.TextBox tBoxDO2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBoxDI1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBoxDI2;
        private System.Windows.Forms.CheckBox cBoxCylinderEnable;
        private System.Windows.Forms.CheckBox cBoxDOEnable1;
        private System.Windows.Forms.CheckBox cBoxDOEnable2;
        private System.Windows.Forms.CheckBox cBoxDIEnable2;
        private System.Windows.Forms.CheckBox cBoxDIEnable1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbCylinderName;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnDOSet1;
        private System.Windows.Forms.Button btnDOSet2;
        private System.Windows.Forms.Timer tmrUpdateCylinder;
        private System.Windows.Forms.ComboBox comboBoxPrimDev;
    }
}
