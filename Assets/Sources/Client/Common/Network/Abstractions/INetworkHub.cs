using System.Threading;
using Cysharp.Threading.Tasks;

namespace Client.Common.Network
{
    public interface INetworkHub
    {
        bool IsConnected { get; }
        
        UniTask ConnectAsync(CancellationToken token = default);
        UniTask CloseAsync(CancellationToken token = default);
        UniTask SendAsync(object target, object data, CancellationToken token = default);
    }
}