namespace Lead.Detect.Element
{
    partial class ElePropsForm
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.buttonAPPLY = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.propertyGrid1.Location = new System.Drawing.Point(13, 14);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(758, 450);
            this.propertyGrid1.TabIndex = 0;
            // 
            // buttonAPPLY
            // 
            this.buttonAPPLY.Location = new System.Drawing.Point(136, 474);
            this.buttonAPPLY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAPPLY.Name = "buttonAPPLY";
            this.buttonAPPLY.Size = new System.Drawing.Size(449, 73);
            this.buttonAPPLY.TabIndex = 1;
            this.buttonAPPLY.Text = "APPLY";
            this.buttonAPPLY.UseVisualStyleBackColor = true;
            this.buttonAPPLY.Click += new System.EventHandler(this.buttonAPPLY_Click);
            // 
            // ElePropsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonAPPLY);
            this.Controls.Add(this.propertyGrid1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ElePropsForm";
            this.Text = "ElePropsForm";
            this.Load += new System.EventHandler(this.ElePropsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button buttonAPPLY;
    }
}