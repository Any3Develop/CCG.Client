using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
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