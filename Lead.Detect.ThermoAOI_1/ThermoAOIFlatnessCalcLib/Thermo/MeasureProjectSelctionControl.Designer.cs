namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    partial class MeasureProjectSelctionControl
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
            this.buttonBrowseMprjFile = new System.Windows.Forms.Button();
            this.buttonEditCurFile = new System.Windows.Forms.Button();
            this.buttonNewMprjFile = new System.Windows.Forms.Button();
            this.groupBoxStation = new System.Windows.Forms.GroupBox();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.richTextBoxFile = new System.Windows.Forms.RichTextBox();
            this.groupBoxMprj = new System.Windows.Forms.GroupBox();
            this.richTextBoxMprj = new System.Windows.Forms.RichTextBox();
            this.groupBoxStation.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxMprj.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBrowseMprjFile
            // 
            this.buttonBrowseMprjFile.Location = new System.Drawing.Point(12, 29);
            this.buttonBrowseMprjFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBrowseMprjFile.Name = "buttonBrowseMprjFile";
            this.buttonBrowseMprjFile.Size = new System.Drawing.Size(150, 35);
            this.buttonBrowseMprjFile.TabIndex = 1;
            this.buttonBrowseMprjFile.Text = "更换测试文件";
            this.buttonBrowseMprjFile.UseVisualStyleBackColor = true;
            this.buttonBrowseMprjFile.Click += new System.EventHandler(this.buttonBrowseMprjFile_Click);
            // 
            // buttonEditCurFile
            // 
            this.buttonEditCurFile.Location = new System.Drawing.Point(170, 29);
            this.buttonEditCurFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditCurFile.Name = "buttonEditCurFile";
            this.buttonEditCurFile.Size = new System.Drawing.Size(150, 35);
            this.buttonEditCurFile.TabIndex = 1;
            this.buttonEditCurFile.Text = "编辑当前文件";
            this.buttonEditCurFile.UseVisualStyleBackColor = true;
            this.buttonEditCurFile.Click += new System.EventHandler(this.buttonEditCurFile_Click);
            // 
            // buttonNewMprjFile
            // 
            this.buttonNewMprjFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewMprjFile.Location = new System.Drawing.Point(438, 29);
            this.buttonNewMprjFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNewMprjFile.Name = "buttonNewMprjFile";
            this.buttonNewMprjFile.Size = new System.Drawing.Size(150, 35);
            this.buttonNewMprjFile.TabIndex = 1;
            this.buttonNewMprjFile.Text = "新建测试文件";
            this.buttonNewMprjFile.UseVisualStyleBackColor = true;
            this.buttonNewMprjFile.Click += new System.EventHandler(this.buttonNewMprjFile_Click);
            // 
            // groupBoxStation
            // 
            this.groupBoxStation.Controls.Add(this.groupBoxFile);
            this.groupBoxStation.Controls.Add(this.groupBoxMprj);
            this.groupBoxStation.Controls.Add(this.buttonBrowseMprjFile);
            this.groupBoxStation.Controls.Add(this.buttonNewMprjFile);
            this.groupBoxStation.Controls.Add(this.buttonEditCurFile);
            this.groupBoxStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStation.Location = new System.Drawing.Point(0, 0);
            this.groupBoxStation.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxStation.Name = "groupBoxStation";
            this.groupBoxStation.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxStation.Size = new System.Drawing.Size(600, 600);
            this.groupBoxStation.TabIndex = 2;
            this.groupBoxStation.TabStop = false;
            this.groupBoxStation.Text = "工站";
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFile.Controls.Add(this.richTextBoxFile);
            this.groupBoxFile.Location = new System.Drawing.Point(8, 71);
            this.groupBoxFile.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxFile.Size = new System.Drawing.Size(584, 140);
            this.groupBoxFile.TabIndex = 3;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "文件路径";
            // 
            // richTextBoxFile
            // 
            this.richTextBoxFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxFile.Location = new System.Drawing.Point(4, 20);
            this.richTextBoxFile.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxFile.Name = "richTextBoxFile";
            this.richTextBoxFile.ReadOnly = true;
            this.richTextBoxFile.Size = new System.Drawing.Size(576, 116);
            this.richTextBoxFile.TabIndex = 1;
            this.richTextBoxFile.Text = "";
            // 
            // groupBoxMprj
            // 
            this.groupBoxMprj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMprj.Controls.Add(this.richTextBoxMprj);
            this.groupBoxMprj.Location = new System.Drawing.Point(8, 219);
            this.groupBoxMprj.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxMprj.Name = "groupBoxMprj";
            this.groupBoxMprj.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxMprj.Size = new System.Drawing.Size(584, 372);
            this.groupBoxMprj.TabIndex = 2;
            this.groupBoxMprj.TabStop = false;
            this.groupBoxMprj.Text = "测试文件信息";
            // 
            // richTextBoxMprj
            // 
            this.richTextBoxMprj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMprj.Location = new System.Drawing.Point(4, 20);
            this.richTextBoxMprj.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxMprj.Name = "richTextBoxMprj";
            this.richTextBoxMprj.ReadOnly = true;
            this.richTextBoxMprj.Size = new System.Drawing.Size(576, 348);
            this.richTextBoxMprj.TabIndex = 1;
            this.richTextBoxMprj.Text = "";
            this.richTextBoxMprj.WordWrap = false;
            // 
            // MeasureProjectSelctionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxStation);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MeasureProjectSelctionControl";
            this.Size = new System.Drawing.Size(600, 600);
            this.Load += new System.EventHandler(this.MeasureProjectSelctionControl_Load);
            this.groupBoxStation.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxMprj.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonBrowseMprjFile;
        private System.Windows.Forms.Button buttonEditCurFile;
        private System.Windows.Forms.Button buttonNewMprjFile;
        private System.Windows.Forms.GroupBox groupBoxStation;
        private System.Windows.Forms.GroupBox groupBoxMprj;
        private System.Windows.Forms.RichTextBox richTextBoxMprj;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.RichTextBox richTextBoxFile;
    }
}
