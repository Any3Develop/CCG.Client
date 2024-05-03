using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Players;

namespace Shared.Game.Collections
{
    public class PlayersCollection : RuntimeCollection<IRuntimePlayer>, IPlayersCollection {}
}