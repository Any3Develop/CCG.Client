using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeEffect : IRuntimeBase
    {
        new EffectData Data { get; }
        new IRuntimeEffectData RuntimeData { get; }
        IEventsSource EventsSource { get; }
    }
}