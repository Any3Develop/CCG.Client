using System;
using System.Collections.Generic;
using Services.UIService.Data;

namespace Services.UIService.Abstractions
{
    public interface IUIAudioSource : IDisposable
    {
        event Action<UIAudioClipData> OnPlayAudio; 
        IEnumerable<IUIAudioHandler> Handlers { get; }
        bool Enabled { get; }
        
        void Enable();
        void Disable();
    }
}