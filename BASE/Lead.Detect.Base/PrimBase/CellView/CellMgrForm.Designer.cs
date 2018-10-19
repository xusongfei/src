namespace Lead.Detect.Base.CellView
{
    partial class CellMgrForm
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
            this.btnCellAdd = new System.Windows.Forms.Button();
            this.pnlCell = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnCellAdd
            // 
            this.btnCellAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCellAdd.Location = new System.Drawing.Point(12, 652);
            this.btnCellAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCellAdd.Name = "btnCellAdd";
            this.btnCellAdd.Size = new System.Drawing.Size(36, 36);
            this.btnCellAdd.TabIndex = 3;
            this.btnCellAdd.Text = "+";
            this.btnCellAdd.UseVisualStyleBackColor = true;
            this.btnCellAdd.Click += new System.EventHandler(this.btnCellAdd_Click);
            // 
            // pnlCell
            // 
            this.pnlCell.AutoScroll = true;
            this.pnlCell.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCell.Location = new System.Drawing.Point(0, 0);
            this.pnlCell.Name = "pnlCell";
            this.pnlCell.Size = new System.Drawing.Size(259, 645);
            this.pnlCell.TabIndex = 4;
            // 
            // CellMgrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 723);
            this.Controls.Add(this.pnlCell);
            this.Controls.Add(this.btnCellAdd);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CellMgrForm";
            this.TabText = "CellMng";
            this.Text = "CellMng";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCellAdd;
        private System.Windows.Forms.Panel pnlCell;
    }
}