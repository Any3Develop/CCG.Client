using System.IO;
using System.Linq;
using Shared.Abstractions.Common.Network;

namespace Shared.Common.Network
{
    public class NetworkStreamFactory : INetworkStreamFactory
    {
        public INetworkStream Create(params object[] args)
        {
            return new NetworkStream(args.OfType<Stream>().First());
        }
    }
}