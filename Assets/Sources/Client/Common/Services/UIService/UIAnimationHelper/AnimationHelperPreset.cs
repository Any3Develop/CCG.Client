using UnityEngine;

namespace Client.Common.Services.UIService
{
    [CreateAssetMenu(fileName = "AnimationHelperPreset", menuName = "Config/AnimationHelperPreset", order = 0)]
    public class AnimationHelperPreset : ScriptableObject
    {
        public DOSettings Settings => _settings;
        [SerializeField] private DOSettings _settings;
    }
}