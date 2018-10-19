using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.MachineIOTest.UserDefine;

namespace Lead.Detect.MachineIOTest
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
            Mutex mutex = new Mutex(false, nameof(MachineIOTest), out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("系统中已有一个相同的程序在运行，请确认！");
                mutex.Dispose();
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show($"MAIN EXCEPTION: {args.Exception.Message}");
            };

            try
            {
                GC.Collect();

                //simulate mode enable
                FrameworkExtenion.IsDebugFramework = true;
                FrameworkExtenion.IsSimulate = true;

                //reflection types load
                {
                    //load prims types
                    EnvironmentManager.Ins.Load(@".\Config\environment.cfg");
                    EnvironmentManager.Ins.Initialize();
                    EnvironmentManager.Ins.LoadPrims(EnvironmentManager.Ins.LastDevProject);

                    //load task types to frameworks
                    FrameworkUserTypeManager.LoadUserTaskTypes(Assembly.GetExecutingAssembly());
                }
                {
                    var devFile = Path.GetFileName(EnvironmentManager.Ins.LastDevProject);
                    var devFolder = Path.GetDirectoryName(EnvironmentManager.Ins.LastDevProject);

                    Machine.Ins.MachineCfgFile = devFile == "devsim.dev" ? $"{devFolder}\\machinesim.cfg" : $"{devFolder}\\machine.cfg";
                    Machine.Ins.MachineFolder = devFolder;

                    //initialize machine settings
                    Machine.Ins.Load();
                    //init drivers , start scan
                    Machine.Ins.Initialize();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"FRAMEWORK EXCEPTION: 加载失败 {ex.Message}", FrameworkExtenion.FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                Application.Run(new MachineForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MAIN EXCEPTION: {ex.Message}", FrameworkExtenion.FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    Machine.Ins.Terminate();
                    Machine.Ins.Save();

                    EnvironmentManager.Ins.SavePrims(EnvironmentManager.Ins.LastDevProject);
                    EnvironmentManager.Ins.Save(@".\Config\environment.cfg");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"FRAMEWORK EXCEPTION: 程序终止失败 {ex.Message}", FrameworkExtenion.FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                mutex.Dispose();
            }

            if (EnvironmentManager.Ins.Reboot)
            {
                System.Diagnostics.Process.Start(@".\Lead.Detect.MachineIOTest.exe");
            }
        }
    }
}
