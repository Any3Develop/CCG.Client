using System;
using System.Net.Sockets;
using Server.Application.Contracts.Network;
using Shared.Abstractions.Common.Network;

namespace Server.Application.Network
{
    public class NetworkClient : IClient
    {
        public INetworkStream NetworkStream { get; }
        public bool IsAuthorized => !string.IsNullOrWhiteSpace(ClientId);
        public bool IsConnected => client is {Connected: true};
        public string ClientId { get; private set; }

        private TcpClient client;
        
        public NetworkClient(TcpClient client, INetworkStream networkStream)
        {
            NetworkStream = networkStream;
            this.client = client;
        }

        public void SetUserId(string userId)
        {
            ClientId = userId;
        }
        
        public void Abort()
        {
            SetUserId(null);
            client?.Close();
            client?.Dispose();
            client = null;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                TcpClient other => other.Equals(client),
                NetworkClient other => other.ClientId == ClientId || other.client == client,
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return !IsConnected ? 0 : HashCode.Combine(client.GetHashCode(), ClientId?.GetHashCode() ?? 0);
        }
    }
}