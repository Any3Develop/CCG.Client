using Shared.Abstractions.Game.Runtime.Players;

namespace Shared.Game.Events.Context.Players
{
    public readonly struct AfterPlayerChangedEvent
    {
        public IRuntimePlayer RuntimePlayer { get; }

        public AfterPlayerChangedEvent(IRuntimePlayer runtimePlayer)
        {
            RuntimePlayer = runtimePlayer;
        }
    }
}