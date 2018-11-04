using System;

namespace Lead.Detect.MachineUtilityLib.Utils
{
    public class TestProcessControl<TData>
    {
        public event Action<TData> TestStartEvent;
        public event Action<TData> TestingEvent;
        public event Action<TData> TestFinishEvent;

        public virtual void OnTestStartEvent(TData obj)
        {
            TestStartEvent?.Invoke(obj);
        }

        public virtual void OnTestingEvent(TData obj)
        {
            TestingEvent?.Invoke(obj);
        }

        public virtual void OnTestFinishEvent(TData obj)
        {
            TestFinishEvent?.Invoke(obj);
        }
    }
}
