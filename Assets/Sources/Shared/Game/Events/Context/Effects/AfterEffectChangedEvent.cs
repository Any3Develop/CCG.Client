using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectChangedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectChangedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}