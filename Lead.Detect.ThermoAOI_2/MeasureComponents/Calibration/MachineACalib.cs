using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIProductLib.Thermo2;

namespace Lead.Detect.MeasureComponents.Calibration
{
    public class MachineACalib : AutoCalib
    {
        public ThermoCameraA Camera1 { get; set; }
        public PlatformEx Platform { get; set; }
        public MeasureProjectA Project { get; set; }

        public override void InitCalib()
        {

            Camera1.Disconnect();
            Camera1.Connect();

            Platform.MoveAbs("Wait");

        }

        public override void DoCalib()
        {
            var index = 0;

            var calib1Height = Platform["Calib1Height"]?.Data()[2] ?? 0;
            var calib2Height = Platform["Calib2Height"]?.Data()[2] ?? 0;

            foreach (var pos in Project.CapturePos)
            {
                index++;

                Platform.Jump(pos, jumpHeight: -20, zLimit: -22);
                Log($"{Platform.Name} Jump Calib {index}");


                var ret = Camera1.TriggerCalib(index);
                var result = Camera1.GetResult(string.Empty);

                Log($"{Camera1} Trigger Calib {index} {ret} {result}");

            }
        }

        public override void UninitCalib()
        {
            Camera1.Disconnect();
            Platform.MoveAbs("Wait");
        }
    }
}
