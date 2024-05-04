using System;
using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.Logic;
using Shared.Common.Logger;
using Shared.Game.Commands.Base;
using Shared.Game.Events.Context;

namespace Shared.Game.Context.Logic
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IContext context;
        private readonly ITypeCollection<string> commandTypeCollection;

        public CommandProcessor(
            IContext context, 
            ITypeCollection<string> commandTypeCollection)
        {
            this.context = context;
            this.commandTypeCollection = commandTypeCollection;
        }

        public void Execute<TCommand>(string executorId, ICommandModel model, bool isNested = false) where TCommand : ICommand
        {
            Execute(executorId, typeof(TCommand), model, isNested);
        }
        
        public void Execute(string executorId, string command, ICommandModel model, bool isNested = false)
        {
            Execute(executorId, commandTypeCollection.Get(command), model, isNested);
        }

        public void Execute(string executorId, Type commandType, ICommandModel model, bool isNested = false)
        {
            try
            {
                var command = CreateInstance(commandType);
                command.Init(executorId, model, context);
                context.SharedEventSource.Publish(new BeforeCommandExecutedEvent(command));
                command.Execute();
                context.SharedEventSource.Publish(new AfterCommandExecutedEvent(command));
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }
        
        private static Command CreateInstance(Type commandType)
        {
            if (commandType == null)
                throw new NullReferenceException($"Can't create a command instance the type of command is missing.");
            
            var constructorInfo = commandType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{commandType.FullName} : default constructor not found.");
          
            return (Command)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}