using System;
using Client.Services.UIService.Data;

namespace Client.Services.UIService
{
    public interface IUIAudioHandler : IDisposable
    {
        event Action<UIAudioClipData> OnPayAudio;
        bool Enabled { get; }
        void Enable();
        void Disable();
    }
}