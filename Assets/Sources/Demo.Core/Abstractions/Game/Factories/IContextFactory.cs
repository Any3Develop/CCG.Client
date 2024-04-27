using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Context;

namespace Demo.Core.Abstractions.Game.Factories
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