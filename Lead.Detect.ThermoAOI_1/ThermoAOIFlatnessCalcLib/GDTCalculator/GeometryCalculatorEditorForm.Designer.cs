namespace Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalculator
{
    partial class GeometryCalculatorEditorForm
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGridGDTCalc = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
            this.comboBoxCalcs = new System.Windows.Forms.ComboBox();
            this.comboBoxCalcType = new System.Windows.Forms.ComboBox();
            this.buttonAddCalc = new System.Windows.Forms.Button();
            this.buttonDeleteCalc = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGridGDTCalc
            // 
            this.propertyGridGDTCalc.Location = new System.Drawing.Point(406, 89);
            this.propertyGridGDTCalc.Name = "propertyGridGDTCalc";
            this.propertyGridGDTCalc.Size = new System.Drawing.Size(424, 272);
            this.propertyGridGDTCalc.TabIndex = 0;
            this.propertyGridGDTCalc.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridGDTCalc_PropertyValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(830, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.新建ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // richTextBoxDetails
            // 
            this.richTextBoxDetails.Location = new System.Drawing.Point(13, 54);
            this.richTextBoxDetails.Name = "richTextBoxDetails";
            this.richTextBoxDetails.Size = new System.Drawing.Size(387, 466);
            this.richTextBoxDetails.TabIndex = 2;
            this.richTextBoxDetails.Text = "";
            // 
            // comboBoxCalcs
            // 
            this.comboBoxCalcs.FormattingEnabled = true;
            this.comboBoxCalcs.Location = new System.Drawing.Point(407, 54);
            this.comboBoxCalcs.Name = "comboBoxCalcs";
            this.comboBoxCalcs.Size = new System.Drawing.Size(182, 20);
            this.comboBoxCalcs.TabIndex = 3;
            this.comboBoxCalcs.SelectedIndexChanged += new System.EventHandler(this.comboBoxCalcs_SelectedIndexChanged);
            // 
            // comboBoxCalcType
            // 
            this.comboBoxCalcType.FormattingEnabled = true;
            this.comboBoxCalcType.Location = new System.Drawing.Point(488, 369);
            this.comboBoxCalcType.Name = "comboBoxCalcType";
            this.comboBoxCalcType.Size = new System.Drawing.Size(182, 20);
            this.comboBoxCalcType.TabIndex = 4;
            // 
            // buttonAddCalc
            // 
            this.buttonAddCalc.Location = new System.Drawing.Point(407, 367);
            this.buttonAddCalc.Name = "buttonAddCalc";
            this.buttonAddCalc.Size = new System.Drawing.Size(75, 23);
            this.buttonAddCalc.TabIndex = 5;
            this.buttonAddCalc.Text = "添加";
            this.buttonAddCalc.UseVisualStyleBackColor = true;
            this.buttonAddCalc.Click += new System.EventHandler(this.buttonAddCalc_Click);
            // 
            // buttonDeleteCalc
            // 
            this.buttonDeleteCalc.Location = new System.Drawing.Point(597, 54);
            this.buttonDeleteCalc.Name = "buttonDeleteCalc";
            this.buttonDeleteCalc.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteCalc.TabIndex = 5;
            this.buttonDeleteCalc.Text = "删除计算项";
            this.buttonDeleteCalc.UseVisualStyleBackColor = true;
            this.buttonDeleteCalc.Click += new System.EventHandler(this.buttonDeleteCalc_Click);
            // 
            // GeometryCalculatorEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 532);
            this.Controls.Add(this.buttonDeleteCalc);
            this.Controls.Add(this.buttonAddCalc);
            this.Controls.Add(this.comboBoxCalcType);
            this.Controls.Add(this.comboBoxCalcs);
            this.Controls.Add(this.richTextBoxDetails);
            this.Controls.Add(this.propertyGridGDTCalc);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GeometryCalculatorEditorForm";
            this.Text = "产品计算文件编辑";
            this.Load += new System.EventHandler(this.GeometryCalculatorControl_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridGDTCalc;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxDetails;
        private System.Windows.Forms.ComboBox comboBoxCalcs;
        private System.Windows.Forms.ComboBox comboBoxCalcType;
        private System.Windows.Forms.Button buttonAddCalc;
        private System.Windows.Forms.Button buttonDeleteCalc;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
    }
}
