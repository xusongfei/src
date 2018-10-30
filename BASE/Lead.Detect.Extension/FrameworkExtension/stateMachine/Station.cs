using System.Collections.Generic;
using System.Linq;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.elementExtension;
using System;
using System.Text;
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
            RunState = RunState.AUTO;
            RunningState = RunningState.WaitReset;
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
        public RunState RunState { get; private set; }
        /// <summary>
        /// 运行状态, handle event based on auto state
        /// </summary>
        public RunningState RunningState { get; private set; }

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

            switch (RunState)
            {
                case RunState.ESTOP:
                    break;
                case RunState.ERROR:
                    break;
                case RunState.MANUAL:
                    break;

                case RunState.AUTO:
                    if (e.EventType == UserEventType.ESTOP)
                    {
                        PassEvent(e);
                        RunningState = RunningState.WaitReset;
                        return;
                    }
                    switch (RunningState)
                    {
                        case RunningState.WaitReset:
                            if (e.EventType == UserEventType.RESET)
                            {
                                //reset to waitreset
                                foreach (var t in Tasks)
                                {
                                    t.Value.RefreshState(RunningState.WaitReset);
                                }
                                PassEvent(e);
                                RunningState = RunningState.Resetting;
                            }
                            else if (e.EventType == UserEventType.STOP || e.EventType == UserEventType.MANUAL)
                            {
                                PassEvent(e);
                                RunningState = RunningState.WaitReset;
                            }
                            break;

                        case RunningState.Resetting:
                            if (e.EventType == UserEventType.STOP)
                            {
                                PassEvent(e);
                                RunningState = RunningState.WaitReset;
                            }
                            break;

                        case RunningState.WaitRun:
                            if (e.EventType == UserEventType.START)
                            {
                                //reset to waitrun
                                foreach (var t in Tasks)
                                {
                                    t.Value.RefreshState(RunningState.WaitRun);
                                }
                                PassEvent(e);
                                RunningState = RunningState.Running;
                            }
                            else if (e.EventType == UserEventType.STOP || e.EventType == UserEventType.MANUAL)
                            {
                                PassEvent(e);
                                RunningState = RunningState.WaitReset;
                            }
                            break;

                        case RunningState.Running:
                            if (e.EventType == UserEventType.PAUSE)
                            {
                                PassEvent(e);
                                RunningState = RunningState.Pause;
                            }
                            else if (e.EventType == UserEventType.STOP)
                            {
                                PassEvent(e);
                                RunningState = RunningState.WaitReset;
                            }
                            break;
                        case RunningState.Pause:
                            if (e.EventType == UserEventType.START)
                            {
                                PassEvent(e);
                                RunningState = RunningState.Running;
                            }
                            else if (e.EventType == UserEventType.CONTINUE)
                            {
                                PassEvent(e);
                                RunningState = RunningState.Running;
                            }
                            else if (e.EventType == UserEventType.STOP)
                            {
                                PassEvent(e);
                                RunningState = RunningState.WaitReset;
                            }
                            break;
                    }
                    break;

            }


        }

        private void PassEvent(UserEvent e)
        {
            foreach (var t in Tasks)
            {
                t.Value.HandleEvent(e);
            }
        }

        private bool _isPauseSignal = false;

        /// <summary>
        /// 更新工站状态
        /// </summary>
        public void runRunStateMachine()
        {
            if (!Enable)
            {
                return;
            }

            switch (RunState)
            {
                case RunState.ESTOP:
                    break;
                case RunState.ERROR:
                    break;
                case RunState.MANUAL:
                    break;
                case RunState.AUTO:
                    switch (RunningState)
                    {
                        case RunningState.WaitReset:
                            if (Tasks.All(t => t.Value.RunningState == RunningState.Resetting))
                            {
                                RunningState = RunningState.Resetting;
                                return;
                            }
                            break;
                        case RunningState.Resetting:
                            //handle pause signal event
                            if (PauseSignals.Count > 0 && PauseSignals.Any(e => e.Value.GetDiSts()))
                            {
                                ShowAlarm($"{Name} 安全信号{string.Join(",", PauseSignals.Select(d => d.Value.Description))} 某一安全信号触发", LogLevel.Error);
                                Machine.PostEvent(UserEventType.STOP, this);
                                return;
                            }

                            //pull up task states
                            if (Tasks.Any(t => t.Value.RunningState == RunningState.WaitReset))
                            {
                                //stop this station
                                Machine.PostEvent(UserEventType.STOP, this);
                                return;
                            }
                            else if (Tasks.All(t => t.Value.RunningState == RunningState.WaitRun))
                            {
                                RunningState = RunningState.WaitRun;
                                return;
                            }
                            break;

                        case RunningState.WaitRun:
                            if (Tasks.All(t => t.Value.RunningState == RunningState.Running))
                            {
                                RunningState = RunningState.Running;
                                return;
                            }
                            break;
                        case RunningState.Running:
                            //handle pause signal event
                            if (PauseSignals.Count > 0 && PauseSignals.Any(e => e.Value.GetDiSts()))
                            {
                                _isPauseSignal = true;
                                //ShowAlarm($"{Name} 暂停信号{string.Join(",", PauseSignals.Select(d => d.Value.Description))} 某一暂停信号触发", LogLevel.Warning);
                                Machine.PostEvent(UserEventType.PAUSE, this);
                                return;
                            }

                            //pull up task states
                            if (Tasks.Any(t => t.Value.RunningState == RunningState.WaitReset))
                            {
                                Machine.PostEvent(UserEventType.STOP, this);
                                return;
                            }
                            break;

                        case RunningState.Pause:
                            //handle pause signal event
                            if (_isPauseSignal && PauseSignals.Count > 0 && PauseSignals.All(e => !e.Value.GetDiSts()))
                            {
                                _isPauseSignal = false;
                                Machine.PostEvent(UserEventType.CONTINUE, this);
                                //ShowAlarm(string.Empty, LogLevel.None);
                                return;
                            }

                            //pull up task states
                            if (Tasks.Any(t => t.Value.RunningState == RunningState.WaitReset))
                            {
                                Machine.PostEvent(UserEventType.STOP, this);
                                return;
                            }
                            else if (Tasks.All(t => t.Value.RunningState == RunningState.Running))
                            {
                                RunningState = RunningState.Running;
                                return;
                            }
                            break;
                    }

                    break;
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
            Machine.OnShowAlarmEvent(s, level);

            //pull task alarm to this station
            if (level == LogLevel.Error || level == LogLevel.Fatal)
            {
                Machine.PostEvent(UserEventType.STOP, this);
                Machine.Beep();
            }
            else if (level == LogLevel.Warning)
            {
                Machine.PostEvent(UserEventType.PAUSE, this);
            }
        }


        public override string ToString()
        {
            return $"{Name} {Id} {Description}";
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

        public string Export()
        {
            var sb = new StringBuilder();

            sb.Append($"{Name} {Id} {Description} BEGIN\r\n");
            sb.Append($"\t\t{string.Join("\r\n\t\t", PauseSignals.Select(s => $"{s.Key} {s.Value.Export()}"))}\r\n");
            sb.Append($"\t\t{string.Join("\r\n\t\t", Tasks.Select(t => $"{t.Key} {t.Value.Export()}"))}\r\n");
            sb.Append($"{Name} END");

            return sb.ToString();

        }
    }
}