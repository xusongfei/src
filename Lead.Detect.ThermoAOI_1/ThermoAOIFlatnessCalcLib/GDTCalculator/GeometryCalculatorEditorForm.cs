using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalcItem;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.GDTCalculator
{
    public partial class GeometryCalculatorEditorForm : Form
    {
        public GeometryCalculatorEditorForm()
        {
            InitializeComponent();
        }

        private void GeometryCalculatorControl_Load(object sender, EventArgs e)
        {
            comboBoxCalcType.Items.AddRange(Enum.GetNames(typeof(GDTType)));
        }


        public GeometryCalculator Calculator;


        public void LoadCalculator(GeometryCalculator calculator)
        {
            Calculator = calculator;
            ReloadCalculator();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Multiselect = false,
                Filter = $"(Fprj计算文件)|*.calc|(所有文件)|*.*",
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Calculator = GeometryCalculator.Load(ofd.FileName);

                    ReloadCalculator();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"打开文件失败：{ex.Message}");
                }


            }

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Calculator = new GeometryCalculator();

                ReloadCalculator();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"新建文件失败：{ex.Message}");
            }


        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = $"(Fprj计算文件)|*.calc|(所有文件)|*.*",
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Calculator.SaveAs(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"另存为文件失败：{ex.Message}");
                }
            }
        }

        private void comboBoxCalcs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cname = comboBoxCalcs.Text;
            if (Calculator.GeoCalcs.Exists(c => c.Name == cname))
            {
                ReloadCalcItem(Calculator.GeoCalcs.First(c => c.Name == cname));
            }
        }

        private void buttonAddCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var cname = comboBoxCalcType.Text;

                switch (cname)
                {
                    case nameof(GDTType.Flatness):
                        Calculator.GeoCalcs.Add(new FlatnessCalc());
                        break;
                    case nameof(GDTType.Parallelism):
                        Calculator.GeoCalcs.Add(new ParallelismCalc());
                        break;
                    case nameof(GDTType.ProfileOfSurface):
                        Calculator.GeoCalcs.Add(new ProfileOfSurfaceCalc());
                        break;

                    default:
                        throw new Exception("not implement type");
                }

                ReloadCalcItem(Calculator.GeoCalcs.Last());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ADD CALC失败：{ex.Message}");
            }

        }

        private void buttonDeleteCalc_Click(object sender, EventArgs e)
        {
            var cname = comboBoxCalcs.Text;
            if (Calculator.GeoCalcs.Exists(c => c.Name == cname))
            {
                Calculator.GeoCalcs.RemoveAll(c => c.Name == cname);

                ReloadCalculator();
            }

        }

        private void propertyGridGDTCalc_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ReloadCalculator();
        }
        private void ReloadCalculator()
        {
            if (Calculator == null)
            {
                return;
            }

          

            try
            {
                richTextBoxDetails.Text = Calculator.ToString();
                comboBoxCalcs.Items.Clear();
                comboBoxCalcs.Items.AddRange(Calculator.GeoCalcs.Select(g => (object)g.Name).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载计算文件失败: {ex.Message}");
            }
        }

        private void ReloadCalcItem(GDTCalc calc)
        {
            propertyGridGDTCalc.SelectedObject = calc;
            propertyGridGDTCalc.ExpandAllGridItems();
        }

    }
}
