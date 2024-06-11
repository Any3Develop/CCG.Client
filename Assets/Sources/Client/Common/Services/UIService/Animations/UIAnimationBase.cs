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
        [SerializeField] protected UIAnimationTrigger playTrigger = UIAnimationTrigger.Show;
        [SerializeField] protected UIAnimationTrigger stopTrigger = UIAnimationTrigger.Hided;
        [SerializeField] protected UIAnimationTrigger resetTrigger = UIAnimationTrigger.Show | UIAnimationTrigger.Hided;
       
        protected UIAnimationPresetData Presset { get; private set; }
        protected IDisposableBlock DisposableBlock { get; private set; }
        protected IUIWindow Window { get; private set; }


        public void Init(IUIWindow window)
        {
            Window = window;
            Presset = presetAsset ? presetAsset.PresetData : UIAnimationPresetAsset.Default();
            DisposableBlock?.Dispose();
            DisposableBlock ??= DisposableExtensions.CreateBlock();
            OnInit();
            Window.EventSource.Publish(new WindowAnimationInitEvent(Window, this));
        }

        public async UniTask PlayAsync(bool forceEnd = false)
        {
            Window.EventSource.Publish(new WindowAnimationPlayEvent(Window, this));
            await OnPlayAsync(forceEnd);
            await StopAsync(forceEnd);
        }

        public async UniTask StopAsync(bool forceEnd = false)
        {
            await OnStopAsync();
            Window.EventSource.Publish(new WindowAnimationStopEvent(Window, this));
        }

        public void Restart()
        {
            OnRestart();
            Window.EventSource.Publish(new WindowAnimationRestartEvent(Window, this));
        }

        protected void OnDestroy()
        {
            OnDisposed();
            DisposableBlock?.Dispose();
            DisposableBlock = null;
            Window = null;
            Presset = null;
        }

        protected virtual void OnInit()
        {
            OnSubscribe(playTrigger, () => PlayAsync());
            OnSubscribe(stopTrigger, () => StopAsync());
            OnSubscribe(resetTrigger, () => { Restart(); return UniTask.CompletedTask; });
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
        
        protected virtual UniTask OnPlayAsync(bool forceEnd = false) => UniTask.CompletedTask;
        
        protected virtual UniTask OnStopAsync(bool forceEnd = false) => UniTask.CompletedTask;
        
        protected virtual void OnRestart() {}

        protected virtual void OnDisposed() {}
    }
}