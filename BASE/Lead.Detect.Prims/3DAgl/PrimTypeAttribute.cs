using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.Prim3DAgl
{
    public class PrimTypeAttributes : IPrimTypeAttributes
    {
        public string Name => "Agl3D";

        public string DisplayName => "Agl";

        public string Description => "";

        public string Group => "Agl";

        public string DisplayGroup => "Agl";

        public byte MajorVersion => 1;

        public byte MinorVersion => 0;

        public Image Icon
        {
            get
            {
                var manager = new ResourceManager(typeof(Resource3DAgl));
                Image img = Image.FromHbitmap(((Icon) manager.GetObject("Agl_3D")).ToBitmap().GetHbitmap());
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