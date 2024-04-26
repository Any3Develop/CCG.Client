using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.RuntimeObjects
{
    public interface IRuntimeEffect : IRuntimeObjectBase
    {
        new EffectData Data { get; }
        new IRuntimeEffectData RuntimeData { get; }
        IEventsSource EventsSource { get; }

        IRuntimeEffect Sync(IRuntimeEffectData runtimeData);
        bool IsExecuteAllowed();
        void Execute();
        void Expire();
    }
}