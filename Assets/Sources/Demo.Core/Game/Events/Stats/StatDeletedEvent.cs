using Demo.Core.Abstractions.Game.Runtime.Stats;

namespace Demo.Core.Game.Events.Stats
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