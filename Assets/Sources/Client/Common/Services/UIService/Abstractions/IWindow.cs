using Client.Common.Services.EventSourceService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public interface IWindow
    {
        IEventSource EventSource { get; }
        RectTransform Container { get; }
        UniTask ShowAsync();
        UniTask HideAsync();
    }
}