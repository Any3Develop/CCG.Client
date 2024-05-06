using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class EndedObjectEffect : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
}