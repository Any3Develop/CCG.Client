using System;

namespace Shared.Abstractions.Common.Network
{
    public interface INetworkHubCallerFactory
    {
        INetworkHubCaller Create(Type hubType);
    }
}