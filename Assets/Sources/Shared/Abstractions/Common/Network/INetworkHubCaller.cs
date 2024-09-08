using System.Threading;
using System.Threading.Tasks;

namespace Shared.Abstractions.Common.Network
{
    public interface INetworkHubCaller
    {
        Task InvokeAsync(object hubInstance, string target, CancellationToken token, params object[] parameters);
    }
}