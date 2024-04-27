using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Objects;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Effects
{
    public interface IRuntimeEffect : IRuntimeObjectBase
    {
        EffectData Data { get; }
        new IRuntimeEffectData RuntimeData { get; }
        IEventsSource EventsSource { get; }

        IRuntimeEffect Sync(IRuntimeEffectData runtimeData);
        bool IsExecuteAllowed();
        void Execute();
        void Expire();
    }
}