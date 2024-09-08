using System;
using System.Collections.Generic;
using Client.Services.UIService.Data;

namespace Client.Services.UIService
{
    public class UIAudioSource : IUIAudioSource
    {
        protected IUIWindow Window { get; private set; }
        protected bool Initialized { get; private set; }
        

        public event Action<UIAudioClipData> OnPlayAudio;
        public IEnumerable<IUIAudioHandler> Handlers { get; private set; }
        public bool Enabled { get; private set; }
        
        public IUIAudioSource Init(IUIWindow window, IEnumerable<IUIAudioHandler> handlers)
        {
            if (Initialized)
                return this;
            
            Window = window;
            Handlers = handlers;
            foreach (var handler in Handlers)
            {
                handler.OnPayAudio += data => OnPlayAudio?.Invoke(data);
                Enabled |= handler.Enabled;
            }

            Initialized = true;
            return this;
        }
        
        public void Dispose()
        {
            if (!Initialized)
                return;
            
            OnDisposed();
            foreach (var handler in Handlers)
                handler?.Dispose();
            
            Handlers = null;
            Initialized = false;
            OnPlayAudio = null;
        }

        public void Enable()
        {
            if (Enabled || !Initialized)
                return;

            Enabled = true;
            foreach (var handler in Handlers)
                handler.Enable();

            OnEnabled();
        }

        public void Disable()
        {
            if (!Enabled || !Initialized)
                return;

            Enabled = false;
            foreach (var handler in Handlers)
                handler.Disable();

            OnDisabled();
        }

        protected virtual void OnEnabled(){}
        protected virtual void OnDisabled(){}
        protected virtual void OnDisposed(){}
    }
}