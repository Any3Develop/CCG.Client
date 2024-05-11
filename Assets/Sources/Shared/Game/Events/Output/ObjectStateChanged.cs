using Shared.Game.Data.Enums;

namespace Shared.Game.Events.Output
{
    public class ObjectStateChanged : GameEvent
    {
        public int Id { get; set; }
        public ObjectState Previous { get; set; }
        public ObjectState State { get; set; }
    }
}