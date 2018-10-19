
namespace Lead.Detect.Prim3DAgl
{
    partial class PrimOutputControl
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerUI = new System.Windows.Forms.SplitContainer();
            this.colorRuler1 = new UserControls.ColorRuler();
            this.map3D1 = new UserControls.Map3D();
            this.lb3DTitle = new System.Windows.Forms.Label();
            this.map2D1 = new UserControls.Map2D();
            this.lb2DTitle = new System.Windows.Forms.Label();
            this.chkBtn2D = new System.Windows.Forms.CheckBox();
            this.chkBtn3D = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUI)).BeginInit();
            this.splitContainerUI.Panel1.SuspendLayout();
            this.splitContainerUI.Panel2.SuspendLayout();
            this.splitContainerUI.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(1142, 33);
            this.lbTitle.TabIndex = 4;
            this.lbTitle.Text = "Agl 3D Output";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbTitle.Click += new System.EventHandler(this.lbTitle_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainerMain.Location = new System.Drawing.Point(0, 33);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerUI);
            this.splitContainerMain.Panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.splitContainerMain.Panel2.Controls.Add(this.chkBtn2D);
            this.splitContainerMain.Panel2.Controls.Add(this.chkBtn3D);
            this.splitContainerMain.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainerMain_Panel2_Paint);
            this.splitContainerMain.Size = new System.Drawing.Size(1142, 606);
            this.splitContainerMain.SplitterDistance = 1072;
            this.splitContainerMain.TabIndex = 5;
            // 
            // splitContainerUI
            // 
            this.splitContainerUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerUI.Location = new System.Drawing.Point(0, 0);
            this.splitContainerUI.Name = "splitContainerUI";
            this.splitContainerUI.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerUI.Panel1
            // 
            this.splitContainerUI.Panel1.Controls.Add(this.colorRuler1);
            this.splitContainerUI.Panel1.Controls.Add(this.map3D1);
            this.splitContainerUI.Panel1.Controls.Add(this.lb3DTitle);
            // 
            // splitContainerUI.Panel2
            // 
            this.splitContainerUI.Panel2.Controls.Add(this.map2D1);
            this.splitContainerUI.Panel2.Controls.Add(this.lb2DTitle);
            this.splitContainerUI.Size = new System.Drawing.Size(1072, 606);
            this.splitContainerUI.SplitterDistance = 325;
            this.splitContainerUI.TabIndex = 0;
            // 
            // colorRuler1
            // 
            this.colorRuler1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorRuler1.Dock = System.Windows.Forms.DockStyle.Right;
            this.colorRuler1.DownColor = System.Drawing.Color.DarkBlue;
            this.colorRuler1.Location = new System.Drawing.Point(1027, 33);
            this.colorRuler1.Name = "colorRuler1";
            this.colorRuler1.Size = new System.Drawing.Size(45, 292);
            this.colorRuler1.TabIndex = 7;
            this.colorRuler1.UpColor = System.Drawing.Color.Gray;
            // 
            // map3D1
            // 
            this.map3D1.AxisColorX = System.Drawing.Color.Red;
            this.map3D1.AxisColorY = System.Drawing.Color.Green;
            this.map3D1.AxisColorZ = System.Drawing.Color.Blue;
            this.map3D1.ColorDisMax = 5D;
            this.map3D1.ColorDisMin = -5D;
            this.map3D1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.map3D1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map3D1.EnableMouseEvents = true;
            this.map3D1.EnableZoom = true;
            this.map3D1.ForeColor = System.Drawing.Color.Yellow;
            this.map3D1.GridCellCount = 10;
            this.map3D1.GridShowXY = false;
            this.map3D1.GridShowXZ = false;
            this.map3D1.GridShowYZ = false;
            this.map3D1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.map3D1.IsUseTexture = false;
            this.map3D1.Location = new System.Drawing.Point(0, 33);
            this.map3D1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.map3D1.Name = "map3D1";
            this.map3D1.PixelsPerMM = 50D;
            this.map3D1.Size = new System.Drawing.Size(1072, 292);
            this.map3D1.TabIndex = 6;
            this.map3D1.Tag = "";
            this.map3D1.ViewMode = CommonStruct.LC3D.ViewMode.Default;
            this.map3D1.Zoom = 1D;
            this.map3D1.Load += new System.EventHandler(this.map3D1_Load);
            // 
            // lb3DTitle
            // 
            this.lb3DTitle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lb3DTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb3DTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb3DTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb3DTitle.Location = new System.Drawing.Point(0, 0);
            this.lb3DTitle.Name = "lb3DTitle";
            this.lb3DTitle.Size = new System.Drawing.Size(1072, 33);
            this.lb3DTitle.TabIndex = 5;
            this.lb3DTitle.Text = "3D Display";
            this.lb3DTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // map2D1
            // 
            this.map2D1.AxisColorX = System.Drawing.Color.Red;
            this.map2D1.AxisColorY = System.Drawing.Color.Green;
            this.map2D1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.map2D1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map2D1.EnableMouseEvents = true;
            this.map2D1.EnableZoom = true;
            this.map2D1.ForeColor = System.Drawing.Color.Yellow;
            this.map2D1.GridCellCount = 10;
            this.map2D1.GridCellInterval = 0.5F;
            this.map2D1.GridLineColorXY = System.Drawing.Color.White;
            this.map2D1.GridLineWidth = 1F;
            this.map2D1.GridShowXY = false;
            this.map2D1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.map2D1.IsUseTexture = false;
            this.map2D1.Location = new System.Drawing.Point(0, 33);
            this.map2D1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.map2D1.Name = "map2D1";
            this.map2D1.PathTexture = "";
            this.map2D1.PixelsPerMM = 50D;
            this.map2D1.Size = new System.Drawing.Size(1072, 244);
            this.map2D1.TabIndex = 7;
            this.map2D1.Tag = "";
            this.map2D1.ViewMode = CommonStruct.LC3D.ViewMode.Top;
            this.map2D1.Zoom = 1D;
            this.map2D1.Load += new System.EventHandler(this.map2D1_Load);
            // 
            // lb2DTitle
            // 
            this.lb2DTitle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lb2DTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb2DTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb2DTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb2DTitle.Location = new System.Drawing.Point(0, 0);
            this.lb2DTitle.Name = "lb2DTitle";
            this.lb2DTitle.Size = new System.Drawing.Size(1072, 33);
            this.lb2DTitle.TabIndex = 6;
            this.lb2DTitle.Text = "2D Display";
            this.lb2DTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkBtn2D
            // 
            this.chkBtn2D.AutoSize = true;
            this.chkBtn2D.Location = new System.Drawing.Point(9, 329);
            this.chkBtn2D.Margin = new System.Windows.Forms.Padding(4);
            this.chkBtn2D.Name = "chkBtn2D";
            this.chkBtn2D.Size = new System.Drawing.Size(51, 24);
            this.chkBtn2D.TabIndex = 26;
            this.chkBtn2D.Text = "2D";
            this.chkBtn2D.UseVisualStyleBackColor = true;
            this.chkBtn2D.CheckedChanged += new System.EventHandler(this.chkBtn2D_CheckedChanged);
            // 
            // chkBtn3D
            // 
            this.chkBtn3D.AutoSize = true;
            this.chkBtn3D.Checked = true;
            this.chkBtn3D.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBtn3D.Location = new System.Drawing.Point(9, 9);
            this.chkBtn3D.Margin = new System.Windows.Forms.Padding(4);
            this.chkBtn3D.Name = "chkBtn3D";
            this.chkBtn3D.Size = new System.Drawing.Size(51, 24);
            this.chkBtn3D.TabIndex = 25;
            this.chkBtn3D.Text = "3D";
            this.chkBtn3D.UseVisualStyleBackColor = true;
            this.chkBtn3D.CheckedChanged += new System.EventHandler(this.chkBtn3D_CheckedChanged);
            // 
            // PrimOutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.lbTitle);
            this.Name = "PrimOutputControl";
            this.Size = new System.Drawing.Size(1142, 639);
            this.Load += new System.EventHandler(this.PrimOutputControl_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerUI.Panel1.ResumeLayout(false);
            this.splitContainerUI.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerUI)).EndInit();
            this.splitContainerUI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerUI;
        private System.Windows.Forms.Label lb3DTitle;
        private System.Windows.Forms.Label lb2DTitle;
        private UserControls.ColorRuler colorRuler1;
        private UserControls.Map2D map2D1;
        private System.Windows.Forms.CheckBox chkBtn2D;
        private System.Windows.Forms.CheckBox chkBtn3D;
        public UserControls.Map3D map3D1;
    }
}
