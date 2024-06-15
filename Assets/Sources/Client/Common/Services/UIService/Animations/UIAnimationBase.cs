using Client.Common.Services.UIService.Events;
using Client.Common.Utils;
using Cysharp.Threading.Tasks;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    public abstract class UIAnimationBase : MonoBehaviour, IUIAnimation
    {
        [SerializeField] private UIAnimationPresetAsset presetAsset;
        [SerializeField] protected UIAnimationData animationData = UIAnimationPresetAsset.Default();
        
        protected IDisposableBlock DisposableBlock { get; private set; }
        protected IUIWindow Window { get; private set; }


        public void Init(IUIWindow window)
        {
            Window = window;
            animationData = presetAsset ? presetAsset.PresetData : animationData;
            DisposableBlock?.Dispose();
            DisposableBlock ??= DisposableExtensions.CreateBlock();
            OnInit();
            Window.EventSource.Publish(new WindowAnimationInitEvent(Window, this));
        }

        public async UniTask PlayAsync()
        {
            Window.EventSource.Publish(new WindowAnimationPlayEvent(Window, this));
            await OnPlayAsync();
            Window.EventSource.Publish(new WindowAnimationStopEvent(Window, this));
        }
        
        public async UniTask ResetAsync()
        {
            await OnResetAsync();
            Window.EventSource.Publish(new WindowAnimationResetEvent(Window, this));
        }

        protected void OnDestroy()
        {
            OnDisposed();
            DisposableBlock?.Dispose();
            DisposableBlock = null;
            Window = null;
        }

        protected virtual void OnInit()
        {
            OnSubscribe(animationData.PlayAt, PlayAsync);
            OnSubscribe(animationData.ResetAt, ResetAsync);
        }

        protected virtual void OnSubscribe(UIAnimationTrigger trigger, Func<UniTask> callBack)
        {
            var eventSource = Window.EventSource;
            if (trigger.HasFlag(UIAnimationTrigger.Show))
                eventSource.Subscribe<WindowShowEvent>(_ => callBack()).AddTo(DisposableBlock);

            if (trigger.HasFlag(UIAnimationTrigger.Showed))
                eventSource.Subscribe<WindowShowedEvent>(_ => callBack()).AddTo(DisposableBlock);

            if (trigger.HasFlag(UIAnimationTrigger.Hide))
                eventSource.Subscribe<WindowHideEvent>(_ => callBack()).AddTo(DisposableBlock);

            if (trigger.HasFlag(UIAnimationTrigger.Hided))
                eventSource.Subscribe<WindowHidedEvent>(_ => callBack()).AddTo(DisposableBlock);
        }

        protected virtual UniTask OnPlayAsync() => UniTask.CompletedTask;

        protected virtual UniTask OnResetAsync() => UniTask.CompletedTask;

        protected virtual void OnDisposed() {}
    }
}