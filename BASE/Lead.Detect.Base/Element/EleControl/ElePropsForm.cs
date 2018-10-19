using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.Element
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