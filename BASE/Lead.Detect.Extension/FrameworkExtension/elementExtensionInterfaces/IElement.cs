using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.FrameworkExtension.elementExtensionInterfaces
{
    public interface IElement
    {
        string Export();

        void Import(string line, StateMachine machine);

    }
}
