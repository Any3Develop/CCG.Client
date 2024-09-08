using System.Linq;
using Shared.Abstractions.Common.Network;
using Shared.Common.Network.Data;

namespace Shared.Common.Network
{
    public class NetworkMessageFactory : INetworkMessageFactory
    {
        private readonly INetworkSerializer networkSerializer;
        public NetworkMessageFactory(INetworkSerializer networkSerializer)
        {
            this.networkSerializer = networkSerializer;
        }

        public Message Create(object target, params object[] args)
        {
            return new Message
            {
                Target = target?.ToString(),
                Args = args.Select(networkSerializer.Serialize).ToArray()
            };
        }
    }
}