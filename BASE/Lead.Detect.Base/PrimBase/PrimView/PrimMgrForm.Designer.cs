namespace Lead.Detect.Base.PrimView
{
    partial class PrimMgrForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("PrimManager");
            this.treePrimMng = new System.Windows.Forms.TreeView();
            this.btnPrimAdd = new System.Windows.Forms.Button();
            this.btnPrimDel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treePrimMng
            // 
            this.treePrimMng.AllowDrop = true;
            this.treePrimMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePrimMng.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treePrimMng.Location = new System.Drawing.Point(0, 0);
            this.treePrimMng.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treePrimMng.Name = "treePrimMng";
            treeNode1.Name = "RootPrim";
            treeNode1.Text = "PrimManager";
            this.treePrimMng.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treePrimMng.Size = new System.Drawing.Size(222, 723);
            this.treePrimMng.TabIndex = 0;
            this.treePrimMng.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treePrimMng_ItemDrag);
            // 
            // btnPrimAdd
            // 
            this.btnPrimAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrimAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrimAdd.Location = new System.Drawing.Point(134, 13);
            this.btnPrimAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrimAdd.Name = "btnPrimAdd";
            this.btnPrimAdd.Size = new System.Drawing.Size(36, 36);
            this.btnPrimAdd.TabIndex = 1;
            this.btnPrimAdd.Text = "+";
            this.btnPrimAdd.UseVisualStyleBackColor = true;
            this.btnPrimAdd.Click += new System.EventHandler(this.btnPrimAdd_Click);
            // 
            // btnPrimDel
            // 
            this.btnPrimDel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrimDel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrimDel.Location = new System.Drawing.Point(176, 13);
            this.btnPrimDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrimDel.Name = "btnPrimDel";
            this.btnPrimDel.Size = new System.Drawing.Size(36, 36);
            this.btnPrimDel.TabIndex = 1;
            this.btnPrimDel.Text = "-";
            this.btnPrimDel.UseVisualStyleBackColor = true;
            this.btnPrimDel.Click += new System.EventHandler(this.btnPrimDel_Click);
            // 
            // PrimMgrForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 723);
            this.Controls.Add(this.btnPrimDel);
            this.Controls.Add(this.btnPrimAdd);
            this.Controls.Add(this.treePrimMng);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrimMgrForm";
            this.TabText = "PrimMng";
            this.Text = "PrimMng";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treePrimMng;
        private System.Windows.Forms.Button btnPrimAdd;
        private System.Windows.Forms.Button btnPrimDel;
    }
}