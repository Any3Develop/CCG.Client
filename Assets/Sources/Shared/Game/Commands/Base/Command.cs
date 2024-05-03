using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;

namespace Shared.Game.Commands.Base
{
    public abstract class Command<T> : Command, ICommand<T> where T : ICommandModel
    {
        public new T Model => (T)base.Model;
    }
    
    public abstract class Command : ICommand
    {
        public string ExecutorId { get; private set; }
        public ICommandModel Model { get; private set; }
        protected IContext Context { get; private set; }

        public void Init(string executorId, ICommandModel model, IContext context)
        {
            ExecutorId = executorId;
            Model = model;
            Context = context;
        }
        
        public void Execute()
        {
            OnExecute();
        }

        protected abstract void OnExecute();
    }
}