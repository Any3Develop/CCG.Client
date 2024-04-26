using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Runtime.Common;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Game.Data;

namespace Demo.Core.Abstractions.Game.Runtime.Effects
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