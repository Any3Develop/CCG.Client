using Shared.Abstractions.Common.EventSource;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;

namespace Shared.Abstractions.Game.Factories
{
    public interface IContextFactory
    {
        IDatabase CreateDatabase(params object[] args);
        IObjectsCollection CreateObjectsCollection(params object[] args);
        IEffectsCollection CreateEffectsCollection(params object[] args);
        IStatsCollection CreateStatsCollection(params object[] args);
        IPlayersCollection CreatePlayersCollection(params object[] args);
        IEventsSource CreateEventsSource(params object[] args);
        IRuntimeIdProvider CreateRuntimeIdProvider(params object[] args);
    }
}