using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    public partial class FrmPassword : Form
    {
        public static string filePath = System.Environment.CurrentDirectory + "\\PassWord\\PassWordSave.obj";
        public bool pwdFlag = false;

        public FrmPassword()
        {
            InitializeComponent();
            pasFileCheck();
        }

        public void pasFileCheck()
        {
            if (!Directory.Exists(System.Environment.CurrentDirectory + "\\PassWord"))
            {
                DirectoryInfo di = Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\PassWord");
            }

            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs1.Close();
                setPassword("111111");
            }
        }

        public bool getPwdFlag()
        {
            return pwdFlag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //   if (textBox_password.Text.Equals(INIHelper.Read("全局配置", "登录密码")))

            //密码比较
            if (textBox_password.Text.Equals(getPassword()))
            {
                pwdFlag = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入正确的密码");
                textBox_password.Text = "";
                pwdFlag = false;
                this.textBox_password.Focus();
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }

        //获取密码
        private string getPassword()
        {
            pasFileCheck();
            //用于序列化和反序列化的对象
            BinaryFormatter serializer = new BinaryFormatter();
            //反序列化
            FileStream loadFile = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            List<password> pasResult = (List<password>) serializer.Deserialize(loadFile);
            loadFile.Close();
            return pasResult[0].passWord;
        }

        //保存密码
        private void setPassword(string passWord)
        {
            password pas = new password {passWord = passWord};

            List<password> pasList = new List<password>();
            pasList.Add(pas);
            //开始序列化
            FileStream loadFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            //用于序列化和反序列化的对象
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(loadFile, pasList);
            loadFile.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.gBoxLogin.Visible = false;
            this.gBoxPasswordChange.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.gBoxLogin.Visible = true;
            this.gBoxPasswordChange.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.tBoxOrgPassword.Text.Equals(getPassword()))
            {
                if (this.tBoxNewPassword.Text != "")
                {
                    //保存新密码
                    setPassword(this.tBoxNewPassword.Text);
                    MessageBox.Show("密码保存成功", "提示");
                    this.gBoxLogin.Visible = true;
                    this.gBoxPasswordChange.Visible = false;
                }
                else
                {
                    MessageBox.Show("新密码不能为空，请重新输入", "提示");
                    this.tBoxNewPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("原密码不正确，请重新输入！", "提示");
                this.tBoxOrgPassword.Focus();
            }
        }
    }
}