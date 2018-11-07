using System;
using System.IO;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.ThermoAOI.Machine1.VersionHelper;
using Lead.Detect.ThermoAOIProductLib.Thermo;
using Lead.Detect.ThermoAOIProductLib.Thermo1;
using Lead.Detect.ThermoAOIProductLib.Thermo1Calculator;

namespace Lead.Detect.ThermoAOI.Machine1.Machine
{
    /// <summary>
    /// 设备定义
    /// </summary>
    ///
    public class Machine : StateMachine
    {
        #region singleton
        private Machine()
        {
        }
        public static Machine Ins { get; } = new Machine();
        #endregion


        /// <summary>
        /// 设备配置
        /// </summary>
        public MachineSettings Settings { get; set; }

        /// <summary>
        /// 从配置文件加载
        /// </summary>
        public override void Load()
        {
            //load all settings!!!
            Settings = MachineSettings.Load();
            if (Settings == null)
            {
                throw new Exception("Load MachineSettings Fail!");
            }

            //转换点位
            //PlatformConvert.ConvertPts();
            //转换测试文件
            FlatnessFprjConvert.ConvertFprj();

            //import machine objects
            if (FrameworkExtenion.IsSimulate)
            {
                Import(@".\Config\machinesim.cfg");
            }
            else
            {
                Import(@".\Config\machine.cfg");
            }

            //load platform positions
            foreach (var p in Platforms)
            {
                p.Value.Load();
            }


            //thermo aoi 测试文件加载
            try
            {
                Thermo1CalculatorMgr.Ins.Export();
                Thermo1CalculatorMgr.Ins.Import();

                var lfprj = MeasureProject.Load(Settings.LeftProjectFilePath, typeof(MeasureProject1));
                var rfprj = MeasureProject.Load(Settings.RightProjectFilePath, typeof(MeasureProject1));
            }
            catch (Exception)
            {
                MessageBox.Show($"未能加载产品测试文件！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// 保存配置文件
        /// </summary>
        public override void Save()
        {
            //save platform positions
            if(File.Exists(@".\Config\platforms.pts"))
            {
                File.Delete(@".\Config\platforms.pts");
            }

            foreach (var p in Platforms.Values)
            {
                p.Save();
                p.SavePts(@".\Config\platforms.pts");
            }

            //save the machine settings
            Settings.Save();

            //export machine objects
            if (FrameworkExtenion.IsSimulate)
            {
                Export(@".\Config\machinesim.cfg");
            }
            else
            {
                Export(@".\Config\machine.cfg");
            }
        }

        /// <summary>
        /// 初始化程序，开启main线程
        /// </summary>
        public override void Initialize()
        {
            try
            {
                //初始化驱动
                Find<IMotionWrapper>("M1").Init(string.Empty);
                Find<IMotionWrapper>("M2").Init(string.Empty);
                Find<IMotionWrapper>("IO1").Init(string.Empty);
                Find<IMotionWrapper>("IO2").Init(string.Empty);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化控制卡失败:{ex.Message}");
                throw ex;
            }

            //初始化关键io
            Find<IDoEx>("LDOBrakeZPress").SetDo(true);
            Find<IDoEx>("LDOBtnLight1").SetDo(false);
            Find<IDoEx>("LDOBtnLight1").SetDo(false);
            Find<IDoEx>("RDOBrakeZPress").SetDo(true);
            Find<IDoEx>("RDOBtnLight1").SetDo(false);
            Find<IDoEx>("RDOBtnLight1").SetDo(false);

            Find<IDoEx>("DoLamp").SetDo(true);

            //启动 main thread
            base.Initialize();
        }


        /// <summary>
        /// 终止程序，终止main线程
        /// </summary>
        public override void Terminate()
        {
            //终止 main thread
            base.Terminate();

            //终止关键io
            Find<IDoEx>("LDOBrakeZPress").SetDo(false);
            Find<IDoEx>("LDOBtnLight1").SetDo(false);
            Find<IDoEx>("LDOBtnLight1").SetDo(false);
            Find<IDoEx>("RDOBrakeZPress").SetDo(false);
            Find<IDoEx>("RDOBtnLight1").SetDo(false);
            Find<IDoEx>("RDOBtnLight1").SetDo(false); ;

            Find<IDoEx>("DoLamp").SetDo(false);

            //终止驱动
            Find<IMotionWrapper>("M1").Uninit();
            Find<IMotionWrapper>("M2").Uninit();
            Find<IMotionWrapper>("IO1").Uninit();
            Find<IMotionWrapper>("IO2").Uninit();
        }

        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}