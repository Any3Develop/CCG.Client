using System;
using System.IO;

namespace Server.Domain.Contracts.Messanger
{
    public interface IClient : IDisposable
    {
        bool IsAuthorized { get; }
        bool IsConnected { get; }
        string UserId { get; }
        Stream GetStream();
        void SetUserId(string userId);
    }
}