using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using Lead.Detect.Interfaces;
using Lead.Detect.PrimGTKeyence;
using Lead.Detect.PrimPlcOmron;

namespace Lead.Detect.Interfaces
{
    public class PrimTypeAttributes : IPrimTypeAttributes
    {
        public string Name
        {
            get
            {
                return "gt";
            }
        }

        public string DisplayName
        {
            get
            {
                return "PLC";
            }
        }

        public string Description
        {
            get
            {
                return "";
            }
        }

        public string Group
        {
            get
            {
                return "Dev";
            }
        }

        public string DisplayGroup
        {
            get
            {
                return "Dev";
            }
        }

        public byte MajorVersion
        {
            get
            {
                return 1;
            }
        }

        public byte MinorVersion
        {
            get
            {
                return 0;
            }
        }

        public Image Icon
        {
            get
            {
                ResourceManager manager = new ResourceManager(typeof(ResourceKeyence));
                return (Image)manager.GetObject("gt");
            }
        }

        public object this[string attrName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<KeyValuePair<string, object>> Attributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
