using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo1;
using MachineUtilityLib.UtilProduct;

namespace Lead.Detect.ThermoAOI.Product
{
    public partial class ProductTestDisplayControl : UserControl
    {
        public ProductTestDisplayControl()
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

                textBoxCT.Text = string.Empty;
                timer1.Stop();

                dataGridView1.DataSource = new DataTable();
            }
        }


        private DateTime _starTime;

        public void UpdateTesting(Thermo1Product product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateTesting), product);

            }
            else
            {
                timer1.Start();
                buttonStatus.Text = "TESTING";
                buttonStatus.BackColor = Color.Yellow;

                textBoxCT.Text = product.CT.ToString("F2");
                _starTime = product.StartTime;

                dataGridView1.DataSource = product.ToDataTable();
            }
        }



        public void UpdateResult(Thermo1Product product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Thermo1Product>(UpdateResult), product);

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
                    int spcIndex = 0;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        var r = dataGridView1.Rows[i];
                        if (r.Cells[0].Value.ToString().StartsWith("SPC"))
                        {
                            spcIndex = i;
                            break;
                        }
                    }

                    foreach (var fai in product.SPCItems)
                    {
                        if (!fai.CheckSpec())
                        {
                            dataGridView1.Rows[spcIndex].Cells[1].Style = new DataGridViewCellStyle()
                            {
                                BackColor = Color.Red,
                            };
                        }
                        spcIndex++;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxCT.Text = $"{(DateTime.Now - _starTime).TotalSeconds:F1}";
        }
    }
}
