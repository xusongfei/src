using System;
using System.IO;
using Lead.Detect.Helper;

namespace Lead.Detect.FrameworkExtension.common
{
    public class LoggerHelper
    {

        private static object _lock = new object();

        public static void Log(string dir, string log, LogLevel level)
        {
            lock (_lock)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                try
                {
                    using (var fs = new FileStream(Path.Combine(dir, $"{DateTime.Now.ToString("yyyyMMdd")}.log"), FileMode.Append))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.WriteLine($"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}:[{level}]:{log}");
                        }
                    }
                }
                catch (Exception )
                {

                }

            }
        }

    }
}
