#if TEXTMESHPRO
using Services.UIService.Data;
using UnityEngine;

namespace Services.UIService
{
    [CreateAssetMenu(fileName = nameof(UITMPDropDownAudioHandler), menuName = "UIService/Audio/" + nameof(UITMPDropDownAudioHandler))]
    public class UITMPDropDownAudioConfig : UIAudioBaseConfig
    {
        [SerializeField] private bool includeDisabled = true;
        [SerializeField] private bool includeInherited = true;
        [SerializeField] private UIAudioClipData selectAudio;
                
        public bool IncludeDisabled => includeDisabled;
        public bool IncludeInherited => includeInherited;
        public UIAudioClipData SelectAudio => selectAudio;
    }
}
#endif