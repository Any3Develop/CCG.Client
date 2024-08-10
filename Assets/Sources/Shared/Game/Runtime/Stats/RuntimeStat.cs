using System;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Abstractions.Game.Runtime.Stats;
using Shared.Game.Data;
using Shared.Game.Events.Context.Stats;
using Shared.Game.Utils;

namespace Shared.Game.Runtime.Stats
{
    public class RuntimeStat : IRuntimeStat
    {
        public StatData Data { get; private set; }
        public IRuntimeStatData RuntimeData { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }
        protected bool Initialized { get; private set; }


        public IRuntimeStat Init(
            StatData data,
            IRuntimeStatData runtimeData,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            EventPublisher = eventPublisher;
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

        public IRuntimeStat Sync(IRuntimeStatData runtimeData, bool notify = true)
        {
            if (!Initialized)
                return this;

            OnBeforeChanged(notify);
            RuntimeData = runtimeData;
            OnAfterChanged(notify);
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
            EventPublisher.Publish<BeforeStatChangeEvent>(Initialized && notify, this);

        protected virtual void OnAfterChanged(bool notify = true) =>
            EventPublisher.Publish<AfterStatChangedEvent>(Initialized && notify, this);

        #endregion

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}