using System.Linq;
using Server.Application.Contracts.Network;

namespace Server.Application.Network
{
    public class NetworkHubFactory : INetworkHubFactory
    {
        public INetworkHub Create(params object[] args)
        {
            return new NetworkHub().SetupHub(args.OfType<INetworkHubConnection>().FirstOrDefault(), args.OfType<IClient>().FirstOrDefault());
        }
    }
}