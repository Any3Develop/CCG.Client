using Shared.Abstractions.Common.Network;

namespace Server.Application.Contracts.Network
{
    public interface IClient
    {
        INetworkStream NetworkStream { get; }
        bool IsAuthorized { get; }
        bool IsConnected { get; }
        string ClientId { get; }
        void SetUserId(string userId);
        void Abort();
    }
}