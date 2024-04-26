using Demo.Core.Abstractions.Game.Runtime.Data;

namespace Demo.Core.Game.Runtime.Data
{
    public class RuntimeStatData : IRuntimeStatData
    {
        public int Id { get; set; }
        public string DataId { get; set; }
        public string OwnerId { get; set; }
        public int RuntimeOwnerId { get; set; }

        public int Max { get; set; }
        public int Value { get; set; }
    }
}