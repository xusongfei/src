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
            IsRunning = false;
            IsPause = false;
            State = TaskState.None;
        }


        #region task state machine run

        /// <summary>
        /// 处理设备事件 start/stop/reset/etc
        /// </summary>
        /// <param name="e"></param>
        public void HandleEvent(UserEvent e)
        {
            switch (e.EventType)
            {
                case UserEventType.START:
                    Start();
                    break;
                case UserEventType.STOP:
                    Stop();
                    break;
                case UserEventType.RESET:
                    Reset();
                    break;
                case UserEventType.PAUSE:
                    Pause();
                    break;
                case UserEventType.CONTINUE:
                    Continue();
                    break;
            }
        }

        /// <summary>
        /// 启动，开启运行线程
        /// </summary>
        public void Start()
        {
            if (IsRunning || IsPause)
            {
                return;
            }

            if (_task != null)
            {
                //should not run to here
                Debug.Assert(_task == null);
                ThrowException($"{Name} not Stop Normally!");
            }

            _task = Task.Run(new Action(Running));
        }

        /// <summary>
        /// 停止，触发停止信号
        /// </summary>
        public void Stop()
        {
            if (IsRunning || IsPause)
            {
                IsRunning = false;
                if (_task != null)
                {
                    _task.Wait();
                    _task = null;
                }

                IsPause = false;
            }
        }

        /// <summary>
        /// 暂停，触发暂停信号
        /// </summary>
        public void Pause()
        {
            if (IsRunning)
            {
                IsPause = true;
            }
        }

        /// <summary>
        /// 继续，取消暂停信号
        /// </summary>
        public void Continue()
        {
            if (IsRunning && IsPause)
            {
                IsPause = false;
            }
        }

        /// <summary>
        /// 复位，开启复位线程
        /// </summary>
        public void Reset()
        {
            if (IsRunning || IsPause)
            {
                return;
            }

            if (_task != null)
            {
                //should not run to here
                Debug.Assert(_task == null);
                ThrowException($"{Name} not Stop Normally!");
            }

            State = TaskState.Resetting;
            _task = Task.Run(new Action(Resetting));
        }

        private Task _task;

        /// <summary>
        /// 运行或停止信号
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        /// 暂停信号
        /// </summary>
        public bool IsPause { get; protected set; }

        /// <summary>
        /// 任务内部状态
        /// </summary>
        public TaskState State { get; protected set; }


        /// <summary>
        /// only called in station
        /// </summary>
        /// <param name="state"></param>
        public void RefreshState(TaskState state)
        {
            if (State == TaskState.WaitReset && state == TaskState.Resetting)
            {
                if (IsRunning || IsPause)
                {
                    return;
                }

                State = state;
            }
        }


        /// <summary>
        /// 复位线程
        /// </summary>
        private void Resetting()
        {
            try
            {
                State = TaskState.Resetting;
                IsRunning = true;

                Log($"{Name} ResetLoop Start...", LogLevel.Debug);
                ResetLoop();
                Log($"{Name} ResetLoop Finish", LogLevel.Debug);

                State = TaskState.WaitRun;
            }
            catch (Exception ex)
            {
                if (ex is TaskCancelException)
                {
                    Log($"复位取消:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Debug);
                }
                else
                {
                    Log($"复位异常:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Error);
                }

                State = TaskState.WaitReset;
                foreach (var t in Station.Tasks)
                {
                    t.Value.IsRunning = false;
                }
            }
            finally
            {
                IsPause = false;
                IsRunning = false;
                _task = null;
            }
        }

        /// <summary>
        /// 运行线程
        /// </summary>
        private void Running()
        {
            try
            {
                State = TaskState.Running;
                IsRunning = true;

                while (IsRunning)
                {
                    Log($"{Name} RunLoop Start...", LogLevel.Debug);
                    if (RunLoop() != 0)
                    {
                        Log($"{Name} RunLoop Break", LogLevel.Debug);
                        break;
                    }

                    Log($"{Name} RunLoop Finish", LogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                if (ex is TaskCancelException)
                {
                    Log($"运行取消:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Debug);
                }
                else
                {
                    Log($"运行异常:[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Error);
                }

                State = TaskState.WaitReset;
                foreach (var t in Station.Tasks)
                {
                    t.Value.IsRunning = false;
                }
            }
            finally
            {
                State = TaskState.WaitReset;
                IsPause = false;
                IsRunning = false;
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
            if (IsPause)
            {
                while (IsPause)
                {
                    AbortIfCancel("JoinIfPause");
                    Thread.Sleep(1);
                }
            }
        }

        /// <summary>
        /// 当停止信号触发，抛出任务取消异常，来退出线程执行
        /// </summary>
        /// <param name="msg"></param>
        public void AbortIfCancel(string msg)
        {
            if (!IsRunning)
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