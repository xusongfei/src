using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lead.Detect.Interfaces;

using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.DefaultCell
{
    public partial class BaseCellOutputForm : DockContent
    {
        private ICell _cell;
        public BaseCellOutputForm()
        {
            InitializeComponent();
        }
        public BaseCellOutputForm(ICell cell)
        {
            InitializeComponent();
            _cell = cell;
        }
    }
}
