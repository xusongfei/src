using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;
using MachineUtilityLib.UtilProduct;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2
{
    public class Thermo2ProductDisplayControl : ProductDataDisplayControl
    {
        public override void DrawDataGridViewColor(DataGridView dataGridView, ProductDataBase product)
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
    }
}
