using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct BeforeEffectExecutedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public BeforeEffectExecutedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}