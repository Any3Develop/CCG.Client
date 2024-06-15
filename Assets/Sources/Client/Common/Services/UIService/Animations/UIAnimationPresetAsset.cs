using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    [CreateAssetMenu(fileName = "UIAnimationPresetAsset", menuName = "UIService/AnimationPresetAsset", order = 0)]
    public class UIAnimationPresetAsset : ScriptableObject
    {
        [SerializeField] protected UIAnimationData presetData = Default();
        public virtual UIAnimationData PresetData => presetData;

        public static UIAnimationData Default() => new();
    }
}