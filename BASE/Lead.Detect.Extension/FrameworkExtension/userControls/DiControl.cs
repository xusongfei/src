using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.userControls
{
    public partial class DiControl : UserControl, IFrameworkControl
    {
        public DiControl()
        {
            InitializeComponent();
        }

        public List<IDiEx> DiExs;

        public void LoadFramework()
        {
            var props = typeof(IDiEx).GetProperties().Where(p => p.PropertyType != typeof(MotionCardWrapper)).ToArray();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            foreach (var p in props)
            {
                var i = dataGridView1.Columns.Add(p.Name, p.Name);
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView1.Columns.Add("Status", "状态");

            if (DiExs != null)
            {
                foreach (var diEx in DiExs)
                {
                    var r = dataGridView1.Rows.Add(1);
                    for (var i = 0; i < props.Length; i++)
                    {
                        dataGridView1.Rows[r].Cells[i].Value = props[i].GetValue(diEx).ToString();
                    }
                }
            }
        }

        private Timer timer;
        public void FrameworkUpdate()
        {
            if (dataGridView1.RowCount > 0 && DiExs != null)
            {
                for (int i = 0; i < DiExs.Count(); i++)
                {
                    dataGridView1.Rows[i].Cells["Status"].Style = DiExs[i].GetDiStsRaw() ? _green : _lightGray;
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

    }
}
