using System;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.buttonLoginCancel = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxLoginPassword = new System.Windows.Forms.TextBox();
            this.groupboxLogin = new System.Windows.Forms.GroupBox();
            this.groupboxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoginCancel
            // 
            this.buttonLoginCancel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLoginCancel.Location = new System.Drawing.Point(188, 77);
            this.buttonLoginCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLoginCancel.Name = "buttonLoginCancel";
            this.buttonLoginCancel.Size = new System.Drawing.Size(94, 47);
            this.buttonLoginCancel.TabIndex = 2;
            this.buttonLoginCancel.Text = "取消";
            this.buttonLoginCancel.UseVisualStyleBackColor = true;
            this.buttonLoginCancel.Click += new System.EventHandler(this.buttonLoginCancel_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLogin.Location = new System.Drawing.Point(30, 77);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(94, 47);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "确定";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(66, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "    密码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textboxLoginPassword
            // 
            this.textboxLoginPassword.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textboxLoginPassword.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textboxLoginPassword.ForeColor = System.Drawing.Color.White;
            this.textboxLoginPassword.Location = new System.Drawing.Point(101, 29);
            this.textboxLoginPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textboxLoginPassword.Name = "textboxLoginPassword";
            this.textboxLoginPassword.PasswordChar = '*';
            this.textboxLoginPassword.Size = new System.Drawing.Size(181, 32);
            this.textboxLoginPassword.TabIndex = 0;
            // 
            // groupboxLogin
            // 
            this.groupboxLogin.Controls.Add(this.textboxLoginPassword);
            this.groupboxLogin.Controls.Add(this.buttonLogin);
            this.groupboxLogin.Controls.Add(this.buttonLoginCancel);
            this.groupboxLogin.Controls.Add(this.label1);
            this.groupboxLogin.Location = new System.Drawing.Point(12, 12);
            this.groupboxLogin.Name = "groupboxLogin";
            this.groupboxLogin.Size = new System.Drawing.Size(319, 148);
            this.groupboxLogin.TabIndex = 8;
            this.groupboxLogin.TabStop = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(339, 166);
            this.Controls.Add(this.groupboxLogin);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(355, 205);
            this.MinimumSize = new System.Drawing.Size(355, 205);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupboxLogin.ResumeLayout(false);
            this.groupboxLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoginCancel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textboxLoginPassword;
        private System.Windows.Forms.GroupBox groupboxLogin;
    }



}