using Client.Services.UIService.Data;
using UnityEngine;

namespace Client.Services.UIService
{
    [CreateAssetMenu(fileName = nameof(UIButtonsAudioHandler), menuName = "UIService/Audio/" + nameof(UIButtonsAudioHandler))]
    public class UIButtonsAudioConfig : UIAudioBaseConfig
    {
        [SerializeField] private bool includeDisabled = true;
        [SerializeField] private bool includeInherited = true;
        [SerializeField] private UIAudioClipData clickAudio;
        
        public bool IncludeDisabled => includeDisabled;
        public bool IncludeInherited => includeInherited;
        public UIAudioClipData ClickAudio => clickAudio;
    }
}