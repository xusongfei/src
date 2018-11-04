﻿using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Lead.Detect.DatabaseHelper;
using Lead.Detect.FrameworkExtension.frameworkManage;

namespace Lead.Detect.ThermoAOI.Machine1
{
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            Mutex mutex = new Mutex(false, nameof(ThermoAOI), out createdNew);
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
                //throw new Exception("Thread Exception");
            };

            try
            {
                GC.Collect();

                //simulate mode enable
                FrameworkExtenion.IsDebugFramework = false;
                FrameworkExtenion.IsSimulate = false;
                if (FrameworkExtenion.IsSimulate)
                {
                    if (MessageBox.Show("进入仿真模式？", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        Environment.Exit(1);
                        return;
                    }
                }


                //reflection types load
                {
                    //load prims types
                    EnvironmentManager.Ins.Initialize();
                    if (FrameworkExtenion.IsSimulate)
                    {
                        EnvironmentManager.Ins.LoadPrims(@".\Config\devsim.dev");
                    }
                    else
                    {
                        EnvironmentManager.Ins.LoadPrims(@".\Config\default.dev");
                    }

                    //load task types to frameworks
                    FrameworkUserTypeManager.LoadUserTaskTypes(Assembly.GetExecutingAssembly());
                }


                SqlLiteHelper.InitDatabase();

                //initialize machine settings
                Machine.Machine.Ins.Load();

                //init drivers , start scan
                Machine.Machine.Ins.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"FRAMEWORK EXCEPTION: 加载失败 {ex.Message}", FrameworkExtenion.FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MAIN EXCEPTION: {ex.Message}", FrameworkExtenion.FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    Machine.Machine.Ins.Terminate();

                    Machine.Machine.Ins.Save();


                    EnvironmentManager.Ins.SavePrims(FrameworkExtenion.IsSimulate ? @".\Config\devsim.dev" : @".\Config\default.dev");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"FRAMEWORK EXCEPTION: 程序终止失败 {ex.Message}", FrameworkExtenion.FrameworkExceptionHead, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                mutex.Dispose();
            }
        }
    }
}