using System;
using CardGame.Services.CommandService;

namespace CardGame.Session
{
    public readonly struct CreateSessionProtocol : IProtocol
    {
        public readonly string Id;

        public CreateSessionProtocol(string id = null)
        {
            Id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
            var sds = id;
        }
    }
}