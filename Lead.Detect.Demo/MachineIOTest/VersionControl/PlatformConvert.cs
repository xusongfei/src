using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.MachineIOTest.VersionControl
{
    public class PlatformConvert
    {
        public static void ConvertPts(string folder)
        {
            var ptsFiles = Directory.GetFiles(folder).ToList().FindAll(f => f.EndsWith(".pts"));

            foreach (var ptsFile in ptsFiles)
            {
                try
                {
                    var platform = Path.GetFileNameWithoutExtension(ptsFile);

                    //export once
                    if (!File.Exists(Path.Combine(folder, $"{platform}.dat")))
                    {
                        var ptsContext = File.ReadAllText(ptsFile, Encoding.UTF8);
                        ptsContext = ptsContext.Replace(" xsi:type=\"PosXYZ\"", string.Empty);

                        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ptsContext)))
                        {
                            var pos = new XmlSerializer(typeof(List<PlatformPos>)).Deserialize(ms) as List<PlatformPos>;


                            //export to new format
                            using (var f = new FileStream(Path.Combine(folder, $"{platform}.dat"), FileMode.Create))
                            {
                                using (var sw = new StreamWriter(f, Encoding.UTF8))
                                {
                                    sw.WriteLine(platform);
                                    if (pos != null)
                                        foreach (var p in pos)
                                        {
                                            sw.WriteLine(p.ToString());
                                        }
                                }
                            }
                        }
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