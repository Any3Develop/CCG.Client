using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct EffectAfterExpireEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectAfterExpireEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}