using System.IO;

namespace Server.Domain.Contracts.Messanger
{
    public interface IClient
    {
        bool IsAuthorized { get; }
        bool IsConnected { get; }
        string UserId { get; }
        Stream GetStream();
        void SetUserId(string userId);
        void Abort();
    }
}