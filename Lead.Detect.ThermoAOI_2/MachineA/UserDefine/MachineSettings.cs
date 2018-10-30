using System;
using System.ComponentModel;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.common;
using MachineUtilityLib.UtilControls;

namespace Lead.Detect.ThermoAOI2.MachineA.UserDefine
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
        public string Name { set; get; } = "MACHINE A";

        [Category("Machine")]
        public string Version { get; set; } = "v0.0.1";

        [Category("Machine")]
        public ProductionCount Production { get; set; } = new ProductionCount();




        [Category("COMMON"), Description("传感器使能")]
        public bool SensorEnable { get; set; }
        [Category("COMMON"), Description("自动测试")]
        public bool AutoDryRun { get; set; }




        [Category("MEASURE"), Description("测试文件路径")]
        public string MeasureProjectFile { get; set; }

        [Category("MEASURE"), Description("拍照错误则退出测量")]

        public bool QuitOnProductError { get; set; } = false;

        [Category("MEASURE"), Description("参考坐标模式")]
        public bool EnableRelCoordMode { get; set; } = false;



        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}