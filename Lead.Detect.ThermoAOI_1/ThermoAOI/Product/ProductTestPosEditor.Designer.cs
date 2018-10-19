namespace Lead.Detect.ThermoAOI.Product
{
    partial class ProductTestPosEditor
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
            this.comboBoxTestPos = new System.Windows.Forms.ComboBox();
            this.buttonMoveToTestPos = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxArrayXCount = new System.Windows.Forms.TextBox();
            this.textBoxArrayYCount = new System.Windows.Forms.TextBox();
            this.textBoxArrayXStep = new System.Windows.Forms.TextBox();
            this.textBoxArrayYStep = new System.Windows.Forms.TextBox();
            this.buttonCreateArrayPos = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxArrayStartX = new System.Windows.Forms.TextBox();
            this.textBoxArrayStartY = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxTestPos = new System.Windows.Forms.RichTextBox();
            this.buttonImportTestPos = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxJump = new System.Windows.Forms.ComboBox();
            this.comboBoxDownGT = new System.Windows.Forms.ComboBox();
            this.comboBoxPlatform = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.listBoxTestPos = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxTestPos
            // 
            this.comboBoxTestPos.FormattingEnabled = true;
            this.comboBoxTestPos.Location = new System.Drawing.Point(41, 34);
            this.comboBoxTestPos.Name = "comboBoxTestPos";
            this.comboBoxTestPos.Size = new System.Drawing.Size(120, 20);
            this.comboBoxTestPos.TabIndex = 1;
            // 
            // buttonMoveToTestPos
            // 
            this.buttonMoveToTestPos.Location = new System.Drawing.Point(388, 43);
            this.buttonMoveToTestPos.Name = "buttonMoveToTestPos";
            this.buttonMoveToTestPos.Size = new System.Drawing.Size(92, 46);
            this.buttonMoveToTestPos.TabIndex = 2;
            this.buttonMoveToTestPos.Text = "Jump至测试点";
            this.buttonMoveToTestPos.UseVisualStyleBackColor = true;
            this.buttonMoveToTestPos.Click += new System.EventHandler(this.buttonMoveToTestPos_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "打开";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "另存为";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.clearToolStripMenuItem.Text = "清除所有点位";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // textBoxArrayXCount
            // 
            this.textBoxArrayXCount.Location = new System.Drawing.Point(156, 34);
            this.textBoxArrayXCount.Name = "textBoxArrayXCount";
            this.textBoxArrayXCount.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayXCount.TabIndex = 4;
            this.textBoxArrayXCount.Text = "0";
            // 
            // textBoxArrayYCount
            // 
            this.textBoxArrayYCount.Location = new System.Drawing.Point(156, 61);
            this.textBoxArrayYCount.Name = "textBoxArrayYCount";
            this.textBoxArrayYCount.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayYCount.TabIndex = 4;
            this.textBoxArrayYCount.Text = "0";
            // 
            // textBoxArrayXStep
            // 
            this.textBoxArrayXStep.Location = new System.Drawing.Point(269, 34);
            this.textBoxArrayXStep.Name = "textBoxArrayXStep";
            this.textBoxArrayXStep.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayXStep.TabIndex = 4;
            this.textBoxArrayXStep.Text = "10";
            // 
            // textBoxArrayYStep
            // 
            this.textBoxArrayYStep.Location = new System.Drawing.Point(269, 61);
            this.textBoxArrayYStep.Name = "textBoxArrayYStep";
            this.textBoxArrayYStep.Size = new System.Drawing.Size(66, 21);
            this.textBoxArrayYStep.TabIndex = 4;
            this.textBoxArrayYStep.Text = "10";
            // 
            // buttonCreateArrayPos
            // 
            this.buttonCreateArrayPos.Location = new System.Drawing.Point(388, 34);
            this.buttonCreateArrayPos.Name = "buttonCreateArrayPos";
            this.buttonCreateArrayPos.Size = new System.Drawing.Size(92, 48);
            this.buttonCreateArrayPos.TabIndex = 5;
            this.buttonCreateArrayPos.Text = "阵列点位";
            this.buttonCreateArrayPos.UseVisualStyleBackColor = true;
            this.buttonCreateArrayPos.Click += new System.EventHandler(this.buttonCreateArrayPos_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxArrayStartX);
            this.groupBox1.Controls.Add(this.textBoxArrayXCount);
            this.groupBox1.Controls.Add(this.buttonCreateArrayPos);
            this.groupBox1.Controls.Add(this.textBoxArrayXStep);
            this.groupBox1.Controls.Add(this.textBoxArrayStartY);
            this.groupBox1.Controls.Add(this.textBoxArrayYStep);
            this.groupBox1.Controls.Add(this.textBoxArrayYCount);
            this.groupBox1.Location = new System.Drawing.Point(286, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 144);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阵列点位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Y步长";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "列数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "起始点Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "X步长";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "行数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "起始点X";
            // 
            // textBoxArrayStartX
            // 
            this.textBoxArrayStartX.Location = new System.Drawing.Point(57, 34);
            this.textBoxArrayStartX.Name = "textBoxArrayStartX";
            this.textBoxArrayStartX.Size = new System.Drawing.Size(57, 21);
            this.textBoxArrayStartX.TabIndex = 4;
            this.textBoxArrayStartX.Text = "0";
            // 
            // textBoxArrayStartY
            // 
            this.textBoxArrayStartY.Location = new System.Drawing.Point(57, 61);
            this.textBoxArrayStartY.Name = "textBoxArrayStartY";
            this.textBoxArrayStartY.Size = new System.Drawing.Size(57, 21);
            this.textBoxArrayStartY.TabIndex = 4;
            this.textBoxArrayStartY.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBoxTestPos);
            this.groupBox2.Controls.Add(this.buttonImportTestPos);
            this.groupBox2.Location = new System.Drawing.Point(286, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 175);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入点位";
            // 
            // richTextBoxTestPos
            // 
            this.richTextBoxTestPos.Location = new System.Drawing.Point(6, 20);
            this.richTextBoxTestPos.Name = "richTextBoxTestPos";
            this.richTextBoxTestPos.Size = new System.Drawing.Size(376, 149);
            this.richTextBoxTestPos.TabIndex = 0;
            this.richTextBoxTestPos.Text = "";
            // 
            // buttonImportTestPos
            // 
            this.buttonImportTestPos.Location = new System.Drawing.Point(388, 20);
            this.buttonImportTestPos.Name = "buttonImportTestPos";
            this.buttonImportTestPos.Size = new System.Drawing.Size(92, 48);
            this.buttonImportTestPos.TabIndex = 5;
            this.buttonImportTestPos.Text = "导入点位";
            this.buttonImportTestPos.UseVisualStyleBackColor = true;
            this.buttonImportTestPos.Click += new System.EventHandler(this.buttonImportTestPos_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonMoveToTestPos);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBoxJump);
            this.groupBox3.Controls.Add(this.comboBoxDownGT);
            this.groupBox3.Controls.Add(this.comboBoxPlatform);
            this.groupBox3.Controls.Add(this.comboBoxTestPos);
            this.groupBox3.Location = new System.Drawing.Point(286, 377);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(486, 171);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "点位测试";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(203, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "Z缩回";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(170, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "GT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "平台";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "点位";
            // 
            // comboBoxJump
            // 
            this.comboBoxJump.FormattingEnabled = true;
            this.comboBoxJump.Location = new System.Drawing.Point(257, 69);
            this.comboBoxJump.Name = "comboBoxJump";
            this.comboBoxJump.Size = new System.Drawing.Size(68, 20);
            this.comboBoxJump.TabIndex = 1;
            this.comboBoxJump.Text = "0";
            // 
            // comboBoxDownGT
            // 
            this.comboBoxDownGT.FormattingEnabled = true;
            this.comboBoxDownGT.Items.AddRange(new object[] {
            "GT1",
            "GT2"});
            this.comboBoxDownGT.Location = new System.Drawing.Point(205, 34);
            this.comboBoxDownGT.Name = "comboBoxDownGT";
            this.comboBoxDownGT.Size = new System.Drawing.Size(120, 20);
            this.comboBoxDownGT.TabIndex = 1;
            this.comboBoxDownGT.Text = "GT1";
            // 
            // comboBoxPlatform
            // 
            this.comboBoxPlatform.FormattingEnabled = true;
            this.comboBoxPlatform.Location = new System.Drawing.Point(41, 69);
            this.comboBoxPlatform.Name = "comboBoxPlatform";
            this.comboBoxPlatform.Size = new System.Drawing.Size(120, 20);
            this.comboBoxPlatform.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "点位列表";
            // 
            // listBoxTestPos
            // 
            this.listBoxTestPos.FormattingEnabled = true;
            this.listBoxTestPos.HorizontalScrollbar = true;
            this.listBoxTestPos.ItemHeight = 12;
            this.listBoxTestPos.Location = new System.Drawing.Point(14, 52);
            this.listBoxTestPos.Name = "listBoxTestPos";
            this.listBoxTestPos.Size = new System.Drawing.Size(266, 496);
            this.listBoxTestPos.TabIndex = 9;
            // 
            // ProductTestPosEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.listBoxTestPos);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProductTestPosEditor";
            this.Text = "ProductTestPosEditor";
            this.Load += new System.EventHandler(this.ProductTestPosEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxTestPos;
        private System.Windows.Forms.Button buttonMoveToTestPos;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxArrayXCount;
        private System.Windows.Forms.TextBox textBoxArrayYCount;
        private System.Windows.Forms.TextBox textBoxArrayXStep;
        private System.Windows.Forms.TextBox textBoxArrayYStep;
        private System.Windows.Forms.Button buttonCreateArrayPos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxArrayStartX;
        private System.Windows.Forms.TextBox textBoxArrayStartY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxPlatform;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox richTextBoxTestPos;
        private System.Windows.Forms.Button buttonImportTestPos;
        private System.Windows.Forms.ListBox listBoxTestPos;
        private System.Windows.Forms.ComboBox comboBoxJump;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxDownGT;
    }
}