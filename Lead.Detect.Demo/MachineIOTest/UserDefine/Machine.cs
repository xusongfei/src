using System.IO;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MachineIOTest.VersionControl;


namespace Lead.Detect.MachineIOTest.UserDefine
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


        public string MachineFolder;
        public string MachineCfgFile;

        /// <summary>
        /// 从配置文件加载
        /// </summary>
        public override void Load()
        {
            //import machine objects
            Import(MachineCfgFile);


            PlatformConvert.ConvertPts(MachineFolder);

            //load platform positions
            foreach (var p in Platforms)
            {
                p.Value.Load(Path.Combine(MachineFolder, $"{p.Value.Name}.dat"));
            }
        }


        /// <summary>
        /// 保存配置文件
        /// </summary>
        public override void Save()
        {
            //save platform positions
            foreach (var p in Platforms)
            {
                p.Value.Save(Path.Combine(MachineFolder, $"{p.Value.Name}.dat"));
            }

            //export machine objects
            Export(MachineCfgFile);
        }

        /// <summary>
        /// 初始化程序，开启main线程
        /// </summary>
        public override void Initialize()
        {
            foreach (var m in MotionExs)
            {
                m.Value.Init(string.Empty);
            }

            //启动 main thread
            //base.Initialize();
        }


        /// <summary>
        /// 终止程序，终止main线程
        /// </summary>
        public override void Terminate()
        {
            //终止 main thread
            //base.Terminate();

            foreach (var m in MotionExs)
            {
                m.Value.Uninit();
            }
        }

        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}