using System;
using System.IO;
using System.Net.Sockets;
using Server.Domain.Contracts.Messanger;

namespace Server.Application.Messenger
{
    public class TcpNetworkClient : IClient
    {
        public bool IsAuthorized => !string.IsNullOrWhiteSpace(UserId);
        public bool IsConnected => connection is {Connected: true};
        public string UserId { get; private set; }

        private TcpClient connection;
        
        public TcpNetworkClient(TcpClient connection)
        {
            this.connection = connection;
        }

        public Stream GetStream()
        {
            return connection?.GetStream();
        }

        public void SetUserId(string userId)
        {
            UserId = userId;
        }
        
        public void Dispose()
        {
            SetUserId(null);
            connection?.Close();
            connection = null;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                TcpClient other => other == connection,
                TcpNetworkClient other => other.UserId == UserId || other.connection == connection,
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(connection?.GetHashCode() ?? 0, UserId?.GetHashCode() ?? 0);
        }
    }
}