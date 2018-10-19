using System;
using System.ComponentModel;
using System.Text;

namespace Lead.Detect.FrameworkExtension.platforms
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TransformParams
    {
        public TransformParams()
        {
            var props = this.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, Activator.CreateInstance(p.PropertyType));
            }
        }

        public TransformParams(double[,] data)
        {
            R11 = data[0, 0];
            R12 = data[0, 1];
            R13 = data[0, 2];

            R21 = data[1, 0];
            R22 = data[1, 1];
            R23 = data[1, 2];

            R31 = data[2, 0];
            R32 = data[2, 1];
            R33 = data[2, 2];

            T1 = data[0, 3];
            T2 = data[1, 3];
            T3 = data[2, 3];

            S41 = data[3, 0];
            S42 = data[3, 1];
            S43 = data[3, 2];
        }

        public double[,] ToDoubles()
        {
            return new double[,]
            {
                {R11, R12, R13, T1},
                {R21, R22, R23, T2},
                {R31, R32, R33, T3},
                {S41, S42, S43, 1}
            };
        }

        public double R11 { get; set; }
        public double R12 { get; set; }
        public double R13 { get; set; }

        public double R21 { get; set; }
        public double R22 { get; set; }
        public double R23 { get; set; }

        public double R31 { get; set; }
        public double R32 { get; set; }
        public double R33 { get; set; }

        public double S41 { get; set; }
        public double S42 { get; set; }
        public double S43 { get; set; }

        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T3 { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{R11:F3},{R12:F3},{R13:F3},{T1:F3}\r\n");
            sb.Append($"{R21:F3},{R22:F3},{R23:F3},{T2:F3}\r\n");
            sb.Append($"{R31:F3},{R32:F3},{R33:F3},{T3:F3}\r\n");
            sb.Append($"{S41:F3},{S42:F3},{S43:F3},{1:F3}\r\n");
            return sb.ToString();
        }
    }
}