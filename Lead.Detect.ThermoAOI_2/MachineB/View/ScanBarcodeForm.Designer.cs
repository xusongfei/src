namespace Lead.Detect.ThermoAOI2.MachineB.View
{
    partial class ScanBarcodeForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.buttonStatus = new System.Windows.Forms.Button();
            this.textBoxBarcode = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonStatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxBarcode);
            this.splitContainer1.Size = new System.Drawing.Size(784, 46);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.SplitterWidth = 13;
            this.splitContainer1.TabIndex = 0;
            // 
            // buttonStatus
            // 
            this.buttonStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStatus.Location = new System.Drawing.Point(0, 0);
            this.buttonStatus.Margin = new System.Windows.Forms.Padding(10);
            this.buttonStatus.Name = "buttonStatus";
            this.buttonStatus.Size = new System.Drawing.Size(246, 46);
            this.buttonStatus.TabIndex = 0;
            this.buttonStatus.Text = "扫描条码";
            this.buttonStatus.UseVisualStyleBackColor = true;
            // 
            // textBoxBarcode
            // 
            this.textBoxBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxBarcode.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBarcode.Location = new System.Drawing.Point(0, 0);
            this.textBoxBarcode.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxBarcode.Name = "textBoxBarcode";
            this.textBoxBarcode.Size = new System.Drawing.Size(525, 45);
            this.textBoxBarcode.TabIndex = 0;
            this.textBoxBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ScanBarcodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 46);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "ScanBarcodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "扫描输入条码";
            this.Load += new System.EventHandler(this.ScanBarcodeForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxBarcode;
        private System.Windows.Forms.Button buttonStatus;
        private System.Windows.Forms.Timer timer1;
    }
}