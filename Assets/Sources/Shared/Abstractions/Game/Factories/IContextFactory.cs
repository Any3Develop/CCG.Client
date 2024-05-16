using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.EventProcessors;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Context.Providers;

namespace Shared.Abstractions.Game.Factories
{
    public interface IContextFactory
    {
        #region Collections
        IObjectsCollection CreateObjectsCollection(params object[] args);
        IEffectsCollection CreateEffectsCollection(params object[] args);
        IStatsCollection CreateStatsCollection(params object[] args);
        IPlayersCollection CreatePlayersCollection(params object[] args);
        #endregion

        #region Logic
        IEventsSource CreateEventsSource(params object[] args);
        IRuntimeIdProvider CreateRuntimeIdProvider(params object[] args);
        IRuntimeOrderProvider CreateRuntimeOrderProvider(params object[] args);
        IRuntimeRandomProvider CreateRuntimeRandomProvider(params object[] args);
        ICommandProcessor CreateCommandProcessor(params object[] args);
        IGameQueueCollector CreateGameQueueCollector(params object[] args);
        IObjectEventProcessor CreateObjectEventProcessor(params object[] args);
        IContextEventProcessor CreateContextEventProcessor(params object[] args);
        IGameEventProcessor CreateGameEventProcessor(params object[] args);
        #endregion

        #region Context
        IDatabase CreateDatabase(params object[] args);
        #endregion

        #region Factories
        ICommandFactory CreateCommandFactory(params object[] args);
        #endregion
    }
}