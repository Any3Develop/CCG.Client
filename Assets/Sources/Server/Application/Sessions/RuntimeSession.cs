using System.Linq;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Factories;
using Shared.Game.Data;

namespace Server.Application.Sessions
{
    public class RuntimeSession : ISession
    {
        public SessionPlayer[] Players { get; private set; }
        public IContext Context { get; }
        public string Id { get; }

        public RuntimeSession(string id, IContextFactory contextFactory)
        {
            Id = id;
        }
        
        public void Build(params object[] args)
        {
            Players = args.OfType<SessionPlayer>().ToArray();
        }
    }
}