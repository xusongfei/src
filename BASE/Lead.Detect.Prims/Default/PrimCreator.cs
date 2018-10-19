using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimDefault
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimDefaultClass();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimDefaultClass(taskPrimConfig);
        }
    }
}