using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class AddedObject : GameEvent
    {
        public IRuntimeObjectData RuntimeData { get; set; }
    }
}