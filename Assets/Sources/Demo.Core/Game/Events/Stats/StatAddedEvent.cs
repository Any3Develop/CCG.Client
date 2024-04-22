using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Stats
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