using Client.Game.Abstractions.Runtime.Models;
using Shared.Game.Data;

namespace Client.Game.Runtime.Models.Effects
{
    public class RuntimeEffectModel : RuntimeModel, IRuntimeEffectModel
    {
        public EffectData Data { get; set; }
        public int EffectOwnerId { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
    }
}