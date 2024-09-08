using Shared.Common.Network.Data;

namespace Shared.Abstractions.Common.Network
{
    public interface INetworkMessageFactory
    {
        Message Create(object traget, params object[] args);
    }
}