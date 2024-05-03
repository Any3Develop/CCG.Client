using System;
using Shared.Abstractions.Game.Commands;

namespace Shared.Abstractions.Game.Context.Logic
{
    public interface ICommandProcessor
    {
        void Execute<TCommand>(string executorId, ICommandModel model, bool isNested = false) where TCommand : ICommand;
        void Execute(string executorId, Type commandType, ICommandModel model, bool isNested = false);
        void Execute(string executorId, string command, ICommandModel model, bool isNested = false);
    }
}