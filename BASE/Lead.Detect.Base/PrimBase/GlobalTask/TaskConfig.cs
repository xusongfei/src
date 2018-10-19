using System.Xml;

namespace Lead.Detect.Base.GlobalTask
{
    public class TaskConfig
    {
        public string TaskType { get; set; }
        public string TaskCreatorFilePath { set; get; }
        public XmlNode TaskXmlNode { get; set; }
    }
}