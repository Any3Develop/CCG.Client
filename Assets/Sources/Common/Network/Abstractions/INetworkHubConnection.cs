using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Common.Network.Data;

namespace Client.Common.Network
{
    public interface INetworkHubConnection : IDisposable
    {
        bool IsConnected { get; }
        event Action<Message> OnMessageReceived;
        
        UniTask ConnectAsync(string hubAdderss, int hubPort, CancellationToken token = default);
        UniTask CloseAsync(CancellationToken token = default);
        UniTask SendAsync(object target, object data, CancellationToken token = default);
    }
}