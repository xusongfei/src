using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    public partial class ThermoProductDisplayControl : UserControl
    {
        public Station Station;
        public ThermoProductDisplayControl()
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

        public void UpdateTesting(ProductBase.Product product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ProductBase.Product>(UpdateTesting), product);

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



        public void UpdateResult(ProductBase.Product product)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<ProductBase.Product>(UpdateResult), product);

            }
            else
            {
                if (product != null)
                {
                    buttonStatus.Text = product.Status.ToString();
                    if (product.Status == ProductStatus.OK)
                    {
                        buttonStatus.BackColor = Color.Lime;
                    }
                    else if (product.Status == ProductStatus.NG)
                    {
                        buttonStatus.BackColor = Color.Red;
                    }
                    else if (product.Status == ProductStatus.ERROR)
                    {
                        buttonStatus.BackColor = Color.DeepPink;
                    }
                    else if (product.Status == ProductStatus.NONE)
                    {
                        buttonStatus.BackColor = Color.LightGray;
                    }

                    timer1.Stop();
                    textBoxCT.Text = product.CT.ToString("F2");

                    dataGridView1.DataSource = product.ToDataTable();

                    if (dataGridView1.RowCount > 0)
                    {
                        DrawDataGridViewColor(dataGridView1, product);
                    }
                }
                else
                {
                    timer1.Stop();
                    buttonStatus.Text = Station.RunningState.ToString();
                    buttonStatus.BackColor = Color.DeepPink;
                }
            }
        }


        public virtual void DrawDataGridViewColor(DataGridView dataGridView, ProductBase.Product product)
        {

            //find spc result start index;
            int spcIndex = 0;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                var r = dataGridView.Rows[i];
                if (r.Cells[0].Value.ToString().StartsWith("SPC"))
                {
                    spcIndex = i;
                    break;
                }

            }

            //draw spc result rows
            ThermoProduct thermoProduct = product as ThermoProduct;
            if (thermoProduct != null)
                foreach (var fai in thermoProduct.SPCItems)
                {
                    if (!fai.CheckSpec())
                    {
                        dataGridView.Rows[spcIndex].Cells[1].Style = new DataGridViewCellStyle()
                        {
                            BackColor = Color.Red,
                        };
                    }

                    spcIndex++;
                }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxCT.Text = $@"{(DateTime.Now - _startDateTime).TotalSeconds:F1}";


            if (Station != null && Station.RunningState == RunningState.WaitReset)
            {
                UpdateResult(null);
            }
        }
    }
}
