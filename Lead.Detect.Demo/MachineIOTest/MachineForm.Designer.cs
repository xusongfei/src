namespace Lead.Detect.MachineIOTest
{
    partial class MachineForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.diControl1 = new Lead.Detect.FrameworkExtension.userControls.DiControl();
            this.doControl1 = new Lead.Detect.FrameworkExtension.userControls.DoControl();
            this.tabPageCY = new System.Windows.Forms.TabPage();
            this.cylinderControl1 = new Lead.Detect.FrameworkExtension.userControls.CylinderControl();
            this.tabPageVIO = new System.Windows.Forms.TabPage();
            this.vioControl1 = new Lead.Detect.FrameworkExtension.userControls.VioControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tabPageCOMMON = new System.Windows.Forms.TabPage();
            this.propertyGridCommonConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGridMachineConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPageMachine = new System.Windows.Forms.TabPage();
            this.richTextBoxMachine = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设备Prim文件编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prims文件devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开machinecfgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageCY.SuspendLayout();
            this.tabPageVIO.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControlConfig.SuspendLayout();
            this.tabPageCOMMON.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageMachine.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageIO);
            this.tabControl1.Controls.Add(this.tabPageCY);
            this.tabControl1.Controls.Add(this.tabPageVIO);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(897, 531);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageIO
            // 
            this.tabPageIO.Controls.Add(this.splitContainer1);
            this.tabPageIO.Location = new System.Drawing.Point(4, 44);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIO.Size = new System.Drawing.Size(889, 483);
            this.tabPageIO.TabIndex = 0;
            this.tabPageIO.Text = "输入输出";
            this.tabPageIO.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.diControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.doControl1);
            this.splitContainer1.Size = new System.Drawing.Size(883, 477);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 0;
            // 
            // diControl1
            // 
            this.diControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diControl1.Location = new System.Drawing.Point(0, 0);
            this.diControl1.Name = "diControl1";
            this.diControl1.Size = new System.Drawing.Size(444, 477);
            this.diControl1.TabIndex = 0;
            // 
            // doControl1
            // 
            this.doControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doControl1.Location = new System.Drawing.Point(0, 0);
            this.doControl1.Name = "doControl1";
            this.doControl1.Size = new System.Drawing.Size(435, 477);
            this.doControl1.TabIndex = 0;
            // 
            // tabPageCY
            // 
            this.tabPageCY.Controls.Add(this.cylinderControl1);
            this.tabPageCY.Location = new System.Drawing.Point(4, 44);
            this.tabPageCY.Name = "tabPageCY";
            this.tabPageCY.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCY.Size = new System.Drawing.Size(889, 483);
            this.tabPageCY.TabIndex = 1;
            this.tabPageCY.Text = "气缸";
            this.tabPageCY.UseVisualStyleBackColor = true;
            // 
            // cylinderControl1
            // 
            this.cylinderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderControl1.Location = new System.Drawing.Point(3, 3);
            this.cylinderControl1.Name = "cylinderControl1";
            this.cylinderControl1.Size = new System.Drawing.Size(883, 477);
            this.cylinderControl1.TabIndex = 0;
            // 
            // tabPageVIO
            // 
            this.tabPageVIO.Controls.Add(this.vioControl1);
            this.tabPageVIO.Location = new System.Drawing.Point(4, 44);
            this.tabPageVIO.Name = "tabPageVIO";
            this.tabPageVIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVIO.Size = new System.Drawing.Size(889, 483);
            this.tabPageVIO.TabIndex = 2;
            this.tabPageVIO.Text = "交互信号";
            this.tabPageVIO.UseVisualStyleBackColor = true;
            // 
            // vioControl1
            // 
            this.vioControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vioControl1.Location = new System.Drawing.Point(3, 3);
            this.vioControl1.Name = "vioControl1";
            this.vioControl1.Size = new System.Drawing.Size(883, 477);
            this.vioControl1.TabIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.tabControlConfig);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(889, 483);
            this.tabPageSettings.TabIndex = 3;
            this.tabPageSettings.Text = "配置";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Controls.Add(this.tabPageCOMMON);
            this.tabControlConfig.Controls.Add(this.tabPage1);
            this.tabControlConfig.Controls.Add(this.tabPageMachine);
            this.tabControlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlConfig.Location = new System.Drawing.Point(0, 0);
            this.tabControlConfig.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.Size = new System.Drawing.Size(889, 483);
            this.tabControlConfig.TabIndex = 1;
            // 
            // tabPageCOMMON
            // 
            this.tabPageCOMMON.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCOMMON.Controls.Add(this.propertyGridCommonConfig);
            this.tabPageCOMMON.Location = new System.Drawing.Point(4, 22);
            this.tabPageCOMMON.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCOMMON.Name = "tabPageCOMMON";
            this.tabPageCOMMON.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageCOMMON.Size = new System.Drawing.Size(881, 457);
            this.tabPageCOMMON.TabIndex = 2;
            this.tabPageCOMMON.Text = "功能设定";
            // 
            // propertyGridCommonConfig
            // 
            this.propertyGridCommonConfig.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridCommonConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridCommonConfig.Location = new System.Drawing.Point(2, 2);
            this.propertyGridCommonConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGridCommonConfig.Name = "propertyGridCommonConfig";
            this.propertyGridCommonConfig.Size = new System.Drawing.Size(877, 453);
            this.propertyGridCommonConfig.TabIndex = 62;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.propertyGridMachineConfig);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(881, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "所有参数";
            // 
            // propertyGridMachineConfig
            // 
            this.propertyGridMachineConfig.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGridMachineConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridMachineConfig.Location = new System.Drawing.Point(2, 2);
            this.propertyGridMachineConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.propertyGridMachineConfig.Name = "propertyGridMachineConfig";
            this.propertyGridMachineConfig.Size = new System.Drawing.Size(877, 453);
            this.propertyGridMachineConfig.TabIndex = 59;
            // 
            // tabPageMachine
            // 
            this.tabPageMachine.Controls.Add(this.richTextBoxMachine);
            this.tabPageMachine.Location = new System.Drawing.Point(4, 22);
            this.tabPageMachine.Name = "tabPageMachine";
            this.tabPageMachine.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMachine.Size = new System.Drawing.Size(881, 457);
            this.tabPageMachine.TabIndex = 3;
            this.tabPageMachine.Text = "设备信息";
            this.tabPageMachine.UseVisualStyleBackColor = true;
            // 
            // richTextBoxMachine
            // 
            this.richTextBoxMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMachine.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxMachine.Name = "richTextBoxMachine";
            this.richTextBoxMachine.ReadOnly = true;
            this.richTextBoxMachine.Size = new System.Drawing.Size(875, 451);
            this.richTextBoxMachine.TabIndex = 0;
            this.richTextBoxMachine.Text = "";
            this.richTextBoxMachine.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设备Prim文件编辑ToolStripMenuItem,
            this.configToolStripMenuItem,
            this.打开machinecfgToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设备Prim文件编辑ToolStripMenuItem
            // 
            this.设备Prim文件编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prims文件devToolStripMenuItem,
            this.设备选择ToolStripMenuItem});
            this.设备Prim文件编辑ToolStripMenuItem.Name = "设备Prim文件编辑ToolStripMenuItem";
            this.设备Prim文件编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设备Prim文件编辑ToolStripMenuItem.Text = "设备";
            // 
            // prims文件devToolStripMenuItem
            // 
            this.prims文件devToolStripMenuItem.Name = "prims文件devToolStripMenuItem";
            this.prims文件devToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.prims文件devToolStripMenuItem.Text = "Prims文件（*.dev）";
            this.prims文件devToolStripMenuItem.Click += new System.EventHandler(this.prims文件devToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.configToolStripMenuItem.Text = "打开Config";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // 打开machinecfgToolStripMenuItem
            // 
            this.打开machinecfgToolStripMenuItem.Name = "打开machinecfgToolStripMenuItem";
            this.打开machinecfgToolStripMenuItem.Size = new System.Drawing.Size(113, 21);
            this.打开machinecfgToolStripMenuItem.Text = "打开machine.cfg";
            this.打开machinecfgToolStripMenuItem.Click += new System.EventHandler(this.打开machinecfgToolStripMenuItem_Click);
            // 
            // 设备选择ToolStripMenuItem
            // 
            this.设备选择ToolStripMenuItem.Name = "设备选择ToolStripMenuItem";
            this.设备选择ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.设备选择ToolStripMenuItem.Text = "设备选择";
            this.设备选择ToolStripMenuItem.Click += new System.EventHandler(this.设备选择ToolStripMenuItem_Click);
            // 
            // MachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 556);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MachineForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MachineForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageCY.ResumeLayout(false);
            this.tabPageVIO.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabControlConfig.ResumeLayout(false);
            this.tabPageCOMMON.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPageMachine.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageIO;
        private System.Windows.Forms.TabPage tabPageCY;
        private System.Windows.Forms.TabPage tabPageVIO;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FrameworkExtension.userControls.DiControl diControl1;
        private FrameworkExtension.userControls.CylinderControl cylinderControl1;
        private FrameworkExtension.userControls.DoControl doControl1;
        private FrameworkExtension.userControls.VioControl vioControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设备Prim文件编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开machinecfgToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.TabPage tabPageCOMMON;
        private System.Windows.Forms.PropertyGrid propertyGridCommonConfig;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PropertyGrid propertyGridMachineConfig;
        private System.Windows.Forms.TabPage tabPageMachine;
        private System.Windows.Forms.RichTextBox richTextBoxMachine;
        private System.Windows.Forms.ToolStripMenuItem prims文件devToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备选择ToolStripMenuItem;
    }
}

