using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Common.Network;
using Shared.Abstractions.Common.Network;
using Shared.Abstractions.Game.Events;
using Shared.Common.Network.Attributes;
using Shared.Common.Network.Routes;

namespace Client.Game.Network
{
    public class GameHub : NetworkHubBase
    {
        protected override string HubUrl => UrLs.APIUrl;
        protected override int HubPort => UrLs.APIPort;

        public GameHub(
            INetworkHubCallerFactory networkHubCallerFactory,
            INetworkSerializer networkSerializer,
            INetworkHubConnectionFactory hubConnectionFactory)
            : base(networkHubCallerFactory, networkSerializer, hubConnectionFactory) {}
        
        // TODO create endpoints
        [HubEndpoint(GameEndpoints.GameEvent)]
        public Task GameEventReceiveAsync(List<IGameEvent> gameEvents)
        {
            return Task.CompletedTask;
        }
    }
}