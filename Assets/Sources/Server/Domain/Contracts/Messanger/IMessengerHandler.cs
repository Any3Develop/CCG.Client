using System;
using Shared.Common.Network.Data;

namespace Server.Domain.Contracts.Messanger
{
    public interface IMessengerHandler
    {
        event Action<IClient, Message> CallBack; 
        void Handle(IClient client, Message message);
    }
}