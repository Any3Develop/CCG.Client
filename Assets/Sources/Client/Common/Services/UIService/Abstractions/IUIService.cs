using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public interface IUIService
    {
        void Initialize();
        void Dispose<T>() where T : IUIWindow;
        T Create<T>(Transform parent = null) where T : IUIWindow;
        T Get<T>() where T : IUIWindow;
        bool TryGet<T>(out T result) where T : IUIWindow;
        T Move<T>(Transform parent, int? sibling = null) where T : IUIWindow;
        UniTask<T> ShowAsync<T>(Transform parent = null) where T : IUIWindow;
        UniTask<T> HideAsync<T>() where T : IUIWindow;
    }
}