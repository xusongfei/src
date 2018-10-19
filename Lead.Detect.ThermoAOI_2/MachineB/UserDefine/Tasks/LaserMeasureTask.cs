using System.Collections.Generic;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.frameworkManage;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using Lead.Detect.FrameworkExtension.stateMachine;
using Lead.Detect.MeasureComponents.LaserControl;
using Lead.Detect.MeasureComponents.LMILaser;
using Lead.Detect.ThermoAOIFlatnessCalcLib.Thermo2;
using MachineUtilityLib.Utils;
using System.Linq;

namespace Lead.Detect.ThermoAOI2.MachineB.UserDefine.Tasks
{
    public class LaserMeasureTask : StationTask
    {
        public int Stage1StepCount;
        public int Stage2StepCount;

        public IVioEx VioMeasureStart1;
        public IVioEx VioMeasureFinish1;


        public ILineLaserEx Laser;
        public PlatformEx Platform;


        public List<PosXYZ> LaserProcess1 = new List<PosXYZ>();
        public List<PosXYZ> LaserProcess2 = new List<PosXYZ>();

        public MachineSettings CfgSettings;
        public MeasureProjectB Project;
        public Thermo2ProductB Product;


        public LaserMeasureTask(int id, string name, Station station) : base(id, name, station)
        {

        }

        protected override int ResetLoop()
        {
            //load cfg in subclass


            //reset vio
            VioMeasureStart1.SetVio(this, false);
            VioMeasureFinish1.SetVio(this, false);

            //connect laser
            Laser.Disconnect();
            Laser.Connect();


            //reset platform
            Platform.EnterAuto(this).Servo();
            Platform.EnterAuto(this).Home();
            Platform.EnterAuto(this).MoveAbs("Wait");
            return 0;
        }


        protected override int RunLoop()
        {
            //start assert
            Platform.AssertAutoMode(this);
            Platform.LocateInPos("Wait");


            var stage1Step = 0;
            while (stage1Step++ < Stage1StepCount)
            {
                //measure process 1
                VioMeasureStart1.WaitVioAndClear(this);

                if (LaserProcess1.Count > 0)
                {
                    //assert
                    Project.AssertNoNull(this);
                    Product.AssertNoNull(this);

                    var i = 0;
                    //move laser trigger start pos                   
                    Platform.MoveAbs(LaserProcess1[i]);
                    //start laser trigger
                    Laser.Trigger(string.Empty);
                    System.Threading.Thread.Sleep(800);
                    Platform.MoveAbs(LaserProcess1[i + 1], new[] { 40, 40, 40d });


                    var data = Laser.GetResult();
                    Log($"{string.Join("\r\n", data.Select(d => d.Select(v => v.ToString("F3"))))})", LogLevel.Info);

                    //todo process all laser 1 data
                    {

                    }
                }

                VioMeasureFinish1.SetVio(this);

            }

            var stage2Step = 0;
            while (stage2Step++ < Stage2StepCount)
            { 
                //measure process 2
                VioMeasureStart1.WaitVioAndClear(this);

                if (LaserProcess2.Count > 0)
                {
                    //assert
                    Project.AssertNoNull(this);
                    Product.AssertNoNull(this);

                    var i = 0;
                    //move laser trigger start pos      
                    Platform.MoveAbs(LaserProcess2[i + 1]);
                    //move laser trigger end pos
                    System.Threading.Thread.Sleep(500);

                    Laser.Trigger(string.Empty);
                    System.Threading.Thread.Sleep(800);
                    Platform.MoveAbs(LaserProcess2[i]);


                    var data = Laser.GetResult();
                    Log($"{string.Join("\r\n", data.Select(d => d.Select(v => v.ToString("F3"))))})", LogLevel.Info);

                    //todo process all laser 2 data
                    {

                    }

                }

                VioMeasureFinish1.SetVio(this);
            }

            return 0;
        }
    }


    public class LaserMeasureUpTask : LaserMeasureTask
    {
        public LaserMeasureUpTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart1 = station.Machine.Find<IVioEx>("VioMeasureL1Start1");
            VioMeasureFinish1 = station.Machine.Find<IVioEx>("VioMeasureL1Finish1");


            if (FrameworkExtenion.IsSimulate)
            {
                Laser = new LineLaserSim();
            }
            else
            {
                Laser = new LmiLaser();
                Laser.Name = "Top";
                Laser.IpStr = "192.168.2.10";
            }

            Platform = station.Machine.Find<PlatformEx>("Laser1Platform");
        }

        protected override int ResetLoop()
        {
            //load settings
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);
            Log($"Load Project {Project.ProjectName}");



            LaserProcess1.Clear();
            LaserProcess1.AddRange(Project.UpLaserPos.FindAll(p => p.Name == "L1"));
            LaserProcess2.Clear();
            LaserProcess2.AddRange(Project.UpLaserPos.FindAll(p => p.Name == "L2"));

            return base.ResetLoop();
        }
    }


    public class LaserMeasureDownTask : LaserMeasureTask
    {
        public LaserMeasureDownTask(int id, string name, Station station) : base(id, name, station)
        {
            VioMeasureStart1 = station.Machine.Find<IVioEx>("VioMeasureL2Start1");
            VioMeasureFinish1 = station.Machine.Find<IVioEx>("VioMeasureL2Finish1");


            if (FrameworkExtenion.IsSimulate)
            {
                Laser = new LineLaserSim();
            }
            else
            {
                Laser = new LmiLaser();
                Laser.Name = "Bottom";
                Laser.IpStr = "192.168.1.10";
            }


            Platform = station.Machine.Find<PlatformEx>("Laser2Platform");
        }

        protected override int ResetLoop()
        {
            //load settings
            CfgSettings = Machine.Ins.Settings;
            Project = MeasureProject.Load(CfgSettings.MeasureProjectFile, typeof(MeasureProjectB)) as MeasureProjectB;
            Project.AssertNoNull(this);

            Log($"Load Project {Project.ProjectName}");




            LaserProcess1.Clear();
            LaserProcess1.AddRange(Project.DownLaserPos.FindAll(p => p.Name == "L1"));
            LaserProcess2.Clear();
            LaserProcess2.AddRange(Project.DownLaserPos.FindAll(p => p.Name == "L2"));

            return base.ResetLoop();
        }
    }
}