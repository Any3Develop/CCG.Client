using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.RuntimeObjects
{
    public abstract class RuntimeObject : IRuntimeObject
    {
        public IDatabase Data { get; }
        public IRuntimeObjectData RuntimeData { get; }
        public IRuntimeStatsCollection StatsCollection { get; }
        public IRuntimeEffectCollection EffectsCollection { get; }
        public IEventsSource EventsSource { get; }

        protected RuntimeObject(
            IDatabase data, 
            IRuntimeObjectData runtimeData,
            IRuntimeStatsCollection statsCollection, 
            IRuntimeEffectCollection effectCollection,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
            StatsCollection = statsCollection;
            EffectsCollection = effectCollection;
            EventsSource = eventsSource;
        }

        public virtual void Dispose()
        {
            EventsSource?.Dispose();
            StatsCollection?.Clear();
            EffectsCollection?.Clear();
        }
    }
}