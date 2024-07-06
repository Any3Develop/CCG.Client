using System;
using Client.Common.Services.UIService.Events;
using Client.Common.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    [RequireComponent(typeof(IUIWindow))]
    public abstract class UIAnimationBase : MonoBehaviour, IUIAnimation
    {
        [SerializeField] private UIAnimationPresetAsset presetAsset;
        [SerializeField] protected UIAnimationData animationData = UIAnimationPresetAsset.Default();
        [SerializeField] protected bool defaultEnabeld = true;
        protected IDisposableBlock DisposableBlock { get; private set; }
        protected IUIWindow Window { get; private set; }
        protected bool Initialized { get; private set; }
        protected bool Enabled { get; private set; }
        
        public void Init(IUIWindow window)
        {
            if (Initialized)
                return;

            Initialized = true;
            Window = window;
            animationData = presetAsset ? presetAsset.PresetData : animationData;
            if (defaultEnabeld)
                Enable();

            OnInit();
        }

        public void Enable()
        {
            if (Enabled || !Initialized)
                return;
            
            Enabled = true;
            OnEnabled();
        }

        public void Disable()
        {
            if (!Enabled || !Initialized)
                return;
            
            Enabled = false;
            OnDisabled();
        }

        public async UniTask PlayAsync()
        {
            if (!Initialized)
                return;
            
            Window.EventSource.Publish(new WindowAnimationPlayEvent(Window, this));
            await OnPlayAsync();
            Window.EventSource.Publish(new WindowAnimationStopEvent(Window, this));
        }
        
        public async UniTask ResetAsync()
        {
            if (!Initialized)
                return;
            
            await OnResetAsync();
            Window.EventSource.Publish(new WindowAnimationResetEvent(Window, this));
        }
        
        protected void OnDestroy()
        {
            if (!Initialized)
                return;

            Initialized = false;
            OnDisposed();
            DisposableBlock?.Dispose();
            DisposableBlock = null;
            Window = null;
        }
        
        protected virtual void OnInit() {}
        
        protected virtual void OnEnabled()
        {
            DisposableBlock?.Dispose();
            DisposableBlock ??= DisposableExtensions.CreateBlock();
            OnSubscribe(animationData.PlayAt, PlayAsync);
            OnSubscribe(animationData.ResetAt, ResetAsync);
        }

        protected virtual void OnDisabled()
        {
            DisposableBlock?.Dispose();
            DisposableBlock = null;
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