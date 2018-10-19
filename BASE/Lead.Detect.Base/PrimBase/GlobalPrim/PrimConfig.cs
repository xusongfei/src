using System.Xml;

namespace Lead.Detect.Base.GlobalPrim
{
    public class PrimConfig
    {
        /// <summary>
        /// Prim 类型
        /// </summary>
        public string VisioPrimType { get; set; }
        public string PrimCreatorFilePath { set; get; }

        /// <summary>
        /// Prim 配置
        /// </summary>
        public XmlNode PrimXmlNode { get; set; }
    }
}