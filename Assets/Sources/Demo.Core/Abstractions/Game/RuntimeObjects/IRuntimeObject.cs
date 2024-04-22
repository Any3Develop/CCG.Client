using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeObject : IRuntimeBase
    {
        new ObjectData Data { get; }
        new IRuntimeObjectData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }
        IEffectsCollection EffectsCollection { get; }
        IEventsSource EventsSource { get; }
    }
}