namespace Server.Application.Contracts.Network
{
    public interface INetworkHubFactory
    {
        INetworkHub Create(params object[] args);
    }
}