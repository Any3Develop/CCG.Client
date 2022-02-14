using System;

namespace CardGame.Services.BootstrapService
{
    public abstract class Command : ICommand
    {
        public event EventHandler Done;
        
        public virtual void Do(){OnDone();}

        protected virtual void OnDone()
        {
            Done?.Invoke(this, EventArgs.Empty);
        }
    }
}