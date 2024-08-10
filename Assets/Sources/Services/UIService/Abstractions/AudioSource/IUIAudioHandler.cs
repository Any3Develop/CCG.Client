using System;
using Services.UIService.Data;

namespace Services.UIService.Abstractions
{
    public interface IUIAudioHandler : IDisposable
    {
        event Action<UIAudioClipData> OnPayAudio;
        bool Enabled { get; }
        void Enable();
        void Disable();
    }
}