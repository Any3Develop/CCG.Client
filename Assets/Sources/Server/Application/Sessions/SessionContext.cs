using System;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Context.Providers;
using Shared.Abstractions.Game.Factories;

namespace Server.Application.Sessions
{
    public class SessionContext : IContext
    {
        public ISharedConfig Config { get; }
        public IDatabase Database { get; }
        public IObjectsCollection ObjectsCollection { get; }
        public IPlayersCollection PlayersCollection { get; }
        public IRuntimeOrderProvider RuntimeOrderProvider { get; }
        public IRuntimeRandomProvider RuntimeRandomProvider { get; }
        public IRuntimeIdProvider RuntimeIdProvider { get; }
        public ICommandProcessor CommandProcessor { get; }
        public IGameQueueCollector GameQueueCollector { get; }
        public IEventsSource EventSource { get; }
        public IEventPublisher EventPublisher { get; }
        public IRuntimeObjectFactory ObjectFactory { get; }
        public IRuntimeEffectFactory EffectFactory { get; }
        public IRuntimeStatFactory StatFactory { get; }

        public SessionContext(IContextFactory contextFactory)
        {
            throw new NotImplementedException();
        }
    }
}