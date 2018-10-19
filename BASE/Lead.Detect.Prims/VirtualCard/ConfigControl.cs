using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Detect.PrimVirtualCard
{
    public partial class ConfigControl : UserControl
    {
        public ConfigControl()
        {
            InitializeComponent();
        }

        public ConfigControl(VirtualCard virtualCard)
        {
            InitializeComponent();


            propertyGrid1.SelectedObject = virtualCard;
        }
    }
}