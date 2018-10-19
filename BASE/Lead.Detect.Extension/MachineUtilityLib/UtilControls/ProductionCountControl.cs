using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineUtilityLib.UtilControls
{
    public partial class ProductionCountControl : UserControl
    {
        public ProductionCountControl()
        {
            InitializeComponent();
        }


        public void SetStation(string station)
        {
            if (string.IsNullOrEmpty(station))
            {
                groupBox1.Text = $"生产统计";
            }
            else
            {
                groupBox1.Text = $"{station}生产统计";
            }
        }


        private ProductionCount _production;

        public void UpdateProduction(ProductionCount production)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ProductionCount>(UpdateProduction), production);
            }
            else
            {
                if (production == null)
                {
                    return;
                }

                textBoxALL.Text = production.TotalCount.ToString();
                textBoxOK.Text = production.OKCount.ToString();
                textBoxNG.Text = production.NGCount.ToString();

                _production = production;
            }
        }

        private void textBox_DoubleClick(object sender, EventArgs e)
        {
            if (_production == null)
            {
                return;
            }

            if (MessageBox.Show($"清除统计:\r\n{_production.ToString()}？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {
                _production.Clear();
                UpdateProduction(_production);
            }
        }
    }
}