using Shared.Game.Data.Enums;

namespace Shared.Game.Events.Context.Logic
{
    public class ChangedObjectState : GameEvent
    {
        public int RuntimeObjectId { get; }
        public ObjectState Previous { get; private set; }
        public ObjectState State { get; private set; }

        public ChangedObjectState(int runtimeObjectId, ObjectState previous, ObjectState state)
        {
            RuntimeObjectId = runtimeObjectId;
            Previous = previous;
            State = state;
        }
    }
}