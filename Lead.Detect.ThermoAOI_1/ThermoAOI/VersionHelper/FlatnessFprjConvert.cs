using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Product;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Thermo1;

namespace Lead.Detect.ThermoAOI.VersionHelper
{
    public class FlatnessFprjConvert
    {
        public static void ConvertFprj(string folder = @".\Config")
        {
            foreach (var directory in Directory.GetDirectories(folder))
            {
                ConvertFprj(directory);
            }


            var fprjFiles = Directory.GetFiles(folder).ToList().FindAll(f => f.EndsWith(".fprj"));
            foreach (var f in fprjFiles)
            {
                var mprjFileName = f.Replace(".fprj", ".mprj");
                if (File.Exists(mprjFileName))
                {
                    continue;
                }

                try
                {

                    var fprj = FlatnessProject.Load(f);
                    if (fprj != null)
                    {
                        var mprj = fprj.Convert();

                        mprj.Save(mprjFileName);
                    }
                }
                catch (Exception)
                {
                    continue;
                }

            }

        }
    }
}
