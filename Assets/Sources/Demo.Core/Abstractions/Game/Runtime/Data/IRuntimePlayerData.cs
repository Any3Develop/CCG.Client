using System.Collections.Generic;

namespace Demo.Core.Abstractions.Game.Runtime.Data
{
    public interface IRuntimePlayerData : IRuntimeDataBase
    {
        List<IRuntimeStatData> Stats { get; }
    }
}