using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.Base
{
    public class DevPrimsFactoryManager
    {
        private DevPrimsFactoryManager()
        {
        }

        public static DevPrimsFactoryManager Instance { get; } = new DevPrimsFactoryManager();


        public Dictionary<string, IPrimCreator> PrimCreators { get; } = new Dictionary<string, IPrimCreator>();

        /// <summary>
        ///     加载所有组件工厂类
        /// </summary>
        /// <param name="primsFolder"></param>
        /// <returns></returns>
        public int LoadFromFolder(string primsFolder)
        {
            if (!Directory.Exists(primsFolder))
            {
                throw new Exception("prim dir error");
            }

            var dir = new DirectoryInfo(primsFolder);
            var files = dir.GetFileSystemInfos("*.dll").ToList().FindAll(f => f.Name.Contains("Prim")).ToArray();
            foreach (var file in files)
            {
                if (file != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(file.FullName))
                        {
                            var assembly = Assembly.LoadFrom(file.FullName);
                            var types = assembly.GetExportedTypes();
                            foreach (var type in types)
                                if (type.IsClass && type.GetInterface(nameof(IPrimCreator)) != null)
                                {
                                    var primCreator = (IPrimCreator)Activator.CreateInstance(type);
                                    if (primCreator != null)
                                    {
                                        PrimCreators.Add(primCreator.PrimProps.Name, primCreator);
                                    }
                                    break;
                                }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("DevPrimsFactoryManager:" + "Get Dev Instance Fail -" + e);
                    }
                }
            }

            return 0;
        }


        public IPrimCreator FindCreator(string primTypeName)
        {
            if (PrimCreators.ContainsKey(primTypeName))
            {
                return PrimCreators[primTypeName];
            }
            return null;
        }


        public IPrim InvokeCreator(string primTypeName)
        {
            try
            {
                return FindCreator(primTypeName)?.Create();
            }
            catch (Exception)
            {
            }

            return null;
        }

        public IPrim InvokeCreator(string primTypeName, XmlNode primConfig)
        {
            try
            {
                var primCreator = FindCreator(primTypeName);
                return primCreator?.Create(primConfig);
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}