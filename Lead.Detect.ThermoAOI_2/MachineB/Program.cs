using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.ThermoAOI2.MachineB.UserDefine;

namespace Lead.Detect.ThermoAOI2.MachineB
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            Mutex mutex = new Mutex(false, nameof(MachineB), out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("系统中已有一个相同的程序在运行，请确认！");
                mutex.Dispose();
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) => { FrameworkExtenion.ShowMsgError($"ThreadException: {args.Exception.Message}"); };


            //initializing
            try
            {
                GC.Collect();

                //simulate mode enable
                FrameworkExtenion.IsDebugFramework = false;
                FrameworkExtenion.IsSimulate = false;
                if (FrameworkExtenion.IsSimulate)
                {
                    if (MessageBox.Show("仿真模式？", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Environment.Exit(1);
                        return;
                    }
                }


                //reflection types load
                {
                    //load prims types
                    EnvironmentManager.Ins.Save(@".\Config\environment.cfg");
                    EnvironmentManager.Ins.Initialize();
                    EnvironmentManager.Ins.LoadPrims(FrameworkExtenion.IsSimulate ? @".\Config\devsim.dev" : @".\Config\default.dev");

                    //load task types to frameworks
                    FrameworkUserTypeManager.LoadUserTaskTypes(Assembly.GetExecutingAssembly());
                }


                //SqlLiteHelper.InitDatabase();

                //initialize machine settings
                Machine.Ins.Load();
                //init drivers , start scan
                Machine.Ins.Initialize();
            }
            catch (Exception ex)
            {
                FrameworkExtenion.ShowMsgError($"FRAMEWORK EXCEPTION: 加载失败 {ex.Message}");
                EnvironmentManager.Ins.Terminate();
                return;
            }


            //run main form
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                FrameworkExtenion.ShowMsgError($"ApplicationRun EXCEPTION: {ex.Message}");
            }
            finally
            {
                //terminate
                try
                {
                    Machine.Ins.Terminate();
                    EnvironmentManager.Ins.Terminate();
                }
                catch (Exception ex)
                {
                    FrameworkExtenion.ShowMsgError($"FRAMEWORK EXCEPTION: 终止失败 {ex.Message}");
                }

                //save settings
                try
                {
                    Machine.Ins.Save();
                    EnvironmentManager.Ins.SavePrims(FrameworkExtenion.IsSimulate ? @".\Config\devsim.dev" : @".\Config\default.dev");
                }
                catch (Exception ex)
                {
                    FrameworkExtenion.ShowMsgError($"FRAMEWORK EXCEPTION: 参数保存失败 {ex.Message}");
                }
                finally
                {
                    mutex.Dispose();
                }
            }
        }
    }
}