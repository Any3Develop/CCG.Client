using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.Logic;
using Shared.Abstractions.Game.Factories;
using Shared.Game.Collections;
using Shared.Game.Context;
using Shared.Game.Context.Logic;
using Shared.Game.Context.Logic.EventSource;
using Shared.Game.Data;

namespace Shared.Game.Factories
{
    public class ContextFactory : IContextFactory
    {
        #region Collections
        public IObjectsCollection CreateObjectsCollection(params object[] args)
        {
            return new ObjectsCollection();
        }

        public IEffectsCollection CreateEffectsCollection(params object[] args)
        {
            return new EffectsCollection(GetRequiredArgument<IEventsSource>(args));
        }

        public IStatsCollection CreateStatsCollection(params object[] args)
        {
            return new StatsCollection(GetRequiredArgument<IEventsSource>(args));
        }

        public IPlayersCollection CreatePlayersCollection(params object[] args)
        {
            return new PlayersCollection();
        }
        #endregion

        #region Logic
        public IEventsSource CreateEventsSource(params object[] args)
        {
            return new EventSource();
        }

        public IRuntimeIdProvider CreateRuntimeIdProvider(params object[] args)
        {
            return new RuntimeIdProvider();
        }

        public ICommandProcessor CreateCommandProcessor(params object[] args)
        {
            return new CommandProcessor(GetRequiredArgument<IContext>(args),
                GetRequiredArgument<ITypeCollection<string>>(args));
        }
        #endregion

        #region Context
        public IDatabase CreateDatabase(params object[] args)
        {
            return new Database
            {
                Objects = new DataCollection<ObjectData>(),
                Effects = new DataCollection<EffectData>(),
                Stats = new DataCollection<StatData>()
            };
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