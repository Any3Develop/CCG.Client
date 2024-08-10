using Services.UIService.Abstractions;
using UnityEngine;

namespace Services.UIService
{
    public abstract class UIAudioBaseConfig : ScriptableObject, IUIAudioConfig
    {
        [SerializeField] private bool enabledByDefault = true;
        [SerializeField] private bool reInitWhenModified;
        
        public bool EnabledByDefault => enabledByDefault;
        public bool ReInitWhenModified => reInitWhenModified;
    }
}