using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lead.Detect.Base;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.FrameworkExtension.common;

namespace Lead.Detect.FrameworkExtension.frameworkManage
{
    /// <summary>
    /// DevPrimsManager的封装
    /// </summary>
    public class EnvironmentManager : UserSettings<EnvironmentManager>
    {
        //prims folder
        public string DefaultPrimsFolder = @"..\..\..\..\binPrims";
        //public string DefaultPrimsFolder = @".\VisioPrims";

        /// <summary>
        /// multi dev support
        /// </summary>
        public string LastDevProject = @".\Config\default.dev";


        public string NewBrowseDevProject = @".\Config\default.dev";

        public bool Reboot = false;

        #region singleton

        private EnvironmentManager()
        {
        }

        public static EnvironmentManager Ins { get; private set; } = new EnvironmentManager();

        #endregion


        private FrameworkDebugForm _debugForm = new FrameworkDebugForm();

        /// <summary>
        /// 初始化PrimsFactory
        /// </summary>
        public void Initialize()
        {
            if (FrameworkExtenion.IsDebugFramework)
            {
                _debugForm?.Show();
            }

            DebugFramework("LoadFromFolder:" + DefaultPrimsFolder);
            DevPrimsFactoryManager.Instance.LoadFromFolder(DefaultPrimsFolder);
        }


        public void Terminate()
        {
            if (FrameworkExtenion.IsDebugFramework)
            {
                //MessageBox.Show("将关闭FrameworkDebug窗口", "FRAMEWORK EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _debugForm?.Hide();
                _debugForm?.Close();
            }
        }


        public new void Load(string env)
        {
            if (File.Exists(env))
            {
                var ins = UserSettings<EnvironmentManager>.Load(env);
                if (ins != null)
                {
                    Ins = ins;
                }
            }
        }

        /// <summary>
        /// 从 devproject 配置文件加载 Prims 到 DevPrimsManager
        /// </summary>
        /// <param name="dev"></param>
        public void LoadPrims(string dev)
        {
            LastDevProject = dev;
            NewBrowseDevProject = LastDevProject;
            Reboot = false;


            LoadPrims(DevPrimsManager.Instance.Prims, dev);

            if (FrameworkExtenion.IsDebugFramework)
            {
                DebugFramework($"加载设备定义的Prims:\n{string.Join("\n", DevPrimsManager.Instance.Prims.Select(p => $"{p.Name} {p.PrimTypeName}"))}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prims"></param>
        /// <param name="dev"></param>
        public void LoadPrims(List<IPrim> prims, string dev)
        {
            //load project
            var devProject = DevProject.Load(dev);
            if (devProject != null)
            {
                //load prims
                prims.Clear();
                foreach (var primConfig in devProject.Prims)
                {
                    var prim = DevPrimsFactoryManager.Instance.InvokeCreator(primConfig.VisioPrimType, primConfig.PrimXmlNode);
                    if (prim == null)
                    {
                        var msg = $"Create Prim {primConfig.VisioPrimType} Fail: {DevPrimsFactoryManager.Instance.PrimCreators.ContainsKey(primConfig.VisioPrimType)}";
                        DebugFramework(msg);
                        throw new Exception(msg);
                    }
                    else
                    {
                        DebugFramework($"Load Prim: {primConfig.VisioPrimType} {primConfig.PrimXmlNode.ToString()}");
                    }

                    prims.Add(prim);
                }
            }
        }


        /// <summary>
        ///  保存 DevPrimsManager 的 Prims 到 devproject 配置文件
        /// </summary>
        /// <param name="dev"></param>
        public void SavePrims(string dev)
        {
            LastDevProject = Path.GetFullPath(dev);
            SavePrims(DevPrimsManager.Instance.Prims, dev);


            if (NewBrowseDevProject != LastDevProject)
            {
                LastDevProject = NewBrowseDevProject;
                Reboot = true;
            }
            else
            {
                Reboot = false;
            }
        }

        public void SavePrims(List<IPrim> prims, string dev)
        {
            //save project
            var devProject = new DevProject()
            {
                ProjectName = Path.GetFileName(dev),
            };
            foreach (var prim in prims)
            {
                var xmlNode1 = prim.ExportConfig();
                devProject.Prims.Add(new PrimConfig
                {
                    VisioPrimType = prim.GetType().Name,
                    PrimXmlNode = xmlNode1
                });
            }

            devProject.Save(dev);
        }


        public override bool CheckIfNormal()
        {
            return true;
        }

        public void ShowPrimsEditor()
        {
            var deviceEditor = new DevPrimsEditorForm()
            {
                DevPrims = DevPrimsManager.Instance.Prims,
            };
            deviceEditor.ShowDialog();
        }

        public void DebugFramework(string log)
        {
            if (!FrameworkExtenion.IsDebugFramework)
            {
                return;
            }

            if (_debugForm != null)
            {
                _debugForm.UpdateLog(log);
            }
        }
    }
}