using System;
using CardGame.Services.StorageService;

namespace CardGame.Services.StateMachine
{
    public class StateMachine : IStateMachine
    {
        public event Action<string> OnSwitchState;
        public int CountStates => _stateStorage.Count - 1;
        public IState Current { get; private set; }
        public IState Previous { get; private set; }
        private readonly IStorage<IState> _stateStorage;
        private const string _defaultStateId = "Empty";

        public StateMachine()
        {
            _stateStorage = new Storage<IState>(); // force local storage
            Clear();
        }

        public void Add(IState state)
        {
            if (state == null)
            {
                throw new ArgumentException("State is Null");
            }

            if (_stateStorage.HasEntity(state.Id))
            {
                throw new ArgumentException($"State alredy exist  {state.Id}");
            }

            _stateStorage.Add(state);
        }

        public void Remove(string stateId)
        {
            if (string.IsNullOrEmpty(stateId))
            {
                throw new ArgumentException("StateID is Null");
            }

            if (stateId == _defaultStateId)
            {
                return;
            }

            if (Previous != null && Previous.Id == stateId)
            {
                Previous = _stateStorage.Get(_defaultStateId);
            }

            if (Current != null && Current.Id == stateId)
            {
                Switch(_defaultStateId);
            }

            if (_stateStorage.HasEntity(stateId))
            {
                _stateStorage.Remove(stateId);
            }
        }

        public void Switch(string stateId, params object[] args)
        {
            if (!_stateStorage.HasEntity(stateId))
            {
                throw new ArgumentException($"State does not exist : {stateId}");
            }

            Current?.OnExit();
            Previous = Current ?? _stateStorage.Get(_defaultStateId);
            Current = _stateStorage.Get(stateId);
            Current.OnEnter(args);
            OnSwitchState?.Invoke(stateId);
        }

        public void Exit(string stateId)
        {
            if (!_stateStorage.HasEntity(stateId))
            {
                throw new ArgumentException($"State does not exist : {stateId}");
            }

            if (stateId == _defaultStateId)
            {
                return;
            }

            if (Previous != null && Previous.Id == stateId)
            {
                Previous.OnExit();
                Previous = _stateStorage.Get(_defaultStateId);
            }

            if (Current != null && Current.Id == stateId)
            {
                Switch(_defaultStateId);
            }
        }

        public bool Any()
        {
            return CountStates > 1;
        }

        public void Clear()
        {
            _stateStorage.Clear();
            Current = Previous = new State(_defaultStateId);
            Add(Current);
        }
    }
}