using System;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Client.Common.Services.UIService.Animations
{
    [Serializable]
    public class UIAnimationPresetData
    {
        public float StartDelay = 0f;
        [FormerlySerializedAs("EndDelay")] public float StopDelay = 0f;
        public float Duration = 1f;
        public Ease Ease = Ease.Linear;
    }
}