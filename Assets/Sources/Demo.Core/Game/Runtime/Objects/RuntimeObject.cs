using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;
using Demo.Core.Game.Data;
using Demo.Core.Game.Data.Enums;
using Demo.Core.Game.Events.Objects;
using Demo.Core.Game.Utils;

namespace Demo.Core.Game.Runtime.Objects
{
    public abstract class RuntimeObject : IRuntimeObject
    {
        public ObjectData Data { get; private set; }
        public IRuntimeObjectData RuntimeData { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEffectsCollection EffectsCollection { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public IRuntimeObject Init(
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
            return this;
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

        public IRuntimeObject Sync(IRuntimeObjectData runtimeData)
        {
            if (!Initialized)
                return this;
            
            RuntimeData = runtimeData;
            return this;
        }

        public void SetState(ObjectState value, bool notify = true)
        {
            if (!Initialized)
                return;
            
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

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}