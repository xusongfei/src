using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo.Product;

namespace Lead.Detect.ThermoAOIFlatnessCalcLib.ThermoDataConvert
{
    public class AvcData
    {

        public string PartId;

        public DateTime TestTime;

        public string ProductId;

        public List<SPCItem> SpcItems;

        public string TestResult;

        public string Machine;



        public string CsvHeaders()
        {
            var sb = new StringBuilder();

            sb.Append($"PartId,");
            sb.Append($"TestTime,");
            sb.Append($"ProductId,");

            foreach (var s in SpcItems)
            {
                sb.Append($"{s.Name},");
            }

            sb.Append($"TestResult,");
            sb.Append($"MachineName");


            return sb.ToString();
        }



        public string CsvValues()
        {
            var sb = new StringBuilder();

            sb.Append($"{PartId},");
            sb.Append($"{TestTime.ToString("yyyyMMdd-HH:mm:ss tt")},");
            sb.Append($"{ProductId},");

            foreach (var s in SpcItems)
            {
                sb.Append($"{s.Value:F3},");
            }

            sb.Append($"{TestResult},");
            sb.Append($"{Machine}");


            return sb.ToString();
        }


        public void Save(string dir = @".\Data")
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            try
            {
                var file = Path.Combine(dir, $"{DateTime.Now.ToString("yyyyMMdd")}.csv");

                if (!File.Exists(file))
                {
                    using (var fs = new FileStream(file, FileMode.Create))
                    {
                        using (var sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.WriteLine(CsvHeaders());
                        }
                    }
                }

                using (var fs = new FileStream(file, FileMode.Append))
                {
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(CsvValues());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Save Error:{ex.Message}");
            }
        }



    }
}