namespace Server.Application.Contracts.Network
{
    public interface INetworkHubConnectionFactory
    {
        INetworkHubConnection Create(params object[] args);
    }
}