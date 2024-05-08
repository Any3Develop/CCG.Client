using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class DeletedObjectEffect : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
}