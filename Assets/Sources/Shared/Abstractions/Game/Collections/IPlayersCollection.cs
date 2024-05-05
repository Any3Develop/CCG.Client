using Shared.Abstractions.Game.Runtime.Players;

namespace Shared.Abstractions.Game.Collections
{
    public interface IPlayersCollection : IRuntimeCollection<IRuntimePlayer>
    {
        IRuntimePlayer Get(string ownerId);
        IRuntimePlayer GetOpposite(string ownerId);
        IRuntimePlayer GetOpposite(IRuntimePlayer runtimePlayer);
        bool TryGet(string ownerId, out IRuntimePlayer result);
        bool Contains(string ownerId);
    }
}