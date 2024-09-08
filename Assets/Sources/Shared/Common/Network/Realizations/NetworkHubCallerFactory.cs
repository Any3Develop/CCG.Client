using System;
using Shared.Abstractions.Common.Network;

namespace Shared.Common.Network
{
    public class NetworkHubCallerFactory : INetworkHubCallerFactory
    {
        public INetworkHubCaller Create(Type hubType)
        {
            return new NetworkHubCaller(hubType);
        }
    }
}