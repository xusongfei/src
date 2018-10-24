using System.Collections.Generic;
using System.Linq;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.Helper;
using Lead.Detect.FrameworkExtension.elementExtension;
using System;
using Lead.Detect.FrameworkExtension.frameworkManage;

namespace Lead.Detect.FrameworkExtension.stateMachine
{
    /// <summary>
    /// 工站
    /// </summary>
    public sealed class Station : BaseObject, IEventHandler, IElement
    {
        public Station(int id, string name, StateMachine machine)
        {
            Id = id;
            Name = name;

            //append to machine
            Machine = machine;
            machine.Stations.Add(id, this);

            //默认状态
            State = StationState.AUTO;
            AutoState = StationAutoState.WaitReset;
        }

        /// <summary>
        /// 设备实例
        /// </summary>
        public StateMachine Machine { get; }

        /// <summary>
        /// 工站使能，todo: not tested
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 自动状态, handle start/stop/reset event only on auto
        /// </summary>
        public StationState State { get; private set; }
        /// <summary>
        /// 运行状态, handle event based on auto state
        /// </summary>
        public StationAutoState AutoState { get; private set; }

        /// <summary>
        /// 工站附属任务
        /// </summary>
        public Dictionary<int, StationTask> Tasks { get; } = new Dictionary<int, StationTask>();

        /// <summary>
        /// 常开的暂停信号
        /// </summary>
        public Dictionary<int, IDiEx> PauseSignals { get; } = new Dictionary<int, IDiEx>();


        /// <summary>
        /// 根据工站状态处理 start/stop/reset 事件: update state by events and pass to tasks
        /// </summary>
        /// <param name="e"></param>
        public void HandleEvent(UserEvent e)
        {
            if (!Enable)
            {
                return;
            }

            switch (e.EventType)
            {
                case UserEventType.START:
                    if (State == StationState.AUTO && AutoState == StationAutoState.WaitRun)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Running;
                    }
                    break;
                case UserEventType.STOP:
                    if (State == StationState.AUTO &&
                        (AutoState == StationAutoState.Running
                         || AutoState == StationAutoState.Resetting
                         || AutoState == StationAutoState.Pause
                         || AutoState == StationAutoState.WaitRun))
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.WaitReset;
                    }
                    break;
                case UserEventType.RESET:
                    if (State == StationState.AUTO && AutoState == StationAutoState.WaitReset)
                    {
                        //resetting orders matters
                        foreach (var t in Tasks)
                        {
                            t.Value.RefreshState(TaskState.Resetting);   
                        }

                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Resetting;
                    }
                    break;
                case UserEventType.PAUSE:
                    if (State == StationState.AUTO && AutoState == StationAutoState.Running)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Pause;
                    }
                    break;
                case UserEventType.CONTINUE:
                    if (State == StationState.AUTO && AutoState == StationAutoState.Pause)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Running;
                    }
                    break;
            }
        }

        private bool _isPauseSignal = false;
        /// <summary>
        /// 更新工站状态
        /// </summary>
        public void runningStateMachine()
        {
            if (!Enable)
            {
                return;
            }

            if (State == StationState.AUTO)
            {
                switch (AutoState)
                {
                    case StationAutoState.Pause:
                        if (_isPauseSignal && PauseSignals.Count > 0 && PauseSignals.All(e => !e.Value.GetDiSts()))
                        {
                            _isPauseSignal = false;
                            foreach (var t in Tasks)
                            {
                                t.Value.Continue();
                            }
                            AutoState = StationAutoState.Running;
                        }

                        //pull up task states
                        if (Tasks.Any(t => t.Value.State == TaskState.WaitReset))
                        {
                            //stop this station
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }
                        else if (Tasks.All(t => t.Value.State == TaskState.WaitRun))
                        {
                            AutoState = StationAutoState.WaitRun;
                        }
                        break;


                    case StationAutoState.Running:
                        if (PauseSignals.Count > 0 && PauseSignals.Any(e => e.Value.GetDiSts()))
                        {
                            foreach (var t in Tasks)
                            {
                                t.Value.Pause();
                            }
                            _isPauseSignal = true;
                            AutoState = StationAutoState.Pause;
                        }

                        //pull up task states
                        if (Tasks.Any(t => t.Value.State == TaskState.WaitReset))
                        {
                            //stop this station
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }
                        break;

                    case StationAutoState.Resetting:
                        if (PauseSignals.Count > 0 && PauseSignals.Any(e => e.Value.GetDiSts()))
                        {
                            //todo: show alarm
                            Machine.Beep();
                            ShowAlarm($"{Name} 安全信号{string.Join(",", PauseSignals.Select(d => d.Value.Description))}触发", LogLevel.Error);
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }


                        //pull up task states
                        if (Tasks.Any(t => t.Value.State == TaskState.WaitReset))
                        {
                            //stop this station
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }
                        else if (Tasks.All(t => t.Value.State == TaskState.WaitRun))
                        {
                            AutoState = StationAutoState.WaitRun;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 报警函数
        /// </summary>
        /// <param name="s"></param>
        /// <param name="level"></param>
        public void ShowAlarm(string s, LogLevel level)
        {
            //push alarm to machine to show alarm
            Machine.OnAlarmEvent(s, level);

            //pull task alarm to this station
            if (level == LogLevel.Error || level == LogLevel.Fatal)
            {
                Machine.PostEvent(new UserEvent() { EventType = UserEventType.STOP, EventTarget = this });
                Machine.Beep();
            }
            else if (level == LogLevel.Warning)
            {
                Machine.PostEvent(new UserEvent() { EventType = UserEventType.PAUSE, EventTarget = this });
            }
        }


        public override string ToString()
        {
            return $"{Name} {Id} {Description}";
        }

        public string Export()
        {
            return $"{Name} {Id} {Description} BEGIN\r\n"
                + $"\t\t{string.Join("\r\n\t\t", PauseSignals.Select(t => $"{t.Key} {t.Value.Export()}"))}\r\n"
                + $"\t\t{string.Join("\r\n\t\t", Tasks.Select(t => $"{t.Key} {t.Value.Export()}"))}\r\n\t{Name} END";

        }

        public void Import(string line, StateMachine machine)
        {
            throw new NotImplementedException();
        }

        public static void Import(string type, string line, StateMachine machine)
        {
            var data = line.Split(' ');

            switch (type)
            {
                case "STATION":
                    if (data.Length == 5)
                    {
                        //load station
                        int i = 0;
                        var id = int.Parse(data[i++]);
                        var name = data[i++];
                        var index = int.Parse(data[i++]);
                        var desc = data[i++];

                        var station = new Station(id, name, machine);
                        if (machine.Stations.ContainsKey(id))
                        {
                            return;
                        }
                        machine.Stations.Add(id, station);
                    }
                    break;
            }
        }
        public static void Import(string type, string line, Station station)
        {

            switch (type)
            {
                case "STATIONTASK":
                    {
                        var data = line.Split(' ');
                        int i = 0;
                        var id = int.Parse(data[i++]);
                        var name = data[i++];
                        var index = int.Parse(data[i++]);
                        var desc = data[i++];
                        var stationName = data[i++];
                        var typeName = data[i++];

                        //create task and bind to station
                        if (FrameworkUserTypeManager.TaskTypes.ContainsKey(typeName))
                        {
                            var task = Activator.CreateInstance(FrameworkUserTypeManager.TaskTypes[typeName], new object[] { id, name, station });
                            //bind to machine
                            station.Machine.Tasks.Add(id, task as StationTask);
                        }
                    }
                    break;

                case "PAUSESIGNAL":
                    {
                        var data = line.Split(' ');
                        if (data.Length == 7)
                        {
                            //load pause signal
                            IDiEx di = new DiEx();
                            di.Import(line, station.Machine);
                            di = station.Machine.Find<IDiEx>(di.Name);
                            station.PauseSignals.Add(station.PauseSignals.Count + 1, di);
                        }
                    }
                    break;

            }
        }
    }
}