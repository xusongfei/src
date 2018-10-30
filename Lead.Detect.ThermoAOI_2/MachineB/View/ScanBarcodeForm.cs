using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.ThermoAOI2.MachineB.View
{
    public partial class ScanBarcodeForm : Form
    {
        public StationTask Task;
        public string Barcode;


        public int BarcodeLen;
        public string BarcodePattern;


        private Regex _barcodeRegex;


        public ScanBarcodeForm()
        {
            InitializeComponent();
        }

        private void ScanBarcodeForm_Load(object sender, EventArgs e)
        {
            _barcodeRegex = new Regex(BarcodePattern);

            Text = $"扫描条码（条码类型 - {BarcodePattern} 条码长度 - {BarcodeLen}）";

            StartPosition = FormStartPosition.CenterParent;
            TopMost = true;


            textBoxBarcode.Text = string.Empty;
            textBoxBarcode.Focus();
            textBoxBarcode.SelectAll();

            timer1.Interval = 300;
            timer1.Enabled = true;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Task.RunningState == RunningState.Running)
            {
                Show();
                BringToFront();


                if (textBoxBarcode.Text.Length >= BarcodeLen)
                {
                    if (_barcodeRegex.IsMatch(textBoxBarcode.Text))
                    {
                        Barcode = textBoxBarcode.Text;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        textBoxBarcode.SelectAll();
                        textBoxBarcode.BackColor = Color.LightCoral;
                        buttonStatus.Text = $"条码NG:{textBoxBarcode.Text.Length}";
                    }

                }
                else if (textBoxBarcode.Text.Length > 0)
                {

                    textBoxBarcode.BackColor = Color.LightSkyBlue;
                    buttonStatus.Text = $"输入中:{textBoxBarcode.Text.Length}";

                }
                else
                {
                    textBoxBarcode.BackColor = Color.White;
                    buttonStatus.Text = "扫描条码";
                }
            }
            else if (Task.RunningState == RunningState.Pause)
            {
                this.Hide();
            }
            else
            {
                Barcode = string.Empty;
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

    }
}
