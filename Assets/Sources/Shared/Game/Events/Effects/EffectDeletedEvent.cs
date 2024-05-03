using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Effects
{
    public readonly struct EffectDeletedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectDeletedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}