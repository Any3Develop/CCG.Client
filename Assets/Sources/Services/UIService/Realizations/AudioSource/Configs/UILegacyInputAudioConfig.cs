using Services.UIService.Data;
using UnityEngine;

namespace Services.UIService
{
    [CreateAssetMenu(fileName = nameof(UILeagcyInputAudioHandler), menuName = "UIService/Audio/" + nameof(UILeagcyInputAudioHandler))]
    public class UILegacyInputAudioConfig : UIAudioBaseConfig
    {
        [SerializeField] private bool includeDisabled = true;
        [SerializeField] private bool includeInherited = true;
        [SerializeField] private UIAudioClipData writeAudio;
        [SerializeField] private UIAudioClipData eraseAudio;
        [SerializeField] private UIAudioClipData submitAudio;
        [SerializeField] private UIAudioClipData selectAudio;
        [SerializeField] private UIAudioClipData deselectAudio;
        
        public bool IncludeDisabled => includeDisabled;
        public bool IncludeInherited => includeInherited;
        public UIAudioClipData WriteAudio => writeAudio;
        public UIAudioClipData EraseAudio => eraseAudio;
        public UIAudioClipData SubmitAudio => submitAudio;
        public UIAudioClipData SelectAudio => selectAudio;
        public UIAudioClipData DeselectAudio => deselectAudio;
    }
}