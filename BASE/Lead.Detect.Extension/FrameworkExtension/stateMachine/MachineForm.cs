using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.loadUtils;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.FrameworkExtension.stateMachine
{
    public partial class MachineForm : Form
    {
        public MachineForm()
        {
            InitializeComponent();
        }

        private void MachineForm_Load(object sender, EventArgs e)
        {
            if (Machine != null)
            {
                Text = Machine.ToString();

                richTextBox1.Text = Machine.SerializeToString();

                comboBoxExType.Items.AddRange(new object[] { "DI", "DO", "VIO", "CY", "PLATFORM", "STATION", "TASK" });

            }


        }

        public StateMachine Machine;

        private void buttonQueryEx_Click(object sender, EventArgs e)
        {
            IEnumerable<IElement> element = null;
            switch (comboBoxExType.Text)
            {
                case "DI":
                    element = Machine.DiExs.Values.ToList();
                    break;
                case "DO":
                    element = Machine.DoExs.Values.ToList();
                    break;
                case "VIO":
                    element = Machine.VioExs.Values.ToList();
                    break;
                case "CY":
                    element = Machine.CylinderExs.Values.ToList();
                    break;
                case "PLATFORM":
                    element = Machine.Platforms.Values.ToList();
                    break;
                case "STATION":
                    element = Machine.Stations.Values.ToList();
                    break;
                case "TASK":
                    element = Machine.Tasks.Values.ToList();
                    break;
                default:
                    return;
            }


            var ele = new ElePropsForm();
            ele.Element = element;
            ele.ShowDialog();
        }

        private void buttonQueryExObj_Click(object sender, EventArgs e)
        {
            object element = null;
            switch (comboBoxExType.Text)
            {
                case "DI":
                    element = Machine.Find<IDiEx>(comboBoxExObj.Text);
                    break;
                case "DO":
                    element = Machine.Find<IDoEx>(comboBoxExObj.Text);
                    break;
                case "VIO":
                    element = Machine.Find<IVioEx>(comboBoxExObj.Text);
                    break;
                case "CY":
                    element = Machine.Find<ICylinderEx>(comboBoxExObj.Text);
                    break;
                case "PLATFORM":
                    element = Machine.Find<PlatformEx>(comboBoxExObj.Text);
                    break;
                case "STATION":
                    element = Machine.Find<Station>(comboBoxExObj.Text);
                    break;
                case "TASK":
                    element = Machine.Find<StationTask>(comboBoxExObj.Text);
                    break;
                default:
                    return;
            }


            var ele = new ElePropsForm();
            ele.Element = element;
            ele.ShowDialog();


        }

        private void comboBoxExType_SelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            object[] element = null;
            switch (comboBoxExType.Text)
            {
                case "DI":
                    element = Machine.DiExs.Values.Select(e => (object)e.Name).ToArray();
                    break;
                case "DO":
                    element = Machine.DoExs.Values.Select(e => (object)e.Name).ToArray();
                    break;
                case "VIO":
                    element = Machine.VioExs.Values.Select(e => (object)e.Name).ToArray();
                    break;
                case "CY":
                    element = Machine.CylinderExs.Values.Select(e => (object)e.Name).ToArray();
                    break;
                case "PLATFORM":
                    element = Machine.Platforms.Values.Select(e => (object)e.Name).ToArray();
                    break;
                case "STATION":
                    element = Machine.Stations.Values.Select(e => (object)e.Name).ToArray();
                    break;
                case "TASK":
                    element = Machine.Tasks.Values.Select(e => (object)e.Name).ToArray();
                    break;
                default:
                    return;
            }

            if (element != null)
            {
                comboBoxExObj.Items.Clear();
                comboBoxExObj.Items.AddRange(element);
            }

        }
    }
}
