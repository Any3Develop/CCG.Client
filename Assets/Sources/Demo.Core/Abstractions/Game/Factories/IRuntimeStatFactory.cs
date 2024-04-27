using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Abstractions.Game.Runtime.Stats;

namespace Demo.Core.Abstractions.Game.Factories
{
    public interface IRuntimeStatFactory : IRuntimeFactory<IRuntimeStat, IRuntimeStatData> {}
}