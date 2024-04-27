using Demo.Core.Abstractions.Game.Runtime.Stats;

namespace Demo.Core.Game.Events.Stats
{
    public readonly struct AfterStatChangedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterStatChangedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}