using Services.UIService.Data;
using UnityEngine;

namespace Services.UIService
{
    [CreateAssetMenu(fileName = nameof(UIDropDownAudioHandler), menuName = "UIService/Audio/" + nameof(UIDropDownAudioHandler))]
    public class UIDropDownAudioConfig : UIAudioBaseConfig
    {
        [SerializeField] private bool includeDisabled = true;
        [SerializeField] private bool includeInherited = true;
        [SerializeField] private UIAudioClipData selectAudio;
        
        public bool IncludeDisabled => includeDisabled;
        public bool IncludeInherited => includeInherited;
        public UIAudioClipData SelectAudio => selectAudio;
    }
}