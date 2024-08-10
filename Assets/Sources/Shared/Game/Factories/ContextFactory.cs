using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.EventProcessors;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Context.Providers;
using Shared.Abstractions.Game.Factories;
using Shared.Game.Collections;
using Shared.Game.Context;
using Shared.Game.Context.EventProcessors;
using Shared.Game.Context.Providers;

namespace Shared.Game.Factories
{
    public class ContextFactory : IContextFactory
    {
        private readonly IEventsSourceFactory eventsSourceFactory;
        public ContextFactory(IEventsSourceFactory eventsSourceFactory)
        {
            this.eventsSourceFactory = eventsSourceFactory;
        }

        #region Collections
        public IObjectsCollection CreateObjectsCollection(params object[] args)
        {
            return new ObjectsCollection(GetRequiredArgument<IEventPublisher>(args));
        }

        public IEffectsCollection CreateEffectsCollection(params object[] args)
        {
            return new EffectsCollection(GetRequiredArgument<IEventPublisher>(args));
        }

        public IStatsCollection CreateStatsCollection(params object[] args)
        {
            return new StatsCollection(GetRequiredArgument<IEventPublisher>(args));
        }

        public IPlayersCollection CreatePlayersCollection(params object[] args)
        {
            return new PlayersCollection();
        }
        #endregion

        #region Logic
        public IEventsSource CreateEventsSource(params object[] args)
        {
            return eventsSourceFactory.Create(args);
        }

        public IEventPublisher CreateEventPublisher(params object[] args)
        {
            return GetRequiredArgument<IEventPublisher>(args);
        }

        public IRuntimeIdProvider CreateRuntimeIdProvider(params object[] args)
        {
            return new RuntimeIdProvider();
        }

        public IRuntimeOrderProvider CreateRuntimeOrderProvider(params object[] args)
        {
            return new RuntimeOrderProvider();
        }
        
        public IRuntimeRandomProvider CreateRuntimeRandomProvider(params object[] args)
        {
            return new RuntimeRandomProvider();
        }

        public ICommandProcessor CreateCommandProcessor(params object[] args)
        {
            return new CommandProcessor(GetRequiredArgument<IContext>(args),
                GetRequiredArgument<ICommandFactory>(args));
        }

        public IGameQueueCollector CreateGameQueueCollector(params object[] args)
        {
            return new GameQueueCollector(GetRequiredArgument<IContext>(args));
        }

        public IObjectEventProcessor CreateObjectEventProcessor(params object[] args)
        {
            return new ObjectEventProcessor(GetRequiredArgument<IGameQueueCollector>(args));
        }

        public IContextEventProcessor CreateContextEventProcessor(params object[] args)
        {
            return new ContextEventProcessor(GetRequiredArgument<IContext>(args));
        }

        public IGameEventProcessor CreateGameEventProcessor(params object[] args)
        {
            return new GameEventProcessor(GetRequiredArgument<IContext>(args));
        }

        #endregion

        #region Factories
        public ICommandFactory CreateCommandFactory(params object[] args)
        {
            return new CommandFactory(GetRequiredArgument<IContext>(),
                GetRequiredArgument<ITypeCollection<string>>());
        }
        #endregion

        #region Common

        private T GetRequiredArgument<T>(params object[] args)
        {
            return args.OfType<T>().FirstOrDefault();
        }

        private IEnumerable<T> GetRequiredArguments<T>(params object[] args)
        {
            return args.OfType<T>().ToArray();
        }

        #endregion
    }
}