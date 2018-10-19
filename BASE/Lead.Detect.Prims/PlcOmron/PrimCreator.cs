using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimPlcOmron
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimOmron();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimOmron(taskPrimConfig);
        }
    }
}