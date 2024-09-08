using System.Threading;
using System.Threading.Tasks;

namespace Shared.Abstractions.Common.Network
{
    public interface INetworkSimulator
    {
        Task TickOptimizerAsync(CancellationToken token);
        Task WaitRandomAsync(CancellationToken token);
    }
}