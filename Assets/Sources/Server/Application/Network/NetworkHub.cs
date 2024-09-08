using System.Threading.Tasks;
using Shared.Common.Network.Attributes;
using Shared.Common.Network.Routes;

namespace Server.Application.Network
{
    public class NetworkHub : NetworkHubBase
    {
        [HubEndpoint(GameEndpoints.ExecuteCommand)]
        public Task PerformCommandAsync() // TODO args
        {
            Caller.Abort(); // TODO
            return Task.CompletedTask;
        }
    }
}