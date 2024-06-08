using System;
using Server.Domain.Contracts.Messanger;
using Shared.Common.Logger;
using Shared.Common.Network.Data;

namespace Server.Application.Messenger
{
    public class TestMessengerHandler : IMessengerHandler, IDisposable
    {
        public event Action<IClient, Message> CallBack;

        public void Handle(IClient client, Message message)
        {
            SharedLogger.Log($"[Server.{GetType().Name}] Received message with {nameof(message.Route)} : {message.Route}, {nameof(message.Data)} : {message.Data}.");
            message.Data = "\"Hello who connected on that server!!!.\"";
            CallBack?.Invoke(client, message);
        }

        public void Dispose()
        {
            CallBack = null;
        }
    }
}