namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    partial class ChangePasswordForm
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
            this.groupboxChangePassword = new System.Windows.Forms.GroupBox();
            this.buttonChangeCancel = new System.Windows.Forms.Button();
            this.buttonChangeConfirm = new System.Windows.Forms.Button();
            this.textboxPasswordNew = new System.Windows.Forms.TextBox();
            this.textboxPasswordOld = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupboxChangePassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupboxChangePassword
            // 
            this.groupboxChangePassword.Controls.Add(this.buttonChangeCancel);
            this.groupboxChangePassword.Controls.Add(this.buttonChangeConfirm);
            this.groupboxChangePassword.Controls.Add(this.textboxPasswordNew);
            this.groupboxChangePassword.Controls.Add(this.textboxPasswordOld);
            this.groupboxChangePassword.Controls.Add(this.label3);
            this.groupboxChangePassword.Controls.Add(this.label2);
            this.groupboxChangePassword.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupboxChangePassword.Location = new System.Drawing.Point(12, 12);
            this.groupboxChangePassword.Name = "groupboxChangePassword";
            this.groupboxChangePassword.Size = new System.Drawing.Size(321, 179);
            this.groupboxChangePassword.TabIndex = 10;
            this.groupboxChangePassword.TabStop = false;
            // 
            // buttonChangeCancel
            // 
            this.buttonChangeCancel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonChangeCancel.Location = new System.Drawing.Point(187, 118);
            this.buttonChangeCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonChangeCancel.Name = "buttonChangeCancel";
            this.buttonChangeCancel.Size = new System.Drawing.Size(94, 47);
            this.buttonChangeCancel.TabIndex = 9;
            this.buttonChangeCancel.Text = "取消";
            this.buttonChangeCancel.UseVisualStyleBackColor = true;
            this.buttonChangeCancel.Click += new System.EventHandler(this.buttonChangeCancel_Click);
            // 
            // buttonChangeConfirm
            // 
            this.buttonChangeConfirm.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonChangeConfirm.Location = new System.Drawing.Point(43, 118);
            this.buttonChangeConfirm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonChangeConfirm.Name = "buttonChangeConfirm";
            this.buttonChangeConfirm.Size = new System.Drawing.Size(94, 47);
            this.buttonChangeConfirm.TabIndex = 8;
            this.buttonChangeConfirm.Text = "确定";
            this.buttonChangeConfirm.UseVisualStyleBackColor = true;
            this.buttonChangeConfirm.Click += new System.EventHandler(this.buttonChangeConfirm_Click);
            // 
            // textboxPasswordNew
            // 
            this.textboxPasswordNew.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textboxPasswordNew.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textboxPasswordNew.ForeColor = System.Drawing.Color.White;
            this.textboxPasswordNew.Location = new System.Drawing.Point(103, 70);
            this.textboxPasswordNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textboxPasswordNew.Name = "textboxPasswordNew";
            this.textboxPasswordNew.PasswordChar = '*';
            this.textboxPasswordNew.Size = new System.Drawing.Size(156, 32);
            this.textboxPasswordNew.TabIndex = 7;
            // 
            // textboxPasswordOld
            // 
            this.textboxPasswordOld.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textboxPasswordOld.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textboxPasswordOld.ForeColor = System.Drawing.Color.White;
            this.textboxPasswordOld.Location = new System.Drawing.Point(103, 20);
            this.textboxPasswordOld.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textboxPasswordOld.Name = "textboxPasswordOld";
            this.textboxPasswordOld.PasswordChar = '*';
            this.textboxPasswordOld.Size = new System.Drawing.Size(156, 32);
            this.textboxPasswordOld.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(39, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "新密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(39, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "原密码：";
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 201);
            this.Controls.Add(this.groupboxChangePassword);
            this.MaximumSize = new System.Drawing.Size(360, 240);
            this.MinimumSize = new System.Drawing.Size(360, 240);
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "更改密码";
            this.groupboxChangePassword.ResumeLayout(false);
            this.groupboxChangePassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupboxChangePassword;
        private System.Windows.Forms.Button buttonChangeCancel;
        private System.Windows.Forms.Button buttonChangeConfirm;
        private System.Windows.Forms.TextBox textboxPasswordNew;
        private System.Windows.Forms.TextBox textboxPasswordOld;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}