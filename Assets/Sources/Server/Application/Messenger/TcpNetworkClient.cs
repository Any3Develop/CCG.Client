using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<int> SendAsync(byte[] data, CancellationToken token = default)
        {
            await using var stream = connection.GetStream();
            await stream.WriteAsync(data, token);
            // await stream.FlushAsync(token);
            return data.Length;
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken token = default)
        {
            await using var stream = connection.GetStream();
            return await stream.ReadAsync(buffer, offset, count, token);
        }

        public void SetUserId(string userId)
        {
            UserId = userId;
        }
        
        public void Abort()
        {
            SetUserId(null);
            connection?.Close();
            connection = null;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                TcpClient other => other.Equals(connection),
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