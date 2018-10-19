using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;
using Lead.Detect.PrimPlcOmron;

namespace Lead.Detect.PrimTest
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }
        private IPrim p;
        private void button1_Click(object sender, EventArgs e)
        { 
            IPrimCreator c = new PrimCreator();
            if(c == null)
            {
                return;
            }
            IPrimTypeAttributes attr = c.PrimProps;
            if(attr == null)
            {
                return;
            }

            p = c.Create();
            if(p == null)
            {
                return;
            }

            richTextBox1.AppendText(attr.DisplayName + System.Environment.NewLine);
            richTextBox1.AppendText(attr.DisplayGroup + System.Environment.NewLine);
            richTextBox1.AppendText(attr.Description + System.Environment.NewLine);
            richTextBox1.AppendText(attr.Name + System.Environment.NewLine);
            richTextBox1.AppendText(attr.Group + System.Environment.NewLine);
            richTextBox1.AppendText(attr.MajorVersion + System.Environment.NewLine);
            richTextBox1.AppendText(attr.MinorVersion + System.Environment.NewLine);

            pictureBox1.Image = attr.Icon;

            Form f1 = new Form();
            f1.Controls.Add(p.PrimConfigUI);
            f1.Text = "f1";
            f1.Show();

            Form f2 = new Form();
            f2.Controls.Add(p.PrimDebugUI);
            f2.Text = "f2";
            f2.Show();

            Form f3 = new Form();
            f3.Controls.Add(p.PrimOutputUI);
            f3.Text = "f3";
            f3.Show();

            Form f4 = new Form();
            f4.Controls.Add(p.PrimConfigUI);
            f4.Text = "f4";
            f4.Show();

            Form f5 = new Form();
            f5.Controls.Add(p.PrimDebugUI);
            f5.Text = "f5";
            f5.Show();

            Form f6 = new Form();
            f6.Controls.Add(p.PrimOutputUI);
            f6.Text = "f6";
            f6.Show();
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(p.PrimOutputUI);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
