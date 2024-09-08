using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Common.Network.Data;

namespace Server.Application.Contracts.Network
{
    public interface INetworkHubConnection : IDisposable
    {
        bool IsConnected { get; }       
        event Action<IClient, Message> OnMessageReceived;
        IEnumerable<IClient> Clients { get; }
        
        Task ConnectAsync(int hubPort, CancellationToken token = default);
        Task CloseAsync(CancellationToken token = default);
        
        Task SendBroadcastAsync(object target, object data = null, CancellationToken token = default);
        Task SendAsync(string clientId, object target, object data = null, CancellationToken token = default);
        Task SendAsync(IClient client, object target, object data = null, CancellationToken token = default);
    }
}