using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimMotionADLink
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimADLink();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimADLink(taskPrimConfig);
        }
    }
}