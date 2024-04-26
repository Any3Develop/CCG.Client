using Demo.Core.Abstractions.Game.Runtime.Common;

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