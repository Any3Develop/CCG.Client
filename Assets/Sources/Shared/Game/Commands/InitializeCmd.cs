using Shared.Game.Commands.Base;
using Shared.Game.Events.Context;
using Shared.Game.Utils;

namespace Shared.Game.Commands
{
    public class InitializeCmd : Command
    {
        protected override void OnExecute()
        {
            Context.EventSource.Publish<PlayerInitializeEvent>(Context.PlayersCollection.TryGet(ExecutorId, out var player), player);
        }
    }
}