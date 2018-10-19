using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.Base.GlobalCell
{
    public partial class CellOutputForm : DockContent
    {
        private ICell _cell;

        public CellOutputForm()
        {
            InitializeComponent();
        }

        public CellOutputForm(ICell cell)
        {
            InitializeComponent();
            _cell = cell;
        }
    }
}