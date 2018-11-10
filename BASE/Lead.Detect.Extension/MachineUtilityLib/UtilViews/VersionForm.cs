using System;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace Lead.Detect.MachineUtilityLib.UtilViews
{
    public partial class VersionForm : DockContent
    {
        public VersionForm()
        {
            InitializeComponent();
        }

        private void VersionForm_Load(object sender, EventArgs e)
        {
            var assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();

            listBox1.Items.Clear();

            var a = Assembly.GetEntryAssembly().GetName();
            listBox1.Items.Add($"{a.Name}: {a.Version}");
            foreach (var ass in assemblies)
            {
                if (ass.Name.StartsWith("Lead"))
                {
                    listBox1.Items.Add($"{ass.Name}: {ass.Version}");
                }
            }
        }
    }
}