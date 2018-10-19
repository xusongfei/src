using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lead.Detect.DefaultCell;
using Lead.Detect.Interfaces;

namespace Lead.Detect.VisioCells.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DefCell cell = new DefCell();
            cell.CellConfigForm.Show();
        }
    }
}
