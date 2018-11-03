using System;
using System.Windows.Forms;

namespace Lead.Detect.Element.EleControl
{
    public partial class ElePropsForm : Form
    {
        public ElePropsForm()
        {
            InitializeComponent();
        }

        private void ElePropsForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterParent;

            if (Element != null)
            {
                Text = Element.ToString();
                propertyGrid1.SelectedObject = Element;
                propertyGrid1.ExpandAllGridItems();
            }
        }

        private void buttonAPPLY_Click(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
            Element = propertyGrid1.SelectedObject;
            DialogResult = DialogResult.OK;
            this.Close();
        }


        public object Element;
    }
}