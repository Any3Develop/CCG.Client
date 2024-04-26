using Demo.Core.Abstractions.Game.Runtime.Common;

namespace Demo.Core.Game.Events.Stats
{
    public readonly struct BeforeStatChangedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public BeforeStatChangedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}