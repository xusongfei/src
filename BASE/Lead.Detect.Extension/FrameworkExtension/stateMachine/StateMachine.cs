using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Lead.Detect.FrameworkExtension.common;
using Lead.Detect.FrameworkExtension.elementExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.loadUtils;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;

namespace Lead.Detect.FrameworkExtension.stateMachine
{
    /// <summary>
    /// 设备
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class StateMachine : UserSettings<StateMachine>, IEventHandler
    {
        public event Action<string, LogLevel> ShowAlarmEvent;

        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<int, IDiEx> DiEstop { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiAuto { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiStart { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiStop { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiReset { get; } = new Dictionary<int, IDiEx>();


        public Dictionary<int, IDiEx> DiPauseSignal { get; } = new Dictionary<int, IDiEx>();


        public Dictionary<int, IDoEx> DoLightRed { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IDoEx> DoLightYellow { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IDoEx> DoLightGreen { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IDoEx> DoBuzzer { get; } = new Dictionary<int, IDoEx>();


        public RunState RunState { get; protected set; } = RunState.AUTO;
        public RunningState RunningState { get; protected set; } = RunningState.WaitReset;

        /// <summary>
        /// 加载配置
        /// </summary>
        public virtual void Load()
        {
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public virtual void Save()
        {
        }

        /// <summary>
        /// 初始化, 开启主线程
        /// </summary>
        public virtual void Initialize()
        {
            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo());
            DoLightGreen.All(d => d.Value.SetDo(false));
            DoBuzzer.All(d => d.Value.SetDo(false));

            _isrunning = true;
            _mainThread = new Thread(runStateMachineLoop);
            _mainThread.IsBackground = true;
            _mainThread.Start();
        }


        /// <summary>
        /// 终止, 结束主线程
        /// </summary>
        public virtual void Terminate()
        {
            PostEvent(UserEventType.STOP, this);

            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo(false));
            DoLightGreen.All(d => d.Value.SetDo(false));
            DoBuzzer.All(d => d.Value.SetDo(false));

            //make sure all events handled
            _isrunning = false;
            if (_mainThread.IsAlive)
            {
                _mainThread.Join();
            }
        }

        public void Start()
        {
            PostEvent(UserEventType.START, this);
        }
        public void Stop()
        {
            PostEvent(UserEventType.STOP, this);
        }
        public void Reset()
        {
            PostEvent(UserEventType.RESET, this);
        }

        public void Beep()
        {
            lock (this)
            {
                DoBuzzer.All(d => d.Value.SetDo(true));
                Thread.Sleep(500);
                DoBuzzer.All(d => d.Value.SetDo(false));
            }
        }

        public virtual void OnShowAlarmEvent(string alarm, LogLevel level)
        {
            ShowAlarmEvent?.Invoke(alarm, level);
        }


        #region  user event function

        /// <summary>
        /// 处理设备事件, pass to stations
        /// </summary>
        /// <param name="e"></param>
        public void HandleEvent(UserEvent e)
        {
            //handle event on runstate
            switch (RunState)
            {
                case RunState.ESTOP:
                    if (e.EventType == UserEventType.ESTOPOFF)
                    {
                        PassEvents(e);
                        RunState = RunState.ERROR;
                        RunningState = RunningState.WaitReset;
                    }
                    break;
                case RunState.ERROR:
                    if (e.EventType == UserEventType.ESTOP)
                    {
                        PassEvents(e);
                        RunState = RunState.ESTOP;
                        RunningState = RunningState.WaitReset;
                    }
                    else if (e.EventType == UserEventType.RESET)
                    {
                        PassEvents(e);
                        RunState = RunState.AUTO;
                        RunningState = RunningState.WaitReset;
                    }

                    break;
                case RunState.MANUAL:
                    if (e.EventType == UserEventType.ESTOP)
                    {
                        PassEvents(e);
                        RunState = RunState.ESTOP;
                        RunningState = RunningState.WaitReset;
                    }
                    else if (e.EventType == UserEventType.AUTO)
                    {
                        RunState = RunState.AUTO;
                        RunningState = RunningState.WaitReset;
                    }
                    break;
                case RunState.AUTO:
                    if (e.EventType == UserEventType.ESTOP)
                    {
                        PassEvents(e);
                        RunState = RunState.ESTOP;
                        RunningState = RunningState.WaitReset;
                        return;
                    }

                    //handle event on runstate auto
                    switch (RunningState)
                    {
                        case RunningState.WaitReset:
                            if (e.EventType == UserEventType.MANUAL)
                            {
                                RunState = RunState.MANUAL;
                                RunningState = RunningState.WaitReset;
                            }
                            else if (e.EventType == UserEventType.RESET)
                            {
                                PassEvents(e);
                                RunningState = RunningState.Resetting;
                            }
                            else if (e.EventType == UserEventType.STOP)
                            {
                                PassEvents(e);
                                RunningState = RunningState.WaitReset;
                            }

                            break;
                        case RunningState.Resetting:
                            if (e.EventType == UserEventType.STOP || e.EventType == UserEventType.PAUSE)
                            {
                                PassEvents(e);
                                RunningState = RunningState.WaitReset;
                            }

                            break;
                        case RunningState.WaitRun:
                            if (e.EventType == UserEventType.MANUAL)
                            {
                                RunState = RunState.MANUAL;
                                RunningState = RunningState.WaitReset;
                            }
                            else if (e.EventType == UserEventType.START)
                            {
                                PassEvents(e);
                                RunningState = RunningState.Running;
                            }
                            else if (e.EventType == UserEventType.STOP)
                            {
                                PassEvents(e);
                                RunningState = RunningState.WaitReset;
                            }

                            break;
                        case RunningState.Running:
                            if (e.EventType == UserEventType.PAUSE)
                            {
                                PassEvents(e);
                                RunningState = RunningState.Pause;
                            }
                            else if (e.EventType == UserEventType.STOP)
                            {
                                PassEvents(e);
                                RunningState = RunningState.WaitReset;
                            }

                            break;
                        case RunningState.Pause:
                            if (e.EventType == UserEventType.CONTINUE || e.EventType == UserEventType.START)
                            {
                                PassEvents(e);
                                RunningState = RunningState.Running;
                            }
                            else if (e.EventType == UserEventType.STOP)
                            {
                                PassEvents(e);
                                RunningState = RunningState.WaitReset;
                            }

                            break;
                    }
                    break;
            }
        }

        private void PassEvents(UserEvent e)
        {
            foreach (var station in Stations)
            {
                station.Value.HandleEvent(e);
            }
        }


        public void PostEvent(UserEventType eventType, IEventHandler eventHandler)
        {
            Blink(eventType);
            Light(eventType);

            var e = new UserEvent() {EventType = eventType, EventTarget = eventHandler};
            priorityQueueEvents.Enqueue(1, e);
        }

        private StateMachine Blink(UserEventType e)
        {
            switch (e)
            {
                case UserEventType.START:
                case UserEventType.CONTINUE:
                    DoLightGreen.All(d => d.Value.Toggle());
                    Thread.Sleep(50);
                    DoLightGreen.All(d => d.Value.Toggle());
                    break;
                case UserEventType.STOP:
                case UserEventType.PAUSE:
                    DoLightRed.All(d => d.Value.Toggle());
                    Thread.Sleep(50);
                    DoLightRed.All(d => d.Value.Toggle());
                    break;
                case UserEventType.RESET:
                    DoLightYellow.All(d => d.Value.Toggle());
                    Thread.Sleep(50);
                    DoLightYellow.All(d => d.Value.Toggle());
                    break;
            }

            return this;
        }

        private StateMachine Light(UserEventType e)
        {
            //buzzer
            switch (e)
            {
                case UserEventType.ESTOP:
                    _estopBeepCount = 0;
                    _estopFlashCount = 0;
                    DoBuzzer.All(d => d.Value.SetDo(true));
                    break;
                case UserEventType.ESTOPOFF:
                    _estopBeepCount = 0;
                    _estopFlashCount = 0;
                    DoBuzzer.All(d => d.Value.SetDo(false));
                    break;
                case UserEventType.STOP:
                    //Beep();
                    break;
            }

            //light
            switch (e)
            {
                case UserEventType.START:
                case UserEventType.CONTINUE:
                    _runningFlashCount = 0;
                    DoLightRed.All(d => d.Value.SetDo(false));
                    DoLightYellow.All(d => d.Value.SetDo(false));
                    DoLightGreen.All(d => d.Value.SetDo(true));
                    break;
                case UserEventType.ESTOP:
                case UserEventType.ESTOPOFF:
                case UserEventType.STOP:
                case UserEventType.AUTO:
                    DoLightRed.All(d => d.Value.SetDo(true));
                    DoLightYellow.All(d => d.Value.SetDo(false));
                    DoLightGreen.All(d => d.Value.SetDo(false));
                    break;
                case UserEventType.PAUSE:
                    DoLightRed.All(d => d.Value.SetDo(false));
                    DoLightYellow.All(d => d.Value.SetDo(false));
                    DoLightGreen.All(d => d.Value.SetDo(true));
                    break;
                case UserEventType.RESET:
                case UserEventType.MANUAL:
                    _resettingFlashCount = 0;
                    DoLightRed.All(d => d.Value.SetDo(false));
                    DoLightYellow.All(d => d.Value.SetDo(true));
                    DoLightGreen.All(d => d.Value.SetDo(false));
                    break;
            }

            return this;
        }

        private ConcurrentPriorityQueue<int, UserEvent> priorityQueueEvents = new ConcurrentPriorityQueue<int, UserEvent>();

        #endregion


        #region  machine state mechanism

        private Thread _mainThread;
        private bool _isrunning;

        /// <summary>
        /// 主线程函数
        /// </summary>
        private void runStateMachineLoop()
        {
            while (_isrunning || priorityQueueEvents.Count > 0)
            {
                runRunStateMachine();

                foreach (var station in Stations)
                {
                    station.Value.runRunStateMachine();
                }

                while (priorityQueueEvents.Count > 0)
                {
                    KeyValuePair<int, UserEvent> e;
                    if (priorityQueueEvents.TryDequeue(out e))
                    {
                        e.Value.Execute();
                    }
                }

                Thread.Sleep(16);
            }
        }

        private int _estopBeepCount;
        private int _estopFlashCount;
        private int _resettingFlashCount;
        private int _runningFlashCount;
        /// <summary>
        /// 更新设备状态
        /// </summary>
        private void runRunStateMachine()
        {
            switch (RunState)
            {
                case RunState.ESTOP:
                    if (DiEstop.Count > 0 && DiEstop.All(e => !e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.ESTOPOFF, this);
                        return;
                    }

                    //stop estop buzzer on timeout
                    if (_estopBeepCount++ > 100)
                    {
                        _estopBeepCount = 100;
                        DoBuzzer.All(d => d.Value.SetDo(false));
                    }
                    if (_estopFlashCount++ > 40)
                    {
                        _estopFlashCount = 0;
                        DoLightRed.All(d => d.Value.Toggle());
                        DoLightYellow.All(d => d.Value.SetDo(false));
                        DoLightGreen.All(d => d.Value.SetDo(false));
                    }
                    break;

                case RunState.ERROR:
                    if (DiEstop.Count > 0 && DiEstop.Any(e => e.Value.GetDiSts()))
                    {
                        _estopBeepCount = 0;
                        PostEvent(UserEventType.ESTOP, this);
                        OnShowAlarmEvent($"{RunState} 急停信号 {string.Join(",", DiEstop.Select(d => d.Value.Description))} 某一信号触发", LogLevel.Error);
                        return;
                    }
                    else if (DiReset.Count > 0 && DiReset.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.RESET, this);
                        return;
                    }
                    break;

                case RunState.MANUAL:
                    if (DiEstop.Count > 0 && DiEstop.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.ESTOP, this);
                        OnShowAlarmEvent($"{RunState} 急停信号 {string.Join(",", DiEstop.Select(d => d.Value.Description))} 某一信号触发", LogLevel.Error);
                        return;
                    }
                    else if (DiAuto.Count > 0 && DiAuto.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.AUTO, this);
                        return;
                    }

                    if (_resettingFlashCount++ > 40)
                    {
                        _resettingFlashCount = 0;
                        DoLightRed.All(d => d.Value.SetDo(false));
                        DoLightYellow.All(d => d.Value.Toggle());
                        DoLightGreen.All(d => d.Value.SetDo(false));
                    }
                    break;

                case RunState.AUTO:
                    if (DiEstop.Count > 0 && DiEstop.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.ESTOP, this);
                        OnShowAlarmEvent($"{RunState} 急停信号 {string.Join(",", DiEstop.Select(d => d.Value.Description))} 某一信号触发", LogLevel.Error);
                        return;
                    }
                    runRunningStateMachine();
                    break;
            }
        }

        private int _pauseStoppingCount;
        private bool _isPauseSignal;
        private void runRunningStateMachine()
        {
            switch (RunningState)
            {
                case RunningState.WaitReset:
                    //check user event
                    if (DiReset.Count > 0 && DiReset.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.RESET, this);
                        OnShowAlarmEvent(string.Empty, LogLevel.None);
                        return;
                    }
                    else if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.STOP, this);
                        return;
                    }
                    else if (DiAuto.Count > 0 && !DiAuto.All(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.MANUAL, this);
                        return;
                    }

                    //pull up stations state
                    if (Stations.All(s => !s.Value.Enable || s.Value.RunningState == RunningState.Resetting))
                    {
                        _resettingFlashCount = 0;
                        Light(UserEventType.RESET);
                        RunningState = RunningState.Resetting;
                        return;
                    }
                    break;

                case RunningState.Resetting:
                    //check user event
                    if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.STOP, this);
                        return;
                    }
                    else if (DiPauseSignal.Count > 0 && DiPauseSignal.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.STOP, this);
                        OnShowAlarmEvent($"{RunState} 安全信号 {string.Join(",", DiPauseSignal.Select(d => d.Value.Description))} 某一信号触发", LogLevel.Error);
                        return;
                    }

                    //pull up stations state
                    if (Stations.All(s => !s.Value.Enable || s.Value.RunningState == RunningState.WaitRun))
                    {
                        Light(UserEventType.START);
                        RunningState = RunningState.WaitRun;
                        return;
                    }
                    else if (Stations.Any(s => s.Value.Enable && s.Value.RunningState == RunningState.WaitReset))
                    {
                        //some station not reset success
                        PostEvent(UserEventType.STOP, this);
                        Beep();
                        return;
                    }

                    if (_resettingFlashCount++ > 40)
                    {
                        _resettingFlashCount = 0;
                        DoLightRed.All(d => d.Value.SetDo(false));
                        DoLightYellow.All(d => d.Value.Toggle());
                        DoLightGreen.All(d => d.Value.SetDo(false));
                    }
                    break;

                case RunningState.WaitRun:
                    //check user event
                    if (DiStart.Count > 0 && DiStart.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.START, this);
                        OnShowAlarmEvent(string.Empty, LogLevel.None);
                        return;
                    }
                    else if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.STOP, this);
                        return;
                    }
                    else if (DiAuto.Count > 0 && !DiAuto.All(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.MANUAL, this);
                        return;
                    }

                    //pull up stations state
                    if (Stations.All(s => !s.Value.Enable || s.Value.RunningState == RunningState.Running))
                    {
                        _runningFlashCount = 0;
                        Light(UserEventType.START);
                        RunningState = RunningState.Running;
                    }
                    else if (Stations.Any(s => s.Value.Enable && s.Value.RunningState == RunningState.WaitReset))
                    {
                        PostEvent(UserEventType.STOP, this);
                        return;
                    }
                    break;

                case RunningState.Running:
                    //check user event
                    if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        //stop button to pause, need manual continue
                        _isPauseSignal = false;
                        PostEvent(UserEventType.PAUSE, this);
                        return;
                    }
                    else if (DiPauseSignal.Count > 0 && DiPauseSignal.Any(e => e.Value.GetDiSts()))
                    {
                        //pause button to pause, auto continue when reset
                        _isPauseSignal = true;
                        PostEvent(UserEventType.PAUSE, this);
                        OnShowAlarmEvent($"{RunState} 暂停信号 {string.Join(",", DiPauseSignal.Values.Select(d => d.Description))} 某一信号触发", LogLevel.Warning);
                        return;
                    }

                    //pull up stations state
                    if (Stations.Any(s => s.Value.Enable && s.Value.RunningState == RunningState.WaitReset))
                    {
                        //some station run error
                        PostEvent(UserEventType.STOP, this);
                        Beep();
                        return;
                    }
                    else if (Stations.Any(s => s.Value.Enable && s.Value.RunningState == RunningState.Pause))
                    {
                        //some station pause
                        Light(UserEventType.PAUSE);
                        RunningState = RunningState.Pause;
                        return;
                    }

                    if (_runningFlashCount++ > 40)
                    {
                        _runningFlashCount = 0;
                        DoLightRed.All(d => d.Value.SetDo(false));
                        DoLightYellow.All(d => d.Value.SetDo(false));
                        DoLightGreen.All(d => d.Value.Toggle());
                    }
                    break;

                case RunningState.Pause:
                    //long press stop to change pause to stop
                    if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        if (_pauseStoppingCount++ > 30)
                        {
                            PostEvent(UserEventType.STOP, this);
                            return;
                        }
                    }
                    else
                    {
                        _pauseStoppingCount = 0;
                    }

                    //check user event
                    if (_isPauseSignal && DiPauseSignal.Count > 0 && DiPauseSignal.All(e => !e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.CONTINUE, this);
                        OnShowAlarmEvent(string.Empty, LogLevel.None);
                        return;
                    }
                    else if (DiStart.Count > 0 && DiStart.Any(e => e.Value.GetDiSts()))
                    {
                        PostEvent(UserEventType.CONTINUE, this);
                        return;
                    }
                    else if (DiReset.Count > 0 && DiReset.Any(e => e.Value.GetDiSts()))
                    {
                        //press reset to change pause to stop
                        PostEvent(UserEventType.STOP, this);
                        return;
                    }

                    //pull up stations state
                    if (Stations.All(s => !s.Value.Enable || s.Value.RunningState == RunningState.Running))
                    {
                        _runningFlashCount = 0;
                        OnShowAlarmEvent(string.Empty, LogLevel.None);
                        RunningState = RunningState.Running;
                    }
                    else if (Stations.Any(s => s.Value.Enable && s.Value.RunningState == RunningState.WaitReset))
                    {
                        //if any station run fail
                        PostEvent(UserEventType.STOP, this);
                        return;
                    }
                    break;
            }
        }

        #endregion

        #region  resource manage

        public Dictionary<int, IMotionWrapper> MotionExs { get; } = new Dictionary<int, IMotionWrapper>();
        public Dictionary<int, IDiEx> DiExs { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDoEx> DoExs { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IVioEx> VioExs { get; } = new Dictionary<int, IVioEx>();
        public Dictionary<int, ICylinderEx> CylinderExs { get; } = new Dictionary<int, ICylinderEx>();
        public Dictionary<int, IAxisEx> AxisExs { get; } = new Dictionary<int, IAxisEx>();
        public Dictionary<int, PlatformEx> Platforms { get; } = new Dictionary<int, PlatformEx>();


        public Dictionary<int, Station> Stations { get; } = new Dictionary<int, Station>();
        public Dictionary<int, StationTask> Tasks { get; } = new Dictionary<int, StationTask>();


        public T Find<T>(string name) where T : class
        {
            var typeName = typeof(T).Name;
            switch (typeName)
            {
                case nameof(IMotionWrapper):
                    return MotionExs.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(IDiEx):
                    return DiExs.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(IDoEx):
                    return DoExs.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(IVioEx):
                    return VioExs.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(ICylinderEx):
                    return CylinderExs.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(IAxisEx):
                    return AxisExs.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(PlatformEx):
                    return Platforms.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(Station):
                    return Stations.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                case nameof(StationTask):
                    return Tasks.FirstOrDefault(ex => ex.Value.Name == name).Value as T;
                default:
                    return null;
            }
        }

        #endregion

        public void Import(string file = @".\Config\machine.cfg")
        {
            this.Deserialize(file);
        }

        public void Export(string file = @".\Config\machine.cfg")
        {
            this.Serialize(file);
        }

        public override bool CheckIfNormal()
        {
            return true;
        }

        public override string ToString()
        {
            return $"{Name} {Description} ESTOP {DiEstop.Count} START {DiStart.Count} STOP {DiStop.Count} RESET {DiReset.Count} DI {DiExs.Count} DO {DoExs.Count} VIO {VioExs.Count} CY {CylinderExs.Count} AXIS {AxisExs.Count} PLATFORM {Platforms.Count} STATION {Stations.Count} TASK {Tasks.Count}";
        }
    }


    public static class MachineExtension
    {
        public static T TryCast<T>(this string name, StateMachine machine) where T : class
        {
            if (typeof(T) == typeof(MotionCardWrapper))
            {
                return machine.MotionExs.First(m => m.Value.Name == name) as T;
            }
            else if (typeof(T) == typeof(DiEx))
            {
                return machine.DiExs.First(m => m.Value.Name == name) as T;
            }
            else if (typeof(T) == typeof(DoEx))
            {
                return machine.DoExs.First(m => m.Value.Name == name) as T;
            }
            else if (typeof(T) == typeof(CylinderEx))
            {
                return machine.CylinderExs.First(m => m.Value.Name == name) as T;
            }
            else if (typeof(T) == typeof(AxisEx))
            {
                return machine.AxisExs.First(m => m.Value.Name == name) as T;
            }

            return null;
        }
    }
}