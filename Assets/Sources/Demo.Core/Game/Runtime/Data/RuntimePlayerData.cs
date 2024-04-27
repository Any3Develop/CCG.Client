using System.Collections.Generic;
using Demo.Core.Abstractions.Game.Runtime.Data;

namespace Demo.Core.Game.Runtime.Data
{
    public class RuntimePlayerData : IRuntimePlayerData
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public List<IRuntimeStatData> Stats { get; set; } = new();
    }
}