using Demo.Core.Abstractions.Game.RuntimeObjects;

namespace Demo.Core.Game.Events.Effects
{
    public struct EffectDeletedEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectDeletedEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}