using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace MachineUtilityLib.Utils
{
    public class MultiClampCylinders
    {
        public List<ICylinderEx> Clamps = new List<ICylinderEx>();

        private ICylinderEx[] _clamps;

        public void Clamp(StationTask task, int timeout = 150, bool? ignoreOrWarningOrError = null)
        {
            if (_clamps == null)
            {
                _clamps = Clamps.ToArray();
            }

            _clamps.SetDoAsync(task, true);

            _clamps.WaitDi(task, _clamps.Select(c => true).ToArray(), timeout, ignoreOrWarningOrError);
        }

        public void Reset(StationTask task, int timeout = 150, bool? ignoreOrWarningOrError = null)
        {
            if (_clamps == null)
            {
                _clamps = Clamps.ToArray();
            }

            _clamps.SetDoAsync(task, false);

            _clamps.WaitDi(task, Clamps.Select(c => false).ToArray(), timeout, ignoreOrWarningOrError);
        }
    }
}