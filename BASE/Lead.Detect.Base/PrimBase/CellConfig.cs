using System.Xml;

namespace Lead.Detect.Base
{
    public class CellConfig
    {
        public string CellType { get; set; }
        public string CellCreatorFilePath { set; get; }
        public XmlNode CellXmlNode { get; set; }
    }
}