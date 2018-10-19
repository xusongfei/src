using System;
using System.ComponentModel;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.common;
using MachineUtilityLib.UtilControls;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine
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
        public string Name { set; get; } = "MACHINE B";

        [Category("Machine")]
        public string Version { get; set; } = "v0.0.1";
        [Category("Machine")]
        public ProductionCount Production { get; set; } = new ProductionCount();


        [Category("BARCODE"), Description("条码使能")]
        public bool BarcodeEnable { get; set; }
        [Category("BARCODE"), Description("条码")]
        public string BarcodePattern { get; set; }




        [Category("COMMON"), Description("传感器使能")]
        public bool SensorEnable { get; set; }
        [Category("COMMON"), Description("自动测试")]
        public bool AutoDryRun { get; set; }




        [Category("MEASURE"), Description("测试文件路径")]
        public string MeasureProjectFile { get; set; }



        [Category("COMPONENTS"), Description("相机")]
        public string CameraIP { get; set; }
        [Category("COMPONENTS"), Description("相机")]
        public string CameraPort { get; set; }


        [Category("COMPONENTS"), Description("上激光")]
        public string Laser1IP { get; set; }
        [Category("COMPONENTS"), Description("上激光")]
        public string Laser1Port { get; set; }
        [Category("COMPONENTS"), Description("下激光")]
        public string Laser2IP { get; set; }
        [Category("COMPONENTS"), Description("下激光")]
        public string Laser2Port { get; set; }




        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}