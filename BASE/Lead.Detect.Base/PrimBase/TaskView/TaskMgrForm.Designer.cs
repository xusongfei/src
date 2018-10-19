namespace Lead.Detect.Base.TaskView
{
    partial class TaskMgrForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("TaskManager");
            this.btnTaskDel = new System.Windows.Forms.Button();
            this.btnTaskAdd = new System.Windows.Forms.Button();
            this.treeTaskMng = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // btnTaskDel
            // 
            this.btnTaskDel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTaskDel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTaskDel.Location = new System.Drawing.Point(176, 13);
            this.btnTaskDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTaskDel.Name = "btnTaskDel";
            this.btnTaskDel.Size = new System.Drawing.Size(36, 36);
            this.btnTaskDel.TabIndex = 4;
            this.btnTaskDel.Text = "-";
            this.btnTaskDel.UseVisualStyleBackColor = true;
            // 
            // btnTaskAdd
            // 
            this.btnTaskAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTaskAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTaskAdd.Location = new System.Drawing.Point(134, 13);
            this.btnTaskAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTaskAdd.Name = "btnTaskAdd";
            this.btnTaskAdd.Size = new System.Drawing.Size(36, 36);
            this.btnTaskAdd.TabIndex = 3;
            this.btnTaskAdd.Text = "+";
            this.btnTaskAdd.UseVisualStyleBackColor = true;
            // 
            // treeTaskMng
            // 
            this.treeTaskMng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTaskMng.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeTaskMng.Location = new System.Drawing.Point(0, 0);
            this.treeTaskMng.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeTaskMng.Name = "treeTaskMng";
            treeNode1.Name = "RootTask";
            treeNode1.Text = "TaskManager";
            this.treeTaskMng.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeTaskMng.Size = new System.Drawing.Size(222, 723);
            this.treeTaskMng.TabIndex = 2;
            // 
            // TaskMgrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 723);
            this.Controls.Add(this.btnTaskDel);
            this.Controls.Add(this.btnTaskAdd);
            this.Controls.Add(this.treeTaskMng);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TaskMgrForm";
            this.TabText = "TaskMng";
            this.Text = "TaskMng";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTaskDel;
        private System.Windows.Forms.Button btnTaskAdd;
        private System.Windows.Forms.TreeView treeTaskMng;
    }
}