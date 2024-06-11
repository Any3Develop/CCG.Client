using System;
using DG.Tweening;

namespace Client.Common.Services.UIService.Animations
{
    [Serializable]
    public class UIAnimationPresetData
    {
        public bool Reversed = false;
        public float StartDelay = 0f;
        public float EndDelay = 0f;
        public float Duration = 1f;
        public Ease Ease = Ease.Linear;
    }
}