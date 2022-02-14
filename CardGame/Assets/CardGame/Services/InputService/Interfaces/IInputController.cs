using System;

namespace CardGame.Services.InputService
{

    public interface IInputController<T> where T : IInputLayer
    {
        Type InputLayer { get; }
        event EventHandler LockChangedEvent;
        bool Locked { get; }
        void Lock();
        void Unlock();
    }
}