using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimSktClient
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimSocketClient();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimSocketClient(taskPrimConfig);
        }
    }
}