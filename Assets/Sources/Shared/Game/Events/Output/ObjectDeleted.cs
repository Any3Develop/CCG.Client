using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class ObjectDeleted : GameEvent
    {
        public IRuntimeObjectData RuntimeData { get; set; }
    }
}