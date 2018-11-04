using System;
using System.ComponentModel;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.ThermoAOI.Machine1.Calibration;
using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOI.Machine1.Machine
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MachineSettings : UserSettings<MachineSettings>
    {
        public MachineSettings()
        {
        }

        public static MachineSettings Load()
        {
            MachineSettings settings = null;
            try
            {
                settings = Load(@".\Config\Settings.cfg");
            }
            catch (Exception)
            {
            }

            if (settings == null)
            {
                if (MessageBox.Show("Load Default MachineSetting?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    settings = new MachineSettings();
                }
            }

            return settings;
        }

        public void Save()
        {
            Save(@".\Config\Settings.cfg");
        }


        [Category("Machine")]
        public string Name { set; get; } = "高度通用量测";
        [Category("Machine")]
        public string Description { set; get; } = "AOI1";
    
        [Category("Machine")]
        public bool EnableFTP { get; set; } = false;
        [Category("Machine")]
        public string FTPAddress { set; get; } = @"192.168.80.10\TestData01";



        [Category("ProjectFile")]
        public string LeftProjectFilePath { get; set; } = @".\Config\left.fprj";

        [Category("ProjectFile")]
        public string RightProjectFilePath { get; set; } = @".\Config\right.fprj";


        [Category("Production")]
        [ReadOnly(true)]
        public ProductionCount ProductionLeft { get; set; } = new ProductionCount();

        [Category("Production")]
        [ReadOnly(true)]
        public ProductionCount ProductionRight { get; set; } = new ProductionCount();

        [Category("Settings")]
        public CommonConfig Common { get; set; } = new CommonConfig();

        [Category("Settings")]
        public CalibrationConfig Calibration { get; set; } = new CalibrationConfig();

        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}