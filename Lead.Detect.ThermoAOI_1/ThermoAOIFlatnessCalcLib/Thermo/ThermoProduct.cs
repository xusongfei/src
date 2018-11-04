using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOIProductLib.Thermo
{
    public class ThermoProduct : ProductBase.Product
    {
        /// <summary>
        /// 测试项
        /// </summary>
        public List<SPCItem> SPCItems { get; set; } = new List<SPCItem>();

        public void ClearSpc()
        {
            foreach (var s in SPCItems)
            {
                s.Value = 0;
                s.ValueMin = 0;
                s.ValueMax = 0;
            }
        }

        public void SetSpcItem(int fai, double val)
        {
            var item = SPCItems.FirstOrDefault(f => f.FAI == fai);
            if (item != null)
            {
                item.Value = val;
            }
        }

        public void SetSpcItem(string spc, double val)
        {
            var item = SPCItems.FirstOrDefault(f => f.SPC == spc);
            if (item != null)
            {
                item.Value = val;
            }
        }

        public void UpdateStatus()
        {
            //update CT
            FinishTime = DateTime.Now;
            CT = (FinishTime - StartTime).TotalSeconds;

            if (Status != ProductStatus.ERROR)
            {
                //update status if no error happen
                Status = SPCItems.Count > 0 && SPCItems.All(t => t.CheckSpec()) ? ProductStatus.OK : ProductStatus.NG;
                if (SPCItems.Count > 0)
                {
                    if (Status == ProductStatus.NG)
                    {
                        var sb = new StringBuilder();
                        foreach (var s in SPCItems)
                        {
                            if (!s.CheckSpec())
                            {
                                sb.Append($"{s.Name} {s.Value:F3} 超差|");
                            }
                        }

                        Error = sb.ToString();
                    }
                }
                else
                {
                    Error = "NO SPC ITEMS";
                }
            }
        }


        public override string CsvHeaders()
        {
            var sb = new StringBuilder();

            sb.Append($"{base.CsvHeaders()},");


            sb.Append("SPC,");
            foreach (var s in SPCItems)
            {
                sb.Append($"{s.SPC}-{s.Description},");
            }

            sb.Append("SPCMIN,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.Name + "MIN,");
            }

            sb.Append("SPCMAX,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.Name + "MAX,");
            }

            sb.Append("SPEC,");
            foreach (var s in SPCItems)
            {
                sb.Append($"SPEC-{s.SPC},UpLimit-{s.SPC},DownLimit-{s.SPC},");
            }


            return sb.ToString();
        }


        public override string CsvValues()
        {
            var sb = new StringBuilder();


            sb.Append(base.CsvValues());
            sb.Append(",");


            sb.Append("SPC,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.Value.ToString("F3"));
                sb.Append(",");
            }

            sb.Append("SPC MIN,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.ValueMin.ToString("F3"));
                sb.Append(",");
            }

            sb.Append("SPC MAX,");
            foreach (var s in SPCItems)
            {
                sb.Append(s.ValueMax.ToString("F3"));
                sb.Append(",");
            }

            sb.Append("SPEC,");
            foreach (var s in SPCItems)
            {
                sb.Append($"{s.SPEC:F2},{s.UpLimit:F3},{s.DownLimit:F2},");
            }

            return sb.ToString();
        }


        public override DataTable ToDataTable()
        {
            var dt = base.ToDataTable();

            foreach (var s in SPCItems)
            {
                var row = dt.Rows.Add();
                row[0] = $"SPC-{s.Name}";
                row[1] = s.ToString();
            }

            return dt;
        }
    }
}