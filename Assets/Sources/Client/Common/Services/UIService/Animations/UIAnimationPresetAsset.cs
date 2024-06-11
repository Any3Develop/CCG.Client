using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    [CreateAssetMenu(fileName = "UIAnimationPresetAsset", menuName = "UIService/AnimationPresetAsset", order = 0)]
    public class UIAnimationPresetAsset : ScriptableObject
    {
        [SerializeField] protected UIAnimationPresetData defaultPresetData = Default();
        public virtual UIAnimationPresetData PresetData => defaultPresetData;

        public static UIAnimationPresetData Default() => new();
    }
}