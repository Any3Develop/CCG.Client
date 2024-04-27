﻿using Demo.Core.Abstractions.Game.Commands;
using Demo.Core.Abstractions.Game.Context;

namespace Demo.Core.Game.Commands.Base
{
    public abstract class Command<T> : Command, ICommand<T> where T : ICommandModel
    {
        public new T Model => (T)base.Model;
    }
    
    public abstract class Command : ICommand
    {
        public ICommandModel Model { get; private set; }
        protected IContext Context { get; private set; }

        public void Init(ICommandModel model, IContext context)
        {
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