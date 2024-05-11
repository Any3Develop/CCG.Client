using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;
using Shared.Game.Data.Enums;
using Shared.Game.Events.Context.Objects;
using Shared.Game.Utils;

namespace Shared.Game.Runtime.Objects
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

        public IRuntimeObject Sync(IRuntimeObjectData runtimeData, bool notify = true)
        {
            if (!Initialized)
                return this;
            
            EventsSource.Publish<BeforeObjectChangeEvent>(notify, this);
            RuntimeData = runtimeData;
            EventsSource.Publish<AfterObjectChangedEvent>(notify, this);
            return this;
        }

        public void SetState(ObjectState value, bool notify = true)
        {
            if (!Initialized)
                return;
            
            EventsSource.Publish<BeforeObjectStateChangeEvent>(notify, this);
            RuntimeData.PreviousState = RuntimeData.State;
            RuntimeData.State = value;
            EventsSource.Publish<AfterObjectStateChangedEvent>(notify, this);
        }

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}