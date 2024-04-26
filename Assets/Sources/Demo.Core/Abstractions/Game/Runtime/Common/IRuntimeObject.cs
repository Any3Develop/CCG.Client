using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Game.Data;
using Demo.Core.Game.Enums;

namespace Demo.Core.Abstractions.Game.Runtime.Common
{
    public interface IRuntimeObject : IRuntimeObjectBase
    {
        new ObjectData Data { get; }
        new IRuntimeObjectData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }
        IEffectsCollection EffectsCollection { get; }
        IEventsSource EventsSource { get; }

        IRuntimeObject Sync(IRuntimeObjectData runtimeData);
        void SetState(RuntimeState value, bool notify = true);
    }
}