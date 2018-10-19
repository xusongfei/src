namespace Lead.Detect.ThermoAOI.View
{
    partial class DevAlarmForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.richTextBoxAlarm = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxAlarm
            // 
            this.richTextBoxAlarm.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBoxAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAlarm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxAlarm.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxAlarm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBoxAlarm.Name = "richTextBoxAlarm";
            this.richTextBoxAlarm.ReadOnly = true;
            this.richTextBoxAlarm.Size = new System.Drawing.Size(784, 261);
            this.richTextBoxAlarm.TabIndex = 0;
            this.richTextBoxAlarm.Text = "";
            this.richTextBoxAlarm.WordWrap = false;
            // 
            // DevAlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.richTextBoxAlarm);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DevAlarmForm";
            this.TabText = "报警";
            this.Text = "报警";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevAlarmForm_FormClosing);
            this.Load += new System.EventHandler(this.DevAlarmForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxAlarm;
    }
}