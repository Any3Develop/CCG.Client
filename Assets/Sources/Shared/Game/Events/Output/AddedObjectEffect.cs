using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class AddedObjectEffect : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
    public class AddedObject : GameEvent
    {
        public IRuntimeObjectData RuntimeData { get; set; }
    }
    public class DeletedObject : GameEvent
    {
        public IRuntimeObjectData RuntimeData { get; set; }
    }
}