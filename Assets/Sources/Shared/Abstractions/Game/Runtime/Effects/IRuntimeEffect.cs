using Shared.Abstractions.Common.EventSource;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;

namespace Shared.Abstractions.Game.Runtime.Effects
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