using UnityEngine;
using Zenject;

namespace CardGame.Services.MonoPoolService
{
    public interface IMonoPoolable : IPoolable
    {
        GameObject GameObject { get; }
        void Relese();
    }
}