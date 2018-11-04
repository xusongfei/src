using System;
using System.ComponentModel;

namespace Lead.Detect.ThermoAOIProductLib.ProductBase
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SPCItem
    {
        public SPCItem()
        {
        }

        public SPCItem(string listString)
        {
            var data = listString.Split('~');

            Value = double.Parse(data[0]);
            Description = data[1];
            FAI = int.Parse(data[2]);
            SPC = data[3];
            SPEC = double.Parse(data[4]);
            UpLimit = double.Parse(data[5]);
            DownLimit = double.Parse(data[6]);
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Description("测试项")]
        public string Name { get; set; }


        /// <summary>
        /// 测试数据
        /// </summary>
        [Description("测试数据")]
        public double Value { get; set; }
        /// <summary>
        /// 测试数据最大值
        /// </summary>
        [Description("测试数据")]
        public double ValueMax { get; set; }
        /// <summary>
        /// 测试数据最小值
        /// </summary>
        [Description("测试数据")]
        public double ValueMin { get; set; }


        /// <summary>
        /// 测试项说明
        /// </summary>
        [Description("测试项说明")]
        public string Description { get; set; }


        /// <summary>
        /// FAI编号
        /// </summary>
        [Description("测试项FAI编号")]
        public int FAI { get; set; }

        /// <summary>
        /// SPC名称编号
        /// </summary>
        [Description("测试项FPC编号")]
        public string SPC { get; set; }

        /// <summary>
        /// 目标值
        /// </summary>
        [Description("目标值")]
        public double SPEC { get; set; }

        /// <summary>
        /// 上公差
        /// </summary>
        [Description("上公差")]
        public double UpLimit { get; set; }

        /// <summary>
        /// 下公差
        /// </summary>
        [Description("下公差")]
        public double DownLimit { get; set; }


        public bool CheckSpec()
        {
            if (Math.Abs(UpLimit) < float.Epsilon && Math.Abs(DownLimit) < float.Epsilon)
            {
                return Value <= SPEC;
            }
            else
            {
                return (Value <= SPEC + UpLimit) && (Value >= SPEC + DownLimit);
            }
        }

        public override string ToString()
        {
            return $"{Name} {FAI} {SPC} {Value:F3} {SPEC:F2} {UpLimit:F2} {DownLimit:F2} {Description}";
        }
    }
}