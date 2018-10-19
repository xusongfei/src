namespace Lead.Detect.ThermoAOI.Product
{
    partial class FlatnessProjectEditorForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonProductTestPosUpEdit = new System.Windows.Forms.Button();
            this.buttonProductTestPosDownEdit = new System.Windows.Forms.Button();
            this.buttonViewUpPos = new System.Windows.Forms.Button();
            this.buttonViewDownPos = new System.Windows.Forms.Button();
            this.comboBoxUpStation = new System.Windows.Forms.ComboBox();
            this.comboBoxDownStation = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonDownOptimize = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.projectToolStripMenuItem.Text = "文件";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "新建";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "打开";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "另存为";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Location = new System.Drawing.Point(12, 77);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(426, 432);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "产品测试配置文件";
            // 
            // buttonProductTestPosUpEdit
            // 
            this.buttonProductTestPosUpEdit.Location = new System.Drawing.Point(444, 42);
            this.buttonProductTestPosUpEdit.Name = "buttonProductTestPosUpEdit";
            this.buttonProductTestPosUpEdit.Size = new System.Drawing.Size(179, 56);
            this.buttonProductTestPosUpEdit.TabIndex = 3;
            this.buttonProductTestPosUpEdit.Text = "上工站测试点位编辑";
            this.buttonProductTestPosUpEdit.UseVisualStyleBackColor = true;
            this.buttonProductTestPosUpEdit.Click += new System.EventHandler(this.buttonProductTestPosUpEdit_Click);
            // 
            // buttonProductTestPosDownEdit
            // 
            this.buttonProductTestPosDownEdit.Location = new System.Drawing.Point(444, 104);
            this.buttonProductTestPosDownEdit.Name = "buttonProductTestPosDownEdit";
            this.buttonProductTestPosDownEdit.Size = new System.Drawing.Size(179, 56);
            this.buttonProductTestPosDownEdit.TabIndex = 3;
            this.buttonProductTestPosDownEdit.Text = "下工站测试点位编辑";
            this.buttonProductTestPosDownEdit.UseVisualStyleBackColor = true;
            this.buttonProductTestPosDownEdit.Click += new System.EventHandler(this.buttonProductTestPosDownEdit_Click);
            // 
            // buttonViewUpPos
            // 
            this.buttonViewUpPos.Location = new System.Drawing.Point(668, 42);
            this.buttonViewUpPos.Name = "buttonViewUpPos";
            this.buttonViewUpPos.Size = new System.Drawing.Size(179, 56);
            this.buttonViewUpPos.TabIndex = 3;
            this.buttonViewUpPos.Text = "上工站测试点位转换";
            this.buttonViewUpPos.UseVisualStyleBackColor = true;
            this.buttonViewUpPos.Click += new System.EventHandler(this.buttonViewUpPos_Click);
            // 
            // buttonViewDownPos
            // 
            this.buttonViewDownPos.Location = new System.Drawing.Point(668, 104);
            this.buttonViewDownPos.Name = "buttonViewDownPos";
            this.buttonViewDownPos.Size = new System.Drawing.Size(179, 56);
            this.buttonViewDownPos.TabIndex = 3;
            this.buttonViewDownPos.Text = "下工站测试点位查看";
            this.buttonViewDownPos.UseVisualStyleBackColor = true;
            this.buttonViewDownPos.Click += new System.EventHandler(this.buttonViewDownPos_Click);
            // 
            // comboBoxUpStation
            // 
            this.comboBoxUpStation.FormattingEnabled = true;
            this.comboBoxUpStation.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBoxUpStation.Location = new System.Drawing.Point(853, 61);
            this.comboBoxUpStation.Name = "comboBoxUpStation";
            this.comboBoxUpStation.Size = new System.Drawing.Size(104, 20);
            this.comboBoxUpStation.TabIndex = 4;
            // 
            // comboBoxDownStation
            // 
            this.comboBoxDownStation.FormattingEnabled = true;
            this.comboBoxDownStation.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBoxDownStation.Location = new System.Drawing.Point(853, 123);
            this.comboBoxDownStation.Name = "comboBoxDownStation";
            this.comboBoxDownStation.Size = new System.Drawing.Size(104, 20);
            this.comboBoxDownStation.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Location = new System.Drawing.Point(445, 247);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(525, 262);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // buttonDownOptimize
            // 
            this.buttonDownOptimize.Location = new System.Drawing.Point(444, 166);
            this.buttonDownOptimize.Name = "buttonDownOptimize";
            this.buttonDownOptimize.Size = new System.Drawing.Size(179, 56);
            this.buttonDownOptimize.TabIndex = 3;
            this.buttonDownOptimize.Text = "下工站测试点位顺序优化";
            this.buttonDownOptimize.UseVisualStyleBackColor = true;
            this.buttonDownOptimize.Click += new System.EventHandler(this.buttonDownOptimize_Click);
            // 
            // FlatnessProjectEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 521);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBoxDownStation);
            this.Controls.Add(this.comboBoxUpStation);
            this.Controls.Add(this.buttonViewDownPos);
            this.Controls.Add(this.buttonDownOptimize);
            this.Controls.Add(this.buttonProductTestPosDownEdit);
            this.Controls.Add(this.buttonViewUpPos);
            this.Controls.Add(this.buttonProductTestPosUpEdit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FlatnessProjectEditorForm";
            this.Text = "产品测试配置文件编辑";
            this.Load += new System.EventHandler(this.ProjectEditorForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonProductTestPosUpEdit;
        private System.Windows.Forms.Button buttonProductTestPosDownEdit;
        private System.Windows.Forms.Button buttonViewUpPos;
        private System.Windows.Forms.Button buttonViewDownPos;
        private System.Windows.Forms.ComboBox comboBoxUpStation;
        private System.Windows.Forms.ComboBox comboBoxDownStation;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonDownOptimize;
    }
}