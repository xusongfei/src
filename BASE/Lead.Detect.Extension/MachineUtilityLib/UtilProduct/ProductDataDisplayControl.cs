using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MachineUtilityLib.UtilProduct
{
    public partial class ProductDataDisplayControl : UserControl
    {
        public ProductDataDisplayControl()
        {
            InitializeComponent();

            buttonStatus.Text = "WAIT";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;

            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
        }


        public void UpdateStart()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateStart));

            }
            else
            {
                buttonStatus.Text = "WAIT";
                buttonStatus.BackColor = Color.LightGray;

                textBoxCT.Text = "0.00";
                timer1.Stop();

                dataGridView1.DataSource = new DataTable();
            }
        }


        private DateTime _startDateTime;

        public void UpdateTesting(ProductDataBase product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ProductDataBase>(UpdateTesting), product);

            }
            else
            {
                timer1.Start();
                buttonStatus.Text = "TESTING";
                buttonStatus.BackColor = Color.Yellow;

                textBoxCT.Text = product.CT.ToString("F2");
                _startDateTime = product.StartTime;

                dataGridView1.DataSource = product.ToDataTable();
            }
        }



        public void UpdateResult(ProductDataBase product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ProductDataBase>(UpdateResult), product);

            }
            else
            {
                buttonStatus.Text = product.Status.ToString();
                buttonStatus.BackColor = product.Status == ProductStatus.OK ? Color.Lime : Color.Red;

                timer1.Stop();
                textBoxCT.Text = product.CT.ToString("F2");

                dataGridView1.DataSource = product.ToDataTable();

                if (dataGridView1.RowCount > 0)
                {
                    DrawDataGridViewColor(dataGridView1, product);
                }
            }
        }


        public virtual void DrawDataGridViewColor(DataGridView dataGridView, ProductDataBase product)
        {
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxCT.Text = $@"{(DateTime.Now - _startDateTime).TotalSeconds:F1}";
        }
    }
}
