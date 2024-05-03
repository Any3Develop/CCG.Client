using System;
using Shared.Abstractions.Common.Collections;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Common.Logger;
using Shared.Game.Commands.Base;

namespace Shared.Game.Context
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IContext context;
        private readonly ITypeCollection<string> commandTypeCollection;

        public CommandProcessor(IContext context, ITypeCollection<string> commandTypeCollection)
        {
            this.context = context;
            this.commandTypeCollection = commandTypeCollection;
        }

        public void Execute<TCommand>(string userId, ICommandModel model, bool isNested = false) where TCommand : ICommand
        {
            Execute(userId, typeof(TCommand), model, isNested);
        }
        
        public void Execute(string userId, string command, ICommandModel model, bool isNested = false)
        {
            Execute(userId, commandTypeCollection.Get(command), model, isNested);
        }

        public void Execute(string userId, Type commandType, ICommandModel model, bool isNested = false)
        {
            try
            {
                var command = CreateCommandInstance(commandType);
                
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }
        
        private Command CreateCommandInstance(Type commandType)
        {
            var constructorInfo = commandType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{commandType.Name} default constructor not found.");
          
            return (Command)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}