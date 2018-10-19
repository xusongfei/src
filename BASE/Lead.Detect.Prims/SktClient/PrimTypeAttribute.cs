using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimSktClient
{
    public class PrimTypeAttributes : IPrimTypeAttributes
    {
        public string Name => nameof(PrimSocketClient);

        public string DisplayName => "SocketClient";

        public string Description => "";

        public string Group => "Comm";

        public string DisplayGroup => "Comm";

        public byte MajorVersion => 1;

        public byte MinorVersion => 0;

        public Image Icon
        {
            get
            {
                var manager = new ResourceManager(typeof(ResourceSocketClient));
                var img = (Image) manager.GetObject("Client");
                return img;
            }
        }

        public object this[string attrName]
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<KeyValuePair<string, object>> Attributes
        {
            get { throw new NotImplementedException(); }
        }
    }
}