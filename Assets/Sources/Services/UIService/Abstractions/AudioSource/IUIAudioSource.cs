using System;
using System.Collections.Generic;
using Client.Services.UIService.Data;

namespace Client.Services.UIService
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