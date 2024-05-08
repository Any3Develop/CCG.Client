using Shared.Abstractions.Game.Runtime.Stats;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectHitEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterObjectHitEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}