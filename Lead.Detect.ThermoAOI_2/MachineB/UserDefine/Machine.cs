using System;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.scriptTask;
using Lead.Detect.MeasureComponents.LMILaser;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine
{
    /// <summary>
    /// 设备定义
    /// </summary>
    public class Machine : StateMachine
    {
        private Machine()
        {
        }

        public static Machine Ins { get; } = new Machine();

        /// <summary>
        /// 设备配置
        /// </summary>
        public MachineSettings Settings { get; set; }

        public override void Load()
        {
            //load all settings!!!
            Settings = MachineSettings.Load();
            if (Settings == null)
            {
                throw new Exception("Load MachineSettings Fail!");
            }

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

            foreach (var p in Tasks.Values)
            {
                if (p is ScriptStationTask)
                {
                    (p as ScriptStationTask)?.Load();
                }
            }
        }


        /// <summary>
        /// 保存配置文件
        /// </summary>
        public override void Save()
        {
            //save platform positions
            foreach (var p in Platforms.Values)
            {
                p.Save();
            }

            foreach (var p in Tasks.Values)
            {
                if (p is ScriptStationTask)
                {
                    (p as ScriptStationTask)?.Save();
                }
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


        public override void Initialize()
        {
            try
            {
                //初始化驱动
                Find<IMotionWrapper>("M1").Init(string.Empty);
                Find<IMotionWrapper>("IO1").Init(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化控制卡失败:{ex.Message}");
                throw ex;
            }

            //初始化关键io
            Find<IDoEx>("DOLamp")?.SetDo(true);

            LmiLaser.Init();

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

            LmiLaser.Uninit();

            //终止关键io


            Find<IDoEx>("DOLamp")?.SetDo(false);

            //终止驱动
            Find<IMotionWrapper>("M1").Uninit();
            Find<IMotionWrapper>("IO1").Uninit();
        }

    }
}
