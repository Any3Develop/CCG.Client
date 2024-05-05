using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct EffectBeforeChangedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectBeforeChangedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}