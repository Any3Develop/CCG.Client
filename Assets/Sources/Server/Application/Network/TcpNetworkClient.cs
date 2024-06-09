using System;
using System.IO;
using System.Net.Sockets;
using Server.Application.Contracts.Network;

namespace Server.Application.Network
{
    public class TcpNetworkClient : IClient
    {
        public bool IsAuthorized => !string.IsNullOrWhiteSpace(UserId);
        public bool IsConnected => client is {Connected: true};
        public string UserId { get; private set; }

        private TcpClient client;
        private Stream connection;
        
        public TcpNetworkClient(TcpClient client)
        {
            this.client = client;
            connection = this.client.GetStream();
        }

        public Stream GetStream()
        {
            return connection;
        }

        public void SetUserId(string userId)
        {
            UserId = userId;
        }
        
        public void Abort()
        {
            SetUserId(null);
            connection = null;
            client?.Close();
            client?.Dispose();
            client = null;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                TcpClient other => other.Equals(client),
                TcpNetworkClient other => other.UserId == UserId || other.client == client,
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return !IsConnected ? 0 : HashCode.Combine(client.GetHashCode(), UserId?.GetHashCode() ?? 0);
        }
    }
}