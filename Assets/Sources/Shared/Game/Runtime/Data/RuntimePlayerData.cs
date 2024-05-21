using System.Collections.Generic;
using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Runtime.Data
{
    public class RuntimePlayerData : IRuntimePlayerData
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string DataId { get; set; }
        public bool Ready { get; set; }
        public List<IRuntimeStatData> Stats { get; set; } = new();
    }
}