using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.Interfaces;
using Lead.Detect.Interfaces.Dev;

namespace Lead.Detect.Element.EleControl
{
    public interface IEleUpdate
    {
        void UpdateEleStatus(IMotionCard motion);
    }
}