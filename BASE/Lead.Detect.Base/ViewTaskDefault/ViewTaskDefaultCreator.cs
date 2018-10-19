using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Lead.Detect.Interface.ViewTask;

namespace Lead.Detect.ViewTaskDefault
{
    public class ViewTaskDefaultCreator : IViewTaskCreator
    {
        public IViewTask Create()
        {
            var d = new ViewTaskDefaultClass();
            return d;
        }

        public IViewTask Create(XmlNode config)
        {
            var d = new ViewTaskDefaultClass(config);
            return d;
        }

        public string VisioPrimType
        {
            get { return "ViewTaskDefault"; }
        }
    }
}