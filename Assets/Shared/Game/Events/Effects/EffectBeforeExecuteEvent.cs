using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Effects
{
    public readonly struct EffectBeforeExecuteEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectBeforeExecuteEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}