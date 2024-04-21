using System;
using Zenject;

namespace CardGame.Services.InputService
{
    public class InputController<T>  : IDisposable, IInitializable, IInputController<T> where T : IInputLayer
    {
        public Type InputLayer => typeof(T);
        public virtual bool Locked => _mainlayer.Locked || _locked;
        public event EventHandler LockChangedEvent;
        
        private readonly IInputController<MainLayer> _mainlayer;
        private bool _locked;
        public InputController(IInputController<MainLayer> mainlayer)
        {
            _mainlayer = mainlayer;
        }
        
        public void Initialize()
        {
            _mainlayer.LockChangedEvent += OnMainLayerLockChangedEventHandler;
        }
        
        public void Dispose()
        {
            _mainlayer.LockChangedEvent -= OnMainLayerLockChangedEventHandler;
        }
        
        private void OnMainLayerLockChangedEventHandler(object sender, EventArgs e)
        {
            LockChangedEvent?.Invoke(this,EventArgs.Empty);
        }

        public void Lock()
        {
            if (Locked)
            {
                return;
            }
            
            _locked = true;
            LockChangedEvent?.Invoke(this,EventArgs.Empty);
        }

        public void Unlock()
        {
            if (_mainlayer.Locked || !_locked)
            {
                return;
            }
            _locked = false;
            LockChangedEvent?.Invoke(this,EventArgs.Empty);
        }
    }
}