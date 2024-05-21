using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Context.Providers;
using Shared.Abstractions.Game.Factories;

namespace Shared.Abstractions.Game.Context
{
    public interface IContext
    {
        ISharedConfig Config { get; }
        IDatabase Database { get; }
        IObjectsCollection ObjectsCollection { get; }
        IPlayersCollection PlayersCollection { get; }
        IRuntimeOrderProvider RuntimeOrderProvider { get; }
        IRuntimeRandomProvider RuntimeRandomProvider { get; }
        ICommandProcessor CommandProcessor { get; }
        IRuntimeIdProvider RuntimeIdProvider { get; }
        IEventsSource EventSource { get; }

        #region Factories

        IRuntimeObjectFactory ObjectFactory { get; }
        IRuntimeEffectFactory EffectFactory { get; }
        IRuntimeStatFactory StatFactory { get; }

        #endregion
    }
}