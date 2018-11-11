using System;
using System.ComponentModel;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks;
using Lead.Detect.ThermoAOIProductLib.ProductBase;
using Lead.Detect.ThermoAOIProductLib.ThermoDataConvert;

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
        public string Description { set; get; } = "AOI3";
        [Category("Machine")]
        public string Version { get; set; } = "v0.0.1";
        [Category("Machine")]
        public ProductionCount Production { get; set; } = new ProductionCount();


        [Category("数据上传设置")]
        public DataUploaderSetting Uploader { get; set; } = new DataUploaderSetting();


        [Category("激光计算模式")]
        public LaserCalculateMode LaserMode { get; set; } = LaserCalculateMode.FlatnessToFitPlane;

        #region barcode

        [Category("BARCODE"), Description("条码使能")]
        public bool BarcodeEnable { get; set; }
        [Category("BARCODE"), Description("条码")]
        public string BarcodePattern { get; set; }
        [Category("BARCODE"), Description("条码长度")]
        public int BarcodeLength { get; set; }

        #endregion

        #region common
        [Category("SENSOR"), Description("传感器使能")]
        public bool SensorEnable { get; set; }


        [Category("COMMON"), Description("自动测试")]
        public bool AutoDryRun { get; set; }
        [Category("COMMON"), Description("测试异常则退出")]
        public bool QuitOnProductError { get; set; }
        [Category("COMMON"), Description("错误则响蜂鸣器")]
        public bool BeepOnProductError { get; set; } = true;



        [Category("MEASURE"), Description("参考坐标模式")]
        public bool EnableRelCoordMode { get; set; } = false;


        [Category("MEASURE"), Description("测试文件路径")]
        public string MeasureProjectFile { get; set; }


        #endregion


        #region components


        [Category("CAMERA"), Description("相机")]
        public string CameraIP { get; set; } = "127.0.0.1";
        [Category("CAMERA"), Description("相机")]
        public int CameraPort { get; set; } = 50000;


        [Category("LASER UP"), Description("上激光")]
        public string Laser1IP { get; set; } = "192.168.1.10";
        [Category("LASER UP"), Description("上激光")]
        public string Laser1AcceleratorIp { get; set; } = "192.168.1.111";


        [Category("LASER DOWN"), Description("下激光")]
        public string Laser2IP { get; set; } = "192.168.2.10";
        [Category("LASER DOWN"), Description("下激光")]
        public string Laser2AcceleratorIp { get; set; } = "192.168.2.111";


        [Category("LASER"), Description("激光")]
        public bool EnableLaserAccelerator { get; set; }
        [Category("LASER"), Description("激光")]
        public bool EnableSaveRec { get; set; }
        [Category("LASER"), Description("激光")]
        public string RecFolder { get; set; } = @"..\..\..";


        #endregion


        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}