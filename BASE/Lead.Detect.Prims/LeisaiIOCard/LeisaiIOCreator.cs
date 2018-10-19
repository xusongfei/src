using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.PrimLeisaiIOCard.Properties;

namespace Lead.Detect.PrimLeisaiIOCard
{
    public class LeisaiIOCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps { get; } = new LeisaiProps();

        public IPrim Create()
        {
            return new LeisaiIOCard();
        }

        public IPrim Create(XmlNode primConfig)
        {
            return new LeisaiIOCard(primConfig);
        }
    }


    public class LeisaiProps : IPrimTypeAttributes
    {
        public string Name { get; } = nameof(LeisaiIOCard);
        public string DisplayName { get; } = nameof(LeisaiIOCard);
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
