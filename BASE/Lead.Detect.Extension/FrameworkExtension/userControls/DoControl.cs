using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.userControls
{
    public partial class DoControl : UserControl, IFrameworkControl
    {
        public DoControl()
        {
            InitializeComponent();
        }

        public List<IDoEx> DoExs;

        public void LoadFramework()
        {
            var props = typeof(IDoEx).GetProperties().Where(p => p.PropertyType != typeof(IMotionWrapper)).ToArray();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            foreach (var p in props)
            {
                var i = dataGridView1.Columns.Add(p.Name, p.Name);
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.Columns.Add("Status", "状态");
            dataGridView1.Columns.Add(new DataGridViewButtonColumn() { Text = "Trigger", HeaderText = "触发" });

            if (DoExs != null)
            {
                foreach (var ex in DoExs)
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
            if (dataGridView1.RowCount > 0 && DoExs != null)
            {
                for (int i = 0; i < DoExs.Count(); i++)
                {
                    dataGridView1.Rows[i].Cells["Status"].Style = DoExs[i].GetDoSts() ? _green : _lightGray;
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
        public void FrameworkActivate()
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Tick += (sender, args) => { FrameworkUpdate(); };
                timer.Interval = 100;
                timer.Enabled = true;
            }
            else
            {
                timer.Interval = 100;
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
                    DoExs?[e.RowIndex].SetDo(!DoExs[e.RowIndex].GetDoSts());
                }
            }
        }
    }
}
