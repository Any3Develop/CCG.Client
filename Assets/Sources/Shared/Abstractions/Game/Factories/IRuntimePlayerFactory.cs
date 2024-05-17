using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Players;

namespace Shared.Abstractions.Game.Factories
{
    public interface IRuntimePlayerFactory : IRuntimeFactory<IRuntimePlayer, IRuntimePlayerData>{}
}