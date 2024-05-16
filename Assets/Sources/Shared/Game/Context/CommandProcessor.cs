using System;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Factories;
using Shared.Common.Logger;
using Shared.Game.Events.Context.Commands;

namespace Shared.Game.Context
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IContext context;
        private readonly ICommandFactory commandFactory;

        public CommandProcessor(
            IContext context, 
            ICommandFactory commandFactory)
        {
            this.context = context;
            this.commandFactory = commandFactory;
        }

        public void Execute<TCommand>(string executorId, ICommandModel model) where TCommand : ICommand
        {
            Execute(commandFactory.Create<TCommand>(executorId, model));
        }
        
        public void Execute(string executorId, string command, ICommandModel model)
        {
            Execute(commandFactory.Create(command, executorId, model));
        }

        private void Execute(ICommand command)
        {
            try
            {
                context.EventSource.Publish(new BeforeCommandExecuteEvent(command));
                command.Execute();
                context.EventSource.Publish(new AfterCommandExecutedEvent(command));
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                throw;
            }
        }
    }
}