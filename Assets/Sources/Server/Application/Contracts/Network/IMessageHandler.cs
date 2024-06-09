using System;
using Shared.Common.Network.Data;

namespace Server.Application.Contracts.Network
{
    public interface IMessageHandler : IDisposable
    {
        event Action<IClient, Message> CallBack; 
        void Handle(IClient client, Message message);
    }
}