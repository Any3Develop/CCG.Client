using Demo.Core.Abstractions.Game.RuntimeObjects;

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