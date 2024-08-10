using System;
using Services.UIService.Abstractions;
using Services.UIService.Data;

namespace Services.UIService
{
#pragma warning disable CS0414
    public sealed class UINoAudioHandler : IUIAudioHandler
    {
        public event Action<UIAudioClipData> OnPayAudio;

        public bool Enabled { get; private set; }

        public void Dispose()
        {
            Disable();
            OnPayAudio = null;
        }

        public void Enable() => Enabled = true;

        public void Disable() => Enabled = false;
    }
}