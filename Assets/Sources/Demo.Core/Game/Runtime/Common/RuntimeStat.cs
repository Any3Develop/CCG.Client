using System;
using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Events.Stats;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.Runtime.Common
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

        public IRuntimeStat Sync(IRuntimeStatData runtimeData)
        {
            if (!Initialized)
                return this;

            RuntimeData = runtimeData;
            return this;
        }

        public virtual void SetValue(int value, bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Value = Math.Min(value, RuntimeData.Max);
            OnAfterChanged(notify);
        }

        public virtual void SetMax(int value, bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Max = value;
            SetValue(RuntimeData.Value, false);
            OnAfterChanged(notify);
        }

        public virtual void Reset(bool notify = true)
        {
            if (!Initialized)
                return;

            OnBeforeChanged(notify);
            RuntimeData.Max = Data.Max;
            RuntimeData.Value = Data.Value;
            OnAfterChanged(notify);
        }

        #region Callbacks

        protected virtual void OnBeforeChanged(bool notify = true) =>
            EventsSource.Publish<BeforeStatChangedEvent>(Initialized && notify, this);

        protected virtual void OnAfterChanged(bool notify = true) =>
            EventsSource.Publish<AfterStatChangedEvent>(Initialized && notify, this);

        #endregion

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;
        IData IRuntimeObjectBase.Data => Data;

        #endregion
    }
}