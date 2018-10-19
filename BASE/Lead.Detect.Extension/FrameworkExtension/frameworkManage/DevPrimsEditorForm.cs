using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    public partial class DevPrimsEditorForm : Form
    {
        public DevPrimsEditorForm()
        {
            InitializeComponent();
        }

        public DevProject Device;


        public List<IPrim> DevPrims;

        private void EnvironmentEditorForm_Load(object sender, EventArgs e)
        {
            if (DevPrims == null)
            {
                DevPrims = new List<IPrim>();
            }

            comboBoxPrimTypes.Items.Clear();
            comboBoxPrimTypes.Items.AddRange(DevPrimsFactoryManager.Instance.PrimCreators.Keys.Select(k => (object)k).ToArray());


            UpdateDevPrims();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Device = new DevProject()
            {
                ProjectName = "new.dev"
            };

            DevPrims = new List<IPrim>();

            UpdateDevPrims();
        }

        private void UpdateDevPrims()
        {
            if (Device != null) Text = Device.ProjectName;

            comboBoxDevPrims.Items.Clear();
            comboBoxDevPrims.Items.AddRange(DevPrims.Select(p => (object)p.Name).ToArray());

            richTextBoxDevPrims.Text = string.Join("\r\n", DevPrims.Select(d => $"{d.Name} {d.PrimTypeName} {d.ToString()}"));

            propertyGridDevPrims.SelectedObject = DevPrims;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "(Device Project)|*.dev";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                EnvironmentManager.Ins.LoadPrims(DevPrims, ofd.FileName);

                UpdateDevPrims();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "(Device Project)|*.dev";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                EnvironmentManager.Ins.SavePrims(DevPrims, sfd.FileName);
            }
        }

        private void buttonAddPrim_Click(object sender, EventArgs e)
        {
            var prim = DevPrimsFactoryManager.Instance.InvokeCreator(comboBoxPrimTypes.Text);
            if (prim == null)
            {
                return;
            }


            var primConfigForm = new DevPrimConfigForm()
            {
                Prim = prim,
            };

            primConfigForm.ShowDialog();

            DevPrims.Add(prim);

            UpdateDevPrims();
        }

        private void buttonDeletePrim_Click(object sender, EventArgs e)
        {
            DevPrims.RemoveAll(p => p.Name == comboBoxDevPrims.Text);

            UpdateDevPrims();
        }

        private void buttonConfigPrim_Click(object sender, EventArgs e)
        {
            var prim = DevPrims.FirstOrDefault(p => p.Name == comboBoxDevPrims.Text);
            if (prim == null)
            {
                return;
            }

            var primConfigForm = new DevPrimConfigForm()
            {
                Prim = prim,
            };

            primConfigForm.ShowDialog();
        }
    }
}