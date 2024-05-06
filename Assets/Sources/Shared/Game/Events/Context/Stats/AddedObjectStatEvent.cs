using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Stats
{
    public readonly struct AddedObjectStatEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AddedObjectStatEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}