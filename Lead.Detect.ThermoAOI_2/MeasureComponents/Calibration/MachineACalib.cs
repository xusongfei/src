using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.platforms.calibrations;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.MeasureComponents.CameraControl;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;

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

            foreach (var pos in Project.CapturePos)
            {
                Platform.Jump(pos, jumpHeight: -20, zLimit: -22);
                Log($"{Platform.Name} Jump Calib {index}");


                var ret = Camera1.TriggerCalib(index++);
                var result = Camera1.GetResult(string.Empty);

                Log($"{Camera1} Trigger Calib {index - 1} {ret} {result}");

            }
        }

        public override void UninitCalib()
        {
            Camera1.Disconnect();
            Platform.MoveAbs("Wait");
        }
    }
}
