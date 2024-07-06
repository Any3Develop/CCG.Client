using System;
using DG.Tweening;

namespace Client.Common.Services.UIService.Animations
{
    [Serializable]
    public class UIAnimationData
    {
        public float Delay;
        public float Duration = 1f;
        public Ease Ease = Ease.Linear;
        public UIAnimationTrigger PlayAt = UIAnimationTrigger.Show;
        public UIAnimationTrigger ResetAt = UIAnimationTrigger.Show | UIAnimationTrigger.Hided;
    }
}