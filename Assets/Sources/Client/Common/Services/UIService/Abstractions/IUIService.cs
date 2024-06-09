using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public interface IUIService
    {
        void Initialize();
        void Dispose<T>() where T : IWindow;
        void Create<T>(Transform parent = null) where T : IWindow;
        T Get<T>() where T : IWindow;
        bool TryGet<T>(out T result) where T : IWindow;
        UniTask<T> ShowAsync<T>(Transform parent = null) where T : IWindow;
        UniTask<T> HideAsync<T>() where T : IWindow;
        void Move<T>(Transform parent) where T : IWindow;
    }
}