using Demo.Core.Abstractions.Game.Collections;
using Demo.Core.Abstractions.Game.Runtime.Players;

namespace Demo.Core.Game.Collections
{
    public class PlayersCollection : RuntimeCollection<IRuntimePlayer>, IPlayersCollection {}
}