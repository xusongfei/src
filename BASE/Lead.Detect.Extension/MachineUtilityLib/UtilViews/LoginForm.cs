using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{


    public partial class LoginForm : Form
    {

        public bool IsLoginSuccess;

        public LoginForm()
        {
            InitializeComponent();
            Password.InitializePassword();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //密码比较
            if (textboxLoginPassword.Text.Equals(Password.ReadPassword()))
            {
                IsLoginSuccess = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入正确的密码!");
                textboxLoginPassword.Text = "";
                IsLoginSuccess = false;
                textboxLoginPassword.Focus();
            }
        }

        private void buttonLoginCancel_Click(object sender, EventArgs e)
        {
            IsLoginSuccess = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }


    [Serializable]
    public class Password
    {
        public static string PasswordFile = Environment.CurrentDirectory + "\\PassWord\\password.dat";

        public static void InitializePassword()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\PassWord"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\PassWord");
            }

            if (!File.Exists(PasswordFile))
            {
                WritePassword("111111");
            }
        }

        //获取密码
        public static string ReadPassword()
        {
            InitializePassword();

            using (var fs = new FileStream(PasswordFile, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(Password));
                return (serializer.Deserialize(fs) as Password)?.Key;
            }
        }

        //保存密码
        public static void WritePassword(string password)
        {
            var users = new Password()
            {
                Key = password
            };


            using (var fs = new FileStream(PasswordFile, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(Password));
                serializer.Serialize(fs, users);
            }
        }


        public string UserName { get; set; }
        public string Key { get; set; }
    }
}