using System.Xml;
using Lead.Detect.Base.GlobalPrim;

namespace Lead.Detect.PrimIOAdlink
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimIO7432();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimIO7432(taskPrimConfig);
        }
    }
}