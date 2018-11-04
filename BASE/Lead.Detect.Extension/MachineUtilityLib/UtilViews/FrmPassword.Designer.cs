using System;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    partial class FrmPassword
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tBoxOrgPassword = new System.Windows.Forms.TextBox();
            this.tBoxNewPassword = new System.Windows.Forms.TextBox();
            this.gBoxLogin = new System.Windows.Forms.GroupBox();
            this.gBoxPasswordChange = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.gBoxLogin.SuspendLayout();
            this.gBoxPasswordChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(198, 88);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(55, 88);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 47);
            this.button2.TabIndex = 3;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(108, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "    密码：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_password.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_password.ForeColor = System.Drawing.Color.White;
            this.textBox_password.Location = new System.Drawing.Point(114, 29);
            this.textBox_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(156, 38);
            this.textBox_password.TabIndex = 0;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(39, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "原密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(39, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "新密码：";
            // 
            // tBoxOrgPassword
            // 
            this.tBoxOrgPassword.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxOrgPassword.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxOrgPassword.ForeColor = System.Drawing.Color.White;
            this.tBoxOrgPassword.Location = new System.Drawing.Point(103, 20);
            this.tBoxOrgPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tBoxOrgPassword.Name = "tBoxOrgPassword";
            this.tBoxOrgPassword.PasswordChar = '*';
            this.tBoxOrgPassword.Size = new System.Drawing.Size(156, 38);
            this.tBoxOrgPassword.TabIndex = 6;
            // 
            // tBoxNewPassword
            // 
            this.tBoxNewPassword.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxNewPassword.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tBoxNewPassword.ForeColor = System.Drawing.Color.White;
            this.tBoxNewPassword.Location = new System.Drawing.Point(103, 70);
            this.tBoxNewPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tBoxNewPassword.Name = "tBoxNewPassword";
            this.tBoxNewPassword.PasswordChar = '*';
            this.tBoxNewPassword.Size = new System.Drawing.Size(156, 38);
            this.tBoxNewPassword.TabIndex = 7;
            // 
            // gBoxLogin
            // 
            this.gBoxLogin.Controls.Add(this.textBox_password);
            this.gBoxLogin.Controls.Add(this.button2);
            this.gBoxLogin.Controls.Add(this.button1);
            this.gBoxLogin.Controls.Add(this.label1);
            this.gBoxLogin.Location = new System.Drawing.Point(25, 23);
            this.gBoxLogin.Name = "gBoxLogin";
            this.gBoxLogin.Size = new System.Drawing.Size(319, 148);
            this.gBoxLogin.TabIndex = 8;
            this.gBoxLogin.TabStop = false;
            this.gBoxLogin.Text = "登录";
            this.gBoxLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // gBoxPasswordChange
            // 
            this.gBoxPasswordChange.Controls.Add(this.button4);
            this.gBoxPasswordChange.Controls.Add(this.button3);
            this.gBoxPasswordChange.Controls.Add(this.tBoxNewPassword);
            this.gBoxPasswordChange.Controls.Add(this.tBoxOrgPassword);
            this.gBoxPasswordChange.Controls.Add(this.label3);
            this.gBoxPasswordChange.Controls.Add(this.label2);
            this.gBoxPasswordChange.Cursor = System.Windows.Forms.Cursors.Default;
            this.gBoxPasswordChange.Location = new System.Drawing.Point(23, 23);
            this.gBoxPasswordChange.Name = "gBoxPasswordChange";
            this.gBoxPasswordChange.Size = new System.Drawing.Size(321, 179);
            this.gBoxPasswordChange.TabIndex = 9;
            this.gBoxPasswordChange.TabStop = false;
            this.gBoxPasswordChange.Text = "密码修改";
            this.gBoxPasswordChange.Visible = false;
            this.gBoxPasswordChange.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(187, 118);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 47);
            this.button4.TabIndex = 9;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(43, 118);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 47);
            this.button3.TabIndex = 8;
            this.button3.Text = "确定";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmPassword
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(377, 210);
            this.Controls.Add(this.gBoxPasswordChange);
            this.Controls.Add(this.gBoxLogin);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassWord";
            this.gBoxLogin.ResumeLayout(false);
            this.gBoxLogin.PerformLayout();
            this.gBoxPasswordChange.ResumeLayout(false);
            this.gBoxPasswordChange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBoxOrgPassword;
        private System.Windows.Forms.TextBox tBoxNewPassword;
        private System.Windows.Forms.GroupBox gBoxLogin;
        private System.Windows.Forms.GroupBox gBoxPasswordChange;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
    }
    [Serializable]
    public class password
    {
        public string passWord { get; set; }
    }
}