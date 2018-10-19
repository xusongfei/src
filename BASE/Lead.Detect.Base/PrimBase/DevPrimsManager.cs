using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.Base
{
    public class DevPrimsManager
    {
        private DevPrimsManager()
        {
        }

        public static DevPrimsManager Instance { get; } = new DevPrimsManager();


        public List<IPrim> Prims { get; } = new List<IPrim>();

        public IPrim GetPrimByName(string primName)
        {
            return Prims.FirstOrDefault(p => p.Name == primName);
        }

        public IPrim GetPrimByGUID(Guid id)
        {
            return Prims.FirstOrDefault(p => p.PrimId == id);
        }

        public int RemovePrimByGUID(Guid id)
        {
            Prims.RemoveAll(p => p.PrimId == id);
            return 0;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var p in Prims)
            {
                sb.AppendLine($"{p.Name} {p.PrimTypeName} {p.ToString()}");
            }

            return sb.ToString();
        }
    }
}