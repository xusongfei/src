using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Lead.Detect.PrimPlcOmron
{
    public class XMLClass
    {
        private readonly XmlDocument _xmlDoc;
        private XmlNode[] _xmlElements;
        private XmlNode _xmlNode;
        private XmlNode _xmlRoot;

        public XMLClass()
        {
            _xmlDoc = new XmlDocument();
            CreateXmlFile();
        }

        public string FileName { get; set; }

        private void CreateElement(List<string> elementNames, out XmlNode[] elements)
        {
            var list = new List<XmlNode>();
            var elem = elementNames.ToArray();
            elements = null;
            var count = elementNames.Count;
            for (var i = 0; i < count; i++)
            {
                var xmlNode = _xmlNode.SelectSingleNode(string.Format("//{0}", elem[i]));
                if (xmlNode == null) xmlNode = _xmlDoc.CreateElement(elem[i]);
                list.Add(xmlNode);
            }

            elements = list.ToArray();
        }

        private void CreateNode(string node)
        {
            _xmlNode = _xmlRoot.SelectSingleNode(string.Format("//{0}", node));
            if (_xmlNode == null) _xmlNode = _xmlDoc.CreateElement(node);
        }

        private void CreateRoot(string root)
        {
            _xmlRoot = _xmlDoc.SelectSingleNode(root);
            if (_xmlRoot == null)
            {
                _xmlRoot = _xmlDoc.CreateElement(root);
                _xmlDoc.AppendChild(_xmlRoot);
            }
        }

        private void CreateXmlFile()
        {
            if (File.Exists(FileName) == false)
            {
                var dec = _xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                _xmlDoc.AppendChild(dec);
                CreateRoot("root");
            }
        }

        public void GetElement(string node, List<string> elementNames, out string[] values)
        {
            _xmlElements = null;
            values = null;
            var count = elementNames.Count;
            var valueList = new List<string>();
            CreateNode(node);
            CreateElement(elementNames, out _xmlElements);
            for (var i = 0; i < count; i++) valueList.Add(_xmlElements[i].InnerText);
            values = valueList.ToArray();
        }

        public void SetElement(string node, List<string> elementNames, List<string> valueList)
        {
            _xmlElements = null;
            var count = elementNames.Count;
            var valueArrary = valueList.ToArray();
            CreateNode(node);
            CreateElement(elementNames, out _xmlElements);
            for (var i = 0; i < count; i++)
            {
                _xmlElements[i].InnerText = valueArrary[i];
                _xmlNode.AppendChild(_xmlElements[i]);
            }

            _xmlRoot.AppendChild(_xmlNode);
        }

        public void XmlLoad()
        {
            _xmlDoc.Load(FileName);
        }

        public void XmlSave()
        {
            _xmlDoc.Save(FileName);
        }
    }
}