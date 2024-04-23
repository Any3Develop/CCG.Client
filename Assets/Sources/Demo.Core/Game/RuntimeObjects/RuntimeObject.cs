using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Enums;
using Demo.Core.Game.Events.Objects;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.RuntimeObjects
{
    public abstract class RuntimeObject : IRuntimeObject
    {
        public ObjectData Data { get; private set; }
        public IRuntimeObjectData RuntimeData { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEffectsCollection EffectsCollection { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public void Init(
            ObjectData data,
            IRuntimeObjectData runtimeData,
            IStatsCollection statsCollection,
            IEffectsCollection effectCollection,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            StatsCollection = statsCollection;
            EffectsCollection = effectCollection;
            EventsSource = eventsSource;
            Initialized = true;
        }

        public virtual void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            EventsSource?.Dispose();
            StatsCollection?.Clear();
            EffectsCollection?.Clear();
            Data = null;
            RuntimeData = null;
            EventsSource = null;
            StatsCollection = null;
            EffectsCollection = null;
        }

        public void SetState(RuntimeState value, bool notify = true)
        {
            OnBeforeStateChanged(notify);
            RuntimeData.PreviousState = RuntimeData.State;
            RuntimeData.State = value;
            OnAfterStateChanged(notify);
        }

        #region Callbacks

        protected virtual void OnBeforeStateChanged(bool notify = true) =>
            EventsSource.Publish<BeforeObjectStateChangedEvent>(Initialized && notify, this);

        protected virtual void OnAfterStateChanged(bool notify = true) =>
            EventsSource.Publish<AfterObjectStateChangedEvent>(Initialized && notify, this);

        #endregion

        #region IRuntimeObjectBase

        IRuntimeData IRuntimeObjectBase.RuntimeData => RuntimeData;
        IData IRuntimeObjectBase.Data => Data;

        #endregion
    }
}