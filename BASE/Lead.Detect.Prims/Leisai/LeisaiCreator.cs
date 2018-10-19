using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.PrimLeisai.Properties;

namespace Lead.Detect.PrimLeisai
{
    public class LeisaiCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps { get; } = new VirtualCardProps();

        public IPrim Create()
        {
            return new LeiSaiCard();
        }

        public IPrim Create(XmlNode primConfig)
        {
            return new LeiSaiCard(primConfig);
        }
    }


    public class VirtualCardProps : IPrimTypeAttributes
    {
        public string Name { get; } = nameof(LeiSaiCard);
        public string DisplayName { get; } = nameof(LeiSaiCard);
        public string Description { get; } = "雷塞卡";
        public string Group { get; }
        public string DisplayGroup { get; }
        public byte MajorVersion { get; } = 1;
        public byte MinorVersion { get; } = 0;
        public Image Icon { get; } = Resources.repeat;

        public object this[string attrName]
        {
            get { throw new NotImplementedException(); }
        }


        public IEnumerable<KeyValuePair<string, object>> Attributes { get; }
    }
}
