namespace Lead.Detect.PrimCameraFocalSpec.Test
{
    partial class FormTest
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnFSPrim1 = new System.Windows.Forms.Button();
            this.btnFSPrim2 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnFSPrim2);
            this.splitContainer1.Panel1.Controls.Add(this.btnFSPrim1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 592);
            this.splitContainer1.SplitterDistance = 103;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnFSPrim1
            // 
            this.btnFSPrim1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFSPrim1.Location = new System.Drawing.Point(0, 0);
            this.btnFSPrim1.Name = "btnFSPrim1";
            this.btnFSPrim1.Size = new System.Drawing.Size(101, 37);
            this.btnFSPrim1.TabIndex = 0;
            this.btnFSPrim1.Text = "FSPrim1";
            this.btnFSPrim1.UseVisualStyleBackColor = true;
            this.btnFSPrim1.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnFSPrim2
            // 
            this.btnFSPrim2.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFSPrim2.Location = new System.Drawing.Point(0, 37);
            this.btnFSPrim2.Name = "btnFSPrim2";
            this.btnFSPrim2.Size = new System.Drawing.Size(101, 37);
            this.btnFSPrim2.TabIndex = 1;
            this.btnFSPrim2.Text = "FSPrim2";
            this.btnFSPrim2.UseVisualStyleBackColor = true;
            this.btnFSPrim2.Click += new System.EventHandler(this.btnFSPrim2_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Size = new System.Drawing.Size(947, 592);
            this.splitContainer2.SplitterDistance = 293;
            this.splitContainer2.TabIndex = 0;
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 592);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnFSPrim1;
        private System.Windows.Forms.Button btnFSPrim2;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}