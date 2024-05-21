using Shared.Abstractions.Game.Runtime.Players;

namespace Shared.Game.Events.Context
{
    public readonly struct PlayerInitializeEvent
    {
        public IRuntimePlayer RuntimePlayer { get; }

        public PlayerInitializeEvent(IRuntimePlayer runtimePlayer)
        {
            RuntimePlayer = runtimePlayer;
        }
    }
}