namespace Client.Common.Network
{
    public interface INetworkHubConnectionFactory
    {
        INetworkHubConnection Create(params object[] args);
    }
}