using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension;

namespace Lead.Detect.FrameworkExtension.userControls
{
    public partial class CylinderControl : UserControl, IFrameworkControl
    {
        public CylinderControl()
        {
            InitializeComponent();
        }

        public List<ICylinderEx> CyExs;

        public void LoadFramework()
        {
            var props = typeof(ICylinderEx).GetProperties().Where(p => p.PropertyType != typeof(IMotionWrapper)).ToArray();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            foreach (var p in props)
            {
                var i = dataGridView1.Columns.Add(p.Name, p.Name);
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.Columns.Add("Status", "状态");
            dataGridView1.Columns.Add(new DataGridViewButtonColumn() { Text = "Trigger", HeaderText = "触发" });

            if (CyExs != null)
            {
                foreach (var ex in CyExs)
                {
                    var r = dataGridView1.Rows.Add(1);
                    for (var i = 0; i < props.Length; i++)
                    {
                        dataGridView1.Rows[r].Cells[i].Value = props[i].GetValue(ex).ToString();
                    }
                }
            }
        }

        private Timer timer;
        public void FrameworkUpdate()
        {
            if (dataGridView1.RowCount > 0 && CyExs != null)
            {
                for (int i = 0; i < CyExs.Count(); i++)
                {
                    dataGridView1.Rows[i].Cells["DiOrg"].Style = CyExs[i].GetDiExs()[0].GetDiSts() ? _green : _lightGray;
                    dataGridView1.Rows[i].Cells["DiWork"].Style = CyExs[i].GetDiExs()[1].GetDiSts() ? _green : _lightGray;

                    dataGridView1.Rows[i].Cells["DoOrg"].Style = CyExs[i].GetDoExs()[0].Port > 0 && CyExs[i].GetDoExs()[0].GetDoSts() ? _green : _lightGray;
                    dataGridView1.Rows[i].Cells["DoWork"].Style = CyExs[i].GetDoExs()[1].Port > 0 && CyExs[i].GetDoExs()[1].GetDoSts() ? _green : _lightGray;


                    if (!CyExs[i].GetDiExs()[0].GetDiSts() && CyExs[i].GetDiExs()[1].GetDiSts())
                    {
                        dataGridView1.Rows[i].Cells["Status"].Style = _green;
                    }
                    else if (CyExs[i].GetDiExs()[0].GetDiSts() && !CyExs[i].GetDiExs()[1].GetDiSts())
                    {
                        dataGridView1.Rows[i].Cells["Status"].Style = _lightGray;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["Status"].Style = _red;
                    }
                }
            }
        }

        private DataGridViewCellStyle _green = new DataGridViewCellStyle()
        {
            BackColor = Color.Lime,
        };

        private DataGridViewCellStyle _lightGray = new DataGridViewCellStyle()
        {
            BackColor = Color.LightGray,
        };

        private DataGridViewCellStyle _red = new DataGridViewCellStyle()
        {
            BackColor = Color.Red,
        };

        public void FrameworkActivate()
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Tick += (sender, args) => { FrameworkUpdate(); };
                timer.Interval = 300;
                timer.Enabled = true;
            }
            else
            {
                timer.Interval = 300;
                timer.Enabled = true;
            }
        }

        public void FrameworkDeactivate()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].CellType == typeof(DataGridViewButtonCell))
                {
                    var doexs = CyExs[e.RowIndex].GetDoExs();
                    var doSetSts1 = doexs[1].Port > 0;
                    var isSet = doexs.WaitDo(null, new[] { false, doSetSts1 }, 0);
                    CyExs?[e.RowIndex].SetDo(null, !isSet, timeout: 0);
                }
            }
        }
    }
}
