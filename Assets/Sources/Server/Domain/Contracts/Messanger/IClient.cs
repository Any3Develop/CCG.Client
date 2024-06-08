using System.Threading;
using System.Threading.Tasks;

namespace Server.Domain.Contracts.Messanger
{
    public interface IClient
    {
        bool IsAuthorized { get; }
        bool IsConnected { get; }
        string UserId { get; }
        Task<int> SendAsync(byte[] data, CancellationToken token = default);
        Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken token = default);
        void SetUserId(string userId);
        void Abort();
    }
}