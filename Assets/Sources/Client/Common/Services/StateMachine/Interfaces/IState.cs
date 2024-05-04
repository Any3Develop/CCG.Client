using CardGame.Services.StorageService;

namespace CardGame.Services.StateMachine
{
    public interface IState : IStorageEntity
    {
        void OnEnter(params object[] args);
        void OnExit();
    }
}