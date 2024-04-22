using System;
using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeObject : IDisposable
    {
        IDatabase Data { get; }
        IRuntimeObjectData RuntimeData { get; }
        IRuntimeStatsCollection StatsCollection { get; }
        IRuntimeEffectCollection EffectsCollection { get; }
        IEventsSource EventsSource { get; }
    }
}