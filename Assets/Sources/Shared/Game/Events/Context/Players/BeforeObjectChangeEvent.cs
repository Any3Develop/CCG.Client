using Shared.Abstractions.Game.Runtime.Players;

namespace Shared.Game.Events.Context.Players
{
    public readonly struct BeforePlayerChangeEvent
    {
        public IRuntimePlayer RuntimePlayer { get; }

        public BeforePlayerChangeEvent(IRuntimePlayer runtimePlayer)
        {
            RuntimePlayer = runtimePlayer;
        }
    }
}