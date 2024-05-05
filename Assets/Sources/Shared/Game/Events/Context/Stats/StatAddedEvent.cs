using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Stats
{
    public readonly struct StatAddedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public StatAddedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}