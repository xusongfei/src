using System;
using System.Windows.Forms;

namespace Lead.Detect.DatabaseHelper
{
    public partial class ProductDatabaseControl : UserControl
    {
        public ProductDatabaseControl()
        {
            InitializeComponent();
        }

        private void buttonQueryProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxProductBarcode.Text))
            {
                MessageBox.Show("条码为空！");
                return;
            }

            if (SqlLiteHelper.DB != null)
            {
                dataGridView1.DataSource = SqlLiteHelper.DB.From<ProductDataEntity>().Where(p => p.Barcode == textBoxProductBarcode.Text).ToDataTable();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }

        }
            

        private void buttonQueryAll_Click(object sender, EventArgs e)
        {
            if (SqlLiteHelper.DB != null)
            {
                dataGridView1.DataSource = SqlLiteHelper.DB.From<ProductDataEntity>().ToDataTable();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("清除所有本地数据？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (SqlLiteHelper.DB != null)
            {
                SqlLiteHelper.DB.DeleteAll<ProductDataEntity>();
                MessageBox.Show("清除完成！");
            }
        }
    }
}
