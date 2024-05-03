using Shared.Abstractions.Common.Config;
using Shared.Abstractions.Common.EventSource;
using Shared.Abstractions.Game.Collections;

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