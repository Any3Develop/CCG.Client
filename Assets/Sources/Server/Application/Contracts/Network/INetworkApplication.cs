using System.Threading.Tasks;

namespace Server.Application.Contracts.Network
{
    public interface INetworkApplication
    {
        Task StartAsync();
        void Stop();
    }
}