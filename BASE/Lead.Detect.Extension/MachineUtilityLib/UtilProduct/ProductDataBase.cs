using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineUtilityLib.UtilProduct
{
    public class ProductDataBase
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime FinishTime { get; set; } = DateTime.Now;
        public ProductStatus Status { get; set; } = ProductStatus.NONE;
        public string Error { get; set; }
        public double CT { get; set; }
        public string Barcode { get; set; }
        public string ProductType { get; set; }
        public string Description { get; set; }

        public virtual string CsvHeaders()
        {
            var sb = new StringBuilder();

            sb.Append($"StartTime,");
            sb.Append($"FinishTime,");
            sb.Append($"Status,");
            sb.Append($"Error,");
            sb.Append($"CT,");
            sb.Append($"Barcode,");
            sb.Append($"ProductType,");
            sb.Append($"Description");

            return sb.ToString();
        }

        public virtual string CsvValues()
        {
            var sb = new StringBuilder();
            sb.Append(StartTime.ToString("yyyyMMdd-HHmmss")); sb.Append(",");
            sb.Append(FinishTime.ToString("yyyyMMdd-HHmmss")); sb.Append(",");
            sb.Append(Status.ToString()); sb.Append(",");
            sb.Append(Error?.ToString()); sb.Append(",");
            sb.Append(CT.ToString("F2")); sb.Append(",");
            sb.Append(Barcode?.ToString()); sb.Append(",");
            sb.Append(ProductType?.ToString()); sb.Append(",");
            sb.Append(Description?.ToString());
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"StartTime:{StartTime.ToString("yyyyMMdd-HHmmss")},");
            sb.Append($"FinishTime:{FinishTime.ToString("yyyyMMdd-HHmmss")},");
            sb.Append($"Status:{Status},");
            sb.Append($"Error:{Error},");
            sb.Append($"CT:{CT:F2},");
            sb.Append($"Barcode:{Barcode},");
            sb.Append($"ProductType:{ProductType},");
            sb.Append($"Description:{Description}");
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

        public virtual DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("测试项");
            dt.Columns.Add("测试值");

            {
                var row = dt.Rows.Add();
                row[0] = "开始时间"; row[1] = StartTime.ToString("yyyyMMdd-HHmmss");
            }
            {
                var row = dt.Rows.Add();
                row[0] = "结束时间"; row[1] = FinishTime.ToString("yyyyMMdd-HHmmss");
            }
            {
                var row = dt.Rows.Add();
                row[0] = "测试结果"; row[1] = Status;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "异常信息"; row[1] = Error;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "测试时间"; row[1] = CT.ToString("F2");
            }
            {
                var row = dt.Rows.Add();
                row[0] = "条码"; row[1] = Barcode;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "产品类型"; row[1] = ProductType;
            }
            {
                var row = dt.Rows.Add();
                row[0] = "产品描述"; row[1] = Description;
            }

            return dt;
        }
    }
}