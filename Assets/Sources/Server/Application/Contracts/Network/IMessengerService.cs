using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Common.Network.Data;

namespace Server.Application.Contracts.Network
{
    public interface IMessengerService : IDisposable
    {
        IEnumerable<IClient> Clients { get; }
        
        void Start(int port);
        void Stop();
        Task BroadcastAsync(Message message, CancellationToken token = default);
        Task SendAsync(string userId, Message message, CancellationToken token = default);
        Task SendAsync(IClient client, Message message, CancellationToken token = default);
    }
}