using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.Prim3DAgl
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimAgl3D();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimAgl3D(taskPrimConfig);
        }
    }
}