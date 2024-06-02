using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Common.Network;
using Shared.Common.Network.Data;

namespace Client.Common.Abstractions.Network
{
    public interface IMessengerClient : IDisposable
    {
        UniTask<bool> ConnectAsync();
        void Close();
        UniTask SendAsync(Route route, object data, CancellationToken token = default);
    }
}