using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectDeletedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectDeletedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}