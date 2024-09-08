using System;
using System.Collections.Generic;
using Client.Services.UIService.Data;

namespace Client.Services.UIService
{
#pragma warning disable CS0414
    public sealed class UINoAudioSource : IUIAudioSource
    {
        public event Action<UIAudioClipData> OnPlayAudio;

        public IEnumerable<IUIAudioHandler> Handlers => Array.Empty<IUIAudioHandler>();

        public bool Enabled { get; private set; }

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;

        public void Dispose()
        {
            Disable();
            OnPlayAudio = null;
        }
    }
}