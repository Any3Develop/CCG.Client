using Shared.Abstractions.Common.EventSource;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.Logic;

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
        ICommandProcessor CreateCommandProcessor(params object[] args);
        #endregion

        #region Context
        IDatabase CreateDatabase(params object[] args);
        #endregion
    }
}