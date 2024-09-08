using Shared.Game.Data;

namespace Client.Game.Abstractions.Runtime.Models
{
    public interface IRuntimeEffectModel : IRuntimeModel
    {
        public EffectData Data { get; set; }
        public int EffectOwnerId { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
    }
}