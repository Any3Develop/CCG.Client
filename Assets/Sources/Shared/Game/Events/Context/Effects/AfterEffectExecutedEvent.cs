using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectExecutedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectExecutedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}