using System;
using System.Collections.Generic;

namespace CardGame.Services.BootstrapService
{
    public class ProcessingCommands : IProcessingCommands
    {
        public event EventHandler AllCommandsDone;
        public bool IsExecuting { get; protected set; }
        public Queue<ICommand> Queue => _queue;
        
        private readonly Queue<ICommand> _queue = new Queue<ICommand>();

        public int Count => _queue.Count;

        public void AddCommand(ICommand cmd)
        {
            if (cmd == null)
            {
                // todo: add error
                return;
            }
            
            _queue.Enqueue(cmd);
            OnAdded(cmd);
        }

        public void Clear()
        {
            _queue.Clear();
        }

        public bool Any()
        {
            return _queue.Count > 0;
        }

        public virtual void StartExecute() { Execute(); }

        protected virtual void Execute() { }

        protected ICommand Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        protected void OnComplete()
        {
            OnAllCommandsDone();
            AllCommandsDone?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnAdded(ICommand cmd){}
        protected virtual void OnAllCommandsDone(){}
    }
}