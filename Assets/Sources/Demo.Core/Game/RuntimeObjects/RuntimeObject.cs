using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;
using Demo.Core.Game.Data;

namespace Demo.Core.Game.RuntimeObjects
{
    public abstract class RuntimeObject : IRuntimeObject
    {
        public ObjectData Data { get; private set; }
        public IRuntimeObjectData RuntimeData { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEffectsCollection EffectsCollection { get; private set; }
        public IEventsSource EventsSource { get; private set; }

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
            return this;
        }

        public virtual void Dispose()
        {
            EventsSource?.Dispose();
            StatsCollection?.Clear();
            EffectsCollection?.Clear();
        }

        #region IRuntimeBase
        IRuntimeData IRuntimeBase.RuntimeData => RuntimeData;
        IData IRuntimeBase.Data => Data;
        #endregion
    }
}