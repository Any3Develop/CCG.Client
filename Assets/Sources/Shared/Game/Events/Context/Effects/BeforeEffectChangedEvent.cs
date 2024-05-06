using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct BeforeEffectChangedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public BeforeEffectChangedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}