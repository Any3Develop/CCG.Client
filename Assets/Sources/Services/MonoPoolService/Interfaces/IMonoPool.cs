using UnityEngine;

namespace CardGame.Services.MonoPoolService
{
    public interface IMonoPool
    {
        Transform PoolContainer { get; }
        T Spawn<T>(Transform parent = null, params object[] args) where T : IMonoPoolable;
        void Despawn(IMonoPoolable item);
        void Destroy(IMonoPoolable item);
    }
}