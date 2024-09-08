using Client.Services.DIService;

namespace Client.Common.Network
{
    public class NetworkHubConnectionFactory : INetworkHubConnectionFactory
    {
        private readonly IAbstractFactory abstractFactory;
        public NetworkHubConnectionFactory(IAbstractFactory abstractFactory)
        {
            this.abstractFactory = abstractFactory;
        }

        public INetworkHubConnection Create(params object[] args)
        {
            return abstractFactory.Instantiate<NetworkHubConnection>(args);
        }
    }
}