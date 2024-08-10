using System;
using Services.UIService.Abstractions;
using Services.UIService.Data;

namespace Services.UIService
{
    public abstract class UIAudioHandlerBase<T> : IUIAudioHandler where T : IUIAudioConfig
    {
        protected T Config { get; private set; }
        protected IUIWindow Window { get; private set; }
        protected bool Initialized { get; private set; }
        
        public event Action<UIAudioClipData> OnPayAudio;
        public bool Enabled { get; private set; }
        
        public IUIAudioHandler Init(IUIWindow window, IUIAudioConfig config)
        {
            Config = (T)config;
            Window = window;
            Initialized = true;
            if (Config.ReInitWhenModified)
            {
                Window.OnChanged -= OnReInit;
                Window.OnChanged += OnReInit;
            }
            
            OnInit();
            
            if (Config.EnabledByDefault)
                Enable();

            return this;
        }
        
        public void Dispose()
        {
            if (!Initialized)
                return;

            if (Config.ReInitWhenModified)
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
            if (!Enabled || !Initialized)
                return;

            Enabled = false;
            OnDisabled();
        }

        protected void PlayAudioClip(UIAudioClipData data)
        {
            if (!Enabled || !Initialized || data.IsEmpty())
                return;

            OnPayAudio?.Invoke(data);
        }

        protected abstract void OnInit();
        
        protected virtual void OnReInit()
        {
            if (!Initialized)
                return;

            var lastEnabled = Enabled;
            
            Disable();
            OnInit();
            
            if (lastEnabled)
                Enable();
        }
        
        protected abstract void OnEnabled();
        protected abstract void OnDisabled();
        protected virtual void OnDisposed() {}
    }
}