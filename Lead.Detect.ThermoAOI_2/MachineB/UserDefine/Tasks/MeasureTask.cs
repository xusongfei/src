using System;
using System.Collections.Generic;
using System.Linq;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MeasureComponents.LaserControl;
using Lead.Detect.MeasureComponents.LMILaser;
using Lead.Detect.MeasureComponents.Thermo2Camera;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks
{
    public class MeasureTask : StationTask
    {
        public IVioEx VioMeasureStart1;
        public IVioEx VioMeasureFinish1;


        public ThermoCameraB Camera;
        public PlatformEx CamPlatform;
        public ILineLaserEx Laser1;
        public PlatformEx L1Platform;
        public ILineLaserEx Laser2;
        public PlatformEx L2Platform;


        public MachineSettings CfgSettings;
        public MeasureProjectB Project;
        public Thermo2ProductB Product;


        public MeasureTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart1 = station.Machine.Find<IVioEx>("VioMeasureC1Start1");
            VioMeasureFinish1 = station.Machine.Find<IVioEx>("VioMeasureC1Finish1");

            if (FrameworkExtenion.IsSimulate)
            {
                Camera = new ThermoCameraBSim();
            }
            else
            {
                Camera = new ThermoCameraB();
            }

            CamPlatform = station.Machine.Find<PlatformEx>("CameraPlatform");
            {
                var toFilePos = new Func<double[], double[]>(d => (new PosXYZ(d) - new PosXYZ(CamPlatform["FocusOrigin"]?.Data())).Data());
                var toMovePos = new Func<double[], double[]>(d => (new PosXYZ(d) + new PosXYZ(CamPlatform["FocusOrigin"]?.Data())).Data());
                CamPlatform.PosConvertFuncs.Clear();
                CamPlatform.PosConvertFuncs.Add("FILE", toFilePos);
                CamPlatform.PosConvertFuncs.Add("MOVE", toMovePos);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                Laser1 = new LineLaserSim();
            }
            else
            {
                Laser1 = new LmiLaser();
                Laser1.Name = "Top";
                Laser1.IpStr = "192.168.2.10";
            }

            L1Platform = station.Machine.Find<PlatformEx>("Laser1Platform");
            {
                var toFilePos = new Func<double[], double[]>(d => (new PosXYZ(d) - new PosXYZ(L1Platform["LaserOrigin"]?.Data())).Data());
                var toMovePos = new Func<double[], double[]>(d => (new PosXYZ(d) + new PosXYZ(L1Platform["LaserOrigin"]?.Data())).Data());
                L1Platform.PosConvertFuncs.Clear();
                L1Platform.PosConvertFuncs.Add("FILE", toFilePos);
                L1Platform.PosConvertFuncs.Add("MOVE", toMovePos);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                Laser2 = new LineLaserSim();
            }
            else
            {
                Laser2 = new LmiLaser();
                Laser2.Name = "Bottom";
                Laser2.IpStr = "192.168.1.10";
            }

            L2Platform = station.Machine.Find<PlatformEx>("Laser2Platform");
            {
                var toFilePos = new Func<double[], double[]>(d => (new PosXYZ(d) - new PosXYZ(L2Platform["LaserOrigin"]?.Data())).Data());
                var toMovePos = new Func<double[], double[]>(d => (new PosXYZ(d) + new PosXYZ(L2Platform["LaserOrigin"]?.Data())).Data());
                L2Platform.PosConvertFuncs.Clear();
                L2Platform.PosConvertFuncs.Add("FILE", toFilePos);
                L2Platform.PosConvertFuncs.Add("MOVE", toMovePos);
            }
        }


        protected override int ResetLoop()
        {
            //load settings
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);

            //reset vio
            VioMeasureStart1.SetVio(this, false);
            VioMeasureFinish1.SetVio(this, false);

            //connect camera server
            try
            {
                Camera.Disconnect();
                var ret = Camera.Connect();
                if (!ret)
                {
                    Log($"{Camera} Connect Error", LogLevel.Error);
                }

                Camera.SwitchProduct(1);
            }
            catch (Exception ex)
            {
                Log($"{Camera} Connect Fail:{ex.Message}");
                return 0;
            }

            try
            {
                //connect laser1
                Laser1.Disconnect();
                var ret = Laser1.Connect();
                if (!ret)
                {
                    Log($"{Laser1} Connect Error", LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Log($"{Laser1} Connect Fail:{ex.Message}");
                return 0;
            }

            try
            {
                //connect laser2
                Laser2.Disconnect();
                var ret = Laser2.Connect();
                if (!ret)
                {
                    Log($"{Laser2} Connect Error", LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Log($"{Laser2} Connect Fail:{ex.Message}");
                return 0;
            }

            //reset platform
            CamPlatform.EnterAuto(this).Servo();
            CamPlatform.EnterAuto(this).Home();
            CamPlatform.EnterAuto(this).MoveAbs("Wait");

            //reset platform
            L1Platform.EnterAuto(this).Servo();
            L1Platform.EnterAuto(this).Home();
            L1Platform.EnterAuto(this).MoveAbs("Wait");

            //reset platform
            L2Platform.EnterAuto(this).Servo();
            L2Platform.EnterAuto(this).Home();
            L2Platform.EnterAuto(this).MoveAbs("Wait");

            return 0;
        }


        protected override int RunLoop()
        {
            //start assert
            CamPlatform.AssertAutoMode(this);
            CamPlatform.LocateInPos("Wait");
            L1Platform.AssertAutoMode(this);
            L1Platform.LocateInPos("Wait");
            L2Platform.AssertAutoMode(this);
            L2Platform.LocateInPos("Wait");

            RunCameraLoop("Camera1", 1, Project.CapturePos.FindAll(p => p.Name == "C1").ToList());
            RunLaserLoop("laser1", Laser1, L1Platform, Project.UpLaserPos.FindAll(p => p.Name == "L1").ToList());
            RunLaserLoop("laser2", Laser2, L2Platform, Project.DownLaserPos.FindAll(p => p.Name == "L2").ToList());
            RunCameraLoop("Camera2", 4, Project.CapturePos.FindAll(p => p.Name == "C2").ToList());

            return 0;
        }


        public void RunCameraLoop(string loopName, int cameraStepIndex, List<PosXYZ> capturePos)
        {
            //measure process 1
            var step = 0;
            while (step++ < capturePos.Count)
            {
                VioMeasureStart1.WaitVioAndClear(this);
                if (capturePos.Count > 0)
                {
                    //assert
                    Project.AssertNoNull(this);
                    Product.AssertNoNull(this);

                    //measure
                    var pos = capturePos[step - 1];
                    {
                        //move capture pos, adjust focus
                        CamPlatform.MoveAbs(pos);

                        //trigger
                        var ret = Camera.TriggerProduct(cameraStepIndex + step - 1);
                        if (!ret)
                        {
                            Product.Error = Camera.LastError;

                            Log($"{loopName} {Camera.Name} Trigger Error {cameraStepIndex + step - 1} {Camera.GetResult(string.Empty)}");
                        }
                        else
                        {
                            Product.Error = Camera.LastError;

                            var res = Camera.GetResult(string.Empty);

                            Log($"{loopName} {Camera.Name} Trigger OK {cameraStepIndex + step - 1} {Camera.GetResult(string.Empty)}");
                        }

                        //todo process cur data
                    }

                    //todo process all data
                    {
                    }
                }

                VioMeasureFinish1.SetVio(this);
            }

            CamPlatform.MoveAbs("Wait");
        }


        public void RunLaserLoop(string loopName, ILineLaserEx laser, PlatformEx laserPlatform, List<PosXYZ> laserPos)
        {
            //Up laser process 
            var step = 0;
            while (step++ < laserPos.Count)
            {
                //measure process 1
                VioMeasureStart1.WaitVioAndClear(this);

                if (laserPos.Count > 0)
                {
                    //assert
                    Project.AssertNoNull(this);
                    Product.AssertNoNull(this);

                    if (step % 2 == 0)
                    {
                        //move laser trigger start pos                   
                        laserPlatform.MoveAbs(laserPos[0]);
                        //start laser trigger
                        laser.Trigger(string.Empty);
                        System.Threading.Thread.Sleep(800);
                        laserPlatform.MoveAbs(laserPos[1]);
                    }
                    else
                    {
                        //move laser trigger start pos                   
                        laserPlatform.MoveAbs(laserPos[1]);
                        //start laser trigger
                        laser.Trigger(string.Empty);
                        System.Threading.Thread.Sleep(800);
                        laserPlatform.MoveAbs(laserPos[0]);
                    }

                    var data = laser.GetResult();
                    Log($"{string.Join("\r\n", Laser1.Name, data.Select(d => d.Select(v => v.ToString("F3"))))})", LogLevel.Info);

                    //todo process all laser 1 data
                    {
                    }
                }

                VioMeasureFinish1.SetVio(this);
            }

            laserPlatform.MoveAbs("Wait");
        }
    }
}