namespace Lead.Detect.PrimCameraDalsa
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbSaveFile = new System.Windows.Forms.CheckBox();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSavepath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPixel = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comBoxTrigMode = new System.Windows.Forms.ComboBox();
            this.doBoxDeyT = new System.Windows.Forms.DomainUpDown();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.groupBox1_Acquisition = new System.Windows.Forms.GroupBox();
            this.button_Freeze = new System.Windows.Forms.Button();
            this.button_Grab = new System.Windows.Forms.Button();
            this.button_Snap = new System.Windows.Forms.Button();
            this.groupBox1_Acq_Options = new System.Windows.Forms.GroupBox();
            this.button_LoadConfig = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox1_Acquisition.SuspendLayout();
            this.groupBox1_Acq_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(941, 33);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Dalsa Config";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbSaveFile);
            this.groupBox2.Controls.Add(this.cbFormat);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSavepath);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnSavePath);
            this.groupBox2.Location = new System.Drawing.Point(517, 38);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(401, 286);
            this.groupBox2.TabIndex = 103;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // cbSaveFile
            // 
            this.cbSaveFile.AutoSize = true;
            this.cbSaveFile.Location = new System.Drawing.Point(313, 39);
            this.cbSaveFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSaveFile.Name = "cbSaveFile";
            this.cbSaveFile.Size = new System.Drawing.Size(65, 24);
            this.cbSaveFile.TabIndex = 93;
            this.cbSaveFile.Text = "Save";
            this.cbSaveFile.UseVisualStyleBackColor = true;
            this.cbSaveFile.CheckedChanged += new System.EventHandler(this.cbSaveFile_CheckedChanged_1);
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.Enabled = false;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Items.AddRange(new object[] {
            "bmp",
            "jpg",
            "gif",
            "tif",
            "png"});
            this.cbFormat.Location = new System.Drawing.Point(79, 40);
            this.cbFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(221, 28);
            this.cbFormat.TabIndex = 95;
            this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbFormat_SelectedIndexChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 94;
            this.label4.Text = "Format:";
            // 
            // txtSavepath
            // 
            this.txtSavepath.Enabled = false;
            this.txtSavepath.Location = new System.Drawing.Point(79, 80);
            this.txtSavepath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSavepath.Multiline = true;
            this.txtSavepath.Name = "txtSavepath";
            this.txtSavepath.Size = new System.Drawing.Size(221, 178);
            this.txtSavepath.TabIndex = 90;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 20);
            this.label7.TabIndex = 91;
            this.label7.Text = "Folder:";
            // 
            // btnSavePath
            // 
            this.btnSavePath.Enabled = false;
            this.btnSavePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePath.Location = new System.Drawing.Point(313, 80);
            this.btnSavePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(64, 26);
            this.btnSavePath.TabIndex = 92;
            this.btnSavePath.Text = "...";
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPixel);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.textBoxWidth);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comBoxTrigMode);
            this.groupBox1.Controls.Add(this.doBoxDeyT);
            this.groupBox1.Controls.Add(this.textBoxHeight);
            this.groupBox1.Location = new System.Drawing.Point(221, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(267, 286);
            this.groupBox1.TabIndex = 102;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 76;
            this.label3.Text = "Image From";
            // 
            // txtPixel
            // 
            this.txtPixel.Location = new System.Drawing.Point(116, 248);
            this.txtPixel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPixel.Name = "txtPixel";
            this.txtPixel.Size = new System.Drawing.Size(128, 27);
            this.txtPixel.TabIndex = 97;
            this.txtPixel.Text = "8";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(21, 251);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 20);
            this.label16.TabIndex = 96;
            this.label16.Text = "Pixel";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(116, 168);
            this.textBoxWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(128, 27);
            this.textBoxWidth.TabIndex = 78;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Device",
            "File"});
            this.comboBox2.Location = new System.Drawing.Point(116, 42);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(128, 28);
            this.comboBox2.TabIndex = 77;
            this.comboBox2.Text = "Device";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 171);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 80;
            this.label5.Text = "Width:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 91);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 20);
            this.label9.TabIndex = 82;
            this.label9.Text = "TrigMode:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 132);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 20);
            this.label10.TabIndex = 83;
            this.label10.Text = "DelayTime:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 210);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 20);
            this.label6.TabIndex = 79;
            this.label6.Text = "Height:";
            // 
            // comBoxTrigMode
            // 
            this.comBoxTrigMode.FormattingEnabled = true;
            this.comBoxTrigMode.Items.AddRange(new object[] {
            "ExtHard",
            "SoftCmd"});
            this.comBoxTrigMode.Location = new System.Drawing.Point(116, 88);
            this.comBoxTrigMode.Margin = new System.Windows.Forms.Padding(4);
            this.comBoxTrigMode.Name = "comBoxTrigMode";
            this.comBoxTrigMode.Size = new System.Drawing.Size(128, 28);
            this.comBoxTrigMode.TabIndex = 84;
            this.comBoxTrigMode.Text = "ExtHard";
            this.comBoxTrigMode.SelectedIndexChanged += new System.EventHandler(this.comBoxTrigMode_SelectedIndexChanged_1);
            // 
            // doBoxDeyT
            // 
            this.doBoxDeyT.Location = new System.Drawing.Point(116, 130);
            this.doBoxDeyT.Margin = new System.Windows.Forms.Padding(4);
            this.doBoxDeyT.Name = "doBoxDeyT";
            this.doBoxDeyT.Size = new System.Drawing.Size(129, 27);
            this.doBoxDeyT.TabIndex = 85;
            this.doBoxDeyT.Text = "0";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(116, 206);
            this.textBoxHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(128, 27);
            this.textBoxHeight.TabIndex = 81;
            // 
            // groupBox1_Acquisition
            // 
            this.groupBox1_Acquisition.Controls.Add(this.button_Freeze);
            this.groupBox1_Acquisition.Controls.Add(this.button_Grab);
            this.groupBox1_Acquisition.Controls.Add(this.button_Snap);
            this.groupBox1_Acquisition.Location = new System.Drawing.Point(4, 38);
            this.groupBox1_Acquisition.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1_Acquisition.Name = "groupBox1_Acquisition";
            this.groupBox1_Acquisition.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1_Acquisition.Size = new System.Drawing.Size(197, 184);
            this.groupBox1_Acquisition.TabIndex = 100;
            this.groupBox1_Acquisition.TabStop = false;
            this.groupBox1_Acquisition.Text = "Acquisition Control";
            // 
            // button_Freeze
            // 
            this.button_Freeze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Freeze.Location = new System.Drawing.Point(31, 128);
            this.button_Freeze.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Freeze.Name = "button_Freeze";
            this.button_Freeze.Size = new System.Drawing.Size(137, 35);
            this.button_Freeze.TabIndex = 2;
            this.button_Freeze.Text = "Freeze";
            this.button_Freeze.UseVisualStyleBackColor = true;
            this.button_Freeze.Click += new System.EventHandler(this.button_Freeze_Click_1);
            // 
            // button_Grab
            // 
            this.button_Grab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Grab.Location = new System.Drawing.Point(31, 80);
            this.button_Grab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Grab.Name = "button_Grab";
            this.button_Grab.Size = new System.Drawing.Size(137, 35);
            this.button_Grab.TabIndex = 1;
            this.button_Grab.Text = "Grap";
            this.button_Grab.UseVisualStyleBackColor = true;
            this.button_Grab.Click += new System.EventHandler(this.button_Grab_Click_1);
            // 
            // button_Snap
            // 
            this.button_Snap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Snap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Snap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Snap.Location = new System.Drawing.Point(31, 32);
            this.button_Snap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Snap.Name = "button_Snap";
            this.button_Snap.Size = new System.Drawing.Size(137, 36);
            this.button_Snap.TabIndex = 0;
            this.button_Snap.Text = "Snap";
            this.button_Snap.UseVisualStyleBackColor = true;
            this.button_Snap.Click += new System.EventHandler(this.button_Snap_Click_1);
            // 
            // groupBox1_Acq_Options
            // 
            this.groupBox1_Acq_Options.Controls.Add(this.button_LoadConfig);
            this.groupBox1_Acq_Options.Location = new System.Drawing.Point(4, 232);
            this.groupBox1_Acq_Options.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1_Acq_Options.Name = "groupBox1_Acq_Options";
            this.groupBox1_Acq_Options.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1_Acq_Options.Size = new System.Drawing.Size(197, 92);
            this.groupBox1_Acq_Options.TabIndex = 101;
            this.groupBox1_Acq_Options.TabStop = false;
            this.groupBox1_Acq_Options.Text = "Acquisition Options";
            // 
            // button_LoadConfig
            // 
            this.button_LoadConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_LoadConfig.Location = new System.Drawing.Point(13, 30);
            this.button_LoadConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_LoadConfig.Name = "button_LoadConfig";
            this.button_LoadConfig.Size = new System.Drawing.Size(155, 35);
            this.button_LoadConfig.TabIndex = 0;
            this.button_LoadConfig.Text = "Load Config";
            this.button_LoadConfig.UseVisualStyleBackColor = true;
            this.button_LoadConfig.Click += new System.EventHandler(this.button_LoadConfig_Click_1);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.LightGreen;
            this.btnApply.Location = new System.Drawing.Point(17, 347);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(104, 51);
            this.btnApply.TabIndex = 104;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // PrimConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox1_Acquisition);
            this.Controls.Add(this.groupBox1_Acq_Options);
            this.Controls.Add(this.lbTitle);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrimConfigControl";
            this.Size = new System.Drawing.Size(941, 418);
            this.Load += new System.EventHandler(this.PrimConfigControl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox1_Acquisition.ResumeLayout(false);
            this.groupBox1_Acq_Options.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbSaveFile;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSavepath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPixel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comBoxTrigMode;
        private System.Windows.Forms.DomainUpDown doBoxDeyT;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.GroupBox groupBox1_Acquisition;
        private System.Windows.Forms.Button button_Freeze;
        private System.Windows.Forms.Button button_Grab;
        private System.Windows.Forms.Button button_Snap;
        private System.Windows.Forms.GroupBox groupBox1_Acq_Options;
        private System.Windows.Forms.Button button_LoadConfig;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnApply;
    }
}
