using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct BeforeObjectHitEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public BeforeObjectHitEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}