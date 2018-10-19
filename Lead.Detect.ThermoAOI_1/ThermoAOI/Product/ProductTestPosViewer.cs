using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.ThermoAOI.Product
{
    public partial class ProductTestPosViewer : Form
    {
        public List<PosXYZ> Positions;

        public ProductTestPosViewer()
        {
            InitializeComponent();
        }

        private void ProductTestPosViewer_Load(object sender, EventArgs e)
        {
            if(Positions!=null)
            {
                var sb = new StringBuilder();
                foreach(var p in Positions)
                {
                    sb.AppendLine($"{p.Index:D3} {p.Name,10} {p.X:000.000} {p.Y:000.000} {p.Z:000.000} {p.Description,10}");
                }

                richTextBox1.Text = sb.ToString();
            }
        }
    }
}
