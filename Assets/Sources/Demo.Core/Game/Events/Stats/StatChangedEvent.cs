using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Stats
{
    public readonly struct StatChangedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public StatChangedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}