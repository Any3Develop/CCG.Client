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
            EventSource.Publish(new WindowInitEvent(this));
        }

        public async UniTask ShowAsync()
        {
            await EventSource.PublishAsync(new WindowShowEvent(this));
            await OnShowedAsync();
            await EventSource.PublishAsync(new WindowShowedEvent(this));
        }

        public async UniTask HideAsync()
        {
            await EventSource.PublishAsync(new WindowHideEvent(this));
            await OnHidedAsync();
            await EventSource.PublishAsync(new WindowHidedEvent(this));
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