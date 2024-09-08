using Server.Application.Contracts.Network;
using Shared.Abstractions.Common.Network;

namespace Server.Application.Network
{
    public class NetworkHubConnectionFactory : INetworkHubConnectionFactory
    {
        private readonly INetworkSimulator networkSimulator;
        private readonly INetworkSerializer networkSerializer;
        private readonly INetworkMessageFactory networkMessageFactory;
        private readonly INetworkStreamFactory networkStreamFactory;

        public NetworkHubConnectionFactory(
            INetworkSimulator networkSimulator,
            INetworkSerializer networkSerializer,
            INetworkMessageFactory networkMessageFactory,
            INetworkStreamFactory networkStreamFactory)
        {
            this.networkSimulator = networkSimulator;
            this.networkSerializer = networkSerializer;
            this.networkMessageFactory = networkMessageFactory;
            this.networkStreamFactory = networkStreamFactory;
        }

        public INetworkHubConnection Create(params object[] args)
        {
            return new NetworkHubConnection(networkSimulator, networkSerializer, networkMessageFactory, networkStreamFactory);
        }
    }
}