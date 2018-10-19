using System;
using System.Xml;

using Lead.Detect.Interfaces;
using Lead.Detect.PrimPlcOmron;

namespace Lead.Detect.Interfaces
{
    public class PrimCreator : IPrimCreator
    {
        public IPrimTypeAttributes ConcretePrimTypeAttributes
        {
            get
            {
                return new PrimTypeAttributes();
            }
        }

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
