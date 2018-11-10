using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOIProductLib.ThermoDataConvert
{
    public class AvcDataUploadHelper : DataUploadHelper
    {
        public override void Upload(ICsvData data)
        {
            //todo test avc upload logic
            if (DataProps.ContainsKey("FTPAddr"))
            {
                var ftp = DataProps["FTPAddr"];
                if (!string.IsNullOrEmpty(ftp))
                {
                    var datafile = $"{DateTime.Now.ToString("yyyyMMdd")}.csv";

                    if (!File.Exists(Path.Combine(ftp, datafile)))
                    {
                        using (var fs = new FileStream(datafile, FileMode.Create))
                        {
                            using (var sw = new StreamWriter(fs, Encoding.UTF8))
                            {
                                sw.WriteLine(data.CsvHeaders());
                            }
                        }
                    }

                    using (var fs = new FileStream(datafile, FileMode.Append))
                    {
                        using (var sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.WriteLine(data.CsvValues());
                        }
                    }
                }
            }
        }
    }
}