using System;

namespace CardGame.Services.StateMachine
{
    public interface IStateMachine
    {
        event Action<string> OnSwitchState;
        int CountStates { get; }
        IState Current { get; }
        IState Previous { get; }

        void Add(IState state);
        void Remove(string stateId);
        void Switch(string stateId, params object[] args);
        void Exit(string stateId);
        bool Any();
        void Clear();
    }
}