using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }


        private void buttonChangeConfirm_Click(object sender, EventArgs e)
        {
            if (this.textboxPasswordOld.Text.Equals(Password.ReadPassword()))
            {
                if (this.textboxPasswordNew.Text != "")
                {
                    //保存新密码
                    Password.WritePassword(this.textboxPasswordNew.Text);
                    MessageBox.Show("密码保存成功", "提示");
                }
                else
                {
                    MessageBox.Show("新密码不能为空，请重新输入", "提示");
                    this.textboxPasswordNew.Focus();
                }
            }
            else
            {
                MessageBox.Show("原密码不正确，请重新输入！", "提示");
                this.textboxPasswordOld.Focus();
            }

        }
        private void buttonChangeCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
