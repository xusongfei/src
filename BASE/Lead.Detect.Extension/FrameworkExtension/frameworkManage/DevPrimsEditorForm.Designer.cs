namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    partial class DevPrimsEditorForm
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
            this.richTextBoxDevPrims = new System.Windows.Forms.RichTextBox();
            this.propertyGridDevPrims = new System.Windows.Forms.PropertyGrid();
            this.comboBoxPrimTypes = new System.Windows.Forms.ComboBox();
            this.buttonAddPrim = new System.Windows.Forms.Button();
            this.buttonDeletePrim = new System.Windows.Forms.Button();
            this.comboBoxDevPrims = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonConfigPrim = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxDevPrims
            // 
            this.richTextBoxDevPrims.Location = new System.Drawing.Point(13, 27);
            this.richTextBoxDevPrims.Name = "richTextBoxDevPrims";
            this.richTextBoxDevPrims.ReadOnly = true;
            this.richTextBoxDevPrims.Size = new System.Drawing.Size(290, 522);
            this.richTextBoxDevPrims.TabIndex = 0;
            this.richTextBoxDevPrims.Text = "";
            // 
            // propertyGridDevPrims
            // 
            this.propertyGridDevPrims.Location = new System.Drawing.Point(310, 27);
            this.propertyGridDevPrims.Name = "propertyGridDevPrims";
            this.propertyGridDevPrims.Size = new System.Drawing.Size(307, 522);
            this.propertyGridDevPrims.TabIndex = 1;
            // 
            // comboBoxPrimTypes
            // 
            this.comboBoxPrimTypes.FormattingEnabled = true;
            this.comboBoxPrimTypes.Location = new System.Drawing.Point(651, 39);
            this.comboBoxPrimTypes.Name = "comboBoxPrimTypes";
            this.comboBoxPrimTypes.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPrimTypes.TabIndex = 2;
            // 
            // buttonAddPrim
            // 
            this.buttonAddPrim.Location = new System.Drawing.Point(687, 65);
            this.buttonAddPrim.Name = "buttonAddPrim";
            this.buttonAddPrim.Size = new System.Drawing.Size(85, 23);
            this.buttonAddPrim.TabIndex = 3;
            this.buttonAddPrim.Text = "AddPrim";
            this.buttonAddPrim.UseVisualStyleBackColor = true;
            this.buttonAddPrim.Click += new System.EventHandler(this.buttonAddPrim_Click);
            // 
            // buttonDeletePrim
            // 
            this.buttonDeletePrim.Location = new System.Drawing.Point(687, 211);
            this.buttonDeletePrim.Name = "buttonDeletePrim";
            this.buttonDeletePrim.Size = new System.Drawing.Size(85, 23);
            this.buttonDeletePrim.TabIndex = 3;
            this.buttonDeletePrim.Text = "DeletePrim";
            this.buttonDeletePrim.UseVisualStyleBackColor = true;
            this.buttonDeletePrim.Click += new System.EventHandler(this.buttonDeletePrim_Click);
            // 
            // comboBoxDevPrims
            // 
            this.comboBoxDevPrims.FormattingEnabled = true;
            this.comboBoxDevPrims.Location = new System.Drawing.Point(651, 185);
            this.comboBoxDevPrims.Name = "comboBoxDevPrims";
            this.comboBoxDevPrims.Size = new System.Drawing.Size(121, 20);
            this.comboBoxDevPrims.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.saveAsToolStripMenuItem.Text = "SaveAs";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // buttonConfigPrim
            // 
            this.buttonConfigPrim.Location = new System.Drawing.Point(687, 240);
            this.buttonConfigPrim.Name = "buttonConfigPrim";
            this.buttonConfigPrim.Size = new System.Drawing.Size(85, 23);
            this.buttonConfigPrim.TabIndex = 3;
            this.buttonConfigPrim.Text = "ConfigPrim";
            this.buttonConfigPrim.UseVisualStyleBackColor = true;
            this.buttonConfigPrim.Click += new System.EventHandler(this.buttonConfigPrim_Click);
            // 
            // DevPrimsEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.comboBoxDevPrims);
            this.Controls.Add(this.buttonConfigPrim);
            this.Controls.Add(this.buttonDeletePrim);
            this.Controls.Add(this.buttonAddPrim);
            this.Controls.Add(this.comboBoxPrimTypes);
            this.Controls.Add(this.propertyGridDevPrims);
            this.Controls.Add(this.richTextBoxDevPrims);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DevPrimsEditorForm";
            this.Text = "DevPrimsEditor";
            this.Load += new System.EventHandler(this.EnvironmentEditorForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxDevPrims;
        private System.Windows.Forms.PropertyGrid propertyGridDevPrims;
        private System.Windows.Forms.ComboBox comboBoxPrimTypes;
        private System.Windows.Forms.Button buttonAddPrim;
        private System.Windows.Forms.Button buttonDeletePrim;
        private System.Windows.Forms.ComboBox comboBoxDevPrims;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Button buttonConfigPrim;
    }
}