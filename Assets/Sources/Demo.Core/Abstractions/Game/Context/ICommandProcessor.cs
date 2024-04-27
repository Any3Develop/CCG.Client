using System;
using Demo.Core.Abstractions.Game.Commands;

namespace Demo.Core.Abstractions.Game.Context
{
    public interface ICommandProcessor
    {
        void Execute<TCommand>(string userId, ICommandModel model, bool isNested = false) where TCommand : ICommand;
        void Execute(string userId, Type commandType, ICommandModel model, bool isNested = false);
        void Execute(string userId, string command, ICommandModel model, bool isNested = false);
    }
}