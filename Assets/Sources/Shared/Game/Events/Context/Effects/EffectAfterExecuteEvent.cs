using Shared.Abstractions.Game.Runtime.Effects;

namespace Shared.Game.Events.Context.Effects
{
    public readonly struct EffectAfterExecuteEvent
    {
        public IRuntimeEffect Effect { get; }

        public EffectAfterExecuteEvent(IRuntimeEffect effect)
        {
            Effect = effect;
        }
    }
}