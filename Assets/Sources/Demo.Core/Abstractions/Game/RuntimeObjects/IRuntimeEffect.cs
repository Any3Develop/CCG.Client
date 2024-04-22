using System;
using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeEffect : IDisposable
    {
        IDatabase Data { get; }
        IRuntimeEffectData RuntimeData { get; }
        IEventsSource EventsSource { get; }
    }
}