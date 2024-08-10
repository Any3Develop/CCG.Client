using System;
using System.Threading;
using System.Threading.Tasks;
using Services.UIService.Abstractions;

namespace Services.UIService
{
    public abstract class UIAnimationBase<T> : IUIAnimation where T : IUIAnimationConfig
    {
	    protected T Config { get; private set; }
        protected IUIWindow Window { get; private set; }
        protected bool Initialized { get; private set; }
        
        public IUIAnimationConfig Configuration { get; private set; }
        public bool Enabled { get; private set; }
        
        public IUIAnimation Init(IUIWindow window, IUIAnimationConfig config)
        {
            if (Initialized)
                return this;
            Config = (T) config;
            Configuration = config;
            Window = window;
            Initialized = true;
            
            if (Config.ReInitWhenModified)
                Window.OnChanged += OnReInit;
            
            OnInit();

            if (Config.EnabledByDefault)
                Enable();

            return this;
        }

        public void Dispose()
        {
            if (!Initialized)
                return;
            
            if (Window != null)
                Window.OnChanged -= OnReInit;
            
            Disable();
            OnDisposed();
            Initialized = false;
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
            if (!Initialized || !Enabled)
                return;

            Enabled = false;
            OnDisabled();
        }

        public virtual Task PlayAsync(CancellationToken token) 
            => RunMainThreadAsync(OnPlay, token);

        public virtual Task StopAsync(CancellationToken token) 
            => RunMainThreadAsync(OnStop, token);

        public virtual Task ResetAsync(CancellationToken token)
            => RunMainThreadAsync(OnReset, token);
        
        protected virtual void OnInit() {}
        
        protected virtual void OnReInit()
        {
            if (!Initialized)
                return;

            var reInitEnabled = Enabled;
            Disable();
            OnInit();

            if (reInitEnabled)
                Enable();
        }
        
        protected virtual void OnDisposed() {}

        protected virtual void OnEnabled() {}

        protected virtual void OnDisabled() {}
        
        protected virtual void OnPlay(Action onCompleted, CancellationToken token) => onCompleted?.Invoke();
        
        protected virtual void OnStop(Action onCompleted, CancellationToken token) => onCompleted?.Invoke();
        
        protected virtual void OnReset(Action onCompleted, CancellationToken token) => onCompleted?.Invoke();
        
        protected static Task RunMainThreadAsync(Action<Action, CancellationToken> action, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>(token);
            action(() => tcs.SetResult(true), token);
            return tcs.Task;
        }
    }
}