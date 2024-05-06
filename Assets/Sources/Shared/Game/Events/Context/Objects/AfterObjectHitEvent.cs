using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectHitEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterObjectHitEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }

    public readonly struct AddedObjectEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AddedObjectEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }

    public readonly struct DeletedObjectEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public DeletedObjectEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}