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

        #region common

        [Category("BARCODE"), Description("条码使能")]
        public bool BarcodeEnable { get; set; }
        [Category("BARCODE"), Description("条码")]
        public string BarcodePattern { get; set; }
        [Category("BARCODE"), Description("条码长度")]
        public int BarcodeLength { get; set; }


        [Category("COMMON"), Description("传感器使能")]
        public bool SensorEnable { get; set; }
        [Category("COMMON"), Description("自动测试")]
        public bool AutoDryRun { get; set; }


        [Category("MEASURE"), Description("测试文件路径")]
        public string MeasureProjectFile { get; set; }

        [Category("MEASURE"), Description("测试异常则退出")]
        public bool QuitOnProductError { get; set; }



        [Category("MEASURE"), Description("参考坐标模式")]
        public bool EnableRelCoordMode { get; set; } = false;

        #endregion


        #region components

        [Category("COMPONENTS"), Description("相机")]
        public string CameraIP { get; set; } = "127.0.0.1";
        [Category("COMPONENTS"), Description("相机")]
        public int CameraPort { get; set; } = 50000;


        [Category("COMPONENTS"), Description("上激光")]
        public string Laser1IP { get; set; } = "192.168.1.10";
        [Category("COMPONENTS"), Description("上激光")]
        public string Laser1AcceleratorIp { get; set; } = "192.168.1.111";
        [Category("COMPONENTS"), Description("上激光")]
        public int Laser1Port { get; set; }


        [Category("COMPONENTS"), Description("下激光")]
        public string Laser2IP { get; set; } = "192.168.2.10";
        [Category("COMPONENTS"), Description("下激光")]
        public string Laser2AcceleratorIp { get; set; } = "192.168.2.111";
        [Category("COMPONENTS"), Description("下激光")]
        public int Laser2Port { get; set; }

        [Category("COMPONENTS"), Description("激光")]
        public bool EnableSaveRec { get; set; }
        [Category("COMPONENTS"), Description("激光")]
        public string RecFolder { get; set; } = @"..\..\..";

        [Category("COMPONENTS"), Description("激光")]
        public bool EnableLaserAccelerator { get; set; }




        #endregion

        #region laser result config


        //[Category("激光测量数据"), Description("上激光")]
        //public int UpLaserRowCount { get; set; } = 8;
        //[Category("激光测量数据"), Description("上激光")]
        //public int UpLaserColumnCount { get; set; } = 25;


        //[Category("激光测量数据"), Description("下激光")]
        //public int DownLaserRowCount { get; set; } = 4;
        //[Category("激光测量数据"), Description("下激光")]
        //public int DownLaserColumnCount { get; set; } = 25;

        #endregion



        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}