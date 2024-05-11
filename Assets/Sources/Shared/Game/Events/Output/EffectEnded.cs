using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class EffectEnded : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
}