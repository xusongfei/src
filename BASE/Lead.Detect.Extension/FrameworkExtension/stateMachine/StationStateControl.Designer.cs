namespace Lead.Detect.FrameworkExtension.stateMachine
{
    partial class StationStateControl
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewStation = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStation)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStation
            // 
            this.dataGridViewStation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStation.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewStation.MultiSelect = false;
            this.dataGridViewStation.Name = "dataGridViewStation";
            this.dataGridViewStation.ReadOnly = true;
            this.dataGridViewStation.RowTemplate.Height = 23;
            this.dataGridViewStation.Size = new System.Drawing.Size(354, 524);
            this.dataGridViewStation.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StationStateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewStation);
            this.Name = "StationStateControl";
            this.Size = new System.Drawing.Size(354, 524);
            this.Load += new System.EventHandler(this.StationStateControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStation;
        private System.Windows.Forms.Timer timer1;
    }
}
