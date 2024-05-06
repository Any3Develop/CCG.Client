using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Context.Providers;

namespace Shared.Abstractions.Game.Context
{
    public interface IContext
    {
        ISharedConfig Config { get; }
        IDatabase Database { get; }
        IObjectsCollection ObjectsCollection { get; }
        IPlayersCollection PlayersCollection { get; }
        IRuntimeOrderProvider RuntimeOrderProvider { get; }
        IRuntimeIdProvider RuntimeIdProvider { get; }
        IEventsSource EventSource { get; }
    }
}