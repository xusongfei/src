using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimCameraDalsa
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps { get; set; } = new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimDalsa();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimDalsa(taskPrimConfig);
        }
    }
}