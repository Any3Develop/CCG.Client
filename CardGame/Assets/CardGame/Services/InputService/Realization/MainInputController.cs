using System;

namespace CardGame.Services.InputService
{
    public class MainInputController  : IInputController<MainLayer>
    {
        public Type InputLayer => typeof(MainLayer);
        public event EventHandler LockChangedEvent;
        public virtual bool Locked { get; protected set; }
        
        public virtual void Lock()
        {
            if (Locked)
            {
                return;
            }
            Locked = true;
            LockChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Unlock()
        {
            if (!Locked)
            {
                return;
            }
            
            Locked = false;
            LockChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}