using Client.Common.Abstractions.Network;
using Shared.Common.Logger;
using Shared.Common.Network.Data;
using Shared.Game.Utils;

namespace Client.Game.Network
{
    public class TestGameMessageHandler : IMessageHandler
    {
        public void Handle(Message message)
        {
            SharedLogger.Log($"[Client.{GetType().Name}] Message received : {message.ReflectionFormat()}");
        }
    }
}