using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Common.Network.Data;

namespace Server.Domain.Contracts.Messanger
{
    public interface IMessengerService : IDisposable
    {
        IEnumerable<IClient> Clients { get; }
        
        void Start(int port);
        void Stop();
        Task BroadcastAsync(Message message);
        Task SendAsync(string userId, Message message);
        Task SendAsync(IClient client, Message message);
    }
}