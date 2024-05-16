using System.Collections.Generic;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Prediction
{
    public interface IPredictionProcessor
    {
        void Execute<TCommand>(ICommandModel model) where TCommand : ICommand;
        void ProcessRemoteQueue(IEnumerable<IGameEvent> remoteQueue);
    }
}