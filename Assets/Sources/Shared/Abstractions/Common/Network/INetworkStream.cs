using System.Threading;
using System.Threading.Tasks;
using Shared.Common.Network.Data;

namespace Shared.Abstractions.Common.Network
{
    public interface INetworkStream
    {
        Task<StreamResult> ReadAsync(CancellationToken token);
        Task<StreamResult> WriteAsync(byte[] buffer, CancellationToken token);
    }
}