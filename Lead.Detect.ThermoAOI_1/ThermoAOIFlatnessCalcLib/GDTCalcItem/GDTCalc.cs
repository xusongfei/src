using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.ThermoAOIProductLib.ProductBase;

namespace Lead.Detect.ThermoAOIProductLib.GDTCalcItem
{

    public enum ExpectCalcValue
    {
        Normal,
        Max,
        Min,
    }


    [XmlInclude(typeof(FlatnessCalc))]
    [XmlInclude(typeof(LocationOfPositionCalc))]
    [XmlInclude(typeof(ParallelismCalc))]
    [XmlInclude(typeof(ProfileOfLineCalc))]
    [XmlInclude(typeof(ProfileOfSurfaceCalc))]
    public class GDTCalc : IGDTCalc
    {
        public GDTCalc()
        {
            IsDatum = false;
            GDTType = GDTType.None;
            ExpectValue = ExpectCalcValue.Normal;

            Data = new List<double>();
        }

        [Description("SPC尺寸名称")]
        public string Name { get; set; }
        [Description("SPC尺寸描述")]
        public string Description { get; set; }

        [ReadOnly(true)]
        [Description("形位公差")]
        public GDTType GDTType { get; set; }
        [Description("是否基准")]
        public bool IsDatum { get; set; }

        [Description("数据选择")]
        public ExpectCalcValue ExpectValue { get; set; }

        /// <summary>
        /// 源数据点
        /// </summary>
        /// 
        [Description("计算数据点类型")]
        public string SourcePos { get; set; }


        /// <summary>
        /// 基准名称
        /// </summary>
        [Description("基准名称")]
        public string DatumName { get; set; }
        /// <summary>
        /// 基准对象
        /// </summary>
        [ReadOnly(true)]
        public object Datum { get; set; }

        /// <summary>
        /// 尺寸限制值
        /// </summary>
        [ReadOnly(true)]
        public double Spec { get; set; }
        /// <summary>
        /// 尺寸限制值
        /// </summary>
        [ReadOnly(true)]
        public double SpecMin { get; set; }
        /// <summary>
        /// 尺寸限制值
        /// </summary>
        [ReadOnly(true)]
        public double SpecMax { get; set; }

        /// <summary>
        /// 计算结果值
        /// </summary>
        [ReadOnly(true)]
        public double Value { get; set; }

        /// <summary>
        /// 源数据点计算值
        /// </summary>
        [ReadOnly(true)]
        public List<double> Data { get; set; }
        [ReadOnly(true)]
        public double Max { get; set; }
        [ReadOnly(true)]
        public double Min { get; set; }
        [ReadOnly(true)]
        public bool Success { get; set; }


        /// <summary>
        /// 清除计算流程变量
        /// </summary>
        public virtual void Clear()
        {
            Datum = null;

            Value = int.MinValue;
            Max = int.MaxValue;
            Min = int.MinValue;

            Data?.Clear();

            Success = false;
        }

        /// <summary>
        /// 设置基准
        /// </summary>
        /// <param name="datum"></param>
        public virtual void SetDatum(object datum)
        {
            Datum = datum;
        }


        /// <summary>
        /// 调用计算过程
        /// </summary>
        /// <param name="pos"></param>
        public virtual void DoCalc(List<PosXYZ> pos)
        {
            Success = true;
        }

        public void SetSpec(double spcSpec, double spcUpLimit, double spcDownLimit)
        {
            Spec = spcSpec;
            SpecMax = spcSpec + spcUpLimit;
            SpecMin = spcSpec + spcDownLimit;
        }

        public void UpdateValue(ref SPCItem spc)
        {
            if(ExpectValue == ExpectCalcValue.Normal)
            {
                spc.Value = Value;
            }
            else if (ExpectValue == ExpectCalcValue.Min)
            {
                spc.Value = Min;
            }
            else if (ExpectValue == ExpectCalcValue.Max)
            {
                spc.Value = Max;
            }

            spc.ValueMin = Min;
            spc.ValueMax = Max;
        }

        public override string ToString()
        {
            var props = GetType().GetProperties();

            return "{\n\t" + string.Join("\n\t", props.Select(p => $"{p.Name}:{p.GetValue(this) ?? string.Empty}")) + "\n}";

        }
    }
}
