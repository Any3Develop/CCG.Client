using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Abstractions.Game.Runtime.Players
{
    public interface IRuntimePlayer : IRuntimeObjectBase
    {
        new IRuntimePlayerData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }

        bool TrySpendMana(int value);
    }
}