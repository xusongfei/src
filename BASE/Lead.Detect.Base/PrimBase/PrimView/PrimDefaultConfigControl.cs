using System.Windows.Forms;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.Base.PrimView
{
    public partial class PrimDefaultConfigControl : UserControl
    {
        public PrimDefaultConfigControl()
        {
            InitializeComponent();
        }

        public PrimDefaultConfigControl(IPrim prim)
        {
            InitializeComponent();


            propertyGrid1.SelectedObject = prim;
        }
    }
}