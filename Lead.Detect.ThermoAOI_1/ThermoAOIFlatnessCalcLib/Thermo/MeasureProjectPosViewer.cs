using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    public partial class MeasureProjectPosViewer : Form
    {
        public List<IPlatformPos> Positions;

        public MeasureProjectPosViewer()
        {
            InitializeComponent();
        }

        private void ProductTestPosViewer_Load(object sender, EventArgs e)
        {
            if (Positions != null)
            {
                var sb = new StringBuilder();
                foreach (var p in Positions)
                {
                    if (p is PosXYZ)
                    {
                        var np = p as PosXYZ;
                        sb.AppendLine($"{np.Index:D3} {np.Name,10} {np.X:000.000} {np.Y:000.000} {np.Z:000.000} {np.Description,10}");
                    }
                    else if (p is PosXYZU)
                    {
                        var np = p as PosXYZU;
                        sb.AppendLine($"{np.Index:D3} {np.Name,10} {np.X:000.000} {np.Y:000.000} {np.Z:000.000} {np.U:000.000} {np.Description,10}");
                    }
                }

                richTextBox1.Text = sb.ToString();
            }
        }
    }
}
