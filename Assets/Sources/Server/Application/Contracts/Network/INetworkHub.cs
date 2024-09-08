using System.Threading;
using System.Threading.Tasks;

namespace Server.Application.Contracts.Network
{
    public interface INetworkHub
    {
        Task BroadcastAsync(object target, object data = null, CancellationToken token = default);
        Task SendAsync(string userId, object target, object data = null, CancellationToken token = default);
        Task SendAsync(IClient client, object target, object data = null, CancellationToken token = default);
    }
}