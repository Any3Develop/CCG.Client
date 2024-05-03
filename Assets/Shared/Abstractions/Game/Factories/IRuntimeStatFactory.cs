using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Abstractions.Game.Factories
{
    public interface IRuntimeStatFactory : IRuntimeFactory<IRuntimeStat, IRuntimeStatData> {}
}