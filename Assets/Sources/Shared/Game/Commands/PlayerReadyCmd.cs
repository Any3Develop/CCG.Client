using System;
using System.Linq;
using Shared.Common.Logger;
using Shared.Game.Commands.Base;
using Shared.Game.Events.Context;
using Shared.Game.Utils;

namespace Shared.Game.Commands
{
    public class PlayerReadyCmd : Command
    {
        protected override void OnExecute()
        {
            if (!Context.PlayersCollection.TryGet(ExecutorId, out var player))
                throw new NullReferenceException("Player who executed the command wasn't found.");

            if (player.RuntimeData.Ready)
            {
                SharedLogger.Log($"Player {ExecutorId} already ready.");
                return;
            }
            
            player.SetReady(true);
            Context.EventSource.Publish<SessionStartEvent>(Context.PlayersCollection.All(x => x.RuntimeData.Ready));
        }
    }
}