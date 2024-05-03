using System.Collections.Generic;

namespace Shared.Abstractions.Game.Runtime.Data
{
    public interface IRuntimePlayerData : IRuntimeDataBase
    {
        List<IRuntimeStatData> Stats { get; }
    }
}