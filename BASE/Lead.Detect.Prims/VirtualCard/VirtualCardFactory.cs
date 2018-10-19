using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.PrimVirtualCard.Properties;

namespace Lead.Detect.PrimVirtualCard
{
    public class VirtualCardFactory : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps { get; } = new VirtualCardProps();

        public IPrim Create()
        {
            return new VirtualCard();
        }

        public IPrim Create(XmlNode primConfig)
        {
            return new VirtualCard(primConfig);
        }
    }


    public class VirtualCardProps : IPrimTypeAttributes
    {
        public string Name { get; } = nameof(VirtualCard);
        public string DisplayName { get; } = nameof(VirtualCard);
        public string Description { get; } = "虚拟";
        public string Group { get; }
        public string DisplayGroup { get; }
        public byte MajorVersion { get; } = 1;
        public byte MinorVersion { get; } = 0;
        public Image Icon { get; } = Resources.air;

        public object this[string attrName]
        {
            get { throw new NotImplementedException(); }
        }


        public IEnumerable<KeyValuePair<string, object>> Attributes { get; }
    }
}