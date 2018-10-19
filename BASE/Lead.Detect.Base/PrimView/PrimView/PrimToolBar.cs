using System;
using System.Drawing;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.View.PrimView
{
    public partial class PrimToolBar : UserControl
    {
        public event Action<string> CreateDevice;

       // public event Action<IPrim> OnAddDevice;

        public PrimToolBar()
        {
            InitializeComponent();
        }

        public int InitializePrims()
        {
            if (DevPrimsFactoryManager.Instance.PrimCreators.Count < 0)
            {
                throw new Exception("prims not found");
            }

            foreach (var kvp in DevPrimsFactoryManager.Instance.PrimCreators)
            {
                var btnPrim = new ToolStripButton
                {
                    ImageScaling = ToolStripItemImageScaling.None,
                    AutoSize = false,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    DisplayStyle = ToolStripItemDisplayStyle.Image,
                    Image = kvp.Value.PrimProps.Icon,
                    Tag = kvp.Key,
                    Size = new Size(56, 52),
                    Text = kvp.Key
                };
                btnPrim.Enabled = true;
                btnPrim.Click += btnPrim_Click;
                btnPrim.MouseDown += btnPrim_MouseDown;
                btnPrim.MouseMove += btnPrim_MouseMove;
                toolStrip.Items.Add(btnPrim);
            }

            return 0;
        }

        private void btnPrim_Click(object sender, EventArgs e)
        {
            var button = sender as ToolStripButton;
            if (button != null)
            {
                var devTypeName = button.Tag.ToString();
                CreateDevice?.Invoke(devTypeName);
            }
        }

        private void btnPrim_MouseDown(object sender, MouseEventArgs e)
        {
            // _isMouseDown = true;
        }


        private void btnPrim_MouseMove(object sender, MouseEventArgs e)
        {
            var button = sender as ToolStripButton;
            if (button != null && (e.Button & MouseButtons.Left) != 0)
            {
                var devTypeName = button.Tag.ToString();
                DoDragDrop(devTypeName, DragDropEffects.Copy);
            }
        }
    }
}