namespace Lead.Detect.FrameworkExtension.scriptTask
{
    partial class ScriptTaskControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageResettingScript = new System.Windows.Forms.TabPage();
            this.buttonSaveResettingScript = new System.Windows.Forms.Button();
            this.buttonOpenResettingScript = new System.Windows.Forms.Button();
            this.buttonCompileResettingScript = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scintillaResettingScript = new ScintillaNET.Scintilla();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxCompileResettingScriptOutput = new System.Windows.Forms.RichTextBox();
            this.tabPageRunningScript = new System.Windows.Forms.TabPage();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabPageResettingScript.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageResettingScript);
            this.tabControl1.Controls.Add(this.tabPageRunningScript);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageResettingScript
            // 
            this.tabPageResettingScript.Controls.Add(this.buttonSaveResettingScript);
            this.tabPageResettingScript.Controls.Add(this.buttonOpenResettingScript);
            this.tabPageResettingScript.Controls.Add(this.buttonCompileResettingScript);
            this.tabPageResettingScript.Controls.Add(this.groupBox2);
            this.tabPageResettingScript.Controls.Add(this.groupBox1);
            this.tabPageResettingScript.Location = new System.Drawing.Point(4, 22);
            this.tabPageResettingScript.Name = "tabPageResettingScript";
            this.tabPageResettingScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResettingScript.Size = new System.Drawing.Size(792, 574);
            this.tabPageResettingScript.TabIndex = 0;
            this.tabPageResettingScript.Text = "ResettingScript";
            this.tabPageResettingScript.UseVisualStyleBackColor = true;
            // 
            // buttonSaveResettingScript
            // 
            this.buttonSaveResettingScript.Location = new System.Drawing.Point(94, 19);
            this.buttonSaveResettingScript.Name = "buttonSaveResettingScript";
            this.buttonSaveResettingScript.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveResettingScript.TabIndex = 4;
            this.buttonSaveResettingScript.Text = "保存";
            this.buttonSaveResettingScript.UseVisualStyleBackColor = true;
            this.buttonSaveResettingScript.Click += new System.EventHandler(this.buttonSaveResettingScript_Click);
            // 
            // buttonOpenResettingScript
            // 
            this.buttonOpenResettingScript.Location = new System.Drawing.Point(13, 19);
            this.buttonOpenResettingScript.Name = "buttonOpenResettingScript";
            this.buttonOpenResettingScript.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenResettingScript.TabIndex = 4;
            this.buttonOpenResettingScript.Text = "打开";
            this.buttonOpenResettingScript.UseVisualStyleBackColor = true;
            // 
            // buttonCompileResettingScript
            // 
            this.buttonCompileResettingScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCompileResettingScript.Location = new System.Drawing.Point(708, 19);
            this.buttonCompileResettingScript.Name = "buttonCompileResettingScript";
            this.buttonCompileResettingScript.Size = new System.Drawing.Size(75, 23);
            this.buttonCompileResettingScript.TabIndex = 4;
            this.buttonCompileResettingScript.Text = "编译";
            this.buttonCompileResettingScript.UseVisualStyleBackColor = true;
            this.buttonCompileResettingScript.Click += new System.EventHandler(this.buttonCompileResettingScript_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.scintillaResettingScript);
            this.groupBox2.Location = new System.Drawing.Point(10, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(773, 401);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Script";
            // 
            // scintillaResettingScript
            // 
            this.scintillaResettingScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaResettingScript.Location = new System.Drawing.Point(3, 17);
            this.scintillaResettingScript.Name = "scintillaResettingScript";
            this.scintillaResettingScript.Size = new System.Drawing.Size(767, 381);
            this.scintillaResettingScript.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBoxCompileResettingScriptOutput);
            this.groupBox1.Location = new System.Drawing.Point(7, 455);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 113);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // richTextBoxCompileResettingScriptOutput
            // 
            this.richTextBoxCompileResettingScriptOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCompileResettingScriptOutput.Location = new System.Drawing.Point(3, 17);
            this.richTextBoxCompileResettingScriptOutput.Name = "richTextBoxCompileResettingScriptOutput";
            this.richTextBoxCompileResettingScriptOutput.Size = new System.Drawing.Size(773, 93);
            this.richTextBoxCompileResettingScriptOutput.TabIndex = 1;
            this.richTextBoxCompileResettingScriptOutput.Text = "";
            // 
            // tabPageRunningScript
            // 
            this.tabPageRunningScript.Location = new System.Drawing.Point(4, 22);
            this.tabPageRunningScript.Name = "tabPageRunningScript";
            this.tabPageRunningScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRunningScript.Size = new System.Drawing.Size(792, 574);
            this.tabPageRunningScript.TabIndex = 1;
            this.tabPageRunningScript.Text = "RunningScript";
            this.tabPageRunningScript.UseVisualStyleBackColor = true;
            // 
            // ScriptTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "ScriptTaskControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ScriptTaskControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageResettingScript.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageResettingScript;
        private System.Windows.Forms.TabPage tabPageRunningScript;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxCompileResettingScriptOutput;
        private System.Windows.Forms.GroupBox groupBox2;
        private ScintillaNET.Scintilla scintillaResettingScript;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonCompileResettingScript;
        private System.Windows.Forms.Button buttonOpenResettingScript;
        private System.Windows.Forms.Button buttonSaveResettingScript;
    }
}
