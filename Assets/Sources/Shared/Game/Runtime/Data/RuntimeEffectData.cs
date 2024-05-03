using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Runtime.Data
{
    public class RuntimeEffectData : IRuntimeEffectData
    {
        public int Id { get; set; }
        public string DataId { get; set; }
        public string OwnerId { get; set; }
        public int EffectOwnerId { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
    }
}