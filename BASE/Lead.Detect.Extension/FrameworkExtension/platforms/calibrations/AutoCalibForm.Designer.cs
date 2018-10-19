namespace Lead.Detect.FrameworkExtension.platforms.calibrations
{
    partial class AutoCalibForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonStartCalib = new System.Windows.Forms.Button();
            this.buttonStopCalib = new System.Windows.Forms.Button();
            this.textBoxCalibInfo = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.propertyGridCalib = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 44);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(395, 485);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // buttonStartCalib
            // 
            this.buttonStartCalib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartCalib.Location = new System.Drawing.Point(404, 492);
            this.buttonStartCalib.Name = "buttonStartCalib";
            this.buttonStartCalib.Size = new System.Drawing.Size(157, 66);
            this.buttonStartCalib.TabIndex = 1;
            this.buttonStartCalib.Text = "开始";
            this.buttonStartCalib.UseVisualStyleBackColor = true;
            this.buttonStartCalib.Click += new System.EventHandler(this.buttonStartCalib_Click);
            // 
            // buttonStopCalib
            // 
            this.buttonStopCalib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopCalib.Location = new System.Drawing.Point(615, 492);
            this.buttonStopCalib.Name = "buttonStopCalib";
            this.buttonStopCalib.Size = new System.Drawing.Size(157, 66);
            this.buttonStopCalib.TabIndex = 1;
            this.buttonStopCalib.Text = "停止";
            this.buttonStopCalib.UseVisualStyleBackColor = true;
            this.buttonStopCalib.Click += new System.EventHandler(this.buttonStopCalib_Click);
            // 
            // textBoxCalibInfo
            // 
            this.textBoxCalibInfo.Location = new System.Drawing.Point(3, 12);
            this.textBoxCalibInfo.Name = "textBoxCalibInfo";
            this.textBoxCalibInfo.Size = new System.Drawing.Size(395, 21);
            this.textBoxCalibInfo.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 535);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(395, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // propertyGridCalib
            // 
            this.propertyGridCalib.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridCalib.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridCalib.Location = new System.Drawing.Point(404, 96);
            this.propertyGridCalib.Name = "propertyGridCalib";
            this.propertyGridCalib.Size = new System.Drawing.Size(368, 383);
            this.propertyGridCalib.TabIndex = 4;
            // 
            // AutoCalibForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.propertyGridCalib);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBoxCalibInfo);
            this.Controls.Add(this.buttonStopCalib);
            this.Controls.Add(this.buttonStartCalib);
            this.Controls.Add(this.richTextBox1);
            this.Name = "AutoCalibForm";
            this.Text = "AutoCalibForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoCalibForm_FormClosing);
            this.Load += new System.EventHandler(this.AutoCalibForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonStartCalib;
        private System.Windows.Forms.Button buttonStopCalib;
        private System.Windows.Forms.TextBox textBoxCalibInfo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PropertyGrid propertyGridCalib;
    }
}