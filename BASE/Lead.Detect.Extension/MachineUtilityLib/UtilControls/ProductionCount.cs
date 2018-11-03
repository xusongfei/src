using System.ComponentModel;
using MachineUtilityLib.UtilProduct;

namespace MachineUtilityLib.UtilControls
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProductionCount
    {
        public int TotalCount { set; get; }

        public int OKCount { set; get; }
        public int NGCount { set; get; }

        public void Clear()
        {
            TotalCount = 0;
            OKCount = 0;
            NGCount = 0;
        }


        public string Display()
        {
            return $"ALL:{TotalCount} OK:{OKCount} NG:{NGCount}";
        }


        public override string ToString()
        {
            return $"ALL:{TotalCount}\r\nOK:{OKCount}\r\nNG:{NGCount}\r\n";
        }

        public void Update(ProductDataBase product)
        {
            TotalCount++;
            if (product.Status == ProductStatus.OK)
            {
                OKCount++;
            }
            else
            {
                NGCount++;
            }
        }
    }
}