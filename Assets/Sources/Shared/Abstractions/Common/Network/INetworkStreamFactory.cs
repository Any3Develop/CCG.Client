namespace Shared.Abstractions.Common.Network
{
    public interface INetworkStreamFactory
    {
        INetworkStream Create(params object[] args);
    }
}