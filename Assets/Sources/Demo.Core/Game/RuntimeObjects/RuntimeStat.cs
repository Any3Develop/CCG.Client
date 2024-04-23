using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Events.Stats;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.RuntimeObjects
{
    public class RuntimeStat : IRuntimeStat
    {
        public StatData Data { get; private set; }
        public IRuntimeStatData RuntimeData { get; private set; }
        protected IEventsSource EventsSource { get; private set; }
        protected bool Initialized { get; private set; }


        public IRuntimeStat Init(
            StatData data,
            IRuntimeStatData runtimeData,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            EventsSource = eventsSource;
            Initialized = true;
            return this;
        }

        public virtual void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            Data = null;
            RuntimeData = null;
            EventsSource = null;
        }

        public virtual void Set(int value, bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Value = value;
            OnAfterChanged(notify);
        }

        public virtual void SetBase(int value, bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Base = value;
            OnAfterChanged(notify);
        }

        public virtual void SetMax(int value, bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Max = value;
            OnAfterChanged(notify);
        }

        public virtual void SetName(string value, bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Name = value;
            OnAfterChanged(notify);
        }

        public virtual void Reset(bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Base = Data.Base;
            RuntimeData.Max = Data.Max;
            RuntimeData.Value = Data.Value;
            RuntimeData.Name = Data.Name;
            OnAfterChanged(notify);
        }

        #region Callbacks

        protected virtual void OnBeforeChanged(bool notify = true) =>
            EventsSource.Publish<BeforeStatChangedEvent>(Initialized && notify, this);

        protected virtual void OnAfterChanged(bool notify = true) =>
            EventsSource.Publish<AfterStatChangedEvent>(Initialized && notify, this);

        #endregion

        #region IRuntimeObjectBase

        IRuntimeData IRuntimeObjectBase.RuntimeData => RuntimeData;
        IData IRuntimeObjectBase.Data => Data;

        #endregion
    }
}