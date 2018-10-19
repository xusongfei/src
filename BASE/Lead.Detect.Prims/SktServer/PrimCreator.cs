using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimSktServer
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimSocketServer();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimSocketServer(taskPrimConfig);
        }
    }
}