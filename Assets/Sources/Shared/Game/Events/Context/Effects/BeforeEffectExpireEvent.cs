using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct BeforeEffectExpireEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public BeforeEffectExpireEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}