using System;
using System.ComponentModel;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.ThermoDataConvert;

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
        public string Description { set; get; } = "AOI2";


        [ReadOnly(true)]
        [Category("Machine")]
        public string Version { get; set; } = "v0.0.1";
        [ReadOnly(true)]
        [Category("Machine")]
        public ProductionCount Production { get; set; } = new ProductionCount();


        public DataUploaderSetting Uploader { get; set; } = new DataUploaderSetting();



        [Category("SENSOR"), Description("FIN 传感器使能")]
        public bool FinSensorEnable { get; set; }
        [Category("SENSOR"), Description("PRD 定位传感器使能")]
        public bool SensorEnable { get; set; }



        [Category("MISC"), Description("自动测试")]
        public bool AutoDryRun { get; set; }

        [Category("MISC"), Description("自动循环测试")]
        public bool AutoTestRun { get; set; }





        [Category("AUTORUN"), Description("参考坐标模式")]
        public bool EnableRelCoordMode { get; set; } = false;

        [Category("AUTORUN"), Description("拍照延时")]
        public int CaptureDelay { get; set; } = 1000;



        [Category("AUTORUN"), Description("拍照错误则退出测量")]
        public bool QuitOnProductError { get; set; } = false;
        [Category("AUTORUN"), Description("错误则响蜂鸣器")]
        public bool BeepOnProductError { get; set; } = true;

        [Category("MEASURE"), Description("测试文件路径")]
        public string MeasureProjectFile { get; set; }



        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}