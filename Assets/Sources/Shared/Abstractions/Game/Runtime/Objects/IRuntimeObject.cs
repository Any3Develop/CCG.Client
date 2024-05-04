using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.Logic;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Game.Data;
using Shared.Game.Data.Enums;

namespace Shared.Abstractions.Game.Runtime.Objects
{
    public interface IRuntimeObject : IRuntimeObjectBase
    {
        ObjectData Data { get; }
        new IRuntimeObjectData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }
        IEffectsCollection EffectsCollection { get; }
        IEventsSource EventsSource { get; }

        IRuntimeObject Sync(IRuntimeObjectData runtimeData);
        void SetState(ObjectState value, bool notify = true);
    }
}