using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Abstractions.Game.Runtime.Players
{
    public interface IRuntimePlayer : IRuntimeObjectBase
    {
        new IRuntimePlayerData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }

        void Sync(IRuntimePlayerData runtimeData, bool notify = true);
        bool TrySpendMana(int value);
    }
}