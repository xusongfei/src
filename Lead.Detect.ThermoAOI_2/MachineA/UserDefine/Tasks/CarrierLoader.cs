using System.Threading;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace Lead.Detect.ThermoAOI2.MachineA.UserDefine.Tasks
{
    public class CarrierLoader
    {
        public StationTask Task;

        public ICylinderEx CyLeft;
        public ICylinderEx CyBack;
        public ICylinderEx CyRight;
        public ICylinderEx CyFront;


        public IDoEx Vaccum1;
        public IDoEx Vaccum2;

        public IDiEx VaccumSensor1;
        public IDiEx VaccumSensor2;


        public IDiEx CarrierSensor1;
        public IDiEx CarrierSensor2;


        public bool IsProductExists()
        {
            return CarrierSensor1.GetDiSts() || CarrierSensor2.GetDiSts();
        }


        public void ClampVC()
        {
            SetDatumn(true, 300, false);
            Clamp(true, 300, null);
            Thread.Sleep(800);
            SetVaccum(true);

            Clamp(false, 300, null);
            SetDatumn(false, 300, null);
        }


        public void ReleaseVC()
        {
            SetDatumn(true, 300, false);
            Clamp(false, 300, null);
            SetVaccum(false);
        }

        public void ClampModule()
        {
            SetDatumn(true, 300, null);
            Clamp(true, 300, null);
            SetVaccum(false);
        }


        public void ReleaseModule()
        {
            SetDatumn(true, 300, null);
            Clamp(false, 300, null);
            SetVaccum(false);
        }

        public void SetDatumn(bool status, int timeout, bool? ignoreOrWarningOrError)
        {
            new[] { CyLeft, CyFront }.SetDo(Task, new[] { status, status }, timeout, ignoreOrWarningOrError);
        }


        public void Clamp(bool status, int timeout, bool? ignoreOrWarningOrError)
        {
            new[] { CyRight, CyBack }.SetDo(Task, new[] { status, status }, timeout, ignoreOrWarningOrError);
        }

        public void SetVaccum(bool status)
        {
            Vaccum1.SetDo(status);
            Vaccum2.SetDo(status);
        }

        public bool CheckProductSensor()
        {
            if (CarrierSensor1.GetDiSts() && CarrierSensor2.GetDiSts())
            {
                return true;
            }

            Task?.Log($"载具定位传感器无信号", LogLevel.Warning);
            return false;
        }
    }
}