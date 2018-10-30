using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;

namespace Lead.Detect.FrameworkExtension.stateMachine
{
    /// <summary>
    /// 工站任务
    /// </summary>
    public abstract class StationTask : BaseObject, IEventHandler, IElement
    {
        public event Action<string, LogLevel> LogEvent;

        public Station Station { get; set; }

        protected StationTask(int id, string name, Station station)
        {
            Id = id;
            Name = name;

            //append to station
            Station = station;
            if (station != null)
            {
                station.Tasks.Add(id, this);
                LogEvent += station.ShowAlarm;
            }

            //默认状态
            RunningState = RunningState.WaitReset;
        }


        #region task state machine run

        private Task _task;

        /// <summary>
        /// 任务内部状态
        /// </summary>
        public RunningState RunningState { get; protected set; }


        /// <summary>
        /// 处理设备事件 start/stop/reset/etc
        /// </summary>
        /// <param name="e"></param>
        public void HandleEvent(UserEvent e)
        {
            if (e.EventType == UserEventType.ESTOP)
            {
                Stop();
                RunningState = RunningState.WaitReset;
            }

            switch (RunningState)
            {
                case RunningState.WaitReset:
                    if (e.EventType == UserEventType.RESET)
                    {
                        Reset();
                    }
                    else if (e.EventType == UserEventType.STOP || e.EventType == UserEventType.MANUAL)
                    {
                        Stop();
                    }
                    break;
                case RunningState.Resetting:
                    if (e.EventType == UserEventType.STOP)
                    {
                        Stop();
                    }
                    break;

                case RunningState.WaitRun:
                    if (e.EventType == UserEventType.START)
                    {
                        Start();
                    }
                    else if (e.EventType == UserEventType.STOP || e.EventType == UserEventType.MANUAL)
                    {
                        Stop();
                    }
                    break;
                case RunningState.Running:
                    if (e.EventType == UserEventType.PAUSE)
                    {
                        RunningState = RunningState.Pause;
                    }
                    else if (e.EventType == UserEventType.STOP)
                    {
                        Stop();
                    }
                    break;

                case RunningState.Pause:
                    if (e.EventType == UserEventType.START)
                    {
                        RunningState = RunningState.Running;
                    }
                    else if (e.EventType == UserEventType.CONTINUE)
                    {
                        RunningState = RunningState.Running;
                    }
                    else if (e.EventType == UserEventType.STOP)
                    {
                        Stop();
                    }
                    break;
            }
        }

        /// <summary>
        /// 启动，开启运行线程
        /// </summary>
        public void Start()
        {
            if (_task != null)
            {
                //should not run to here
                Debug.Assert(_task == null);
                ThrowException($"{Name} not Stop Normally!");
            }

            RunningState = RunningState.Running;
            _task = Task.Run(new Action(RunningLoopTask));
        }

        /// <summary>
        /// 停止，触发停止信号
        /// </summary>
        public void Stop()
        {
            RunningState = RunningState.WaitReset;
            if (_task != null)
            {
                _task.Wait();
                _task = null;
            }
        }

        /// <summary>
        /// 复位，开启复位线程
        /// </summary>
        public void Reset()
        {
            if (_task != null)
            {
                //should not run to here
                Debug.Assert(_task == null);
                ThrowException($"{Name} not Stop Normally!");
            }

            RunningState = RunningState.Resetting;
            _task = Task.Run(new Action(ResettingLoopTask));
        }

        /// <summary>
        /// only called in station
        /// </summary>
        /// <param name="state"></param>
        public void RefreshState(RunningState state)
        {
            RunningState = state;
        }


        /// <summary>
        /// 复位线程
        /// </summary>
        private void ResettingLoopTask()
        {
            try
            {
                RunningState = RunningState.Resetting;

                Log($"{Name} ResetLoop Start...", LogLevel.Info);
                ResetLoop();
                Log($"{Name} ResetLoop Finish", LogLevel.Info);

                RunningState = RunningState.WaitRun;
            }
            catch (Exception ex)
            {
                if (ex is TaskCancelException)
                {
                    Log($"复位取消:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Warning);
                }
                else
                {
                    Log($"复位异常:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Error);
                }

                RunningState = RunningState.WaitReset;
                Station.Machine.PostEvent(UserEventType.STOP, Station);
            }
            finally
            {
                _task = null;
            }
        }

        /// <summary>
        /// 运行线程
        /// </summary>
        private void RunningLoopTask()
        {
            try
            {
                RunningState = RunningState.Running;

                while (RunningState == RunningState.Running || RunningState == RunningState.Pause)
                {
                    Log($"{Name} RunLoop Start...", LogLevel.Info);
                    if (RunLoop() != 0)
                    {
                        Log($"{Name} RunLoop Break", LogLevel.Info);
                        break;
                    }

                    Log($"{Name} RunLoop Finish", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                if (ex is TaskCancelException)
                {
                    Log($"运行取消:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Warning);
                }
                else
                {
                    Log($"运行异常:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Error);
                }

                RunningState = RunningState.WaitReset;
                Station.Machine.PostEvent(UserEventType.STOP, Station);
            }
            finally
            {
                RunningState = RunningState.WaitReset;
                _task = null;
            }
        }

        #endregion

        #region process controls

        /// <summary>
        /// 复位过程函数， run once
        /// </summary>
        /// <returns></returns>
        protected virtual int ResetLoop()
        {
            return 0;
        }


        /// <summary>
        /// 运行过程函数， run in while loop， return -1 to exit; return 0 to restart from begin
        /// </summary>
        /// <returns></returns>
        protected virtual int RunLoop()
        {
            return 0;
        }

        /// <summary>
        /// logevent invoker
        /// Warning log 将触发工站暂停信号
        /// Error log 将触发工站停止信号
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level"></param>
        public virtual void Log(string log, LogLevel level = LogLevel.Debug)
        {
            LogEvent?.Invoke(log, level);
        }

        /// <summary>
        /// 当暂停信号触发，卡住线程在此函数，暂停信号取消继续执行；
        /// 当暂停时，且停止信号触发，抛出任务取消异常
        /// </summary>
        public void JoinIfPause()
        {
            if (RunningState == RunningState.Pause)
            {
                while (RunningState == RunningState.Pause)
                {
                    AbortIfCancel(Name);
                    Thread.Sleep(1);
                }
                AbortIfCancel(Name);
            }
        }

        /// <summary>
        /// 当停止信号触发，抛出任务取消异常，来退出线程执行
        /// </summary>
        /// <param name="msg"></param>
        public void AbortIfCancel(string msg)
        {
            if (RunningState == RunningState.WaitReset)
            {
                throw new TaskCancelException(this, msg);
            }
        }

        /// <summary>
        /// 抛出任务错误异常，退出线程执行
        /// </summary>
        /// <param name="msg"></param>
        public void ThrowException(string msg = null)
        {
            throw new Exception(msg);
        }

        #endregion

        public override string ToString()
        {
            return $"{Name} {Id} {Description ?? string.Empty} {this.GetType().Name}";
        }

        public string Export()
        {
            return $"{Name} {Id} {Description ?? string.Empty} {Station.Name} {GetType().Name}";
        }

        public void Import(string line, StateMachine machine)
        {
            throw new NotImplementedException();
        }
    }
}