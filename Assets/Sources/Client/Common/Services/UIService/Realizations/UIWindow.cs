using Client.Common.Services.EventSourceService;
using Client.Common.Services.UIService.Events;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public abstract class UIWindow : MonoBehaviour, IUIWindow
    {
        [SerializeField] private RectTransform selfContainer;
        public IEventSource EventSource { get; private set; }
        public RectTransform Container => selfContainer;

        public void Init(IEventSource eventSource)
        {
            EventSource = eventSource;
            if (!selfContainer)
                selfContainer = GetComponent<RectTransform>();

            OnInit();
        }

        public async UniTask ShowAsync()
        {
            await EventSource.PublishParallelAsync(new WindowShowEvent(this));
            await OnShowedAsync();
            await EventSource.PublishParallelAsync(new WindowShowedEvent(this));
        }

        public async UniTask HideAsync()
        {
            await EventSource.PublishParallelAsync(new WindowHideEvent(this));
            await OnHidedAsync();
            await EventSource.PublishParallelAsync(new WindowHidedEvent(this));
        }

        protected void OnDestroy()
        {
            OnDisposed();
            EventSource.Clear();
            EventSource = null;
            selfContainer = null;
        }
        
        protected virtual void OnInit() {}
        protected virtual UniTask OnShowedAsync() => UniTask.CompletedTask;
        protected virtual UniTask OnHidedAsync() => UniTask.CompletedTask;
        protected virtual void OnDisposed(){}
    }
}