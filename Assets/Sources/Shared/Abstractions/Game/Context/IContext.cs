using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.Logic;

namespace Shared.Abstractions.Game.Context
{
    public interface IContext
    {
        ISharedConfig Config { get; }
        IDatabase Database { get; }
        IObjectsCollection ObjectsCollection { get; }
        IPlayersCollection PlayersCollection { get; }
        IEventsSource SharedEventSource { get; }
    }
}