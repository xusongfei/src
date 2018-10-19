using System.Xml;
using Lead.Detect.Base.GlobalPrim;
using Lead.Detect.Interfaces;

namespace Lead.Detect.PrimCameraFocalSpec
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes PrimProps => new PrimTypeAttributes();

        public IPrim Create()
        {
            return new PrimFocalSpec();
        }

        public IPrim Create(XmlNode taskPrimConfig)
        {
            return new PrimFocalSpec(taskPrimConfig);
        }
    }
}