using System.Xml;

namespace Lead.Detect.Base.GlobalPrim
{
    public interface IPrimCreator
    {
        IPrimTypeAttributes PrimProps { get; }

        IPrim Create();

        IPrim Create(XmlNode primConfig);
    }
}