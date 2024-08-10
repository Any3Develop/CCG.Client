using Services.UIService.Data;
using UnityEngine;

namespace Services.UIService
{
    [CreateAssetMenu(fileName = nameof(UIScrollBarAudioHandler), menuName = "UIService/Audio/" + nameof(UIScrollBarAudioHandler))]
    public class UIScrollBarAudioConfig : UIAudioBaseConfig
    {
        [SerializeField] private bool includeDisabled = true;
        [SerializeField] private bool includeInherited = true;
        [SerializeField, Range(0, 1f)] private float gap = 0.1f;
        [SerializeField] private UIAudioClipData scrollAudio;
        
        public bool IncludeDisabled => includeDisabled;
        public bool IncludeInherited => includeInherited;
        public float Gap => gap;
        public UIAudioClipData ScrollAudio => scrollAudio;
    }
}