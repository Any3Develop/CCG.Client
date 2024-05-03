using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Stats
{
    public readonly struct StatDeletedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public StatDeletedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}