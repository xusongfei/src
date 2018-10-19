namespace Lead.Detect.FrameworkExtension.stateMachine
{
    partial class MachineForm
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
            this.comboBoxExType = new System.Windows.Forms.ComboBox();
            this.comboBoxExObj = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonQueryExObj = new System.Windows.Forms.Button();
            this.buttonQueryEx = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(399, 536);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // comboBoxExType
            // 
            this.comboBoxExType.FormattingEnabled = true;
            this.comboBoxExType.Location = new System.Drawing.Point(67, 20);
            this.comboBoxExType.Name = "comboBoxExType";
            this.comboBoxExType.Size = new System.Drawing.Size(249, 20);
            this.comboBoxExType.TabIndex = 1;
            this.comboBoxExType.SelectedIndexChanged += new System.EventHandler(this.comboBoxExType_SelectedIndexChanged);
            // 
            // comboBoxExObj
            // 
            this.comboBoxExObj.FormattingEnabled = true;
            this.comboBoxExObj.Location = new System.Drawing.Point(67, 75);
            this.comboBoxExObj.Name = "comboBoxExObj";
            this.comboBoxExObj.Size = new System.Drawing.Size(249, 20);
            this.comboBoxExObj.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxExObj);
            this.groupBox1.Controls.Add(this.buttonQueryExObj);
            this.groupBox1.Controls.Add(this.buttonQueryEx);
            this.groupBox1.Controls.Add(this.comboBoxExType);
            this.groupBox1.Location = new System.Drawing.Point(419, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 274);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备对象";
            // 
            // buttonQueryExObj
            // 
            this.buttonQueryExObj.Location = new System.Drawing.Point(241, 101);
            this.buttonQueryExObj.Name = "buttonQueryExObj";
            this.buttonQueryExObj.Size = new System.Drawing.Size(75, 23);
            this.buttonQueryExObj.TabIndex = 2;
            this.buttonQueryExObj.Text = "Query";
            this.buttonQueryExObj.UseVisualStyleBackColor = true;
            this.buttonQueryExObj.Click += new System.EventHandler(this.buttonQueryExObj_Click);
            // 
            // buttonQueryEx
            // 
            this.buttonQueryEx.Location = new System.Drawing.Point(241, 46);
            this.buttonQueryEx.Name = "buttonQueryEx";
            this.buttonQueryEx.Size = new System.Drawing.Size(75, 23);
            this.buttonQueryEx.TabIndex = 2;
            this.buttonQueryEx.Text = "Query";
            this.buttonQueryEx.UseVisualStyleBackColor = true;
            this.buttonQueryEx.Click += new System.EventHandler(this.buttonQueryEx_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "名称";
            // 
            // MachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "MachineForm";
            this.Text = "MachineForm";
            this.Load += new System.EventHandler(this.MachineForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBoxExType;
        private System.Windows.Forms.ComboBox comboBoxExObj;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonQueryEx;
        private System.Windows.Forms.Button buttonQueryExObj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}